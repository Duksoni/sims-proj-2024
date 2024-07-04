using PetNetwork.Domain.Interfaces;
using PetNetwork.Domain.Models;
using PetNetwork.Repositories;

namespace PetNetwork.Application.Utility;

public class Injector
{

    private static readonly Dictionary<Type, object> Implementations = new()
    {
        { typeof(IRepository<UserAccount>), new JsonRepository<UserAccount>("user_accounts.json") },
        { typeof(IRepository<Person>), new JsonRepository<Person>("user_personal_info.json") },
        { typeof(IRepository<PetSociety>), new JsonRepository<PetSociety>("pet_society.json") },
        { typeof(IRepository<Post>), new JsonRepository<Post>("posts.json")},
        { typeof(IRepository<Comment>), new JsonRepository<Comment>("comments.json")},
        { typeof(IRepository<PostLike>), new JsonRepository<PostLike>("post_likes.json")},
        { typeof(IRepository<PostRating>), new JsonRepository<PostRating>("post_ratings.json")},
        { typeof(IRepository<Message>), new JsonRepository<Message>("messages.json")},
        { typeof(IRepository<MessageGroup>), new JsonRepository<MessageGroup>("message_groups.json")},
        { typeof(IRepository<Payment>), new JsonRepository<Payment>("payments.json")},
        { typeof(IRepository<Pet>), new JsonRepository<Pet>("pets.json")},
        { typeof(IRepository<AnimalType>), new JsonRepository<AnimalType>("animal_types.json")}
    };

    public static T CreateInstance<T>()
    {
        var type = typeof(T);

        if (Implementations.TryGetValue(type, out var implementation)) return (T)implementation;
        throw new ArgumentException($"No implementation found for type {type}");
    }
}