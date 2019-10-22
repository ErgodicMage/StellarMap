using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

using StellarMap.Progression;
using StellarMap.Progression.DefaultSettingMaps;

using Newtonsoft.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProgressionTests
{
    [TestClass]
    public class SerializationTests
    {
        string folder = @"C:\Development\StellarMap\TestData\";

        [TestMethod]
        public void SerializeEarth()
        {
            ProgressionMap map = new ProgressionMap("Earth");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateEarth();

            SerializeMap(map, "Earth.json");
        }

        [TestMethod]
        public void DeSerializeEarth()
        {
            string filename = "Earth.json";
            if (!File.Exists(folder + filename))
                SerializeEarth();

            ProgressionMap map = DeSerializeMap("Earth.json");
        }

        [TestMethod]
        public void SerializeSolSystem()
        {
            ProgressionMap map = new ProgressionMap("Sol System");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateSolSystem();

            SerializeMap(map, "SolSystem.json");
        }

        [TestMethod]
        public void DeSerializeSolSystem()
        {
            string filename = "SolSystem.json";
            if (!File.Exists(folder + filename))
                SerializeSolSystem();

            ProgressionMap map = DeSerializeMap("SolSystem.json");
        }

        [TestMethod]
        public void SerializeSolCluster()
        {
            ProgressionMap map = new ProgressionMap("Sol Cluster");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateSolCluster();

            SerializeMap(map, "SolCluster.json");
        }

        [TestMethod]
        public void DeSerializeSolCluster()
        {
            string filename = "SolCluster.json";
            if (!File.Exists(folder + filename))
                SerializeSolCluster();

            ProgressionMap map = DeSerializeMap("SolCluster.json");
        }

        [TestMethod]
        public void SerializeLocalSector()
        {
            ProgressionMap map = new ProgressionMap("Local Sector");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateLocalSector();

            SerializeMap(map, "LocalSector.json");
        }

        [TestMethod]
        public void DeSerializeLocalSector()
        {
            string filename = "LocalSector.json";
            if (!File.Exists(folder + filename))
                SerializeLocalSector();

            ProgressionMap map = DeSerializeMap("LocalSector.json");
        }

        public void SerializeMap(ProgressionMap map, string file)
        {
            string filename = folder + file;

            if (File.Exists(filename))
                File.Delete(filename);

            string json = JsonConvert.SerializeObject(map, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(json);
            }
        }

        public ProgressionMap DeSerializeMap(string file)
        {
            string filename = folder + file;
            string json = string.Empty;

            using (StreamReader reader = new StreamReader(filename))
            {
                json = reader.ReadToEnd();
            }

            ProgressionMap map = JsonConvert.DeserializeObject<ProgressionMap>(json);

            return map;
        }
    }
}
