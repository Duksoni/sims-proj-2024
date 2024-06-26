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

    };

    public static T CreateInstance<T>()
    {
        var type = typeof(T);

        if (Implementations.TryGetValue(type, out var implementation)) return (T)implementation;
        throw new ArgumentException($"No implementation found for type {type}");
    }
}