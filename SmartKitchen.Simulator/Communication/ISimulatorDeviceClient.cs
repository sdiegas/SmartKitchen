using System;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Communication
{
    public interface ISimulatorDeviceClient<T> 
        : IDisposable
        where T : DeviceDto
    {
        Task InitAsync(T device);
        
        Task<ICommand<T>> CheckCommandsAsync(T device);
        Task SendNotificationAsync(INotification<T> notification);
    }
}
