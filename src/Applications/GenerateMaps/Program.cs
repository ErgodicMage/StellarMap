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

namespace StellarMap.GenerateMaps
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
            
            HabHYGCsvReader reader = new HabHYGCsvReader();
            reader.Load(catalogueFile);

            var records = reader.Catalogue.Where<HabHygRecord>(c => c.Distance < ((1.5 * ly) / 3.261633));
            IList<HabHygRecord> stars = records.ToList<HabHygRecord>();

            CubeAreaStarLocation areaLocations = new CubeAreaStarLocation();
            IDictionary<string, IList<HabHygRecord>> areaMappings = areaLocations.GetAreaMappings(ly, stars);

            if (File.Exists(outfile))
                File.Delete(outfile);

            using (StreamWriter sw = new StreamWriter(outfile))
            {
                foreach (var area in areaMappings)
                {
                    sw.Write("Area ");
                    sw.Write(area.Key);
                    sw.WriteLine(":");

                    foreach(HabHygRecord rec in area.Value)
                    {
                        sw.Write(rec.DisplayName);
                        sw.Write(" -- (");
                        sw.Write(rec.Xg);
                        sw.Write(",");
                        sw.Write(rec.Yg);
                        sw.Write(",");
                        sw.Write(rec.Zg);
                        sw.WriteLine(")");
                    }
                    sw.WriteLine("-------");
                }
            }
        }
    }
}
