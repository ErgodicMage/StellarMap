using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Types;

namespace StellarMap.Storage
{
    public static class MapStorageFactory
    {
        #region Storage Types
        public const string JsonStorage = "JsonStorage";
        public const string ZipStorage = "ZipStorage";
        #endregion

        public static IMapStorage GetStorage(string type)
        {
            IMapStorage storage = null;

            switch (type)
            {
                case JsonStorage:
                    storage = new JSonMapStorage();
                    break;
                case ZipStorage:
                    storage = new ZipMapStorage();
                    break;
            }

            return storage;
        }
    }
}
