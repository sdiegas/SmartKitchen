using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices.Communication
{
    [DataContract]
    [KnownType(typeof(FridgeDto))]
    [KnownType(typeof(OvenDto))]
    [KnownType(typeof(StoveDto))]
    public class DeviceNotification<T> 
        : INotification<T>
        where T : DeviceDto
    {
        public DeviceNotification(T deviceInfo, string type = Notifications.Update)
        {
            DeviceInfo = deviceInfo;
            Type = type;
        } 

        [DataMember]
        public T DeviceInfo { get; set; }

        [DataMember]
        public string Type { get; set; }

        public bool HasDeviceInfo => DeviceInfo != null;

        public override string ToString()
        {
            return $"Device: {(HasDeviceInfo ? DeviceInfo.Key.ToString() : "???")} / Type: {Type} / Info: {DeviceInfo}";
        }
    }
}
