using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices.Communication
{
    [DataContract]
    public class NullCommand<T>
        : ICommand<T>
        where T : DeviceDto
    {
        [DataMember]
        public string Command { get; set; } = Commands.None;

        [DataMember]
        public T DeviceConfig { get; set; } = null;

        [DataMember]
        public bool HasDeviceConfig { get; set; } = false;
    }
}
