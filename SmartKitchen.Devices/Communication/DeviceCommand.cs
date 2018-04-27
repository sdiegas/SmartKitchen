using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices.Communication
{
    [DataContract]
    [KnownType(typeof(FridgeDto))]
    [KnownType(typeof(OvenDto))]
    [KnownType(typeof(StoveDto))]
    public class DeviceCommand<T> 
        : ICommand<T> 
        where T : DeviceDto
    {
        public DeviceCommand(string command, T deviceConfig)
        {
            Command = command ?? Commands.None;
            DeviceConfig = deviceConfig;
        }

        [DataMember]
        public string Command { get; set; }
        [DataMember]
        public T DeviceConfig { get; set; }

        public bool HasDeviceConfig => DeviceConfig != null;
    }
}
