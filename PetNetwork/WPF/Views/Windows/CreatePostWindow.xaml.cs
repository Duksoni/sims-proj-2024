using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels;
using System.Windows;

namespace PetNetwork.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for CreatePostWindow.xaml
    /// </summary>
    public partial class CreatePostWindow : Window
    {
        public PostViewModel PostViewModel { get; set; }

        public CreatePostWindow()
        {
            InitializeComponent();
            DataContext = this;
            PostViewModel = new PostViewModel();
            SetupContents();
        }

        private void SetupContents()
        {
            if (UserSession.Session!.Account.Role == AccountRole.RegularUser)
            {
                CreateTitle.Text = "Create Post Request";
                CreateButton.Content = "Create Post Request";
            }
            else
            {
                CreateTitle.Text = "Create Post";
                CreateButton.Content = "Create Post";
            }
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            PostViewModel.Author = UserSession.Session!.Account.Id;
            if (!PostViewModel.IsValid)
            {
                MessageBox.Show(this, "Post creation unsuccessful!");
                return;
            }
            if (UserSession.Session!.Account.Role == AccountRole.RegularUser)
            {
                PostViewModel.Status = PostStatus.PendingApproval;
            }
            else
            {
                PostViewModel.Status = PostStatus.Active;
            }

            var postRepo = Injector.CreateInstance<IRepository<Post>>();
            var postService = new PostService(postRepo);
            postService.AddPost(PostViewModel.ToPost());
            Close();
        }
    }
}
