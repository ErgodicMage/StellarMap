using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;

namespace StellarMap.Progression
{
    [DataContract (Name = ProgressionConstants.BodyType.ERBridge)]
    public class ERBridge : StellarBody, IEqualityComparer<ERBridge>
    {
        #region Constructors
        public ERBridge()
        {            
        }
        
        public ERBridge(string type, StarSystem system1, StarSystem system2) : 
            base(string.Empty, ProgressionConstants.BodyType.ERBridge)
        {
            BodyType = ProgressionConstants.BodyType.ERBridge;
            BridgeType = type;
            Map = system1.Map;
            (Map as ProgressionMap).Add(this);

            Portals = new Portal[2];
            Portals[0].StarIdentifier = system1.Identifier;
            Portals[0].ERBridgeIdentifier = this.Identifier;
            Portals[1].StarIdentifier = system2.Identifier;
            Portals[1].ERBridgeIdentifier = this.Identifier;
            SetName();
        }
        #endregion

        #region Properties

        [DataMember (Order = 11)]
        public string BridgeType { get; set; }

        [DataMember (Order = 12)]
        public Portal[] Portals { get; set; }

        public StarSystem GetStarSystem(int end)
        {
            string identifier = Portals[end].StarIdentifier;
            StarSystem system = (Map as ProgressionMap).Get<StarSystem>(identifier);
            return system;
        }
        #endregion

        #region Get Methods
        public StarSystem GetStarSystem(string name)
        {
            StarSystem system = null;

            foreach (Portal p in Portals)
            {
                system = (Map as ProgressionMap).Get<StarSystem>(p.StarIdentifier);
                if (system != null && system.Name == name)
                    break;
            }

            return system;
        }

        public IDictionary<string, StarSystem> GetStarSystem()
        {
            IDictionary<string, StarSystem> systems = new Dictionary<string, StarSystem>();

            foreach (Portal p in Portals)
            {
                StarSystem s = (Map as ProgressionMap).Get<StarSystem>(p.StarIdentifier);
                systems.Add(s.Name, s);
            }

            return systems;
        }
        #endregion

        #region Protected Methods
        protected void SetName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Bridge: ");
            bool bFirst = true;
            foreach (Portal p in Portals)
            {
                StarSystem system = (Map as ProgressionMap).Get<StarSystem>(p.StarIdentifier);
                if (system != null)
                {
                    if (!bFirst)
                        sb.Append("-");
                    sb.Append(system.Name);
                    bFirst = false;
                }
            }
            Name = sb.ToString();
        }
        #endregion

        #region IEqualityComparer
        public bool Equals(ERBridge x, ERBridge y) => ERBridgeEqualityComparer.Comparer.Equals(x, y);

        public override bool Equals(object obj) => ERBridgeEqualityComparer.Comparer.Equals(this, obj as ERBridge);

        public int GetHashCode(ERBridge obj) => ERBridgeEqualityComparer.Comparer.GetHashCode(obj);

        public override int GetHashCode() => ERBridgeEqualityComparer.Comparer.GetHashCode(this);
        #endregion
    }

    public sealed class ERBridgeEqualityComparer : IEqualityComparer<ERBridge>
    {
        public bool Equals(ERBridge x, ERBridge y)
        {
            bool bRet = true;

            if (x is null || y is null)
                bRet = false;
            else if (!ReferenceEquals(x, y))
            {
                bRet = StellarBodyEqualityComparer.Comparer.Equals(x, y) && 
                       x.BridgeType == y.BridgeType && x.Portals != null && y.Portals != null &&
                       x.Portals[0].Equals(y.Portals[0]) && x.Portals[1].Equals(y.Portals[1]);
            }

            return bRet;
        }

        public int GetHashCode(ERBridge obj)
        {
            int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
            if (!string.IsNullOrEmpty(obj.BridgeType))
                hash ^= obj.BridgeType.GetHashCode();
            if (obj.Portals != null)
            {
                foreach (Portal p in obj.Portals)
                    hash ^= p.GetHashCode();
            }

            return hash;
        }

        public static IEqualityComparer<ERBridge> Comparer { get; } = new ERBridgeEqualityComparer();
    }
}
