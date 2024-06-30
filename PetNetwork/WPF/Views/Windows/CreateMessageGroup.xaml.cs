using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
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

    public CreateMessageGroup()
    {
        InitializeComponent();
        DataContext = this;

        _messageGroupService = new MessageGroupService(Injector.CreateInstance<IRepository<MessageGroup>>());
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

        _messageGroupService.AddGroup(new MessageGroup(groupName, members));
        Close();
    }
}
