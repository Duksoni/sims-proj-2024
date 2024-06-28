using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class PostDisplayViewModel
{
    public Post Post { get; set; }

    public bool CanLike { get; set; }


    public PostDisplayViewModel(Post post, bool canLike)
    {
        Post = post;
        CanLike = canLike;
    }
}

