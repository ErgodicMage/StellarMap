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
            //LocateStarsInCube(20);

            JsonGenerateLocalSector();
            JsonRetrieveLocalSector();

            ZipGenerateLocalSector();
            ZipRetrieveLocalSector();

            StoreSolarSystem();
            RetrieveSolarSystem();

            Console.WriteLine("Complete");
        }

        static string dir = "/home/harry/Development/StellarMap/Data/";

        public static void JsonGenerateLocalSector()
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

        public static void JsonRetrieveLocalSector()
        {
            string filename = dir + "LocalSector.json";

            //if (!File.Exists(filename))
                //GenerateLocalSector();

            IStellarMap map;
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            using (StreamReader reader = new StreamReader(filename))
            {
                map = store.Retreive<ProgressionMap>(reader);
            }
        }

        public static void ZipGenerateLocalSector()
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

        public static void ZipRetrieveLocalSector()
        {
            string filename = dir + "LocalSector.zip";

            //if (!File.Exists(filename))
                //GenerateLocalSector();

            IStellarMap map;
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.ZipStorage);

            using (StreamReader reader = new StreamReader(filename))
            {
                map = store.Retreive<ProgressionMap>(reader);
            }
        }
        public static void StoreSolarSystem()
        {
            IStellarMap map = SolarSystem.CreateSolSystem();

            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            string filename = dir + "SolarSystem.json";

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                store.Store(map, writer);
            }
        }

        public static void RetrieveSolarSystem()
        {
            string filename = dir + "SolarSystem.json";

            if (!File.Exists(filename))
                StoreSolarSystem();

            IStellarMap map;
            IMapStorage store = MapStorageFactory.GetStorage(MapStorageFactory.JsonStorage);

            using (StreamReader reader = new StreamReader(filename))
            {
                map = store.Retreive<BaseStellarMap>(reader);
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
