namespace StellarMap.Storage;

public class JSonMapStorage : IMapStorage
{
    #region Constructors
    public JSonMapStorage()
    {
    }
    #endregion

    public Result Store(IStellarMap map, StreamWriter writer)
    {
        Result guardResult = GuardClause.Null(map).Null(writer);
        if (!guardResult.Success) return guardResult;

        return Result.Try<IStellarMap, StreamWriter>((map, writer) =>
            {
                string json = JsonConvert.SerializeObject(map, Formatting.Indented);
                writer.Write(json);
            },
            map, writer);
    }

    public Result<T> Retreive<T>(StreamReader reader) where T : IStellarMap, new()
    {
        Result guardResult = GuardClause.Null(reader);
        if (!guardResult.Success) return guardResult;

        return Result<T>.Try<StreamReader>((reader) =>
            {
                string json = reader.ReadToEnd();
                T map = JsonConvert.DeserializeObject<T>(json);
                map?.SetMap();
                return map!;
            },
            reader);
    }
}

