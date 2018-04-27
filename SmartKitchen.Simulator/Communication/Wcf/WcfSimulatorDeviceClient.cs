using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;
using Hsr.CloudSolutions.SmartKitchen.Service.Interface;
using Hsr.CloudSolutions.SmartKitchen.UI;
using Hsr.CloudSolutions.SmartKitchen.UI.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Communication.WCF
{
    public class WcfSimulatorDeviceClient<T> : BaseClient, ISimulatorDeviceClient<T>
        where T : DeviceDto
    {
        private ChannelFactory<ISimulatorService> _channelFactory;
        private ISimulatorService _client;

        private readonly IDialogService _dialogService;

        public WcfSimulatorDeviceClient(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task InitAsync(T device)
        {
            await Task.Run(() =>
            {
                _channelFactory = new ChannelFactory<ISimulatorService>("SimulatorService");
                _client = _channelFactory.CreateChannel();
            });
        }

        public async Task<ICommand<T>> CheckCommandsAsync(T device)
        {
            if (device == null)
            {
                return new NullCommand<T>();
            }
            try
            {
                return await _client.ReceiveCommandAsync(device) as ICommand<T> ?? new NullCommand<T>();
            }
            catch (Exception ex)
            {
                LogException("Checking for commands failed.", ex);
            }
            return new NullCommand<T>();
        }

        public async Task SendNotificationAsync(INotification<T> notification)
        {
            if (notification == null)
            {
                return;
            }
            try
            {
                await _client.PublishNotificationAsync(notification);
            }
            catch (Exception ex)
            {
                LogException("Send notification failed.", ex);
                _dialogService.ShowException(ex);
            }
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
