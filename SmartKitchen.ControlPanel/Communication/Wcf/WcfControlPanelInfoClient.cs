using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Service.Interface;
using Hsr.CloudSolutions.SmartKitchen.UI.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication.WCF
{
    /// <summary>
    /// This class is used to retrieve a full ist of devices.
    /// </summary>
    public class WcfControlPanelInfoClient : BaseClient, IControlPanelInfoClient
    {
        private ChannelFactory<IControlPanelService> _channelFactory;
        private IControlPanelService _client;

        public async Task InitAsync()
        {
            await Task.Run(() =>
            {
                _channelFactory = new ChannelFactory<IControlPanelService>("ControlPanelService");
                _client = _channelFactory.CreateChannel();
            });
        }

        public async Task<IEnumerable<DeviceDto>> LoadDevicesAsync()
        {
            var devices = new List<DeviceDto>();
            try
            {
                devices.AddRange(await _client.GetRegisteredDevicesAsync());
            }
            catch (Exception ex)
            {
                LogException("Loading devices failed.", ex);
            }
            return devices;
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
