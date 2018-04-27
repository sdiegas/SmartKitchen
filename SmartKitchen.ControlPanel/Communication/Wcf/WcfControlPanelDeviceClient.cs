using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;
using Hsr.CloudSolutions.SmartKitchen.Service.Interface;
using Hsr.CloudSolutions.SmartKitchen.UI;
using Hsr.CloudSolutions.SmartKitchen.UI.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication.WCF
{
    public class WcfControlPanelDeviceClient<T> : BaseClient, IControlPanelDeviceClient<T>
        where T : DeviceDto
    {
        private readonly IDialogService _dialogService;

        private ChannelFactory<IControlPanelService> _channelFactory;
        private IControlPanelService _client;

        public WcfControlPanelDeviceClient(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task InitAsync(T device)
        {
            await Task.Run(() =>
            {
                _channelFactory = new ChannelFactory<IControlPanelService>("ControlPanelService");
                _client = _channelFactory.CreateChannel();
            });
            IsInitialized = true;
        }

        public bool IsInitialized { get; private set; } = false;

        public async Task SendCommandAsync(ICommand<T> command)
        {
            if (command == null)
            {
                return;
            }
            try
            {
                await _client.SendCommandAsync(command);
            }
            catch (Exception ex)
            {
                LogException("Sending command failed.", ex);
                _dialogService.ShowException(ex);
            }
        }

        public async Task<INotification<T>> CheckNotificationsForAsync(T device)
        {
            if (device == null)
            {
                return new NullNotification<T>();
            }
            try
            {
                return await _client.PeekNotificationAsync(device) as INotification<T> ?? new NullNotification<T>();
            }
            catch (Exception ex)
            {
                LogException("Checking for notifications failed.", ex);
            }
            return new NullNotification<T>();
        }

        protected override void OnDispose()
        {
            try
            {
                (_client as ICommunicationObject)?.Close();
            }
            catch (Exception ex)
            {
                LogException("Closing client failed.", ex);
            }
            try
            {
                if (_channelFactory.State != CommunicationState.Faulted)
                {
                    _channelFactory.Close();
                }
            }
            catch (Exception ex)
            {
                LogException("Closing channelfactory failed.", ex);
            }
        }
    }
}
