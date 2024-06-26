using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;
public class PostLikeService
{
    private readonly IRepository<PostLike> _postLikeRepository;

    public PostLikeService(IRepository<PostLike> postLikeRepository)
    {
        _postLikeRepository = postLikeRepository;
    }

    public void AddPostLike(PostLike postLike)
    {
        _postLikeRepository.Add(postLike);
    }

    public void DeletePostLike(PostLike postLike)
    {
        postLike.Deleted = true;
        _postLikeRepository.Update(postLike);
    }

    public void UpdatePostLike(PostLike newPostLike)
    {
        if (newPostLike.Deleted) return;
        PostLike? oldPostLike = _postLikeRepository.Get(newPostLike.Id);
        if (oldPostLike == null) return;
        _postLikeRepository.Update(newPostLike);
    }

    public PostLike? GetPostLike(string id)
    {
        return _postLikeRepository.Get(id);
    }
}

