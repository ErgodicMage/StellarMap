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
        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void JsonStoreSolarSystem()
        {
            IStellarMap map = SolarSystem.CreateSolSystem();

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.json");

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(map, writer);
            }
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

            using (StreamReader reader = new StreamReader(filename))
            {
                map = store.Retreive<BaseStellarMap>(reader);
            }
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

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(map, writer);
            }
        }

        [TestMethod]
        [TestCategory(TestCategories.FunctionalTest)]
        public void ZipRetrieveSolarSystem()
        {
            string filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.zip");

            if (!File.Exists(filename))
                JsonStoreSolarSystem();

            IStellarMap map;
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

            using (StreamReader reader = new StreamReader(filename))
            {
                map = store.Retreive<BaseStellarMap>(reader);
            }

            // now serialize it to json file to inspect
            filename = Path.Combine(TestingUtilities.Config["DataPath"], "SolarSystem.json");

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
