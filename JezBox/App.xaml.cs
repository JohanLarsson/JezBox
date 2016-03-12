using System.Windows;
using Ninject;

namespace JezBox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var kernel = new StandardKernel();
            kernel.Bind<AssetSyncServiceClientSettings>()
                .ToConstant(JezBox.Properties.Settings.Default.AssetSyncServiceClientSettings);
            kernel.Bind<IAssetSyncServiceClient, AssetSyncServiceClient>()
                .To<AssetSyncServiceClient>()
                .InSingletonScope();
            var window = kernel.Get<MainWindow>();
            window.Show();
        }
    }
}
