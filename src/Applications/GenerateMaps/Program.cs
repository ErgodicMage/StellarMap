using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

using StellarMap.Storage;

using StellarMap.Catalogues;

using StellarMap.Progression;
using StellarMap.Progression.DefaultSettingMaps;

namespace GenerateMaps
{
    class Program
    {
        static void Main(string[] args)
        {
            LocateStarsInCube(20);
            //GenerateLocalSectorJson();
            Console.WriteLine("Complete");
        }

        static string dir = "/home/harry/Development/StellarMap/Data/";

        public static void GenerateLocalSectorJson()
        {
            ProgressionMap localsector = new ProgressionMap("Local Sector");

            LocalSectorMap create = new LocalSectorMap(localsector);
            create.CreateLocalSector();

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

            string filename = dir + "LocalSector.zip";

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(localsector, writer);
            }
        }

        public static void LocateStarsInCube(int ly)        
        {
            string outfile = dir + "AreasForStars_" + ly.ToString() + "LY.txt";
            string catalogueFile = dir + "HabHYG.csv";

            if (File.Exists(outfile))
                File.Delete(outfile);

            HabHYGCsvReader reader = new HabHYGCsvReader();
            reader.Load(catalogueFile);

            double parsecs = ly / 3.261633;

            var records = reader.Catalogue.Where<HabHygRecord>(c => c.Distance < parsecs);

            using (StreamWriter sw = new StreamWriter(outfile))
            {
                records.ToList().ForEach(c => {sw.WriteLine(c.DisplayName + " " + c.Distance.ToString());});
            }
        }
    }
}
