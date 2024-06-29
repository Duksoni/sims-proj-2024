using PetNetwork.WPF.ViewModels;

namespace PetNetwork.WPF.Views.UserControls;

/// <summary>
/// Interaction logic for AllUsersView.xaml
/// </summary>
public partial class AllUsersView
{
    public AllUsersView()
    {
        InitializeComponent();
        DataContext = new AllUsersViewModel();
    }
}