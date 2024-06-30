using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels;
using System.Windows;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for SendMessageWindow.xaml
/// </summary>
public partial class SendMessageWindow : Window
{
    public MessageViewModel MessageViewModel { get; set; }

    public SendMessageWindow(string recipient, bool group)
    {
        InitializeComponent();
        DataContext = this;
        MessageViewModel = new MessageViewModel();

        MessageViewModel.Sender = UserSession.Session!.Account.Id;
        if (group)
            MessageViewModel.GroupName = recipient;
        else 
            MessageViewModel.Recipient = recipient;
    }

    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        if (!MessageViewModel.IsValid)
        {
            MessageBox.Show(this, "Message sending was unsuccessful");
            return;
        }

        var messageGroupService = new MessageGroupService(Injector.CreateInstance<IRepository<MessageGroup>>());
        var messageService = new MessageService(Injector.CreateInstance<IRepository<Message>>(), messageGroupService);

        messageService.AddMessage(MessageViewModel.ToMessage());
        Close();
    }
}
