using StellarMap.Core.Math;

namespace StellarMap.Progression;

public static class ERBridgeHelper
{
    public static ERBridge CreateStarSystemBridge(ProgressionMap map, Cluster cluster, string starname1, string starname2)
    {
        // force mapping
        cluster.Map = map;

        StarSystem? system1 = cluster.GetStarSystem(starname1);
        StarSystem? system2 = cluster.GetStarSystem(starname2);

        if (system1 is null || system2 is null)
            return null;

        // force mapping
        system1.Map = map;
        system2.Map = map;

        ERBridge bridge = new ERBridge(ProgressionConstants.BridgeTypes.StarSystem, system1, system2);

        system1.Add(bridge.Portals[0]);
        system2.Add(bridge.Portals[1]);

        if (system1.BasicProperties.ContainsKey(StellarMap.Core.Types.Constants.PropertyNames.Position) && 
            system2.BasicProperties.ContainsKey(StellarMap.Core.Types.Constants.PropertyNames.Position))
        {
            if (Point3d.TryParse(system1.BasicProperties[StellarMap.Core.Types.Constants.PropertyNames.Position], out var p1) &&
                Point3d.TryParse(system2.BasicProperties[StellarMap.Core.Types.Constants.PropertyNames.Position], out var p2))
            {
                double distance = MathFunctions.Distance(p1, p2);
                bridge.BasicProperties.Add(StellarMap.Core.Types.Constants.PropertyNames.Distance, distance.ToString());

            }
        }

        return bridge;
    }

    public static ERBridge? CreateClusterBridge(ProgressionMap map, Sector sector, string starname1, string starname2)
    {
        sector.Map = map;

        StarSystem? system1 = default;
        Cluster? cluster1 = default;
        StarSystem? system2 = default;
        Cluster? cluster2 = default;

        foreach(var kvp in map.Clusters)
        {
            if (system1 == null)
            {
                system1 = kvp.Value.GetStarSystem(starname1);
                if (system1 is not null)
                    cluster1 = kvp.Value;
            }
            if (system2 == null)
            {
                system2 = kvp.Value.GetStarSystem(starname2);
                if (system2 is not null)
                    cluster2 = kvp.Value;
            }

            if (system1 != null && system2 != null)
                break;
        }


        if (system1 is null || system2 is null || cluster1 is null || cluster2 is null || cluster1.Identifier != cluster2.Identifier)
            return default;

        ERBridge bridge = new ERBridge(ProgressionConstants.BridgeTypes.Cluster, system1, system2);

        system1.Add(bridge.Portals[0]);
        system2.Add(bridge.Portals[1]);

        return bridge;
    }
}
