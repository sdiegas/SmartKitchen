using System;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication
{
    public interface IControlPanelDeviceClient<T> 
        : IDisposable 
        where T : DeviceDto
    {
        Task InitAsync(T device);
        bool IsInitialized { get; }

        Task SendCommandAsync(ICommand<T> command);
        Task<INotification<T>> CheckNotificationsForAsync(T device);
    }
}
