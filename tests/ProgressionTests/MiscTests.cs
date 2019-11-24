using System.IO;
using System.Linq;

using StellarMap.Core.Types;

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
                    if (double.TryParse(bridge.BasicProperties[Constants.PropertyNames.Distance], out var distance))
                    {
                        distance = distance * 3.261633;
                        sw.Write(bridge.Name);
                        sw.Write(":");
                        sw.WriteLine(distance.ToString());
                    }               
                }
            }
        }
    }
}
