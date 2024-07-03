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

        public ManagePost()
        {
            InitializeComponent();
            Posts = new ObservableCollection<ManagePostViewModel>();
            DataContext = this;
            PostsListView.ItemsSource = Posts;
            AcceptCommand = new RelayCommand(AcceptPost);
            RejectCommand = new RelayCommand(RejectPost);
            DeleteCommand = new RelayCommand(DeletePost);
        }

        private void RequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            var petPostService = new PetPostService(Injector.CreateInstance<IRepository<PetPost>>());
            Posts = new ObservableCollection<ManagePostViewModel>();

            foreach (var post in petPostService.GetPendingPetPosts())
            {
                Posts.Add(new ManagePostViewModel(post, true, false));
            }

            PostsListView.ItemsSource = Posts;
        }

        private void ActivePostsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var petPostService = new PetPostService(Injector.CreateInstance<IRepository<PetPost>>());
            var postService = new PostService(Injector.CreateInstance<IRepository<Post>>());
            Posts = new ObservableCollection<ManagePostViewModel>();

            foreach (var post in petPostService.GetAllActivePosts())
            {
                Posts.Add(new ManagePostViewModel(post, false, true));
            }

            foreach (var post in postService.GetAllActivePosts())
            {
                Posts.Add(new ManagePostViewModel(post, false, true));
            }

            PostsListView.ItemsSource = Posts;
        }

        private void AcceptPost(object parameter)
        {
            if (parameter is ManagePostViewModel viewModel)
            {
                viewModel.Post.Status = PostStatus.Active;
                var petPostService = new PetPostService(Injector.CreateInstance<IRepository<PetPost>>());
                petPostService.UpdatePost((PetPost)viewModel.Post);
                Posts = new ObservableCollection<ManagePostViewModel>();
                foreach (var post in petPostService.GetPendingPetPosts())
                {
                    Posts.Add(new ManagePostViewModel(post, true, false));
                }
                PostsListView.ItemsSource = Posts;

            }
        }

        private void RejectPost(object parameter)
        {
            if (parameter is ManagePostViewModel viewModel)
            {
                viewModel.Post.Status = PostStatus.Rejected;
                var petPostService = new PetPostService(Injector.CreateInstance<IRepository<PetPost>>());
                petPostService.UpdatePost((PetPost)viewModel.Post);
                Posts = new ObservableCollection<ManagePostViewModel>();
                foreach (var post in petPostService.GetPendingPetPosts())
                {
                    Posts.Add(new ManagePostViewModel(post, true, false));
                }
                PostsListView.ItemsSource = Posts;

            }
        }

        private void DeletePost(object parameter)
        {
            if (parameter is ManagePostViewModel viewModel)
            {
                //viewModel.Post.Status = PostStatus.Deleted;
                var petPostService = new PetPostService(Injector.CreateInstance<IRepository<PetPost>>());
                var postService = new PostService(Injector.CreateInstance<IRepository<Post>>());
                Posts = new ObservableCollection<ManagePostViewModel>();
                if (viewModel.Post is PetPost petPost)
                {
                    petPostService.DeletePost(petPost);
                }
                else
                {
                    postService.DeletePost(viewModel.Post);
                }
                foreach (var post in petPostService.GetAllActivePosts())
                {
                    Posts.Add(new ManagePostViewModel(post, false, true));
                }

                foreach (var post in postService.GetAllActivePosts())
                {
                    Posts.Add(new ManagePostViewModel(post, false, true));
                }

                PostsListView.ItemsSource = Posts;
            }
        }
    }
}
