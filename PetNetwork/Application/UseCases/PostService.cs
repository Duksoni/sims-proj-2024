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


}
