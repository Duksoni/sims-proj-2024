using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PetNetwork.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for RatingWindow.xaml
    /// </summary>
    public partial class RatingWindow : Window
    {
        public Post Post { get; set; }


        public RatingWindow()
        {
            InitializeComponent();
        }

        public RatingWindow(Post post)
        {
            InitializeComponent();
            this.Post = post;

        }

        private void RateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (RatingComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select a rating");
                return;
            }

            var selectedComboBoxItem = (ComboBoxItem)RatingComboBox.SelectedItem;
            string ratingStr = selectedComboBoxItem.Content.ToString()!;
            int rating = Int32.Parse(ratingStr);

            var postRatingViewModel = new PostRatingViewModel(UserSession.Session!.Account.Id, rating, Post);
            if (!postRatingViewModel.IsValid) return;

            var postRatingRepo = Injector.CreateInstance<IRepository<PostRating>>();
            var postRatingService = new PostRatingService(postRatingRepo);
            postRatingService.AddPostRating(postRatingViewModel.ToPostRating());
            MessageBox.Show(this, "Rating successful");
            Close();
        }
    }
}
