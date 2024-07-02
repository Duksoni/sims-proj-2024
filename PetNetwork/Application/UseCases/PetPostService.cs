using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;
public class PetPostService
{
    private readonly IRepository<PetPost> _petPostRepository;

    public PetPostService(IRepository<PetPost> postRepository)
    {
        _petPostRepository = postRepository;
    }

    public void AddPost(PetPost post)
    {
        _petPostRepository.Add(post);
    }

    public void DeletePost(PetPost post)
    {
        post.Status = PostStatus.Deleted;
        _petPostRepository.Update(post);
    }

    public void UpdatePost(PetPost newPost)
    {
        if (newPost.Status == PostStatus.Deleted) return;
        PetPost? oldPost = _petPostRepository.Get(newPost.Id);
        if (oldPost == null) return;
        _petPostRepository.Update(newPost);
    }

    public PetPost? GetPost(string id)
    {
        return _petPostRepository.Get(id);
    }

    public IList<PetPost> GetAllPosts()
    {
        return _petPostRepository.GetAll();
    }

    public IList<PetPost> GetAllActivePosts()
    {
        IList<PetPost> actives = new List<PetPost>();
        foreach (var post in _petPostRepository.GetAll())
        {
            if (post.Status == PostStatus.Active) actives.Add(post);
        }

        return actives;
    }

    public void IncrementLikeCount(string id)
    {
        PetPost? post = _petPostRepository.Get(id);
        if (post == null) return;
        post.LikeCount++;
        _petPostRepository.Update(post);
    }

    public IList<PetPost> SearchPosts(string pattern)
    {
        IList<PetPost> posts = new List<PetPost>();
        foreach (var post in GetAllActivePosts())
        {
            if (post.Title.ToLower().Contains(pattern.ToLower())) posts.Add(post);
        }

        return posts;
    }
}

