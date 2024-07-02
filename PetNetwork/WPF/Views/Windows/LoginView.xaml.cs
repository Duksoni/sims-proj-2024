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
    private readonly LoginViewModel _viewModel;

    public LoginView()
    {
        InitializeComponent();
        _viewModel = new LoginViewModel();
        DataContext = _viewModel;
        _viewModel.PropertyChanged +=  (_, args) => {
            if (args.PropertyName == nameof(_viewModel.LoginResultMessage) && 
                _viewModel.LoginResultMessage == string.Empty)
                DialogResult = true;
        };
        EmailTBlock.MouseDown += (_, _) => { EmailAddressTBox.Focus(); };
        PasswordTBlock.MouseDown += (_, _) => { PasswordTBox.Focus(); };
    }

    private void PasswordTBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox pBox) return;
        _viewModel.Password = pBox.Password;
    }
}