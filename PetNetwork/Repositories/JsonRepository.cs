using Newtonsoft.Json;
using PetNetwork.Domain.Interfaces;
using System.IO;
using System.Text.Json;

namespace PetNetwork.Repositories;

public class JsonRepository<T> : IRepository<T> where T : ISerializable
{
    private const string DataFolderPath = "../../../Data";
    private readonly string _filePath = DataFolderPath + "/{0}";

    public JsonRepository(string fileName)
    {
        _filePath = string.Format(_filePath, fileName);
    }

    public virtual void Add(T entry)
    {
        var data = Load();
        data.Add(entry.Id, entry);
        Save(data.Values.ToList());
    }

    public virtual void Update(T updatedEntry)
    {
        if (updatedEntry.Deleted)
        {
            Remove(updatedEntry.Id);
            return;
        }
        var data = Load();
        if (!data.ContainsKey(updatedEntry.Id)) return;
        data[updatedEntry.Id] = updatedEntry;
        Save(data.Values.ToList());
    }

    public virtual void Remove(string id)
    {
        var data = Load();
        if (!data.TryGetValue(id, out var foundEntry)) return;
        foundEntry.Deleted = true;
        Save(data.Values.ToList());
    }

    public virtual T? Get(string id)
    {
        var enumeratedEntries = GetEntriesFromFile();

        foreach (var entry in enumeratedEntries.Select(DeserializeEntry).Where(entry => entry.Id == id))
            return entry;

        return default;
    }

    public virtual IList<T> GetAll(bool includeRemoved = false)
    {
        var data = Load();
        return includeRemoved ? data.Values.ToList() : data.Values.Where(entry => !entry.Deleted).ToList();
    }

    public virtual IList<T> Find(Func<T, bool> predicate)
    {
        var data = GetAll();
        return data.Where(predicate).ToList();
    }

    protected JsonElement.ArrayEnumerator GetEntriesFromFile()
    {
        try
        {
            using StreamReader reader = new(_filePath);
            var fileContents = reader.ReadToEnd();
            var jsonDocument = JsonDocument.Parse(fileContents);
            return jsonDocument.RootElement.EnumerateArray();
        }
        catch (DirectoryNotFoundException)
        {
            Directory.CreateDirectory(DataFolderPath);
            File.WriteAllText(_filePath, "[]");
        }
        catch (FileNotFoundException)
        {
            File.WriteAllText(_filePath, "[]");
        }
        return new JsonElement.ArrayEnumerator();
    }

    protected void Save(IList<T> data)
    {
        if (!Directory.Exists(_filePath)) Directory.CreateDirectory(DataFolderPath);
        using StreamWriter writer = new(_filePath);
        writer.Write(JsonConvert.SerializeObject(data, Formatting.Indented));
    }

    protected IDictionary<string, T> Load()
    {
        var enumeratedEntries = GetEntriesFromFile();
        return enumeratedEntries.Select(DeserializeEntry).ToDictionary(entry => entry.Id);
    }

    private static T DeserializeEntry(JsonElement entry) => JsonConvert.DeserializeObject<T>(entry.GetRawText()) ?? throw new JsonReaderException();
}