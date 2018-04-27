using System;
using Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication;
using Hsr.CloudSolutions.SmartKitchen.ControlPanel.Communication.WCF;
using Hsr.CloudSolutions.SmartKitchen.ControlPanel.ViewModels;
using Hsr.CloudSolutions.SmartKitchen.Devices;
using Hsr.CloudSolutions.SmartKitchen.UI;
using Hsr.CloudSolutions.SmartKitchen.Util.Serializer;
using Unity;
using Unity.Lifetime;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel
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

            container.SetupDeviceController();
        }

        private static void SetupCommunication(this IUnityContainer container)
        {
            // If Azure support is implemented, comment this type registration since it's no longer needed
            container.RegisterType<IControlPanelInfoClient, WcfControlPanelInfoClient>();
            container.RegisterType(typeof(IControlPanelDeviceClient<>),
                typeof(WcfControlPanelDeviceClient<>));

            // TODO: To support Azure, uncomment this type registration
            //container.RegisterType<IControlPanelInfoClient, AzureControlPanelInfoClient>();
            //container.RegisterType(typeof (IControlPanelDeviceClient<>),
            //    typeof (AzureControlPanelDeviceClient<>));
        }

        private static void SetupSerialization(this IUnityContainer container)
        {
            container.RegisterType<IDeviceSerializer, DeviceSerializer>();

            //You may want to use a serializer. Here you can choose if you want to use
            //XML or JSON as protocol.
            //container.RegisterType<ISerializer, XmlSerializer>();
            container.RegisterType<ISerializer, JsonSerializer>();
        }

        private static void SetupDeviceController(this IUnityContainer container)
        {
            container.RegisterType(typeof (IDeviceControllerViewModel<>), typeof (UnknownDeviceControllerViewModel));
            container.RegisterType<IDeviceControllerViewModel<FridgeDto>, FridgeControllerViewModel>();
            container.RegisterType<IDeviceControllerViewModel<OvenDto>, OvenControllerViewModel>();
            container.RegisterType<IDeviceControllerViewModel<StoveDto>, StoveControllerViewModel>();
        }
    }
}
