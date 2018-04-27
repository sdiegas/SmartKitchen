using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Devices;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Devices
{
    public class SimulatorDeviceController<T> 
        : IDeviceController<T>
        where T : DeviceDto
    {
        public event EventHandler<ICommand<T>> CommandReceived;

        private readonly ISimulatorInfoClient<T> _infoClient;
        private readonly ISimulatorDeviceClient<T> _deviceClient; 
        private readonly DispatcherTimer _commandTimer;

        public SimulatorDeviceController(ISimulatorInfoClient<T> infoClient, ISimulatorDeviceClient<T> deviceClient)
        {
            _infoClient = infoClient;
            _deviceClient = deviceClient;
            
            _commandTimer = new DispatcherTimer();
            _commandTimer.Interval = TimeSpan.FromMilliseconds(500);
            _commandTimer.Tick += CheckCommands;
        }

        private T _dto;

        public async Task InitAsync(T device)
        {
            _dto = device;
            await _infoClient.InitAsync();
            await _deviceClient.InitAsync(device);

            await _infoClient.RegisterDeviceAsync(_dto);

            _commandTimer.Start();

            IsInitialized = true;
        }

        public bool IsInitialized { get; private set; } = false;

        private bool _checking;

        private async void CheckCommands(object sender, EventArgs e)
        {
            if (_checking)
            {
                return;
            }
            try
            {
                _checking = true;
                if (_dto == null)
                {
                    return;
                }
                var command = await _deviceClient.CheckCommandsAsync(_dto);
                if (command is NullCommand<T>)
                {
                    return;
                }
                CommandReceived?.Invoke(this, command);
            }
            finally
            {
                _checking = false;
            }
        }

        public async void Send(INotification<T> notification)
        {
            await _deviceClient.SendNotificationAsync(notification);
        }

        public void Dispose()
        {
            if (_dto != null)
            {
                Task.Run(() => _infoClient.UnregisterDeviceAsync(_dto)).Wait();
            }
            _commandTimer.Stop();

            _deviceClient.Dispose();
            _infoClient.Dispose();
        }
    }
}
