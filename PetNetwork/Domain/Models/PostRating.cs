﻿using PetNetwork.Domain.Interfaces;

namespace PetNetwork.Domain.Models;
public class PostRating : ISerializable
{
    public string Id { get; set; }
    public string Author { get; set; }
    public Post Post { get; set; }
    public int Rating { get; set; } // rating can be 1-5
    public bool Deleted { get; set; }


    public PostRating(string id, string author, Post post, int rating)
    {
        Id = id;
        Author = author;
        Post = post;
        Rating = rating;
        Deleted = false;
    }
}

