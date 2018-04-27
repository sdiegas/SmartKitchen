using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication
{
    public interface IControlPanelInfoClient 
        : IDisposable
    {
        Task InitAsync();

        Task<IEnumerable<DeviceDto>> LoadDevicesAsync();
    }
}
