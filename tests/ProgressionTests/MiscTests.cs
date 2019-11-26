using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using StellarMap.Core.Types;
using StellarMap.Math;
using StellarMap.Math.Types;
using StellarMap.Progression;
using StellarMap.Progression.DefaultSettingMaps;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProgressionTests
{
    [TestClass]
    public class MiscTests
    {
        string folder = @"C:\Development\StellarMap\TestData\";

        [TestMethod]
        public void GetBridgeDistances()
        {
            ProgressionMap map = new ProgressionMap("Local Sector");
            LocalSectorMap c = new LocalSectorMap(map);
            c.CreateLocalSector();

            var bridges = map.Bridges.Values.Where(b => b.BridgeType == ProgressionConstants.BridgeTypes.StarSystem);

            string bridgeFile = Path.Combine(folder, "bridges.txt");
            if (File.Exists(bridgeFile))
                File.Delete(bridgeFile);

            using (StreamWriter sw = new StreamWriter(bridgeFile))
            {
                foreach (ERBridge bridge in bridges)
                {                   
                    if (double.TryParse(bridge.BasicProperties[StellarMap.Core.Types.Constants.PropertyNames.Distance], out var distance))
                    {
                        distance = distance * 3.261633;
                        sw.Write(bridge.Name);
                        sw.Write(":");
                        sw.WriteLine(distance.ToString());
                    }               
                }
            }
        }

        [TestMethod]
        public void NearestStars()
        {
            LocalSectorStarsReader reader = new LocalSectorStarsReader();
            reader.Load(folder + "LocalSector Stars.csv");
            IList<LocalSectorStar> stars = reader.Stars;

            string starName = "Sun";
            LocalSectorStar star = stars.Where(s => s.Name == starName).First();

            double distance = 10 / 3.261633;
            var nearest = stars.Where(s => AstronomicalFunctions.Distance(s.Position, star.Position) <= distance);

            if (nearest != null)
            {
                using (StreamWriter writer = new StreamWriter(folder + "Closest.txt"))
                {
                    foreach (LocalSectorStar s in nearest)
                    {
                        writer.Write(s.Designation);
                        writer.Write(" - ");
                        writer.Write(s.Name);
                        writer.Write(" - ");
                        writer.Write(s.Cluster);
                        writer.Write(" - ");
                        writer.WriteLine(AstronomicalFunctions.Distance(s.Position, star.Position) * 3.261633);
                    }
                }
            }
        }
    }
}
