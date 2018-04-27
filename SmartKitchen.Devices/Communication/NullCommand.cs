using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices.Communication
{
    [DataContract]
    public class NullCommand 
        : ICommand<DeviceDto>
    {
        [DataMember]
        public string Command { get; set; } = Commands.None;

        [DataMember]
        public DeviceDto DeviceConfig { get; set; } = null;

        [DataMember]
        public bool HasDeviceConfig { get; set; } = false;
    }
}
