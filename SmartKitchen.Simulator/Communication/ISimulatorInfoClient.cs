using System;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Communication
{
    public interface ISimulatorInfoClient<in T> 
        : IDisposable
        where T : DeviceDto
    {
        Task InitAsync();

        Task RegisterDeviceAsync(T device);
        Task UnregisterDeviceAsync(T device);
    }
}
