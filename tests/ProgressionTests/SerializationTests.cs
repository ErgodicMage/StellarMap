using System.IO;

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
        public void SerializeFileEarth()
        {
            ProgressionMap map = new ProgressionMap("Earth");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateEarth();

            string filename = folder + "Earth.json";
            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                SerializeMap(map, writer);
            }
        }

        [TestMethod]
        public void DeSerializeFileEarth()
        {
            string filename = folder + "Earth.json";

            if (!File.Exists(folder + filename))
                SerializeFileEarth();

            ProgressionMap map;

            using (StreamReader reader = new StreamReader(filename))
            {
                map = DeSerializeMap(reader);
            }
        }

        [TestMethod]
        public void SerializeFileSolSystem()
        {
            ProgressionMap map = new ProgressionMap("Sol System");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateSolSystem();

            string filename = folder + "SolSystem.json";
            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                SerializeMap(map, writer);
            }
        }

        [TestMethod]
        public void DeSerializeFileSolSystem()
        {
            string filename = folder + "SolSystem.json";

            if (!File.Exists(folder + filename))
                SerializeFileSolSystem();

            ProgressionMap map;

            using (StreamReader reader = new StreamReader(filename))
            {
                map = DeSerializeMap(reader);
            }
        }

        [TestMethod]
        public void SerializeFileSolCluster()
        {
            ProgressionMap map = new ProgressionMap("Sol Cluster");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateSolCluster();

            string filename = folder + "SolCluster.json";
            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                SerializeMap(map, writer);
            }
        }

        [TestMethod]
        public void DeSerializeFileSolCluster()
        {
            string filename = folder + "SolCluster.json";

            if (!File.Exists(folder + filename))
                SerializeFileSolCluster();

            ProgressionMap map;

            using (StreamReader reader = new StreamReader(filename))
            {
                map = DeSerializeMap(reader);
            }
        }

        [TestMethod]
        public void SerializeFileLocalSector()
        {
            ProgressionMap map = new ProgressionMap("Local Sector");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateLocalSector();

            string filename = folder + "LocalSector.json";
            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                SerializeMap(map, writer);
            }
        }

        [TestMethod]
        public void DeSerializeFileLocalSector()
        {
            string filename = folder + "LocalSector.json";

            if (!File.Exists(folder + filename))
                SerializeFileLocalSector();

            ProgressionMap map;

            using (StreamReader reader = new StreamReader(filename))
            {
                map = DeSerializeMap(reader);
            }
        }

        public void SerializeMap(ProgressionMap map, StreamWriter writer)
        {
            string json = JsonConvert.SerializeObject(map, Newtonsoft.Json.Formatting.Indented);

            writer.Write(json);
        }

        public ProgressionMap DeSerializeMap(StreamReader reader)
        {
            string json = string.Empty;

            json = reader.ReadToEnd();

            ProgressionMap map = JsonConvert.DeserializeObject<ProgressionMap>(json);
            map.SetMap();

            return map;
        }
    }
}
