using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Progression;
using StellarMap.Progression.DefaultSettingMaps;
using StellarMap.Storage;

namespace StorageTests
{
    [TestClass]
    public class ProgressionStorage
    {
        string dir = @"C:\Development\StellarMap\TestData\";

        [TestMethod]
        public void JsonStoreLocalSector()
        {
            ProgressionMap localsector = new ProgressionMap("Local Sector");

            LocalSectorMap create = new LocalSectorMap(localsector);
            create.CreateLocalSector();

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            string filename = dir + "LocalSector.json";

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(localsector, writer);
            }
        }

        [TestMethod]
        public void JsonRetrieveSolarSystem()
        {
            string filename = dir + "LocalSector.json";

            if (!File.Exists(filename))
                JsonStoreLocalSector();

            IStellarMap map = new ProgressionMap();
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            using (StreamReader reader = new StreamReader(filename))
            {
                store.Retreive(reader, map);
            }
        }

        [TestMethod]
        public void ZipStoreLocalSector()
        {
            ProgressionMap localsector = new ProgressionMap("Local Sector");

            LocalSectorMap create = new LocalSectorMap(localsector);
            create.CreateLocalSector();

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

            string filename = dir + "LocalSector.zip";

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(localsector, writer);
            }
        }

        [TestMethod]
        public void ZipRetrieveSolarSystem()
        {
            string filename = dir + "LocalSector.zip";

            if (!File.Exists(filename))
                ZipStoreLocalSector();

            IStellarMap map = new ProgressionMap();
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

            using (StreamReader reader = new StreamReader(filename))
            {
                store.Retreive(reader, map);
            }

            // now serialize it to json file to inspect
            filename = dir + "LocalSectorCheck.json";

            if (File.Exists(filename))
                File.Delete(filename);

            store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(map, writer);
            }
        }

    }
}
