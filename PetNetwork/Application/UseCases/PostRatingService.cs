using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;
public class PostRatingService
{
    private readonly IRepository<PostRating> _postRatingRepository;

    public PostRatingService(IRepository<PostRating> postRatingRepository)
    {
        _postRatingRepository = postRatingRepository;
    }

    public void AddPostRating(PostRating postRating)
    {
        _postRatingRepository.Add(postRating);
    }

    public void DeletePostRating(PostRating postRating)
    {
        postRating.Deleted = true;
        _postRatingRepository.Update(postRating);
    }

    public void UpdatePostRating(PostRating postRating)
    {
        if (postRating.Deleted) return;
        PostRating? oldPostRating = _postRatingRepository.Get(postRating.Id);
        if (oldPostRating == null) return;
        _postRatingRepository.Update(postRating);
    }

    public PostRating? GetPostRating(string id)
    {
        return _postRatingRepository.Get(id);
    }

    public bool CanUserRate(string email, string postId)
    {
        foreach (var rating in _postRatingRepository.GetAll())
        {
            if (rating.Author == email && rating.Post.Id == postId) return false;
        }

        return true;
    }
}

