using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace Serialization
{
    [TestClass]
    public class XmlSerializerTest
    {
        string filename = @"C:\Development\StellarMap\SolarSystem.xml";

        [TestMethod]
        public void SolarSystemSerialize()
        {
            Star sol = SolarSystem.CreateSolSystem();

            if (File.Exists(filename))
                File.Delete(filename);

            DataContractSerializer ser = new DataContractSerializer(typeof(BaseStellarMap));

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

            DataContractSerializer ser = new DataContractSerializer(typeof(BaseStellarMap));
            BaseStellarMap map = null;

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                object obj = ser.ReadObject(fs);
                map = obj as BaseStellarMap;
            }
        }
    }
}
