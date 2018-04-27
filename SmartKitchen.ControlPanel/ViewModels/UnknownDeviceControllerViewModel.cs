using Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication;
using Hsr.CloudSolutions.SmartKitchen.Devices;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.ViewModels
{
    public class UnknownDeviceControllerViewModel : BaseDeviceControllerViewModel<DeviceDto>
    {
        public UnknownDeviceControllerViewModel(IControlPanelDeviceClient<DeviceDto> client) : base(client, d => d)
        {
            
        }

        protected override void Configure(DeviceDto config)
        {
            
        }

        protected override void OnUpdate(DeviceDto update)
        {

        }

        protected override void Prepare(DeviceDto dto)
        {
            
        }
    }
}
