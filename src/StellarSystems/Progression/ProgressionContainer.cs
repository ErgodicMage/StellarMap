using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    public class ProgressionContainer : StellarParentBody
    {
        #region Constructors
        public ProgressionContainer()
        {            
        }
        
        public ProgressionContainer(string name, string bodytype) : base(name, bodytype)
        {
            ContainerGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-ProgressionContainer");
        }
        #endregion

        #region Properties
        [DataMember (Order = 11)]
        public string ContainerType { get; protected set; }

        [DataMember(Order = 12)]
        public GroupNamedIdentifiers ContainerGroupIdentifiers { get; set; }

        [IgnoreDataMember]
        public IDictionary<string, string> Bridges { get { return ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.ERBridges].Identifiers; } }
        #endregion

        #region Get Methods
        public virtual ERBridge GetBridge(string name) => Get<ERBridge>(name, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.ERBridges);

        public virtual IDictionary<string, ERBridge> GetBridges() => GetAll<ERBridge>(ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.ERBridges);
        #endregion

        #region Add Methods
        public void Add(ERBridge bridge) => Add<ERBridge>(bridge, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.ERBridges);
        #endregion

    }
}
