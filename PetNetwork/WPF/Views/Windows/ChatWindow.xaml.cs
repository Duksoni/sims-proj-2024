using System.Windows;

namespace PetNetwork.WPF.Views.Windows;

/// <summary>
/// Interaction logic for ChatWindow.xaml
/// </summary>
public partial class ChatWindow : Window
{
    public ChatWindow(string recipient, bool group)
    {
        InitializeComponent();
    }
}
