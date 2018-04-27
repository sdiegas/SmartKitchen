using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices
{
    [DataContract]
    public enum DoorState
    {
        [EnumMember]
        Closed = 0,
        [EnumMember]
        Open = 1,
    }
}
