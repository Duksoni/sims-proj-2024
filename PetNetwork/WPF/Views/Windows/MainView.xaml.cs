using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.WPF.Views.UserControls;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainView
{
    public MainView()
    {
        InitializeComponent();
        Main.Content = UserSession.Session?.Account.Role switch
        {
            AccountRole.RegularUser => new RegularUserView(),
            AccountRole.Volunteer => new VolunteerView(),
            AccountRole.Admin => new AdminView(),
            _ => new DefaultView()
        };
    }

}