using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    public class ProgressionContainer : StellarBodywithObjects
    {
        #region Progression Container Types
        public static class ContainerTypes
        {
            public static string StarSystem = "StarSystem";
            public static string Cluster = "Cluster";
            public static string Sector = "Sector";
            public static string Region = "Region";
            public static string District = "District";
            public static string Zone = "Zone";
            public static string Sphere = "Sphere";
            public static string Quadrant = "Quadrant";
            public static string Galaxy = "Galaxy";
        }
        #endregion

        #region Constructors
        public ProgressionContainer()
        {
        }

        public ProgressionContainer(string name) : base(name)
        {
        }
        #endregion

        #region Properties
        public string ContainerType { get; protected set; }

        [DataMember(Order = 11)]
        public GroupNamedIdentifiers ContainerGroupIdentifiers { get; set; }

        public IDictionary<string, string> Bridges { get { return ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.ERBridges].Identifiers; } }
        #endregion

        #region Get Methods
        public virtual ERBridge GetBridge(string name) => Get<ERBridge>(name);

        public virtual IDictionary<string, ERBridge> GetBridges() => GetAll<ERBridge>();
        #endregion

        #region Add Methods
        public void Add(ERBridge bridge) => Add<ERBridge>(bridge);
        #endregion

        #region Protected Functions
        protected override void Initialize()
        {
            base.Initialize();
            ContainerGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-ProgressionContainer");
        }

        protected override ObjectNamedIdentifiers GetObjectNamedIdentifiers(string name, bool create)
        {
            ObjectNamedIdentifiers identifiers = base.GetObjectNamedIdentifiers(name, create);

            if (identifiers == null && name == "ERBridge")
            {
                if (ContainerGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionGroupNamedIdentifiers.ERBridges))
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.ERBridges];
                else if (create)
                {
                    ContainerGroupIdentifiers.Add(ProgressionGroupNamedIdentifiers.ERBridges);
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.ERBridges];
                }
            }

            return identifiers;
        }
        #endregion
    }
}
