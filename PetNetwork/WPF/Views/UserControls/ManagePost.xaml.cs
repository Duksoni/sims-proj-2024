using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PetNetwork.WPF.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ManagePost.xaml
    /// </summary>
    public partial class ManagePost : UserControl
    {
        public ObservableCollection<ManagePostViewModel> Posts { get; set; }

        public ICommand AcceptCommand { get; set; }
        public ICommand RejectCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private readonly PetPostService _petPostService;
        private readonly PostService _postService;

        public ManagePost()
        {
            InitializeComponent();
            Posts = new ObservableCollection<ManagePostViewModel>();
            DataContext = this;

            _petPostService = new PetPostService(Injector.CreateInstance<IRepository<PetPost>>());
            _postService = new PostService(Injector.CreateInstance<IRepository<Post>>());

            PostsListView.ItemsSource = Posts;
            AcceptCommand = new RelayCommand(AcceptPost);
            RejectCommand = new RelayCommand(RejectPost);
            DeleteCommand = new RelayCommand(DeletePost);
        }

        private void RequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoadPosts(_petPostService.GetPendingPetPosts(), true, false);
        }

        private void ActivePostsButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoadPosts(_petPostService.GetAllActivePosts(), false, true);
            LoadPosts(_postService.GetAllActivePosts(), false, true);
        }

        private void LoadPosts(IEnumerable<Post> posts, bool isPending, bool isActive)
        {
            foreach (var post in posts)
            {
                Posts.Add(new ManagePostViewModel(post, isPending, isActive));
            }
        }

        private void RefreshPendingPosts()
        {
            Posts.Clear();
            LoadPosts(_petPostService.GetPendingPetPosts(), true, false);
        }

        private void AcceptPost(object parameter)
        {
            if (parameter is ManagePostViewModel viewModel)
            {
                UpdatePostStatus(viewModel, PostStatus.Active);
                RefreshPendingPosts();
            }
        }

        private void RejectPost(object parameter)
        {
            if (parameter is ManagePostViewModel viewModel)
            {
                UpdatePostStatus(viewModel, PostStatus.Rejected);
                RefreshPendingPosts();
            }
        }

        private void DeletePost(object parameter)
        {
            if (parameter is ManagePostViewModel viewModel)
            {
                if (viewModel.Post is PetPost petPost)
                {
                    _petPostService.DeletePost(petPost);
                }
                else
                {
                    _postService.DeletePost(viewModel.Post);
                }
                RefreshActivePosts();
            }
        }

        private void UpdatePostStatus(ManagePostViewModel viewModel, PostStatus status)
        {
            viewModel.Post.Status = status;

            if (viewModel.Post is PetPost petPost)
            {
                _petPostService.UpdatePost(petPost);
            }
            else
            {
                _postService.UpdatePost(viewModel.Post);
            }
        }

        private void RefreshActivePosts()
        {
            Posts.Clear();
            LoadPosts(_petPostService.GetAllActivePosts(), false, true);
            LoadPosts(_postService.GetAllActivePosts(), false, true);
        }
    }

}
