using System.Runtime.Serialization;

namespace Hsr.CloudSolutions.SmartKitchen.Devices
{
    [DataContract]
    public class StoveDto : DeviceDto
    {
        [DataMember]
        public double Temperature { get; set; }

        [DataMember]
        public bool HasPan { get; set; }

        public override string ToString()
        {
            return $"Temp.: {Temperature:##.00} / Pan: {HasPan}";
        }
    }
}
