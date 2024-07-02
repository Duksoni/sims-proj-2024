using PetNetwork.Domain.Enums;
using PetNetwork.WPF.ViewModels;
using System.Windows;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for UserRegistrationView.xaml
/// </summary>
public partial class UserRegistrationView
{
    private readonly UserRegistrationViewModel _viewModel;

    public UserRegistrationView(AccountRole role = AccountRole.RegularUser)
    {
        if (role == AccountRole.Admin) throw new ArgumentException("Can't add new admin");
        InitializeComponent();
        _viewModel = new UserRegistrationViewModel(role);
        DataContext = _viewModel;

        PasswordBox.PasswordChanged += (_, _) => { TrySettingPassword(); };
        _viewModel.PropertyChanged +=  (_, args) => {
            if (args.PropertyName == nameof(_viewModel.RegistrationSuccess))
                DialogResult = true;
        };
        FirstNameTBlock.MouseDown += (_, _) => { FirstNameTBox.Focus(); };
        LastNameTBlock.MouseDown += (_, _) => { LastNameTBox.Focus(); };
        PhoneTBlock.MouseDown += (_, _) => { PhoneTBox.Focus(); };
        StreetTBlock.MouseDown += (_, _) => { StreetTBox.Focus(); };
        TownTBlock.MouseDown += (_, _) => { TownTBox.Focus(); };
        StreetNoTBlock.MouseDown += (_, _) => { StreetNoTBox.Focus(); };
        PostalCodeTBlock.MouseDown += (_, _) => { PostalCodeTBox.Focus(); };
        IdCardTBlock.MouseDown += (_, _) => { IdCardTBox.Focus(); };
        EmailTBlock.MouseDown += (_, _) => { EmailAddressTBox.Focus(); };
        PasswordTBlock.MouseDown += (_, _) => { PasswordBox.Focus(); };
    }

    private void TrySettingPassword()
    {
        _viewModel.Account.Password = PasswordBox.Password;
        var errorMessage = _viewModel.Account[nameof(_viewModel.Account.Password)];
        TogglePasswordErrorMessage(errorMessage);
    }

    private void TogglePasswordErrorMessage(string errorMessage)
    {
        PasswordValidationMessageTBlock.Text = errorMessage;
        PasswordValidationMessageTBlock.Visibility = errorMessage == string.Empty ? Visibility.Hidden : Visibility.Visible;
    }
}