using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels;
using PetNetwork.WPF.Views.Windows;
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

        public ObservableCollection<PostDisplayViewModel> Posts { get; set; }

        public ICommand LikeCommand { get; }

        public ICommand CommentCommand { get; }

        public DisplayPost()
        {
            InitializeComponent();
            DataContext = this;
            SetupButtonNames();

            var postRepo = Injector.CreateInstance<IRepository<Post>>();
            var postLikeRepo = Injector.CreateInstance<IRepository<PostLike>>();
            var postLikeService = new PostLikeService(postLikeRepo);
            var postService = new PostService(postRepo);
            Posts = new ObservableCollection<PostDisplayViewModel>();
            foreach (var post in postService.GetAllPosts())
            {
                if (UserSession.Session == null)
                {
                    Posts.Add(new PostDisplayViewModel(post, false));
                }
                else
                {
                    Posts.Add(new PostDisplayViewModel(post,
                        !postLikeService.UserAlreadyLiked(UserSession.Session!.Account.Id, post.Id)));
                }

            }
            PostsListView.ItemsSource = Posts;
            LikeCommand = new RelayCommand(LikePost);
            CommentCommand = new RelayCommand(CommentPost);

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
            if (parameter is PostDisplayViewModel viewModel)
            {
                var postRepo = Injector.CreateInstance<IRepository<Post>>();
                var postLikeRepo = Injector.CreateInstance<IRepository<PostLike>>();
                var postService = new PostService(postRepo);
                var postLikeService = new PostLikeService(postLikeRepo);
                var postLikeViewModel = new PostLikeViewModel(UserSession.Session!.Account.Id, viewModel.Post);

                if (!postLikeViewModel.IsValid) return;
                postLikeService.AddPostLike(postLikeViewModel.ToPostLike());
                postService.IncrementLikeCount(viewModel.Post.Id);
                Posts = new ObservableCollection<PostDisplayViewModel>();
                foreach (var postToCheck in postService.GetAllPosts())
                {
                    Posts.Add(new PostDisplayViewModel(postToCheck,
                        !postLikeService.UserAlreadyLiked(UserSession.Session.Account.Id, postToCheck.Id)));
                }

                PostsListView.ItemsSource = Posts;
            }
        }

        private void CommentPost(object parameter)
        {
            if (parameter is PostDisplayViewModel viewModel)
            {
                var commentWindow = new CommentWindow(viewModel.Post);
                commentWindow.Show();
            }
        }


    }
}
