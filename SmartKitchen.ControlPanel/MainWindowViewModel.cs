using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CommonServiceLocator;
using Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication;
using Hsr.CloudSolutions.SmartKitchen.ControlPanel.ViewModels;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.UI.ViewModels;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IControlPanelInfoClient _client;
        private readonly DispatcherTimer _devicesUpdateTimer;

        public MainWindowViewModel(IServiceLocator serviceLocator, IControlPanelInfoClient client)
        {
            _serviceLocator = serviceLocator;
            _client = client;
            
            _devicesUpdateTimer = new DispatcherTimer();
            _devicesUpdateTimer.Interval = TimeSpan.FromSeconds(1);
            _devicesUpdateTimer.Tick += OnRequestDeviceUpdate;
        }

        public async Task InitAsync()
        {
            await _client.InitAsync();
            _devicesUpdateTimer.Start();
        }

        private async void OnRequestDeviceUpdate(object sender, EventArgs e)
        {
            var devices = (await _client.LoadDevicesAsync()).ToList();
            foreach (var device in devices)
            {
                if (_controllerViewModels.Any(vm => vm.IsControllerFor(device)))
                {
                    continue;
                }

                var controllerVm = await CreateControllerViewModelForAsync(device);
                Add(controllerVm);
            }
            foreach (var controller in _controllerViewModels.ToList())
            {
                if (devices.Any(d => controller.IsControllerFor(d)))
                {
                    continue;
                }
                Remove(controller);
                controller.Dispose();
            }
        }

        private async Task<IDeviceControllerViewModel> CreateControllerViewModelForAsync(DeviceDto device)
        {
            var viewModelType = typeof(IDeviceControllerViewModel<>).MakeGenericType(device.GetType());
            var viewModel = (IDeviceControllerViewModel)_serviceLocator.GetService(viewModelType);
            await viewModel.InitAsync(device);
            return viewModel;
        }

        private readonly ObservableCollection<IDeviceControllerViewModel> _controllerViewModels  = new ObservableCollection<IDeviceControllerViewModel>();

        private void Add(IDeviceControllerViewModel viewModel)
        {
            Application.Current.Dispatcher.Invoke(() => _controllerViewModels.Add(viewModel));
        }

        private void Remove(IDeviceControllerViewModel viewModel)
        {
            Application.Current.Dispatcher.Invoke(() => _controllerViewModels.Remove(viewModel));
        }

        public IEnumerable<IDeviceControllerViewModel> DeviceControllers => _controllerViewModels;

        protected override void OnDispose()
        {
            _devicesUpdateTimer.Stop();
            _client.Dispose();
            base.OnDispose();
        }
    }
}
