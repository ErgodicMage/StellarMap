using System;
using System.Collections.Generic;
using System.IO;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

using StellarMap.Storage;

using StellarMap.Progression;
using StellarMap.Progression.DefaultSettingMaps;

namespace GenerateMaps
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateLocalSectorJson();
            Console.WriteLine("Complete");
        }

        static string dir = "/home/harry/Development/StellarMap/Data/";

        public static void GenerateLocalSectorJson()
        {
            ProgressionMap localsector = new ProgressionMap("Local Sector");

            LocalSectorMap create = new LocalSectorMap(localsector);
            create.CreateLocalSector();

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            string filename = dir + "LocalSector.json";

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(localsector, writer);
            }
        }
    }
}
