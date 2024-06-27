using PetNetwork.WPF.Views.Windows;
using System.Windows;

namespace PetNetwork;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        MainWindow = new MainView();
        MainWindow.Show();
    }

    public void ResetMainWindow()
    {
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
        MainWindow?.Close();
        ShutdownMode = ShutdownMode.OnMainWindowClose;
        MainWindow = new MainView();
        MainWindow.Show();
    }
}