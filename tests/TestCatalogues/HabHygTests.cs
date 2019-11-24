using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StellarMap.Core.Types;
using StellarMap.Catalogues;
using StellarMap.Math;
using StellarMap.Math.Types;

namespace TestCatalogues
{
    [TestClass]
    public class HabHygTests
    {
        readonly string folder = @"C:\Development\StellarMap\Catalogues\";
        readonly string testdata = @"C:\Development\StellarMap\TestData\";

        [TestMethod]
        public void LoadCatalogue()
        {
            HabHygCatalogue catalogue = new HabHygCatalogue();
            catalogue.Location = folder + "HabHYG.csv";

            BaseStellarMap map = new BaseStellarMap("HabHyg All");
            catalogue.Get(map);

            map = new BaseStellarMap("HabHyg within 5ly");
            catalogue.GetWithin(map, 5);

            map = new BaseStellarMap("HabHyg within 10ly");
            catalogue.GetWithin(map, 10);

            map = new BaseStellarMap("HabHyg within 15ly");
            catalogue.GetWithin(map, 15);

            map = new BaseStellarMap("HabHyg within 20ly");
            catalogue.GetWithin(map, 20);

            map = new BaseStellarMap("HabHyg within 30ly");
            catalogue.GetWithin(map, 30);

            map = new BaseStellarMap("HabHyg within 40ly");
            catalogue.GetWithin(map, 40);

            map = new BaseStellarMap("HabHyg within 50ly");
            catalogue.GetWithin(map, 50);

            map = new BaseStellarMap("HabHyg within 75ly");
            catalogue.GetWithin(map, 75);

            map = new BaseStellarMap("HabHyg within 100ly");
            catalogue.GetWithin(map, 100);

            map = new BaseStellarMap("HabHyg higher than magnitude 4");
            catalogue.GetMagnitude(map, 4);

            map = new BaseStellarMap("HabHyg within 10ly higher that magnitude 4");
            catalogue.GetWithin(map, 10, 4);
        }

        [TestMethod]
        public void LocateStarsAreas()
        {
            HabHYGCsvReader reader = new HabHYGCsvReader();
            reader.Load(@"C:\Development\StellarMap\Catalogues\HabHYG.csv");

            double length = 20;
            
            var records = reader.Catalogue.Where<HabHygRecord>(c => c.Distance < (2 * length / 3.261633));
            IList<HabHygRecord> stars = records.ToList<HabHygRecord>();

            CubeAreaStarLocation areaLocations = new CubeAreaStarLocation();
            IDictionary<string, IList<HabHygRecord>> areaMappings = areaLocations.GetAreaMappings(length, stars);

            string outfile = testdata + "Areas" + length.ToString() + ".csv";
            if (File.Exists(outfile))
                File.Delete(outfile);

            using (StreamWriter sw = new StreamWriter(outfile))
            {
                foreach (var area in areaMappings)
                {
                    sw.WriteLine("\"" + area.Key +"\",,");

                    foreach(HabHygRecord rec in area.Value)
                    {
                        sw.Write("\"");
                        sw.Write(rec.DisplayName);
                        sw.Write("\",\"(");
                        sw.Write(rec.Xg);
                        sw.Write(",");
                        sw.Write(rec.Yg);
                        sw.Write(",");
                        sw.Write(rec.Zg);
                        sw.WriteLine(")\",");
                    }
                    sw.WriteLine("");
                }
            }
        }

        [TestMethod]
        public void ConvertCoordinates()
        {
            // TeeGarden 12.58ly RA 02h 53m 00.85s Dec +16d 52m 53.3s
            Point3d p = AstronomicalFunctions.ConvertToCartesian(12.58, 2, 53, 0.85, 16, 52, 53.3);

            // SCR 1845 (Ophir) 12.57ly RA 18h 45m 05.26s Dec ?63d 57m 47.8s
            p = AstronomicalFunctions.ConvertToCartesian(12.57, 18, 45, 5.26, -63, 57, 47.8);

            // UGPS 0722-05 (Knob) 13.4ly RA 07h 22m 27.29s Dec ?05d 40m 30.0s
            p = AstronomicalFunctions.ConvertToCartesian(13.4, 7, 22, 27.29, -5, 40, 30.0);

            // DEN 1048-3956 (Flare) 13.15ly RA 10h 48m 14.640s Dec ?39d 56m 06.24s 
            p = AstronomicalFunctions.ConvertToCartesian(13.15, 10, 48, 14.64, -39, 56, 6.24);

            // GL 628 (Lapis) 14.04ly RA 16h 30m 18.0584s Dec –12d 39m 45.325s
            p = AstronomicalFunctions.ConvertToCartesian(14.04, 16, 30, 18.06, -12, 39, 45.32);

            // GJ 205 (Bellerophon) 18.5ly RA 05h 31m 27.39595s Dec ?03° 40? 38.0311?
            p = AstronomicalFunctions.ConvertToCartesian(18.5, 5, 31, 27.4, -3, 40, 38.03);


            Point3d p2 = new Point3d(p.x / 3.261633, p.y / 3.261633, p.z / 3.261633);
            Console.WriteLine(p2);
        }
    }
}
