using StellarMap.Core.Math;
using System.Diagnostics.Metrics;

namespace StellarMap.Progression;

public static class ERBridgeHelper
{
    public static Result<ERBridge> CreateStarSystemBridge(ProgressionMap map, Cluster cluster, string starname1, string starname2)
    {
        Result guardResult = GuardClause.Null(map).Null(cluster).NullOrWhiteSpace(starname1).NullOrWhiteSpace(starname2);
        if (!guardResult.Success) return guardResult;

        // force mapping
        cluster.Map = map;

        var sysResult1 = cluster.GetStarSystem(starname1);
        var sysResult2 = cluster.GetStarSystem(starname2);

        if (!sysResult1.Success) return (Result)sysResult1;
        if (!sysResult2.Success) return (Result)sysResult2;

        var system1 = sysResult1.Value;
        var system2 = sysResult2.Value;

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

    public static Result<ERBridge> CreateClusterBridge(ProgressionMap map, Sector sector, string starname1, string starname2)
    {
        Result guardResult = GuardClause.Null(map).Null(sector).NullOrWhiteSpace(starname1).NullOrWhiteSpace(starname2);
        if (!guardResult.Success) return guardResult;

        //sector.Map = map;

        Result<StarSystem> system1 = Result.Error("");
        Result<Cluster> cluster1 = Result.Error("");
        Result<StarSystem> system2 = Result.Error("");
        Result<Cluster> cluster2 = Result.Error("");

        foreach(var kvp in map.Clusters)
        {
            if (!system1.Success)
            {
                system1 = kvp.Value.GetStarSystem(starname1);
                if (system1.Success)
                    cluster1 = kvp.Value;
            }
            if (!system2.Success)
            {
                system2 = kvp.Value.GetStarSystem(starname2);
                if (system2.Success)
                    cluster2 = kvp.Value;
            }

            if (system1.Success && system2.Success)
                break;
        }


        if (!system1.Success || !system2.Success || !cluster1.Success || !cluster2.Success)
            return Result.Error("ERBridgeHelper:CreateClusterBridge Can not find system or cluser");

        if (cluster1.Value.Identifier == cluster2.Value.Identifier)
            return Result.Error("ERBridgeHelper:CreateClusterBridge Systems within same Cluster, use CreateStarSystemBridge");

        ERBridge bridge = new ERBridge(ProgressionConstants.BridgeTypes.Cluster, system1, system2);

        system1.Value.Add(bridge.Portals[0]);
        system2.Value.Add(bridge.Portals[1]);

        return bridge;
    }
}
