using System;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Devices
{
    public interface IDeviceController<T> 
        : IDisposable
        where T : DeviceDto
    {
        Task InitAsync(T device);
        bool IsInitialized { get; }

        event EventHandler<ICommand<T>> CommandReceived; 
        void Send(INotification<T> notification);
    }
}
