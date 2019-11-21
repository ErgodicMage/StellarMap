using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    public class ProgressionContainer : StellarParentBody, IEquatable<ProgressionContainer>
    {
        #region Constructors
        public ProgressionContainer()
        {
            ContainerGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-ProgressionContainer");
        }
        
        public ProgressionContainer(string name, string bodytype) : base(name, bodytype)
        {
            ContainerGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-ProgressionContainer");
        }
        #endregion

        #region Properties
        [DataMember (Order = 11)]
        public string ContainerType { get; set; }

        [DataMember(Order = 12)]
        public GroupNamedIdentifiers ContainerGroupIdentifiers { get; set; }

        [IgnoreDataMember]
        public IDictionary<string, string> Bridges { get { return ContainerGroupIdentifiers.GroupIdentifiers.Get(ProgressionConstants.NamedIdentifiers.ERBridges); } }
        #endregion

        #region Get Methods
        public virtual ERBridge GetBridge(string name) => Get<ERBridge>(name, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.ERBridges);

        public virtual IDictionary<string, ERBridge> GetBridges() => GetAll<ERBridge>(ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.ERBridges);
        #endregion

        #region Add Methods
        public void Add(ERBridge bridge) => Add<ERBridge>(bridge, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.ERBridges);
        #endregion

        #region IEquatable
        public bool Equals(ProgressionContainer other) => other!=null && ContainerType.Equals(other.ContainerType) && 
                                                            base.Equals(other as StellarParentBody) && 
                                                            ContainerGroupIdentifiers.Equals(other.ContainerGroupIdentifiers);

        public override bool Equals(object o) => Equals(o as ProgressionContainer);

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            if (!string.IsNullOrEmpty(ContainerType))
                hash = hash ^ ContainerType.GetHashCode();
            if (ContainerGroupIdentifiers != null)
                hash = hash ^ ContainerGroupIdentifiers.GetHashCode();

            return hash;
        }
        #endregion
    }
}
