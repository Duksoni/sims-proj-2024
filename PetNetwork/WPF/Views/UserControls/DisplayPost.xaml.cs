﻿using PetNetwork.Application.UseCases;
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

        private readonly PostService _postService;
        private readonly PostLikeService _postLikeService;
        private readonly PostRatingService _postRatingService;

        public DisplayPost()
        {
            InitializeComponent();
            DataContext = this;
            SetupButtonNames();

            _postService = new PostService(Injector.CreateInstance<IRepository<Post>>());
            _postLikeService = new PostLikeService(Injector.CreateInstance<IRepository<PostLike>>());
            _postRatingService = new PostRatingService(Injector.CreateInstance<IRepository<PostRating>>());

            Posts = new ObservableCollection<PostDisplayViewModel>();
            LoadPosts(_postService.GetAllActivePosts());

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
                var postLikeViewModel = new PostLikeViewModel(UserSession.Session!.Account.Id, viewModel.Post);

                if (!postLikeViewModel.IsValid) return;

                _postLikeService.AddPostLike(postLikeViewModel.ToPostLike());
                _postService.IncrementLikeCount(viewModel.Post.Id);

                LoadPosts(_postService.GetAllActivePosts());
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
                ratingWindow.Closed += (s, e) => LoadPosts(_postService.GetAllActivePosts());
                ratingWindow.Show();
            }
        }

        private void CreatePostButton_OnClick(object sender, RoutedEventArgs e)
        {
            var createPostWindow = new CreatePostWindow();
            createPostWindow.Closed += (s, e) => LoadPosts(_postService.GetAllActivePosts());
            createPostWindow.Show();
        }

        private void RankDateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sortedPosts = _postService.GetAllActivePosts().OrderByDescending(post => post.CreatedAt);
            LoadPosts(sortedPosts);
        }

        private void RankLikeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sortedPosts = _postService.GetAllActivePosts().OrderByDescending(post => post.LikeCount);
            LoadPosts(sortedPosts);
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            var pattern = SearchBox.Text.Trim();
            var posts = string.IsNullOrEmpty(pattern) ? _postService.GetAllActivePosts() : _postService.SearchPosts(pattern);
            LoadPosts(posts);
        }

        private void LoadPosts(IEnumerable<Post> posts)
        {
            Posts.Clear();
            foreach (var post in posts)
            {
                var canLike = UserSession.Session == null ? false : !_postLikeService.UserAlreadyLiked(UserSession.Session!.Account.Id, post.Id);
                var canRate = UserSession.Session == null ? false : _postRatingService.CanUserRate(UserSession.Session!.Account.Id, post.Id);

                Posts.Add(new PostDisplayViewModel(post, canLike, canRate));
            }
        }
    }

}