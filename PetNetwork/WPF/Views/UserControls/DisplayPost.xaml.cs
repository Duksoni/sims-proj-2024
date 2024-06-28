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

        public ICommand RateCommand { get; }

        public DisplayPost()
        {
            InitializeComponent();
            DataContext = this;
            SetupButtonNames();

            var postRepo = Injector.CreateInstance<IRepository<Post>>();
            var postLikeRepo = Injector.CreateInstance<IRepository<PostLike>>();
            var postRatingRepo = Injector.CreateInstance<IRepository<PostRating>>();
            var postLikeService = new PostLikeService(postLikeRepo);
            var postService = new PostService(postRepo);
            var postRatingService = new PostRatingService(postRatingRepo);

            Posts = new ObservableCollection<PostDisplayViewModel>();
            foreach (var post in postService.GetAllActivePosts())
            {
                if (UserSession.Session == null)
                {
                    Posts.Add(new PostDisplayViewModel(post, false, false));
                }
                else
                {
                    Posts.Add(new PostDisplayViewModel(post,
                        !postLikeService.UserAlreadyLiked(UserSession.Session!.Account.Id, post.Id),
                        postRatingService.CanUserRate(UserSession.Session!.Account.Id, post.Id)));
                }

            }
            PostsListView.ItemsSource = Posts;
            LikeCommand = new RelayCommand(LikePost);
            CommentCommand = new RelayCommand(CommentPost);
            RateCommand = new RelayCommand(RatePost);
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
                var postRatingRepo = Injector.CreateInstance<IRepository<PostRating>>();
                var postService = new PostService(postRepo);
                var postLikeService = new PostLikeService(postLikeRepo);
                var postLikeViewModel = new PostLikeViewModel(UserSession.Session!.Account.Id, viewModel.Post);
                var postRatingService = new PostRatingService(postRatingRepo);

                if (!postLikeViewModel.IsValid) return;
                postLikeService.AddPostLike(postLikeViewModel.ToPostLike());
                postService.IncrementLikeCount(viewModel.Post.Id);
                Posts = new ObservableCollection<PostDisplayViewModel>();
                foreach (var postToCheck in postService.GetAllActivePosts())
                {
                    Posts.Add(new PostDisplayViewModel(postToCheck,
                        !postLikeService.UserAlreadyLiked(UserSession.Session.Account.Id, postToCheck.Id),
                        postRatingService.CanUserRate(UserSession.Session!.Account.Id, postToCheck.Id)));
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

        private void RatePost(object parameter)
        {
            if (parameter is PostDisplayViewModel viewModel)
            {
                var ratingWindow = new RatingWindow(viewModel.Post);
                ratingWindow.Closed += RatingWindow_Closed;
                ratingWindow.Show();
            }
        }

        private void RatingWindow_Closed(object sender, EventArgs e)
        {
            var postRepo = Injector.CreateInstance<IRepository<Post>>();
            var postLikeRepo = Injector.CreateInstance<IRepository<PostLike>>();
            var postRatingRepo = Injector.CreateInstance<IRepository<PostRating>>();
            var postService = new PostService(postRepo);
            var postLikeService = new PostLikeService(postLikeRepo);
            var postRatingService = new PostRatingService(postRatingRepo);

            Posts = new ObservableCollection<PostDisplayViewModel>();
            foreach (var postToCheck in postService.GetAllActivePosts())
            {
                Posts.Add(new PostDisplayViewModel(postToCheck,
                    !postLikeService.UserAlreadyLiked(UserSession.Session!.Account.Id, postToCheck.Id),
                    postRatingService.CanUserRate(UserSession.Session!.Account.Id, postToCheck.Id)));
            }

            PostsListView.ItemsSource = Posts;
        }


        private void CreatePostButton_OnClick(object sender, RoutedEventArgs e)
        {
            var createPostWindow = new CreatePostWindow();
            createPostWindow.Closed += CreatePost_Closed;
            createPostWindow.Show();
        }

        private void CreatePost_Closed(object sender, EventArgs e)
        {
            var postRepo = Injector.CreateInstance<IRepository<Post>>();
            var postLikeRepo = Injector.CreateInstance<IRepository<PostLike>>();
            var postRatingRepo = Injector.CreateInstance<IRepository<PostRating>>();
            var postService = new PostService(postRepo);
            var postLikeService = new PostLikeService(postLikeRepo);
            var postRatingService = new PostRatingService(postRatingRepo);

            Posts = new ObservableCollection<PostDisplayViewModel>();
            foreach (var postToCheck in postService.GetAllActivePosts())
            {
                Posts.Add(new PostDisplayViewModel(postToCheck,
                    !postLikeService.UserAlreadyLiked(UserSession.Session!.Account.Id, postToCheck.Id),
                    postRatingService.CanUserRate(UserSession.Session!.Account.Id, postToCheck.Id)));
            }

            PostsListView.ItemsSource = Posts;
        }

        private void RankDateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var postRepo = Injector.CreateInstance<IRepository<Post>>();
            var postLikeRepo = Injector.CreateInstance<IRepository<PostLike>>();
            var postRatingRepo = Injector.CreateInstance<IRepository<PostRating>>();
            var postService = new PostService(postRepo);
            var postLikeService = new PostLikeService(postLikeRepo);
            var postRatingService = new PostRatingService(postRatingRepo);

            Posts = new ObservableCollection<PostDisplayViewModel>();
            var activeCourses = postService.GetAllActivePosts().OrderByDescending(post => post.CreatedAt);
            foreach (var postToCheck in activeCourses)
            {
                if (UserSession.Session == null)
                {
                    Posts.Add(new PostDisplayViewModel(postToCheck, false, false));
                }
                else
                {
                    Posts.Add(new PostDisplayViewModel(postToCheck,
                        !postLikeService.UserAlreadyLiked(UserSession.Session!.Account.Id, postToCheck.Id),
                        postRatingService.CanUserRate(UserSession.Session!.Account.Id, postToCheck.Id)));
                }
            }

            PostsListView.ItemsSource = Posts;
        }

        private void RankLikeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var postRepo = Injector.CreateInstance<IRepository<Post>>();
            var postLikeRepo = Injector.CreateInstance<IRepository<PostLike>>();
            var postRatingRepo = Injector.CreateInstance<IRepository<PostRating>>();
            var postService = new PostService(postRepo);
            var postLikeService = new PostLikeService(postLikeRepo);
            var postRatingService = new PostRatingService(postRatingRepo);

            Posts = new ObservableCollection<PostDisplayViewModel>();
            var activeCourses = postService.GetAllActivePosts().OrderByDescending(post => post.LikeCount);
            foreach (var postToCheck in activeCourses)
            {
                if (UserSession.Session == null)
                {
                    Posts.Add(new PostDisplayViewModel(postToCheck, false, false));
                }
                else
                {
                    Posts.Add(new PostDisplayViewModel(postToCheck,
                        !postLikeService.UserAlreadyLiked(UserSession.Session!.Account.Id, postToCheck.Id),
                        postRatingService.CanUserRate(UserSession.Session!.Account.Id, postToCheck.Id)));
                }
            }

            PostsListView.ItemsSource = Posts;
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            var postRepo = Injector.CreateInstance<IRepository<Post>>();
            var postLikeRepo = Injector.CreateInstance<IRepository<PostLike>>();
            var postRatingRepo = Injector.CreateInstance<IRepository<PostRating>>();
            var postLikeService = new PostLikeService(postLikeRepo);
            var postService = new PostService(postRepo);
            var postRatingService = new PostRatingService(postRatingRepo);
            var pattern = SearchBox.Text;
            pattern = pattern.Trim();
            if (pattern == string.Empty || pattern == "")
            {
                Posts = new ObservableCollection<PostDisplayViewModel>();
                foreach (var post in postService.GetAllActivePosts())
                {
                    if (UserSession.Session == null)
                    {
                        Posts.Add(new PostDisplayViewModel(post, false, false));
                    }
                    else
                    {
                        Posts.Add(new PostDisplayViewModel(post,
                            !postLikeService.UserAlreadyLiked(UserSession.Session!.Account.Id, post.Id),
                            postRatingService.CanUserRate(UserSession.Session!.Account.Id, post.Id)));
                    }

                }
            }
            else
            {
                Posts = new ObservableCollection<PostDisplayViewModel>();
                foreach (var post in postService.SearchPosts(pattern))
                {
                    if (UserSession.Session == null)
                    {
                        Posts.Add(new PostDisplayViewModel(post, false, false));
                    }
                    else
                    {
                        Posts.Add(new PostDisplayViewModel(post,
                            !postLikeService.UserAlreadyLiked(UserSession.Session!.Account.Id, post.Id),
                            postRatingService.CanUserRate(UserSession.Session!.Account.Id, post.Id)));
                    }
                }
            }
            PostsListView.ItemsSource = Posts;


        }
    }
}
