using PetNetwork.Domain.Enums;
using PetNetwork.WPF.ViewModels;
using PetNetwork.WPF.Views.Windows;

namespace PetNetwork.WPF.Views.UserControls;

/// <summary>
/// Interaction logic for AdminView.xaml
/// </summary>
public partial class AdminView
{
    public AdminView()
    {
        InitializeComponent();
        var viewModel = new AdminViewModel();
        DataContext = viewModel;
        RegisterVolunteerBtn.Click += (_, _) =>
        {
            if (new UserRegistrationView(AccountRole.Volunteer).ShowDialog() == true)
            {
                RegisterVolunteerBtn.IsEnabled = false;
            }
        };
        if (!viewModel.CanAddVolunteer)
            RegisterVolunteerBtn.ToolTip = "There are already registered volunteers";
    }
}