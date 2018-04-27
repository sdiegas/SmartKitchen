using System;
using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices
{
    [DataContract]
    public class DeviceKey
    {
        public DeviceKey(Type type, Guid id)
        {
            Type = type;
            Id = id;
        }

        [DataMember]
        public Type Type { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as DeviceKey;
            if (other == null)
            {
                return false;
            }
            return Type == other.Type 
                && Id == other.Id;
        }

        public override int GetHashCode()
        {
            return $"{Type.FullName}{Id}".GetHashCode();
        }

        public override string ToString()
        {
            return $"{Type.Name}_{Id}";
        }
    }
}
