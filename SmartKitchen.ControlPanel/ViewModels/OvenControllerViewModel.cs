using System.Windows.Input;
using Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;
using Hsr.CloudSolutions.SmartKitchen.UI.Commands;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.ViewModels
{
    public class OvenControllerViewModel : BaseDeviceControllerViewModel<OvenDto>
    {
        public OvenControllerViewModel(IControlPanelDeviceClient<OvenDto> client) : base(client, d => (OvenDto)d)
        {
        }

        private ICommand _emergencyShutdownCommand;

        public ICommand EmergencyShutdownCommand => _emergencyShutdownCommand ??
                                                    (_emergencyShutdownCommand = new RelayCommand(Shutdown));

        private void Shutdown(object obj)
        {
            _temperature = 0;
            SendCommand(Commands.EmergencyStop);
            OnPropertyChanged(nameof(Temperature));
        }

        private DoorState _door;
        public DoorState Door
        {
            get { return _door; }
            private set
            {
                if (_door == value)
                {
                    return;
                }
                _door = value;
                OnPropertyChanged(nameof(Door));
            }
        }

        private double _temperature;
        public double Temperature
        {
            get { return _temperature; }
            set
            {
                if (_temperature == value)
                {
                    return;
                }
                _temperature = value;
                SendCommand(Commands.ChangeTemperature);
                OnPropertyChanged(nameof(Temperature));
            }
        }

        private double _currentTemperature;

        public double CurrentTemperature
        {
            get { return _currentTemperature; }
            private set
            {
                if (_currentTemperature == value)
                {
                    return;
                }
                _currentTemperature = value;
                OnPropertyChanged(nameof(CurrentTemperature));
            }
        }

        protected override void Configure(OvenDto config)
        {
            Door = config.Door;
            Temperature = config.Temperature;
            CurrentTemperature = config.Temperature;
        }

        protected override void OnUpdate(OvenDto update)
        {
            Door = update.Door;
            CurrentTemperature = update.Temperature;
        }

        protected override void Prepare(OvenDto dto)
        {
            dto.Temperature = Temperature;
        }
    }
}
