using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace Serialization
{
    [TestClass]
    public class JsonSerializerTests
    {
        string filename = @"C:\Development\StellarMap\TestData\SolarSystem.ms.json";

        [TestMethod]
        public void SolarSystemSerialize()
        {
            Star sol = SolarSystem.CreateSolSystem();

            if (File.Exists(filename))
                File.Delete(filename);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BaseStellarMap));

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                ser.WriteObject(fs, sol.Map);
            }
        }

        [TestMethod]
        public void SolarSystemDeSerialization()
        {
            if (!File.Exists(filename))
               SolarSystemSerialize();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BaseStellarMap));
            BaseStellarMap map = null;

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                object obj = ser.ReadObject(fs);
                map = obj as BaseStellarMap;
            }
        }
    }
}
