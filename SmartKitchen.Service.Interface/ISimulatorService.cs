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
    public interface ISimulatorService
    {
        [OperationContract]
        Task RegisterDeviceAsync(DeviceDto device);

        [OperationContract]
        Task UnregisterDeviceAsync(DeviceDto device);

        [OperationContract]
        Task<ICommand<DeviceDto>> ReceiveCommandAsync(DeviceDto device);

        [OperationContract]
        Task PublishNotificationAsync(INotification<DeviceDto> update);
    }
}
