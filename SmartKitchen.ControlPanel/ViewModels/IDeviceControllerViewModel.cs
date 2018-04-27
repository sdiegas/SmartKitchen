using System;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.ViewModels
{
    public interface IDeviceControllerViewModel 
        : IDisposable
    {
        bool IsControllerFor(DeviceDto device);
        Task InitAsync(DeviceDto config);
    }
}
