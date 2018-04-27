using System;
using System.Threading.Tasks;
using CommonServiceLocator;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Devices;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Devices
{
    public static class SimulatorDeviceControllerExtensions
    {
        public static async Task RegisterAsync<T>(this ISimDevice<T> device, IServiceLocator serviceLocator)
            where T : DeviceDto
        {
            if (device == null)
            {
                return;
            }
            if (serviceLocator == null)
            {
                throw new ArgumentNullException(nameof(serviceLocator));
            }
            var infoClient = serviceLocator.GetInstance<ISimulatorInfoClient<T>>();
            var deviceClient = serviceLocator.GetInstance<ISimulatorDeviceClient<T>>();
            await device.RegisterAsync(new SimulatorDeviceController<T>(infoClient, deviceClient));
        }
    }
}
