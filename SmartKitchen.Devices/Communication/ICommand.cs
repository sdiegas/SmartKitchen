namespace Hsr.CloudSolutions.SmartKitchen.Devices.Communication
{
    public interface ICommand<out T>
        where T : DeviceDto
    {
        string Command { get; }
        T DeviceConfig { get; }
        bool HasDeviceConfig { get; }
    }
}
