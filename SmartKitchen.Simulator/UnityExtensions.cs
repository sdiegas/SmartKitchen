using System;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Communication;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Communication.WCF;
using Hsr.CloudSolutions.SmartKitchen.Simulator.Devices;
using Hsr.CloudSolutions.SmartKitchen.UI;
using Hsr.CloudSolutions.SmartKitchen.Util.Serializer;
using Unity;
using Unity.Lifetime;

namespace Hsr.CloudSolutions.SmartKitchen.Simulator
{
    public static class UnityExtensions
    {
        public static void Setup(this IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.RegisterType<MainWindow>(new ContainerControlledLifetimeManager());
            container.RegisterType<MainWindowViewModel>(new ContainerControlledLifetimeManager());

            container.RegisterType<IDialogService, DialogService>();

            container.SetupCommunication();
            container.SetupSerialization();

            container.SetupDevices();
        }

        private static void SetupCommunication(this IUnityContainer container)
        {
            // If Azure support is implemented, comment this type registration since it's no longer needed
            container.RegisterType(typeof(ISimulatorInfoClient<>),
                typeof(WcfSimulatorInfoClient<>));
            container.RegisterType(typeof(ISimulatorDeviceClient<>),
                typeof(WcfSimulatorDeviceClient<>));

            // TODO: To support Azure, uncomment this type registration
            //container.RegisterType(typeof (ISimulatorInfoClient<>),
            //    typeof (AzureSimulatorInfoClient<>));
            //container.RegisterType(typeof (ISimulatorDeviceClient<>),
            //    typeof (AzureSimulatorDeviceClient<>));
        }

        private static void SetupSerialization(this IUnityContainer container)
        {
            container.RegisterType<IDeviceSerializer, DeviceSerializer>();

            //You may want to use a serializer. Here you can choose if you want to use
            //XML or JSON as protocol.
            //container.RegisterType<ISerializer, XmlSerializer>();
            container.RegisterType<ISerializer, JsonSerializer>();
        }

        private static void SetupDevices(this IUnityContainer container)
        {
            container.RegisterType<ISimulatorDeviceCollection, SimulatorDeviceCollection>();
            container.RegisterType<ISimulatorDeviceFactory, SimulatorDeviceFactory>();
            container.RegisterType<IDeviceLoader, SimulatorDeviceLoader>();
            //container.RegisterType<IDeviceIdFactory, StaticDeviceIdFactory>();
            container.RegisterType<IDeviceIdFactory, DynamicDeviceIdeFactory>();
        }
    }
}
