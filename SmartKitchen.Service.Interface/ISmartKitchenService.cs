using System.ServiceModel;

namespace Hsr.CloudSolutions.SmartKitchen.Service.Interface
{
    [ServiceContract]

    public interface ISmartKitchenService 
        : IControlPanelService
        , ISimulatorService
    {
        
    }
}
