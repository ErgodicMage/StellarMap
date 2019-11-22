
namespace StellarMap.Progression
{
    public static class ERBridgeHelper
    {
        public static ERBridge CreateStarSystemBridge(ProgressionMap map, Cluster cluster, string starname1, string starname2)
        {
            // force mapping
            cluster.Map = map;

            StarSystem system1 = cluster.GetStarSystem(starname1);
            StarSystem system2 = cluster.GetStarSystem(starname2);

            if (system1 == null || system2 == null)
                return null;

            // force mapping
            system1.Map = map;
            system2.Map = map;

            ERBridge bridge = new ERBridge(ProgressionConstants.BridgeTypes.StarSystem, system1, system2);

            system1.Add(bridge.Portals[0]);
            system2.Add(bridge.Portals[1]);

            return bridge;
        }

        public static ERBridge CreateClusterBridge(ProgressionMap map, Sector sector, string starname1, string starname2)
        {
            sector.Map = map;

            StarSystem system1 = null;
            Cluster cluster1 = null;
            StarSystem system2 = null;
            Cluster cluster2 = null;

            foreach(var kvp in map.Clusters)
            {
                if (system1 == null)
                {
                    system1 = kvp.Value.GetStarSystem(starname1);
                    if (system1 != null)
                        cluster1 = kvp.Value;
                }
                if (system2 == null)
                {
                    system2 = kvp.Value.GetStarSystem(starname2);
                    if (system2 != null)
                        cluster2 = kvp.Value;
                }

                if (system1 != null && system2 != null)
                    break;
            }


            if (system1 == null || system2 == null || cluster1 == null || cluster2 == null || cluster1.Identifier == cluster2.Identifier)
                return null;

            ERBridge bridge = new ERBridge(ProgressionConstants.BridgeTypes.Cluster, system1, system2);

            system1.Add(bridge.Portals[0]);
            system2.Add(bridge.Portals[1]);

            return bridge;
        }
    }
}
