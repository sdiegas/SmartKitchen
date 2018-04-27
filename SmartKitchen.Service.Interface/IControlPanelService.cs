using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.Service.Interface
{
    [ServiceContract]
    [ServiceKnownType(typeof(FridgeDto))]
    [ServiceKnownType(typeof(OvenDto))]
    [ServiceKnownType(typeof(StoveDto))]
    [ServiceKnownType(typeof(NullCommand))]
    [ServiceKnownType(typeof(DeviceCommand<FridgeDto>))]
    [ServiceKnownType(typeof(DeviceCommand<OvenDto>))]
    [ServiceKnownType(typeof(DeviceCommand<StoveDto>))]
    [ServiceKnownType(typeof(NullNotification))]
    [ServiceKnownType(typeof(DeviceNotification<FridgeDto>))]
    [ServiceKnownType(typeof(DeviceNotification<OvenDto>))]
    [ServiceKnownType(typeof(DeviceNotification<StoveDto>))]
    public interface IControlPanelService
    {
        [OperationContract]
        Task<IEnumerable<DeviceDto>> GetRegisteredDevicesAsync();

        [OperationContract]
        Task SendCommandAsync(ICommand<DeviceDto> command);

        [OperationContract]
        Task<INotification<DeviceDto>> PeekNotificationAsync(DeviceDto device);
    }
}
