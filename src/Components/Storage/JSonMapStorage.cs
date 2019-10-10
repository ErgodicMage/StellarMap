using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

using Newtonsoft.Json;

namespace StellarMap.Storage
{
    public class JSonMapStorage : IMapStorage
    {
        #region Constructors
        public JSonMapStorage()
        {
        }
        #endregion

        public void Store(IStellarMap map, StreamWriter writer)
        {
            string json = JsonConvert.SerializeObject(map, Formatting.Indented);

            writer.Write(json);
        }

        public T Retreive<T>(StreamReader reader) where T : IStellarMap, new()
        {
            string json = reader.ReadToEnd();

            T map = JsonConvert.DeserializeObject<T>(json);

            return map;
        }
    }
}
