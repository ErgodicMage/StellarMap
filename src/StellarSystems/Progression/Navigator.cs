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

        public IList<ERBridgeRoute>? GetPaths(string systemname1, string systemname2)
        {
            string system1 = GetStarSystemIdentifier(systemname1);
            string system2 = GetStarSystemIdentifier(systemname2);
            if (string.IsNullOrEmpty(system1) || string.IsNullOrEmpty(system2))
                return default;

            return GetPathsFromIdentifiers(system1, system2); ;
        }

        public IList<ERBridgeRoute>? GetPathsFromIdentifiers(string systemid1, string systemid2)
        {
            StarSystem? system1 = Map.Get<StarSystem>(systemid1);
            StarSystem? system2 = Map.Get<StarSystem>(systemid2);

            if (system1 is null || system2 is null)
                return default;

            IList<ERBridgeRoute> paths = new List<ERBridgeRoute>();

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

        public IList<ERBridgeRoute> GetPathsWithinCluster(string clusterid, string systemid1, string systemid2)
        {
            IList<ERBridgeRoute> bridges = new List<ERBridgeRoute>();

            // GetCluster
            Cluster? cluster = Map.Get<Cluster>(clusterid);
            string directbridgeid = string.Empty;
            // first check for directly connected
            foreach (var id in cluster?.Bridges?.Values)
            {
                ERBridge? bridge = Map.Get<ERBridge>(id);
                if (CheckBridgeHasSystems(bridge, systemid1, systemid2))
                {
                    ERBridgeRoute path = new ERBridgeRoute();
                    path.Bridges.Add(bridge);
                    bridges.Add(path);
                    directbridgeid = bridge.Identifier;
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

            return bridges;
        }

        protected bool CheckBridgeHasSystems(ERBridge? bridge, string system1, string system2) =>
            (bridge?.Portals[0].StarSystemIdentifier == system1 || bridge?.Portals[0].StarSystemIdentifier == system2) &&
            (bridge?.Portals[1].StarSystemIdentifier == system1 || bridge?.Portals[1].StarSystemIdentifier == system2);
    }
}
