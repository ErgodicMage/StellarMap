namespace StellarMap.Storage;

public static class MapStorageFactory
{
    #region Storage Types
    public const string JsonStorage = "JsonStorage";
    public const string ZipStorage = "ZipStorage";
    #endregion

    public static Result<IMapStorage> GetStorage(string type)
    {
        Result guardResult = GuardClause.NullOrWhiteSpace(type);
        if (!guardResult.Success) return guardResult;

        return type switch
        {
            JsonStorage => new JSonMapStorage(),
            ZipStorage => new ZipMapStorage(),
            _ => Result.Error($"MapStorageFactory:GetStorage invalid storage type {type}")
        };
    }
}
