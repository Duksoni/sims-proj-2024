using PetNetwork.Domain.Enums;
using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;

public class PostService
{
    private readonly IRepository<Post> _postRepository;

    public PostService(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }

    public void AddPost(Post post)
    {
        _postRepository.Add(post);
    }

    public void DeletePost(Post post)
    {
        post.Status = PostStatus.Deleted;
        _postRepository.Update(post);
    }

    public void UpdatePost(Post newPost)
    {
        if (newPost.Status == PostStatus.Deleted) return;
        Post? oldPost = _postRepository.Get(newPost.Id);
        if (oldPost == null) return;
        _postRepository.Update(newPost);
    }

    public Post? GetPost(string id)
    {
        return _postRepository.Get(id);
    }

    public IList<Post> GetAllPosts()
    {
        return _postRepository.GetAll();
    }

    public IList<Post> GetAllActivePosts()
    {
        IList<Post> actives = new List<Post>();
        foreach (var post in _postRepository.GetAll())
        {
            if (post.Status == PostStatus.Active) actives.Add(post);
        }

        return actives;
    }

    public void IncrementLikeCount(string id)
    {
        Post? post = _postRepository.Get(id);
        if (post == null) return;
        post.LikeCount++;
        _postRepository.Update(post);
    }

    public IList<Post> SearchPosts(string pattern)
    {
        IList<Post> posts = new List<Post>();
        foreach (var post in GetAllActivePosts())
        {
            if (post.Title.ToLower().Contains(pattern.ToLower())) posts.Add(post);
        }

        return posts;
    }

}
