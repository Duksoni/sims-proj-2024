using PetNetwork.Application.UseCases;
using PetNetwork.Application.Utility;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace PetNetwork.WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for CommentWindow.xaml
    /// </summary>
    public partial class CommentWindow : Window
    {
        public Post Post { get; set; }

        public ObservableCollection<Comment> Comments { get; set; }

        public CommentWindow()
        {
            InitializeComponent();
            CommentButton.IsEnabled = UserSession.Session != null;

        }

        public CommentWindow(Post post)
        {
            InitializeComponent();
            Post = post;
            CommentButton.IsEnabled = UserSession.Session != null;
            Title = $"Comments for post: {Post.Title}";
            var commentRepo = Injector.CreateInstance<IRepository<Comment>>();
            var commentService = new CommentService(commentRepo);
            Comments = new ObservableCollection<Comment>(commentService.GetCommentsByPost(Post.Id));
            CommentListView.ItemsSource = Comments;
        }

        private void CommentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var text = CommentTextBox.Text;
            if (text == string.Empty || text == "") return;
            var commentViewModel = new CommentViewModel(text, UserSession.Session!.Account.Id, Post);
            if (!commentViewModel.IsValid) return;
            var commentRepo = Injector.CreateInstance<IRepository<Comment>>();
            var commentService = new CommentService(commentRepo);
            commentService.AddComment(commentViewModel.ToComment());
            Comments = new ObservableCollection<Comment>(commentService.GetCommentsByPost(Post.Id));
            CommentListView.ItemsSource = Comments;
            CommentTextBox.Text = "";
        }
    }
}
