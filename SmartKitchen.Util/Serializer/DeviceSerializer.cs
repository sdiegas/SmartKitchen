using System;
using System.Collections.Generic;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.Util.Serializer
{
    public class DeviceSerializer 
        : IDeviceSerializer
    {
        private readonly IEnumerable<Type> _knownTypes = new List<Type>
        {
            typeof (FridgeDto),
            typeof (OvenDto),
            typeof (StoveDto),
            typeof (NullCommand),
            typeof (DeviceCommand<FridgeDto>),
            typeof (DeviceCommand<OvenDto>),
            typeof (DeviceCommand<StoveDto>),
            typeof (NullNotification),
            typeof (DeviceNotification<FridgeDto>),
            typeof (DeviceNotification<OvenDto>),
            typeof (DeviceNotification<StoveDto>),
        };

        private readonly ISerializer _serializer;

        public DeviceSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }


        public string Serialize<T>(T dto) where T : DeviceDto
        {
            return _serializer.Serialize(dto, _knownTypes);
        }

        public T DeserializeDto<T>(string serializedDto) where T : DeviceDto
        {
            return _serializer.Deserialize<T>(serializedDto, _knownTypes);
        }

        public string Serialize<T>(ICommand<T> command) where T : DeviceDto
        {
            return _serializer.Serialize(command, _knownTypes);
        }

        public ICommand<T> DeserializeCommand<T>(string serializedCommand) where T : DeviceDto
        {
            return _serializer.Deserialize<ICommand<T>>(serializedCommand, _knownTypes);
        }

        public string Serialize<T>(INotification<T> notification) where T : DeviceDto
        {
            return _serializer.Serialize(notification, _knownTypes);
        }

        public INotification<T> DeserializeNotification<T>(string serializedNotification) where T : DeviceDto
        {
            return _serializer.Deserialize<INotification<T>>(serializedNotification, _knownTypes);
        }

    }
}
