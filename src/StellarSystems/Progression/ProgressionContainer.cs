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

        public IDictionary<string, string> Bridges { get { return ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.ERBridges].Identifiers; } }
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

            if (identifiers == null && name == ProgressionConstants.BodyType.ERBridge)
            {
                if (ContainerGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionConstants.NamedIdentifiers.ERBridges))
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.ERBridges];
                else if (create)
                {
                    ContainerGroupIdentifiers.Add(ProgressionConstants.NamedIdentifiers.ERBridges);
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.ERBridges];
                }
            }

            return identifiers;
        }
        #endregion
    }
}
