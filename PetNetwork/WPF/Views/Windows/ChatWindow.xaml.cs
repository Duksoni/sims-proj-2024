﻿using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for ChatWindow.xaml
/// </summary>
public partial class ChatWindow : Window
{
    public string Recipient;
    public bool IsGroup;

    private readonly MessageGroupService _messageGroupService;
    private readonly MessageService _messageService;

    public ObservableCollection<MessageViewModel> Messages { get; set; }

    public ChatWindow(string recipient, bool isGroup)
    {
        InitializeComponent();
        DataContext = this;

        _messageGroupService = new MessageGroupService(Injector.CreateInstance<IRepository<MessageGroup>>());
        _messageService = new MessageService(Injector.CreateInstance<IRepository<Message>>(), _messageGroupService);

        Recipient = recipient;
        IsGroup = isGroup;

        Messages = new ObservableCollection<MessageViewModel>();
        LoadMessages(_messageService.GetMessagesForChat(UserSession.Session!.Account.Id, Recipient));
        MessagesListView.ItemsSource = Messages;

    }

    private void LoadMessages(IList<Message> messages)
    {
        Messages.Clear();
        foreach (var message in messages)
        {
            Messages.Add(new MessageViewModel(message));
        }
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void SendMessageButton_Click(object sender, RoutedEventArgs e)
    {
        var sendMessageWindow = new SendMessageWindow(Recipient, IsGroup);
        sendMessageWindow.Closed += (s, e) => LoadMessages(_messageService.GetMessagesForChat(UserSession.Session!.Account.Id, Recipient));
        sendMessageWindow.Show();
    }
}