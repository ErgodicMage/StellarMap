﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;

namespace StellarMap.Progression
{
    [DataContract (Name = ProgressionConstants.BodyType.ERBridge)]
    public class ERBridge : StellarBody, IEquatable<ERBridge>
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

        #region IEquatable
        public bool Equals(ERBridge other) => 
            other!=null && base.Equals(other as StellarBody) && BridgeType.Equals(other.BridgeType) &&
            Portals != null && other.Portals != null && Portals[0].Equals(other.Portals[0]) && 
            Portals[1].Equals(other.Portals[1]);

        public override bool Equals(object obj) => Equals(obj as ERBridge);

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            if (!string.IsNullOrEmpty(BridgeType))
                hash = hash ^ BridgeType.GetHashCode();
            if (Portals != null)
            {
                foreach (Portal p in Portals)
                    hash = hash ^ p.GetHashCode();
            }

            return hash;
        }
        #endregion
    }
}
