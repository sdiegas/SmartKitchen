using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;
using Hsr.CloudSolutions.SmartKitchen.Service.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Hsr.CloudSolutions.SmartKitchen.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SmartKitchenService
        : ISmartKitchenService
    {
        private readonly ConcurrentDictionary<DeviceKey, DeviceDto> _devices = new ConcurrentDictionary<DeviceKey, DeviceDto>();

        public async Task RegisterDeviceAsync(DeviceDto device)
        {
            if (device == null)
            {
                return;
            }
            await Task.Run(() =>
            {
                Log("Register", device);
                if (_devices.Any(d => d.Key.Equals(device.Key)))
                {
                    return;
                }
                _devices.TryAdd(device.Key, device);
            });
        }

        public async Task UnregisterDeviceAsync(DeviceDto device)
        {
            if (device == null)
            {
                return;
            }
            await Task.Run(() =>
            {
                Log("Unregister", device);
                _devices.TryRemove(device.Key, out _);
            });
        }

        public async Task<IEnumerable<DeviceDto>> GetRegisteredDevicesAsync()
        {
            return await Task.Run(() =>
            {
                Log("Reading devices");
                return _devices
                    .ToArray() // Thread-safe
                    .Select(d => d.Value)
                    .ToList();
            });
        }

        private readonly ConcurrentDictionary<DeviceKey, ConcurrentQueue<ICommand<DeviceDto>>> _commandQueues = new ConcurrentDictionary<DeviceKey, ConcurrentQueue<ICommand<DeviceDto>>>();
        public async Task SendCommandAsync(ICommand<DeviceDto> command)
        {
            if (command == null || !command.HasDeviceConfig)
            {
                return;
            }
            await Task.Run(() =>
            {
                var queue = GetQueue(command.DeviceConfig, _commandQueues);
                Log("Sending command", command);
                queue.Enqueue(command);
            });
        }

        public async Task<ICommand<DeviceDto>> ReceiveCommandAsync(DeviceDto device)
        {
            if (device == null)
            {
                return new NullCommand();
            }
            return await Task.Run(() =>
            {
                if (!_commandQueues.TryGetValue(device.Key, out var queue))
                {
                    return new NullCommand();
                }
                Log("Checking commands", device);

                if (queue.Count > 0)
                {
                    queue.TryDequeue(out ICommand<DeviceDto> result);
                    return result;
                }
                return new NullCommand();
            });
        }

        private readonly ConcurrentDictionary<DeviceKey, ConcurrentQueue<INotification<DeviceDto>>> _notificationQueues = new ConcurrentDictionary<DeviceKey, ConcurrentQueue<INotification<DeviceDto>>>();

        public async Task PublishNotificationAsync(INotification<DeviceDto> update)
        {
            if (update == null || !update.HasDeviceInfo)
            {
                return;
            }
            await Task.Run(() =>
            {
                var queue = GetQueue(update.DeviceInfo, _notificationQueues);
                Log("Publish notification", update);
                queue.Enqueue(update);
            });
        }

        public async Task<INotification<DeviceDto>> PeekNotificationAsync(DeviceDto device)
        {
            if (device == null)
            {
                return new NullNotification();
            }
            return await Task.Run(() =>
            {
                if (!_notificationQueues.TryGetValue(device.Key, out var queue))
                {
                    return new NullNotification();
                }

                if (queue.Count > 0)
                {
                    queue.TryDequeue(out INotification<DeviceDto> result);
                    Log("Found notification", result);

                    return result;
                }
                return new NullNotification();
            });
        }

        private ConcurrentQueue<T> GetQueue<T>(DeviceDto device, IDictionary<DeviceKey, ConcurrentQueue<T>> queues)
        {
            if (!queues.TryGetValue(device.Key, out var queue))
            {
                queue = new ConcurrentQueue<T>();
                queues.Add(device.Key, queue);
            }
            return queue;
        }

        private void Log<T>(string message, T obj)
        {
            var typeName = "???";
            var typeInfo = "???";
            if (obj != null)
            {
                typeName = obj.GetType().Name;
                typeInfo = obj.ToString();
            }
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffff}: {message} => {typeName} / {typeInfo}");
        }

        private void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffff}: {message}");
        }
    }
}
