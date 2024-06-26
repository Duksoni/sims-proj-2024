using System.Windows;

namespace PetNetwork.Application.Utility;

public static class MessageDisplay
{
    public static void SuccessMessage(string message, string caption) => 
        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);

    public static void ErrorMessage(string message, string caption) => 
        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
}