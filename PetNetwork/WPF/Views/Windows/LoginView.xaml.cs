using System.Windows;
using System.Windows.Controls;
using PetNetwork.Application.Utility;
using PetNetwork.WPF.ViewModels;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginView
{
    private readonly LoginViewModel _loginViewModel;

    public LoginView()
    {
        InitializeComponent();
        _loginViewModel = new LoginViewModel();
        DataContext = _loginViewModel;
        LoginBtn.Click += (_, _) =>
        {
            _loginViewModel.TryLoginCommand.Execute();
            if (UserSession.Session != null)
                DialogResult = true;
        };
        EmailTBlock.MouseDown += (_, _) => { EmailAddressTBox.Focus(); };
        PasswordTBlock.MouseDown += (_, _) => { PasswordTBox.Focus(); };
    }

    private void PasswordTBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox pBox) return;
        _loginViewModel.Password = pBox.Password;
    }
}