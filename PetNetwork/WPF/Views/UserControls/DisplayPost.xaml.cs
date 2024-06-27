using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PetNetwork.WPF.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PostView.xaml
    /// </summary>
    public partial class DisplayPost : UserControl
    {

        public ObservableCollection<Post> Posts;

        public ObservableCollection<bool> LikeButtonEnabled;

        public ICommand LikeCommand { get; }

        public DisplayPost()
        {
            InitializeComponent();
            DataContext = this;
            SetupButtonNames();

            var postRepo = Injector.CreateInstance<IRepository<Post>>();
            var postService = new PostService(postRepo);
            List<bool> list = new List<bool>();
            list.Add(false);
            list.Add(false);
            LikeButtonEnabled = new(list);
            Posts = new ObservableCollection<Post>(postService.GetAllPosts());
            PostsListView.ItemsSource = Posts;
            LikeCommand = new RelayCommand(LikePost);

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

        private void LikePost(object parameter)
        {
            if (parameter is Post post)
            {
                var postRepo = Injector.CreateInstance<IRepository<Post>>();
                var postService = new PostService(postRepo);
                postService.IncrementLikeCount(post.Id);
                Posts = new ObservableCollection<Post>(postService.GetAllPosts());

                PostsListView.ItemsSource = Posts;
            }
        }


    }
}
