namespace StellarMap.Storage;

public interface IMapStorage
{
    Result Store(IStellarMap map, StreamWriter writer);

    Result<IStellarMap> Retreive<T>(StreamReader reader) where T : IStellarMap, new();
}

