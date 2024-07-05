using System.Windows.Controls;

namespace PetNetwork.WPF.Views.UserControls;

/// <summary>
/// Interaction logic for VolunteerView.xaml
/// </summary>
public partial class VolunteerView
{
    public VolunteerView()
    {
        InitializeComponent();
    }

    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.Source is TabControl)
        {
            TabItem selectedTab = (sender as TabControl).SelectedItem as TabItem;
            if (selectedTab != null && selectedTab.Header.ToString() == "Home")
            {
                displayPostControl.LoadPosts(displayPostControl.GetAllPosts());
            }
        }
    }
}