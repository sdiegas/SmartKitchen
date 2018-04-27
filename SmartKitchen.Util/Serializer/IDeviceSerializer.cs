using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.Util.Serializer
{
    public interface IDeviceSerializer
    {
        string Serialize<T>(T dto) where T : DeviceDto;
        T DeserializeDto<T>(string serializedDto) where T : DeviceDto;

        string Serialize<T>(ICommand<T> command) where T : DeviceDto;
        ICommand<T> DeserializeCommand<T>(string serializedCommand) where T : DeviceDto;

        string Serialize<T>(INotification<T> notification) where T : DeviceDto;
        INotification<T> DeserializeNotification<T>(string serializedNotification) where T : DeviceDto;
    }
}
