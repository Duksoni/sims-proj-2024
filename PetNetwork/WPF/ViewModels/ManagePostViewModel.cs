using PetNetwork.Domain.Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PetNetwork.WPF.ViewModels;
public class ManagePostViewModel
{
    public Post Post { get; set; }

    public bool CanAccept { get; set; }

    public bool CanDelete { get; set; }

    public Pet? Pet { get; set; }

    public ImageSource? ImageSource { get; set; }

    public MediaElement? MediaElement { get; set; }


    public ManagePostViewModel(Post post, bool canAccept, bool canDelete)
    {
        Post = post;
        CanAccept = canAccept;
        CanDelete = canDelete;
        ImageSource = LoadImage();
        MediaElement = LoadVideo();

        if (post is PetPost petPost)
        {
            Pet = petPost.Pet;
        }
    }


    private ImageSource? LoadImage()
    {
        // Convert imageUrl to ImageSource
        if (!string.IsNullOrEmpty(Post.ImageUrl))
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(Post.ImageUrl);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            ImageSource = bitmap;
        }
        else
        {
            ImageSource = null; // Or set a default image source
        }
        return ImageSource;
    }

    private MediaElement? LoadVideo()
    {
        // Convert videoUrl to MediaElement
        if (!string.IsNullOrEmpty(Post.VideoUrl))
        {
            MediaElement = new MediaElement();
            MediaElement.Source = new Uri(Post.VideoUrl);
            MediaElement.LoadedBehavior = MediaState.Manual; // Ensure the video doesn't auto-play
        }
        else
        {
            MediaElement = null; // Or set a default media element
        }
        return MediaElement;
    }
}

