using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ProtoBuf;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace Serialization
{
    [TestClass]
    public class ProtobufNetSerializerTests
    {
        string filename = @"C:\Development\StellarMap\TestData\SolarSystem.proto";
        static bool inheritanceHandled = false;

        [TestMethod]
        public void SolarSystemSerialize()
        {
            HandleInheritance();

            Star sol = SolarSystem.CreateSolSystem();

            if (File.Exists(filename))
                File.Delete(filename);

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                Serializer.Serialize<IStellarMap>(fs, sol.Map);
            }
        }

        [TestMethod]
        public void SolarSystemDeSerialization()
        {
            if (!File.Exists(filename))
                SolarSystemSerialize();
            else
                HandleInheritance();

            BaseStellarMap map = null;

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                object obj = Serializer.Deserialize<BaseStellarMap>(fs);
                map = obj as BaseStellarMap;
            }
        }

        public void HandleInheritance()
        {
            if (!inheritanceHandled)
            {
                ProtoBuf.Meta.RuntimeTypeModel.Default[typeof(StellarBody)].AddSubType(1001, typeof(Planet));
                ProtoBuf.Meta.RuntimeTypeModel.Default[typeof(StellarBody)].AddSubType(1002, typeof(Satellite));
                ProtoBuf.Meta.RuntimeTypeModel.Default[typeof(StellarBody)].AddSubType(1003, typeof(Asteroid));
                ProtoBuf.Meta.RuntimeTypeModel.Default[typeof(StellarBody)].AddSubType(1004, typeof(Comet));
                ProtoBuf.Meta.RuntimeTypeModel.Default[typeof(StellarBody)].AddSubType(1005, typeof(Star));

                inheritanceHandled = true;
            }
        }
    }
}
