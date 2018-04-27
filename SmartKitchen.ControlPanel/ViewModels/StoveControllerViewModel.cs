using System;
using Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel.ViewModels
{
    public class StoveControllerViewModel : BaseDeviceControllerViewModel<StoveDto>
    {
        public StoveControllerViewModel(IControlPanelDeviceClient<StoveDto> client) : base(client, d => (StoveDto)d)
        {
        }

        private bool _hasPan;

        public bool HasPan
        {
            get { return _hasPan; }
            private set
            {
                if (_hasPan == value)
                {
                    return;
                }
                _hasPan = value;
                OnPropertyChanged(nameof(HasPan));
            }
        }

        private const double TemperatureStepSize = 15.0;
        public int TemperatureStep
        {
            get { return (int) Math.Round(Temperature / TemperatureStepSize); }
            set
            {
                if (TemperatureStep == value)
                {
                    return;
                }
                Temperature = value*TemperatureStepSize;
            }
        }

        private double _temperature;
        public double Temperature
        {
            get { return _temperature; }
            private set
            {
                if (_temperature == value)
                {
                    return;
                }
                _temperature = value;
                SendCommand(Commands.ChangeTemperature);
                OnPropertyChanged(nameof(Temperature));
                OnPropertyChanged(nameof(TemperatureStep));
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



        protected override void Configure(StoveDto config)
        {
            HasPan = config.HasPan;
            Temperature = config.Temperature;
        }

        protected override void OnUpdate(StoveDto update)
        {
            HasPan = update.HasPan;
            CurrentTemperature = update.Temperature;
        }

        protected override void Prepare(StoveDto dto)
        {
            dto.Temperature = Temperature;
        }
    }
}
