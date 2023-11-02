namespace StorageTests;

[TestClass]
public class CoreStorage
{
    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void JsonStoreSolarSystem()
    {
        IStellarMap map = SolarSystem.CreateSolSystem();

        Result<IMapStorage> store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);
        Assert.IsTrue(store.Success);
        Assert.IsNotNull(store.Value);

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.json");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        store.Value.Store(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void JsonRetrieveSolarSystem()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.json");

        if (!File.Exists(filename))
            JsonStoreSolarSystem();

        Result<IMapStorage> store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);
        Assert.IsTrue(store.Success);
        Assert.IsNotNull(store.Value);

        using StreamReader reader = new StreamReader(filename);
        Result<IStellarMap> map = store.Value.Retreive<BaseStellarMap>(reader);
        Assert.IsTrue(map.Success);
        Assert.IsNotNull(map.Value);

        IStellarMap generatedMap = SolarSystem.CreateSolSystem();

        Assert.IsTrue(BaseStellarMapEqualityComparer.Comparer.Equals(map.Value as BaseStellarMap, generatedMap as BaseStellarMap));
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void ZipStoreSolarSystem()
    {
        IStellarMap map = SolarSystem.CreateSolSystem();

        Result<IMapStorage> store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);
        Assert.IsTrue(store.Success);
        Assert.IsNotNull(store.Value);

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.zip");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        store.Value.Store(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void ZipRetrieveSolarSystem()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.zip");

        if (!File.Exists(filename))
            ZipStoreSolarSystem();

        Result<IMapStorage> store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);
        Assert.IsTrue(store.Success);
        Assert.IsNotNull(store.Value);

        using StreamReader reader = new StreamReader(filename);
        Result<IStellarMap> map = store.Value.Retreive<BaseStellarMap>(reader);
        Assert.IsTrue(map.Success);
        Assert.IsNotNull(map.Value);

#pragma warning disable S125
        //// now serialize it to json file to inspect
        //filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem from zip.json");

        //if (File.Exists(filename))
        //    File.Delete(filename);

        //store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

        //using StreamWriter writer = new StreamWriter(filename);
        //store.Store(map, writer);
#pragma warning restore S125

        IStellarMap generatedMap = SolarSystem.CreateSolSystem();

        Assert.IsTrue(BaseStellarMapEqualityComparer.Comparer.Equals(map.Value as BaseStellarMap, generatedMap as BaseStellarMap));
    }
}
