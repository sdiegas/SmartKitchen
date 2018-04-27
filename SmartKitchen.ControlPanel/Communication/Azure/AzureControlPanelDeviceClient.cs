using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;
using Hsr.CloudSolutions.SmartKitchen.UI;
using Hsr.CloudSolutions.SmartKitchen.UI.Communication;
using Hsr.CloudSolutions.SmartKitchen.Util.Serializer;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication.Azure
{
    /// <summary>
    /// This class is used to send commands to devices and for receiving their notifications.
    /// </summary>
    /// <typeparam name="T">The type of DeviceDTO this client is used for.</typeparam>
    public class AzureControlPanelDeviceClient<T> 
        : BaseClient
        , IControlPanelDeviceClient<T>
        where T : DeviceDto
    {
        private readonly IDialogService _dialogService; // Can display exception in a dialog.
        private readonly IDeviceSerializer _serializer; // Can be used to serialize/deserialize DTO's, commands and notifications.

        public AzureControlPanelDeviceClient(IDialogService dialogService, IDeviceSerializer serializer)
        {
            _dialogService = dialogService;
            _serializer = serializer;
        } 

        /// <summary>
        /// Used to establish the communication.
        /// </summary>
        /// <param name="device">The device this client is responsible for.</param>
        public Task InitAsync(T device)
        {
            throw new System.NotImplementedException();
            IsInitialized = true;
        }

        /// <summary>
        /// True if InitAsync was called and client is initialized.
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// Send a command to the simulator.
        /// </summary>
        /// <param name="command">Command to send</param>
        public Task SendCommandAsync(ICommand<T> command)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks if a notification for the <paramref name="device" /> is pending.
        /// </summary>
        /// <param name="device">The device to check notifications for.</param>
        /// <returns>A received notification or NullNotification&lt;T&gt;</returns>
        public async Task<INotification<T>> CheckNotificationsForAsync(T device)
        {
            throw new System.NotImplementedException();
            return new NullNotification<T>();
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
