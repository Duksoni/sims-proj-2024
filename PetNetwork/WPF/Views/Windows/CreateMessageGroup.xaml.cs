using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Windows;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for CreateMessageGroup.xaml
/// </summary>
public partial class CreateMessageGroup : Window
{
    private readonly MessageGroupService _messageGroupService;
    public bool VolunteerGroup = false;

    public CreateMessageGroup()
    {
        InitializeComponent();
        DataContext = this;

        _messageGroupService = new MessageGroupService(Injector.CreateInstance<IRepository<MessageGroup>>());

        if (UserSession.Session!.Account.Role == AccountRole.RegularUser)
            VolunteerButton.Visibility = Visibility.Hidden;
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        var groupName = GroupNameTextBox.Text;
        if (groupName == null)
        {
            MessageBox.Show("Group name can't be empty");
            return;
        }
        
        IList<string> members = new List<string>
        {
            UserSession.Session!.Account.Id
        };

        var group = new MessageGroup(groupName, VolunteerGroup, members);
        _messageGroupService.AddGroup(group);

        if (VolunteerGroup)
            _messageGroupService.AddAllVolunteers(group);

        Close();
    }

    private void VolunteerButton_Click(object sender, RoutedEventArgs e)
    {
        if ((string)VolunteerButton.Content == "Add all volunteers")
        {
            VolunteerButton.Content = "Remove all volunteers";
            VolunteerGroup = true;
        }
        else
        {
            VolunteerButton.Content = "Add all volunteers";
            VolunteerGroup = false;
        }
    }
}
