namespace StorageTests;

[TestClass]
public class CoreStorage
{
    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void JsonStoreSolarSystem()
    {
        IStellarMap map = SolarSystem.CreateSolSystem();

        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.json");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        store.Store(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void JsonRetrieveSolarSystem()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.json");

        if (!File.Exists(filename))
            JsonStoreSolarSystem();

        IStellarMap map;
        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

        using StreamReader reader = new StreamReader(filename);
        map = store.Retreive<BaseStellarMap>(reader);

        IStellarMap generatedMap = SolarSystem.CreateSolSystem();

        Assert.IsTrue(BaseStellarMapEqualityComparer.Comparer.Equals(map as BaseStellarMap, generatedMap as BaseStellarMap));
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void ZipStoreSolarSystem()
    {
        IStellarMap map = SolarSystem.CreateSolSystem();

        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.zip");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        store.Store(map, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void ZipRetrieveSolarSystem()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.zip");

        if (!File.Exists(filename))
            ZipStoreSolarSystem();

        IStellarMap map;
        IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

        using StreamReader reader = new StreamReader(filename);
        map = store.Retreive<BaseStellarMap>(reader);

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

        Assert.IsTrue(BaseStellarMapEqualityComparer.Comparer.Equals(map as BaseStellarMap, generatedMap as BaseStellarMap));
    }
}
