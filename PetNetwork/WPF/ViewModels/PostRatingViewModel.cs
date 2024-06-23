﻿using PetNetwork.Application.Utility;
using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;

public class PostRatingViewModel : BaseViewModel
{
    private string _id;
    private string _author;
    private string _postId;
    private int _rating;

    public string Id
    {
        get => _id;
        set
        {
            if (_id == value) return;
            _id = value;
            OnPropertyChanged();
        }
    }

    public string Author
    {
        get => _author;
        set
        {
            if (_author == value) return;
            _author = value;
            OnPropertyChanged();
        }
    }

    public string PostId
    {
        get => _postId;
        set
        {
            if (_postId == value) return;
            _postId = value;
            OnPropertyChanged();
        }
    }

    public int Rating
    {
        get => _rating;
        set
        {
            if (_rating == value) return;
            _rating = value;
            OnPropertyChanged();
        }
    }

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                "Author" => Author != string.Empty ? string.Empty : "Author can't be empty",
                "PostId" => PostId != string.Empty ? string.Empty : "PostId can't be empty",
                "Rating" => Rating >= 1 && Rating <= 5 ? string.Empty : "Rating must be between 1 and 5",
                _ => string.Empty
            };
        }
    }

    private readonly string[] _validatedProperties = { "Author", "PostId", "Rating" };

    public bool IsValid => _validatedProperties.All(property => this[property] == string.Empty);

    public PostRatingViewModel()
    {
        _id = IdGenerator.Generate();
        _author = string.Empty;
        _postId = string.Empty;
        _rating = 1; // default rating to 1
    }

    public PostRating ToPostRating() => new PostRating(Id, Author, PostId, Rating);
}
