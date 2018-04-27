using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices.Communication
{
    [DataContract]
    public class NullNotification
        : INotification<DeviceDto>
    {
        [DataMember]
        public DeviceDto DeviceInfo { get; set; } = null;

        [DataMember]
        public bool HasDeviceInfo { get; set; } = false;

        [DataMember]
        public string Type { get; set; } = Notifications.None;
    }
}
