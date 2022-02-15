namespace StellarMap.Storage;

public interface IMapStorage
{
    void Store(IStellarMap map, StreamWriter writer);

    T Retreive<T>(StreamReader reader) where T : IStellarMap, new();
}

