using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace CoreTests
{
    [TestClass]
    public class SerializationTests
    {
        string folder = @"C:\Development\StellarMap\TestData\";

        [TestMethod]
        public void SerializeFileEarth()
        {
            TestStellarMap map = new TestStellarMap("Earth");
            CreateEarth(map);

            string filename = folder + "BaseEarth.json";
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
            string filename = folder + "BaseEarth.json";

            if (!File.Exists(folder + filename))
                SerializeFileEarth();

            TestStellarMap map;

            using (StreamReader reader = new StreamReader(filename))
            {
                map = DeSerializeMap(reader);
            }
        }

        [TestMethod]
        public void CompareEarthTest()
        {
            TestStellarMap originalMap = new TestStellarMap("Earth");
            TestStellarMap finalMap;

            using (MemoryStream memory = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memory, Encoding.Unicode, 1024, true))
                {
                    SerializeMap(originalMap, writer);
                    writer.Flush();
                }

                memory.Position = 0;

                using (StreamReader reader = new StreamReader(memory))
                {
                    finalMap = DeSerializeMap(reader);
                }

                Assert.IsTrue(originalMap.Equals(finalMap));
            }
        }

        [TestMethod]
        public void SerializeFileSol()
        {
            TestStellarMap map = new TestStellarMap("Sol");
            CreateEarth(map);

            string filename = folder + "BaseSol.json";
            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                SerializeMap(map, writer);
            }
        }

        [TestMethod]
        public void DeSerializeFileSol()
        {
            string filename = folder + "BaseSol.json";

            if (!File.Exists(folder + filename))
                SerializeFileEarth();

            TestStellarMap map;

            using (StreamReader reader = new StreamReader(filename))
            {
                map = DeSerializeMap(reader);
            }
        }

        [TestMethod]
        public void CompareSolTest()
        {
            TestStellarMap originalMap = new TestStellarMap("Sol");
            TestStellarMap finalMap;

            using (MemoryStream memory = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memory, Encoding.Unicode, 1024, true))
                {
                    SerializeMap(originalMap, writer);
                    writer.Flush();
                }

                memory.Position = 0;

                using (StreamReader reader = new StreamReader(memory))
                {
                    finalMap = DeSerializeMap(reader);
                }

                Assert.IsTrue(originalMap.Equals(finalMap));
            }
        }

        public void SerializeMap(TestStellarMap map, StreamWriter writer)
        {
            string json = JsonConvert.SerializeObject(map, Newtonsoft.Json.Formatting.Indented);

            writer.Write(json);
        }

        public TestStellarMap DeSerializeMap(StreamReader reader)
        {
            string json = string.Empty;

            json = reader.ReadToEnd();

            TestStellarMap map = JsonConvert.DeserializeObject<TestStellarMap>(json);
            map.SetMap();

            return map;
        }

        public void CreateEarth(TestStellarMap map)
        {
            Planet earth = new Planet("Earth");

            map.Add<Planet>(earth);

            Satellite moon = new Satellite("Moon");
            earth.Add(moon);
        }

        public void CreateSol(TestStellarMap map)
        {
            Star sol = new Star("Sol");
            map.Add<Star>(sol);
            sol.Add(new Planet("Mercury"));
            sol.Add(new Planet("Venus"));

            Planet earth = new Planet("Earth");

            map.Add<Planet>(earth);

            Satellite moon = new Satellite("Moon");
            earth.Add(moon);

            sol.Add(new Planet("Mars"));
            sol.Add(new Planet("Jupiter"));
            sol.Add(new Planet("Saturn"));
            sol.Add(new Planet("Uranus"));
            sol.Add(new Planet("Neptune"));
            sol.Add(new Planet("Pluto"));

            sol.Add(new Asteroid("Ceres"));
            sol.Add(new Asteroid("Pallas"));
            sol.Add(new Asteroid("Juno"));

            sol.Add(new Comet("Haley's"));
            sol.Add(new Comet("Caeser's"));
        }
    }
}
