using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Storage;

namespace StorageTests
{
    [TestClass]
    public class CoreStorage
    {
        string dir = @"C:\Development\StellarMap\TestData\";

        [TestMethod]
        public void JsonStoreSolarSystem()
        {
            IStellarMap map = SolarSystem.CreateSolSystem();

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            string filename = dir + "SolarSystem.json";

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(map, writer);
            }
        }

        [TestMethod]
        public void JsonRetrieveSolarSystem()
        {
            string filename = dir + "SolarSystem.json";

            if (!File.Exists(filename))
                JsonStoreSolarSystem();

            IStellarMap map = new BaseStellarMap();
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            using (StreamReader reader = new StreamReader(filename))
            {
                store.Retreive(reader, map);
            }
        }

        [TestMethod]
        public void ZipStoreSolarSystem()
        {
            IStellarMap map = SolarSystem.CreateSolSystem();

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

            string filename = dir + "SolarSystem.zip";

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(map, writer);
            }
        }

        [TestMethod]
        public void ZipRetrieveSolarSystem()
        {
            string filename = dir + "SolarSystem.zip";

            if (!File.Exists(filename))
                JsonStoreSolarSystem();

            IStellarMap map = new BaseStellarMap();
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

            using (StreamReader reader = new StreamReader(filename))
            {
                store.Retreive(reader, map);
            }

            // now serialize it to json file to inspect
            filename = dir + "SolarSystem.json";

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
