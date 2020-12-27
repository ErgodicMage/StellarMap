using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace Serialization
{
    [TestClass]
    public class JsonNetSerializerTests
    {
        string filename = @"C:\Development\StellarMap\TestData\SolarSystem.net.json";

        [TestMethod]
        public void SolarSystemSerialize()
        {
            Star sol = SolarSystem.CreateSolSystem();

            if (File.Exists(filename))
                File.Delete(filename);

            string json = JsonConvert.SerializeObject(sol.Map);//, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(json);
            }
        }

        [TestMethod]
        public void SolarSystemDeSerialization()
        {
            if (!File.Exists(filename))
                SolarSystemSerialize();

            string json = string.Empty;

            using (StreamReader reader = new StreamReader(filename))
            {
                json = reader.ReadToEnd();
            }

            BaseStellarMap map = JsonConvert.DeserializeObject<BaseStellarMap>(json);
        }
    }
}
