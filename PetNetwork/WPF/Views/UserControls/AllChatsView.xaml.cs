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

        ChatListView.ItemsSource = Chats;  
    }

    private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void JoinGroupButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void LeaveGroupButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void LoadChats(IList<string> chats) 
    {
        Chats.Clear();
        foreach (var chat in chats)
        {
            Chats.Add(chat);
        }
    }

    private void LoadMembers(IList<Person> members)
    {
        Members.Clear();
        foreach (var member in members)
        {
            Members.Add(member.Id);
        }
    }

    private void SendMessage_Click(object sender, RoutedEventArgs e)
    {
        var sendMessageWindow = new SendMessageWindow((string)MembersComboBox.SelectedValue, false);
        sendMessageWindow.Closed += (s, e) => LoadChats(_messageService.GetAllChats(UserSession.Session!.Account.Id));
        sendMessageWindow.Show();
    }
}
