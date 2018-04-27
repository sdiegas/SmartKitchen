using System;
using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices
{
    [DataContract]
    public class DeviceDto
    {
        [DataMember]
        public Guid Id { get; set; }

        public DeviceKey Key => new DeviceKey(GetType(), Id);
    }
}
