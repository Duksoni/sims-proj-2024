namespace PetNetwork.Domain.Interfaces;

public interface IRepository<T> where T : ISerializable
{
    void Add(T newEntry);
    void Update(T updatedEntry);
    void Remove(string id);
    T? Get(string id);
    IList<T> GetAll();
    IList<T> Find(Func<T, bool> predicate);
}