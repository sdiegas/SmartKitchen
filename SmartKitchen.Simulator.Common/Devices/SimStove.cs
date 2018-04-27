using System;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Devices.Core;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Engine;
using Hsr.CloudSolutions.SmartKitchen.Util;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Devices
{
    public class SimStove : BaseSimDevice<StoveDto>
    {
        public SimStove(Guid id, Point coordinate, Point size) : base(id, "Stove", coordinate, size)
        {
            _temperature = SimulationEnvironment.Current.RoomTemperature;
            HasPan = false;
            TargetTemperature = 0;
        }

        protected override void Prepare(StoveDto dto)
        {
            dto.HasPan = HasPan;
            dto.Temperature = Temperature;
        }

        protected override void OnCommandReceived(ICommand<StoveDto> command)
        {
            switch (command.Command)
            {
                case Commands.ChangeTemperature:
                    {
                        if (command.HasDeviceConfig)
                        {
                            ChangeTemperatureTo(command.DeviceConfig.Temperature);
                        }
                        break;
                    }
                case Commands.EmergencyStop:
                    {
                        ChangeTemperatureTo(0);
                        break;
                    }
            }
        }

        public void ChangeTemperatureTo(double temperature)
        {
            TargetTemperature = temperature;
        }

        private bool _hasPan;

        public bool HasPan
        {
            get { return _hasPan; }
            set
            {
                if (_hasPan == value)
                {
                    return;
                }
                _hasPan = value;
                SimulateTemperatureChange();
                OnPropertyChanged(nameof(HasPan));
            }
        }

        private double _targetTemperature;
        
        public double TargetTemperature
        {
            get { return _targetTemperature; }
            private set
            {
                if (_targetTemperature == value)
                {
                    return;
                }
                _targetTemperature = value;
                SimulateTemperatureChange();
                OnPropertyChanged(nameof(TargetTemperature));
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
                OnPropertyChanged(nameof(Temperature));
            }
        }

        private ISimulation _temperatureSimulation;
        private DeviceTemperatureState _temperatureState = DeviceTemperatureState.None;

        private void SimulateTemperatureChange()
        {
            if (!ChangeSimulation() && HasPan)
            {
                return;
            }
            _temperatureSimulation?.Dispose();

            if (SimulationTargetTemperature > Temperature)
            {
                _temperatureSimulation = Heating(TimeSpan.FromMilliseconds(1000));
            }
            else if (SimulationTargetTemperature < Temperature)
            {
                _temperatureSimulation = Cooling(TimeSpan.FromMilliseconds(2000));
            }
            else
            {
                _temperatureSimulation = Idle();
            }
        }

        private double SimulationTargetTemperature 
            => HasPan
                ? TargetTemperature
                : SimulationEnvironment.Current.RoomTemperature;

        private bool ChangeSimulation()
        {
            switch (_temperatureState)
            {
                case DeviceTemperatureState.Heating:
                    return TargetTemperature <= Temperature;
                case DeviceTemperatureState.Cooling:
                    return TargetTemperature >= Temperature;
                default:
                    return true;
            }
        }

        private ISimulation Heating(TimeSpan interval)
        {
            if (Temperature.Equals(SimulationTargetTemperature))
            {
                return Idle();
            }
            return new IntervalSimulation<double>(dt => Temperature += dt, () => 1.0, interval, () => Temperature >= SimulationTargetTemperature, () => _temperatureState = DeviceTemperatureState.Heating, () => _temperatureState = DeviceTemperatureState.None);
        }

        private ISimulation Cooling(TimeSpan interval)
        {
            if (Temperature.Equals(SimulationTargetTemperature))
            {
                return Idle();
            }
            return new IntervalSimulation<double>(dt => Temperature -= dt, () => .2, interval, () => Temperature <= SimulationTargetTemperature, () => _temperatureState = DeviceTemperatureState.Cooling, () => _temperatureState = DeviceTemperatureState.None);
        }

        private ISimulation Idle()
        {
            return new NullSimulation(() => _temperatureState = DeviceTemperatureState.None);
        }
    }
}
