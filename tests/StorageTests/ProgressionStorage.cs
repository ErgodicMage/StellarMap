using StellarMap.Progression;
using StellarMap.Progression.DefaultSettingMaps;

namespace StorageTests;

[TestClass]
public class ProgressionStorage
{
    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void JsonStoreLocalSector()
    {
        ProgressionMap localsector = new ProgressionMap("Local Sector");

        LocalSectorMap create = new LocalSectorMap(localsector);
        create.CreateLocalSector();

        Result<IMapStorage> store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);
        Assert.IsTrue(store.Success);
        Assert.IsNotNull(store.Value);

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.json");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        store.Value.Store(localsector, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void JsonRetrieveSolarSystem()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.json");

        if (!File.Exists(filename))
            JsonStoreLocalSector();

        Result<IMapStorage> store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);
        Assert.IsTrue(store.Success);
        Assert.IsNotNull(store.Value);

        using StreamReader reader = new StreamReader(filename);
        Result<IStellarMap> map = store.Value.Retreive<ProgressionMap>(reader);
        Assert.IsTrue(map.Success);
        Assert.IsNotNull(map.Value);

        ProgressionMap localsector = new ProgressionMap("Local Sector");

        LocalSectorMap create = new LocalSectorMap(localsector);
        create.CreateLocalSector();

        Assert.IsTrue(BaseStellarMapEqualityComparer.Comparer.Equals(map.Value as ProgressionMap, localsector as ProgressionMap));
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void ZipStoreLocalSector()
    {
        ProgressionMap localsector = new ProgressionMap("Local Sector");

        LocalSectorMap create = new LocalSectorMap(localsector);
        create.CreateLocalSector();

        Result<IMapStorage> store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);
        Assert.IsTrue(store.Success);
        Assert.IsNotNull(store.Value);

        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.zip");

        if (File.Exists(filename))
            File.Delete(filename);

        using StreamWriter writer = new StreamWriter(filename);
        store.Value.Store(localsector, writer);
    }

    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void ZipRetrieveLocalSector()
    {
        string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.zip");

        if (!File.Exists(filename))
            ZipStoreLocalSector();

        Result<IMapStorage> store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);
        Assert.IsTrue(store.Success);
        Assert.IsNotNull(store.Value);

        using StreamReader reader = new StreamReader(filename);
        Result<IStellarMap> map = store.Value.Retreive<ProgressionMap>(reader);

        ProgressionMap localsector = new ProgressionMap("Local Sector");

        LocalSectorMap create = new LocalSectorMap(localsector);
        create.CreateLocalSector();

        Assert.IsTrue(BaseStellarMapEqualityComparer.Comparer.Equals(map.Value as ProgressionMap, localsector as ProgressionMap));
    }

}
