using Skillz.Services.Services.Domain;

namespace Skillz.Services.Services.Repositories;

public interface IDataStore
{
    void Init<T>() where T : IEntity;
    List<T> GetCollection<T>() where T : IEntity;
}

public class InMemoryDataStore : IDataStore
{
    private readonly IDictionary<Type, List<IEntity>?> data = new Dictionary<Type, List<IEntity>?>();

    public void Init<T>() where T : IEntity => data[typeof(T)] = [];
    
    public List<T> GetCollection<T>() where T : IEntity
    {
        if (data.TryGetValue(typeof(T), out var found))
            return (found as List<T>)!;

        throw new ArgumentException($"{typeof(T)} not found in data store");
    }
}