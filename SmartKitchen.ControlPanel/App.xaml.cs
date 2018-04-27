using System.Windows;
using System.Windows.Threading;
using Hsr.CloudSolutions.SmartKitchen.UI;
using Unity;
using Unity.ServiceLocation;

namespace Hsr.CloudSolutions.SmartKitchen.ControlPanel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private UnityServiceLocator _serviceLocator;
        private IDialogService _dialogService;

        private async void App_OnStartup(object sender, StartupEventArgs e)
        {
            _serviceLocator = SetupDependencies();

            _dialogService = _serviceLocator.GetInstance<IDialogService>();

            var viewModel = _serviceLocator.GetInstance<MainWindowViewModel>();
            await viewModel.InitAsync();

            var view = _serviceLocator.GetInstance<MainWindow>();
            view.DataContext = viewModel;

            MainWindow = view;

            MainWindow.Show();
        }

        private UnityServiceLocator SetupDependencies()
        {
            var container = new UnityContainer();
            container.Setup();
            return new UnityServiceLocator(container);
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            _serviceLocator?.Dispose();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _dialogService.ShowException(e.Exception);
            e.Handled = true;
        }
    }
}
