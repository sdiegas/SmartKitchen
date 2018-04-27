using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices
{
    [DataContract]
    public class FridgeDto : DeviceDto
    {
        [DataMember]
        public DoorState Door { get; set; }

        [DataMember]
        public double Temperature { get; set; }

        public override string ToString()
        {
            return $"Temp.: {Temperature:##.00} / Door: {Door.ToString()}";
        }
    }
}
