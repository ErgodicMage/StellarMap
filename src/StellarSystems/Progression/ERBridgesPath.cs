using System.Linq;

namespace StellarMap.Progression
{
    public class ERBridgesPath
    {
        public ERBridgesPath() => Bridges = new List<ERBridge>();

        public ERBridgesPath(ERBridgesPath path) => Bridges = new List<ERBridge>(path.Bridges);


        public IList<ERBridge> Bridges { get; init; }

        public int NumberofTolls() => Bridges.Count;

        public int TollCosts()
        {
            return Bridges.Sum(b => b.BridgeType switch
                                        {
                                            ProgressionConstants.BridgeTypes.StarSystem => 1,
                                            ProgressionConstants.BridgeTypes.Cluster => 10,
                                            ProgressionConstants.BridgeTypes.Sector => 100,
                                            ProgressionConstants.BridgeTypes.Region => 1000,
                                            ProgressionConstants.BridgeTypes.District => 10000,
                                            ProgressionConstants.BridgeTypes.Zone => 100000,
                                            ProgressionConstants.BridgeTypes.Sphere => 1000000,
                                            ProgressionConstants.BridgeTypes.Quadrant => 10000000,
                                            _ => 0
                                        }
                                );
        }

        public double TotalDistance() => Bridges.Sum(b => MathFunctions.Distance(b.Portals[0].Position, b.Portals[1].Position));
    }
}
