using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using System.Windows;
using System.Windows.Controls;

namespace PetNetwork.WPF.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PostView.xaml
    /// </summary>
    public partial class PostView : UserControl
    {
        public PostView()
        {
            InitializeComponent();
            SetupButtonNames();
        }

        private void SetupButtonNames()
        {
            if (UserSession.Session == null)
            {
                CreatePostButton.Visibility = Visibility.Hidden;
            }
            else if (UserSession.Session.Account.Role == AccountRole.RegularUser)
            {
                CreatePostButton.Content = "Create Post Request";
            }
            else
            {
                CreatePostButton.Content = "Create Post";
            }
        }
    }
}
