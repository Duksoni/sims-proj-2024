using PetNetwork.Application.Utility;

namespace PetNetwork.WPF.ViewModels;

public class MainMenuBarViewModel
{
    public RelayCommand LoginCommand => new(_ => Login());

    public RelayCommand LogoutCommand => new(_ => Logout());


    private static void Login()
    {
        var app = System.Windows.Application.Current as App;
        app?.ResetMainWindow();
    }

    private static void Logout()
    {
        UserSession.Stop();
        var app = System.Windows.Application.Current as App;
        app?.ResetMainWindow();
    }
}