using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;

namespace PetNetwork.Application.UseCases;

public class CommentService
{
    private readonly IRepository<Comment> _commentRepository;

    public CommentService(IRepository<Post> commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public void AddComment(Comment comment)
    {
        _commentRepository.Add(comment);
    }

    public void DeleteComment(Comment comment)
    {
        comment.Deleted = true;
        _commentRepository.Update(comment);
    }

    public void UpdateComment(Comment newComment)
    {
        if (newComment.Deleted) return;
        Comment? oldComment = _commentRepository.Get(newComment.Id);
        if (oldComment == null) return;
        _commentRepository.Update(newComment);
    }

    public Comment? GetComment(string id)
    {
        return _commentRepository.Get(id);
    }


}