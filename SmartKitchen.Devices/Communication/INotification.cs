namespace Hsr.CloudSolutions.SmartKitchen.Devices.Communication
{
    public interface INotification<out T>
        where T : DeviceDto
    {
        T DeviceInfo { get; }
        bool HasDeviceInfo { get; }
        string Type { get; }
    }
}
