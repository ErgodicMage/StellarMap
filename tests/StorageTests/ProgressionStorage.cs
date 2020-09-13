using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StellarMap.Core.Types;
using StellarMap.Progression;
using StellarMap.Progression.DefaultSettingMaps;
using StellarMap.Storage;

namespace StorageTests
{
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

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.json");

            if (File.Exists(filename))
                File.Delete(filename);

            using StreamWriter writer = new StreamWriter(filename);
            store.Store(localsector, writer);
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void JsonRetrieveSolarSystem()
        {
            string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.json");

            if (!File.Exists(filename))
                JsonStoreLocalSector();

            IStellarMap map;
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            using StreamReader reader = new StreamReader(filename);
            map = store.Retreive<ProgressionMap>(reader);
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void ZipStoreLocalSector()
        {
            ProgressionMap localsector = new ProgressionMap("Local Sector");

            LocalSectorMap create = new LocalSectorMap(localsector);
            create.CreateLocalSector();

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

            string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.zip");

            if (File.Exists(filename))
                File.Delete(filename);

            using StreamWriter writer = new StreamWriter(filename);
            store.Store(localsector, writer);
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void ZipRetrieveLocalSector()
        {
            string filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector.zip");

            if (!File.Exists(filename))
                ZipStoreLocalSector();

            IStellarMap map;
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

            using StreamReader reader = new StreamReader(filename);
            map = store.Retreive<ProgressionMap>(reader);

            // now serialize it to json file to inspect
            filename = Path.Combine(TestingUtilities.Config["DataPath"], "LocalSectorCheck.json");

            if (File.Exists(filename))
                File.Delete(filename);

            store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            using StreamWriter writer = new StreamWriter(filename);
            store.Store(map, writer);
        }

    }
}
