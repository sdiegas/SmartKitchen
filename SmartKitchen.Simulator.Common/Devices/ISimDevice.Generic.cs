﻿using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Devices
{
    public interface ISimDevice<T> 
        : ISimDevice
        where T : DeviceDto
    {
        T ToDto();
        Task RegisterAsync(IDeviceController<T> controller);
    }
}
