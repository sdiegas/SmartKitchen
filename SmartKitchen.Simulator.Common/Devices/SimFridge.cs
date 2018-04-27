﻿using System;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.Devices.Communication;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Devices.Core;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Engine;
using Hsr.CloudSolutions.SmartKitchen.Util;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator.Common.Devices
{
    public class SimFridge : BaseSimDevice<FridgeDto>
    {
        public SimFridge(Guid id, Point coordinate, Point size) 
            : base(id, "Fridge", coordinate, size)
        {
            _targetTemperature = 0;
            _temperature = 0;
            _door = DoorState.Closed;
        }

        public void ConfigureWith(FridgeDto dto)
        {
            if (dto == null)
            {
                return;
            }
            Temperature = dto.Temperature;
            TargetTemperature = dto.Temperature;
            Door = dto.Door;
        }

        protected override void Prepare(FridgeDto dto)
        {
            dto.Temperature = Temperature;
            dto.Door = Door;
        }

        protected override void OnCommandReceived(ICommand<FridgeDto> command)
        {
            if (command.Command == Commands.ChangeTemperature)
            {
                if (!command.HasDeviceConfig)
                {
                    return;
                }
                ChangeTemperatureTo(command.DeviceConfig.Temperature);
            }
        }

        public void ChangeTemperatureTo(double temperature)
        {
            TargetTemperature = temperature;
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

        private DoorState _door;

        public DoorState Door
        {
            get { return _door; }
            set
            {
                if (_door == value)
                {
                    return;
                }
                _door = value;
                SimulateTemperatureChange();
                OnPropertyChanged(nameof(Door));
            }
        }

        private ISimulation _temperatureSimulation;
        private DeviceTemperatureState _temperatureState = DeviceTemperatureState.None;

        private void SimulateTemperatureChange()
        {
            if (!ChangeSimulation() && Door == DoorState.Closed)
            {
                return;
            }
            _temperatureSimulation?.Dispose();
            TimeSpan interval = TimeSpan.FromMilliseconds(1000);
            if (Door == DoorState.Open)
            {
                interval = TimeSpan.FromMilliseconds(2000);
            }
            
            if (SimulationTargetTemperature > Temperature)
            {
                _temperatureSimulation = Heating(interval);
            }
            else if (SimulationTargetTemperature < Temperature)
            {
                _temperatureSimulation = Cooling(interval);
            }
            else
            {
                _temperatureSimulation = Idle();
            }
        }

        private double SimulationTargetTemperature
        {
            get
            {
                return Door == DoorState.Closed
                    ? TargetTemperature
                    : SimulationEnvironment.Current.RoomTemperature;
            }
        }

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
            return new IntervalSimulation<double>(dt => Temperature += dt, () => .1, interval, () => Temperature >= SimulationTargetTemperature, () => _temperatureState = DeviceTemperatureState.Heating, () => _temperatureState = DeviceTemperatureState.None);
        }

        private ISimulation Cooling(TimeSpan interval)
        {
            if (Temperature.Equals(SimulationTargetTemperature))
            {
                return Idle();
            }
            return new IntervalSimulation<double>(dt => Temperature -= dt, () => .1, interval, () => Temperature <= SimulationTargetTemperature, () => _temperatureState = DeviceTemperatureState.Cooling, () => _temperatureState = DeviceTemperatureState.None);
        }

        private ISimulation Idle()
        {
            return new NullSimulation(() => _temperatureState = DeviceTemperatureState.None);
        }
    }
}
