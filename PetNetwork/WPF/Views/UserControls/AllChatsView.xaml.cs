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
    public ObservableCollection<string> GroupChats { get; set; }
    public ObservableCollection<string> Members { get; set; }
    public ObservableCollection<string> Groups { get; set; }
    public ObservableCollection<string> JoinedGroups { get; set; }

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
        ChatListView.ItemsSource = Chats;  

        GroupChats = new ObservableCollection<string>();
        LoadGroupChats(_messageService.GetAllGroupChats(UserSession.Session!.Account.Id));
        GroupChatListView.ItemsSource = GroupChats;  

        Members = new ObservableCollection<string>();
        LoadRecipients(_userService.GetAllPersonalInfo());

        Groups = new ObservableCollection<string>();
        LoadGroups(_messageGroupService.GetAll());

        JoinedGroups = new ObservableCollection<string>();
        LoadJoinedGroups(_messageGroupService.GetJoinedGroups(UserSession.Session!.Account.Id));

    }

    private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
    {
        var createGroupWindow = new CreateMessageGroup();
        createGroupWindow.Closed += (s, e) => LoadGroups(_messageGroupService.GetAll());
        createGroupWindow.Closed += (s, e) => LoadJoinedGroups(_messageGroupService.GetJoinedGroups(UserSession.Session!.Account.Id));
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
        LoadJoinedGroups(_messageGroupService.GetJoinedGroups(UserSession.Session!.Account.Id));
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
        LoadJoinedGroups(_messageGroupService.GetJoinedGroups(UserSession.Session!.Account.Id));
    }

    private void LoadChats(IList<string> chats) 
    {
        Chats.Clear();
        foreach (var chat in chats)
            Chats.Add(chat);
    }

    private void LoadGroupChats(IList<string> chats)
    {
        GroupChats.Clear();
        foreach (var chat in chats)
            GroupChats.Add(chat);
    }

    private void LoadRecipients(IList<Person> members)
    {
        Members.Clear();
        foreach (var member in members)
        {
            if (member.Id == UserSession.Session!.Account.Id) continue;
            Members.Add(member.Id);
        }
    }

    private void LoadJoinedGroups(IList<MessageGroup> groups)
    {
        JoinedGroups.Clear();
        foreach (var group in groups)
            JoinedGroups.Add(group.Id);
    }

    private void LoadGroups(IList<MessageGroup> groups)
    {
        Groups.Clear();
        foreach (var group in groups)
        {
            if (group.VolunteerGroup) continue;
            Groups.Add(group.Id);
        }
    }

    private void SendMessage_Click(object sender, RoutedEventArgs e)
    {
        var recipient = RecipientsComboBox.SelectedValue;
        var group = JoinedGroupsComboBox.SelectedValue;
        if (recipient != null) 
        {
            var sendMessageWindow = new SendMessageWindow((string)recipient, false);
            sendMessageWindow.Closed += (s, e) => LoadChats(_messageService.GetAllChats(UserSession.Session!.Account.Id));
            sendMessageWindow.Show();
        }
        else if (group != null)
        {
            var sendMessageWindow = new SendMessageWindow((string)group, true);
            sendMessageWindow.Closed += (s, e) => LoadGroupChats(_messageService.GetAllChats(UserSession.Session!.Account.Id));
            sendMessageWindow.Show();

        }
        else
        {
            MessageBox.Show("Must choose a recipient");
            return;
        }
    }

    private void EmptyRecipientButton_Click(object sender, RoutedEventArgs e)
    {
        RecipientsComboBox.SelectedItem = null;   
    }

    private void EmptyGroupButton_Click(object sender, RoutedEventArgs e)
    {
        JoinedGroupsComboBox.SelectedItem = null;
    }

    private void OpenChat_TextBlock(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (sender is TextBlock textBlock)
        {
            string recipient = textBlock.Text;
            var chat = new ChatWindow(recipient, false);
            chat.Show();
        }
    }

    private void OpenGroupChat_TextBlock(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (sender is TextBlock textBlock)
        {
            string recipient = textBlock.Text;
            var groupChat = new ChatWindow(recipient, true);
            groupChat.Show();
        }
    }
}
