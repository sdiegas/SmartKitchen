using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;
using Hsr.CloudSolutions.SmartKitchen.UI;
using Hsr.CloudSolutions.SmartKitchen.UI.Communication;
using Hsr.CloudSolutions.SmartKitchen.Util.Serializer;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Communication.Azure
{
    /// <summary>
    /// This class is used to receive commands and send notifications.
    /// </summary>
    /// <typeparam name="T">The device this client is used for.</typeparam>
    public class AzureSimulatorDeviceClient<T> : BaseClient, ISimulatorDeviceClient<T>
        where T : DeviceDto
    {
        private readonly IDialogService _dialogService; // Can be used to display dialogs when exceptions occur.
        private readonly IDeviceSerializer _serializer; // Can be used to serialize/deserialize DTO's, commands and notifications.

        AzureSimulatorDeviceClient(IDialogService dialogService, IDeviceSerializer serializer)
        {
            _dialogService = dialogService;
            _serializer = serializer;
        } 

        /// <summary>
        /// Establishes the connections used to talk to the Cloud.
        /// </summary>
        /// <param name="device">The device this client is used for.</param>
        public Task InitAsync(T device)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks if a command should be executed.
        /// </summary>
        /// <param name="device">The device to check for commands.</param>
        /// <returns>A received command or NullCommand&lt;T&gt;</returns>
        public async Task<ICommand<T>> CheckCommandsAsync(T device)
        {
            throw new System.NotImplementedException();
            return new NullCommand<T>();
        }

        /// <summary>
        /// Sends a notification to the control panel.
        /// </summary>
        /// <param name="notification">The notification to send.</param>
        public Task SendNotificationAsync(INotification<T> notification)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Use this method to tear down established connections.
        /// </summary>
        protected override void OnDispose()
        {
            base.OnDispose();
        }
    }
}
