using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace PetNetwork.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for CreatePostWindow.xaml
    /// </summary>
    public partial class CreatePostWindow : Window
    {
        public PostViewModel PostViewModel { get; set; }

        public ObservableCollection<Pet> Pets { get; set; }

        public CreatePostWindow()
        {
            InitializeComponent();
            DataContext = this;
            PostViewModel = new PostViewModel();
            SetupContents();
            var petService = new PetService(Injector.CreateInstance<IRepository<Pet>>());
            Pets = new ObservableCollection<Pet>(petService.GetAll());

            PetComboBox.ItemsSource = Pets;
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

            if (PetComboBox.SelectedIndex == -1) // creating normal post
            {
                var postRepo = Injector.CreateInstance<IRepository<Post>>();
                var postService = new PostService(postRepo);
                postService.AddPost(PostViewModel.ToPost());
            }
            else // creating petpost
            {
                var selectedPet = (Pet)PetComboBox.SelectedItem;
                var petPostViewModel = new PetPostViewModel
                {
                    Id = PostViewModel.Id,
                    Title = PostViewModel.Title,
                    Description = PostViewModel.Description,
                    Author = PostViewModel.Author,
                    ImageUrl = PostViewModel.ImageUrl,
                    VideoUrl = PostViewModel.VideoUrl,
                    LikeCount = PostViewModel.LikeCount,
                    Status = PostViewModel.Status,
                    CreatedAt = PostViewModel.CreatedAt,
                    Pet = selectedPet
                };

                var petPostRepo = Injector.CreateInstance<IRepository<PetPost>>();
                var petPostService = new PetPostService(petPostRepo);
                petPostService.AddPost(petPostViewModel.ToPetPost());
            }


            Close();
        }
    }
}
