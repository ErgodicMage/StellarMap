using System;
using System.Collections.Generic;
using System.Linq;
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

        public IList<ERBridgesPath> GetPaths(string systemname1, string systemname2)
        {
            string system1 = GetStarSystemIdentifier(systemname1);
            string system2 = GetStarSystemIdentifier(systemname2);
            if (string.IsNullOrEmpty(system1) || string.IsNullOrEmpty(system2))
                return null;

            return GetPathsFromIdentifiers(system1, system2); ;
        }

        public IList<ERBridgesPath> GetPathsFromIdentifiers(string systemid1, string systemid2)
        {
            StarSystem system1 = Map.Get<StarSystem>(systemid1);
            StarSystem system2 = Map.Get<StarSystem>(systemid2);

            if (system1 == null || system2 == null)
                return null;

            IList<ERBridgesPath> paths = new List<ERBridgesPath>();

            if (system1.ParentIdentifier == system2.ParentIdentifier)
                return GetPathsWithinCluster(system1.ParentIdentifier, systemid1, systemid2);

            return paths;
        }

        public string GetStarSystemIdentifier(string starname)
        {
            var system = Map.StarSystems.Where(kvp => kvp.Value.Name == starname).FirstOrDefault();
            if (!string.IsNullOrEmpty(system.Key))
                return system.Value?.Identifier;
            return string.Empty;
        }

        public IList<ERBridgesPath> GetPathsWithinCluster(string clusterid, string system1, string system2)
        {
            IList<ERBridgesPath> bridges = new List<ERBridgesPath>();

            // GetCluster
            Cluster cluster = Map.Get<Cluster>(clusterid);
            
            // first check for directly connected
            foreach (var id in cluster.Bridges.Values)
            {
                ERBridge bridge = Map.Get<ERBridge>(id);
                if (CheckBridgeHasSystems(bridge, system1, system2))
                {
                    ERBridgesPath path = new ERBridgesPath();
                    path.Bridges.Add(bridge);
                    bridges.Add(path);
                    break;
                }
            }

            return bridges;
        }

        protected bool CheckBridgeHasSystems(ERBridge bridge, string system1, string system2) =>
            (bridge.Portals[0].StarIdentifier == system1 || bridge.Portals[0].StarIdentifier == system2) &&
            (bridge.Portals[1].StarIdentifier == system1 || bridge.Portals[1].StarIdentifier == system2);
    }
}
