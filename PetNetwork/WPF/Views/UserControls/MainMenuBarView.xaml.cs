using PetNetwork.Application.Utility;
using PetNetwork.WPF.ViewModels;
using PetNetwork.WPF.Views.Windows;
using System.Windows;

namespace PetNetwork.WPF.Views.UserControls;

/// <summary>
/// Interaction logic for MainMenuBarView.xaml
/// </summary>
public partial class MainMenuBarView
{
    public MainMenuBarView()
    {
        InitializeComponent();

        var viewModel = new MainMenuBarViewModel();
        DataContext = viewModel;

        Exit.Command = new RelayCommand(_ => Window.GetWindow(this)?.Close());

        if (UserSession.Session != null)
        {
            Logout.IsEnabled = true;
            return;
        }

        Login.IsEnabled = true;
        Login.Click += (_, _) =>
        {
            if (new LoginView().ShowDialog() == true)
                viewModel.LoginCommand.Execute();
        };
        Register.IsEnabled = true;
        Register.Click += (_, _) =>
        {
            if (new UserRegistrationView().ShowDialog() == true)
                Register.IsEnabled = false;
        };
    }
}