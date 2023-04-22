using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StellarMap.Progression
{
    public class Navigator
    {
        #region Constructors
        public Navigator()
        { }

        public Navigator(ProgressionMap map) => Map = map;
        #endregion

        #region Public Properties
        public ProgressionMap Map { get; set; }
        #endregion

        public Result<IList<ERBridgeRoute>> GetPaths(string systemname1, string systemname2)
        {
            Result guardResult = GuardClause.NullOrWhiteSpace(systemname1).NullOrWhiteSpace(systemname2);
            if (!guardResult.Success) return guardResult;

            var system1 = GetStarSystemIdentifier(systemname1);
            if (!system1.Success) return Result.Error(system1.ErrorMessage, system1.Exception);

            var system2 = GetStarSystemIdentifier(systemname2);
            if (!system2.Success) return Result.Error(system2.ErrorMessage, system2.Exception);


            return GetPathsFromIdentifiers(system1, system2); ;
        }

        public Result<IList<ERBridgeRoute>> GetPathsFromIdentifiers(string systemid1, string systemid2)
        {
            Result guardResult = GuardClause.NullOrWhiteSpace(systemid1).NullOrWhiteSpace(systemid2);
            if (!guardResult.Success) return guardResult;

            var system1 = Map.Get<StarSystem>(systemid1);
            if (!system1.Success) Result.Error(system1.ErrorMessage, system1.Exception);

            var system2 = Map.Get<StarSystem>(systemid2);
            if (!system2.Success) Result.Error(system2.ErrorMessage, system2.Exception);

            if (system1.Value.ParentIdentifier == system2.Value.ParentIdentifier)
                return GetPathsWithinCluster(system1.Value.ParentIdentifier, systemid1, systemid2);

            IList<ERBridgeRoute> paths = new List<ERBridgeRoute>();

            return Result<IList<ERBridgeRoute>>.Ok(paths);
        }

        public Result<string> GetStarSystemIdentifier(string starname)
        {
            Result guardResult = GuardClause.NullOrWhiteSpace(starname);
            if (!guardResult.Success) return guardResult;

            if (Map.StarSystems is null)
                return Result.Error("Navigator:GetStarSystemIdentifier map does not have any StarSystems.");

            var system = Map.StarSystems.Where(kvp => kvp.Value.Name == starname).FirstOrDefault();
            if (string.IsNullOrEmpty(system.Key) || system.Value is null)
                return Result.Error($"Navigator:GetStarSystemIdentifier can not StarSystem {starname} in map.");

            return system.Value.Identifier!;
        }

        public Result<IList<ERBridgeRoute>> GetPathsWithinCluster(string clusterid, string systemid1, string systemid2)
        {
            Result guardResult = GuardClause.NullOrWhiteSpace(clusterid).NullOrWhiteSpace(systemid1).NullOrWhiteSpace(systemid2);
            if (!guardResult.Success) return guardResult;

            // GetCluster
            var clusterResult = Map.Get<Cluster>(clusterid);
            if (!clusterResult.Success) return Result.Error($"Navigator:vGetPathsWithinCluster can not get Cluster for id {clusterid}");

            Cluster cluster = clusterResult;
            string directbridgeid = string.Empty;
            IList<ERBridgeRoute> bridges = new List<ERBridgeRoute>();

            // first check for directly connected
            foreach (var id in cluster.Bridges?.Values)
            {
                var bridge = Map.Get<ERBridge>(id);
                if (bridge.Success && CheckBridgeHasSystems(bridge, systemid1, systemid2))
                {
                    ERBridgeRoute path = new ERBridgeRoute();
                    path.Bridges.Add(bridge);
                    bridges.Add(path);
                    directbridgeid = bridge.Value.Identifier!;
                    break;
                }
            }

            // now drill down each portal starting at system1
            StarSystem system1 = Map.Get<StarSystem>(systemid1);
            foreach(Portal p in system1.Portals)
            {
                ERBridge bridge = Map.Get<ERBridge>(p.ERBridgeIdentifier);
                if (bridge.Identifier == directbridgeid) // we've already found this one
                    continue;


            }

            return Result<IList<ERBridgeRoute>>.Ok(bridges);
        }

        protected bool CheckBridgeHasSystems(ERBridge? bridge, string system1, string system2) =>
            (bridge?.Portals[0].StarSystemIdentifier == system1 || bridge?.Portals[0].StarSystemIdentifier == system2) &&
            (bridge?.Portals[1].StarSystemIdentifier == system1 || bridge?.Portals[1].StarSystemIdentifier == system2);
    }
}
