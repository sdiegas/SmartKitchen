using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Service.Interface;
using Hsr.CloudSolutions.SmartKitchen.UI.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Communication.WCF
{
    public class WcfSimulatorInfoClient<T> : BaseClient, ISimulatorInfoClient<T>
        where T : DeviceDto
    {
        private ChannelFactory<ISimulatorService> _channelFactory;
        private ISimulatorService _client;

        public async Task InitAsync()
        {
            await Task.Run(() =>
            {
                _channelFactory = new ChannelFactory<ISimulatorService>("SimulatorService");
                _client = _channelFactory.CreateChannel();
            });
        }

        public async Task RegisterDeviceAsync(T device)
        {
            if (device == null)
            {
                return;
            }
            try
            {
                await _client.RegisterDeviceAsync(device);
            }
            catch (Exception ex)
            {
                LogException("Register device failed.", ex);
            }
        }

        public async Task UnregisterDeviceAsync(T device)
        {
            if (device == null)
            {
                return;
            }
            try
            {
                await _client.UnregisterDeviceAsync(device);
            }
            catch (Exception ex)
            {
                LogException("Unregister device failed.", ex);
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
