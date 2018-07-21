using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract (Name = ProgressionConstants.BodyType.ERBridge)]
    public class ERBridge : StellarBody
    {
        #region Constructors
        public ERBridge()
        {

        }

        public ERBridge(string type, StarSystem system1, StarSystem system2) // : base(string.Empty, ProgressionConstants.BodyType.ERBridge)
        {
            BodyType = ProgressionConstants.BodyType.ERBridge;
            BridgeType = type;
            Map = system1.Map;
            (Map as ProgressionMap).Add(this);
            Initialize();
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
        protected override void Initialize()
        {
            base.Initialize();
            Portals = new Portal[2];
        }

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
    }
}
