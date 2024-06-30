using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PetNetwork.WPF.Views.UserControls;

/// <summary>
/// Interaction logic for AllChatsView.xaml
/// </summary>
public partial class AllChatsView : UserControl
{
    public ObservableCollection<string> Chats { get; set; }
    public ObservableCollection<string> Members { get; set; }
    public ObservableCollection<string> Groups { get; set; }

    private readonly MessageGroupService _messageGroupService;
    private readonly MessageService _messageService;
    private readonly UserService _userService;

    public AllChatsView()
    {
        InitializeComponent();
        DataContext = this;

        _messageGroupService = new MessageGroupService(Injector.CreateInstance<IRepository<MessageGroup>>());
        _messageService = new MessageService(Injector.CreateInstance<IRepository<Message>>(), _messageGroupService);
        var personRepo = Injector.CreateInstance<IRepository<Person>>();
        var userRepo = Injector.CreateInstance<IRepository<UserAccount>>();
        _userService = new UserService(userRepo, personRepo);

        Chats = new ObservableCollection<string>();
        LoadChats(_messageService.GetAllChats(UserSession.Session!.Account.Id));

        Members = new ObservableCollection<string>();
        LoadMembers(_userService.GetAllPersonalInfo());

        Groups = new ObservableCollection<string>();
        LoadGroups(_messageGroupService.GetAll());

        ChatListView.ItemsSource = Chats;  
    }

    private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
    {
        var createGroupWindow = new CreateMessageGroup();
        createGroupWindow.Closed += (s, e) => LoadGroups(_messageGroupService.GetAll());
        createGroupWindow.Show();
    }

    private void JoinGroupButton_Click(object sender, RoutedEventArgs e)
    {
        var groupName = GroupsComboBox.SelectedValue;
        if (groupName == null)
        {
            MessageBox.Show("Must choose a group");
            return;
        }

        var group = _messageGroupService.GetGroup((string)groupName);
        if (group == null)
        {
            MessageBox.Show("Group doesn't exist");
            return;
        }

        if (_messageGroupService.IsMember((string)groupName, UserSession.Session!.Account.Id))
        {
            MessageBox.Show("Already a member");
            return;
        }

        _messageGroupService.JoinGroup(group, UserSession.Session!.Account.Id);
        MessageBox.Show("Successfully joined", (string)groupName);
        GroupsComboBox.SelectedItem = null;
    }

    private void LeaveGroupButton_Click(object sender, RoutedEventArgs e)
    {
        var groupName = GroupsComboBox.SelectedValue;
        if (groupName == null)
        {
            MessageBox.Show("Must choose a group");
            return;
        }

        var group = _messageGroupService.GetGroup((string)groupName);
        if (group == null)
        {
            MessageBox.Show("Group doesn't exist");
            return;
        }

        if (!_messageGroupService.IsMember((string)groupName, UserSession.Session!.Account.Id))
        {
            MessageBox.Show("Not a member");
            return;
        }

        _messageGroupService.LeaveGroup(group, UserSession.Session!.Account.Id);
        MessageBox.Show("Successfully left", (string)groupName);
        GroupsComboBox.SelectedItem = null;
    }

    private void LoadChats(IList<string> chats) 
    {
        Chats.Clear();
        foreach (var chat in chats)
            Chats.Add(chat);
    }

    private void LoadMembers(IList<Person> members)
    {
        Members.Clear();
        foreach (var member in members)
            Members.Add(member.Id);
    }

    private void LoadGroups(IList<MessageGroup> groups)
    {
        Groups.Clear();
        foreach (var group in groups)
            Groups.Add(group.Id);
    }

    private void SendMessage_Click(object sender, RoutedEventArgs e)
    {
        var sendMessageWindow = new SendMessageWindow((string)MembersComboBox.SelectedValue, false);
        sendMessageWindow.Closed += (s, e) => LoadChats(_messageService.GetAllChats(UserSession.Session!.Account.Id));
        sendMessageWindow.Show();
    }
}
