using System.Collections.Generic;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.UI;
using Hsr.CloudSolutions.SmartKitchen.UI.Communication;
using Hsr.CloudSolutions.SmartKitchen.Util.Serializer;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication.Azure
{
    /// <summary>
    /// This class is used to receive the registered devices.
    /// </summary>
    public class AzureControlPanelInfoClient 
        : BaseClient
        , IControlPanelInfoClient
    {
        private readonly IDialogService _dialogService; // Can display exception in a dialog.
        private readonly IDeviceSerializer _serializer; // Can be used to serialize/deserialize DTO's, commands and notifications.

        public AzureControlPanelInfoClient(IDialogService dialogService, IDeviceSerializer serializer)
        {
            _dialogService = dialogService;
            _serializer = serializer;
        }

        /// <summary>
        /// Used to establish the communication.
        /// </summary>
        public Task InitAsync()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Loads the registerd devices from the simulator.
        /// </summary>
        /// <returns>The list of all known devices.</returns>
        public Task<IEnumerable<DeviceDto>> LoadDevicesAsync()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Use this method to tear down any established connections.
        /// </summary>
        protected override void OnDispose()
        {
            base.OnDispose();
        }
    }
}
