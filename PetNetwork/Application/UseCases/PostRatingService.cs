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
        _postRatingRepository.Get(id);
    }
}

