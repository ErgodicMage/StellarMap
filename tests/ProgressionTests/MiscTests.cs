﻿namespace ProgressionTests;

[TestClass]
public class MiscTests
{
    [TestMethod]
    [TestCategory(TestCategories.FunctionalTest)]
    public void GetBridgeDistances()
    {
        ProgressionMap map = new ProgressionMap("Local Sector");
        LocalSectorMap c = new LocalSectorMap(map);
        c.CreateLocalSector();

        var bridges = map.Bridges.Values.Where(b => b.BridgeType == ProgressionConstants.BridgeTypes.StarSystem);

        string bridgeFile = Path.Combine(TestingUtilities.Config["DataPath"], "bridges.txt");
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
    [TestCategory(TestCategories.FunctionalTest)]
    public void NearestStars()
    {
        LocalSectorStarsReader reader = new LocalSectorStarsReader();
        reader.Load(Path.Combine(TestingUtilities.Config["DataPath"], "LocalSector Stars.csv"));
        IList<LocalSectorStar> stars = reader.Stars;

        string starName = "Little Ophiuchi";
        LocalSectorStar star = stars.Where(s => s.Name == starName).First();

        double distance = 20 / 3.261633;
        var nearest = stars.Where(s => MathFunctions.Distance(s.Position, star.Position) <= distance);

        if (nearest != null)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(TestingUtilities.Config["DataPath"], "Closest.txt")))
            {
                foreach (LocalSectorStar s in nearest)
                {
                    writer.Write(s.Designation);
                    writer.Write(" - ");
                    writer.Write(s.Name);
                    writer.Write(" - ");
                    writer.Write(s.Cluster);
                    writer.Write(" - ");
                    writer.WriteLine(MathFunctions.Distance(s.Position, star.Position) * 3.261633);
                }
            }
        }
    }
}
