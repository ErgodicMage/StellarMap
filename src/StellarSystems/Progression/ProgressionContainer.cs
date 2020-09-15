using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Progression
{
    public class ProgressionContainer : StellarParentBody, IEqualityComparer<ProgressionContainer>
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
        public IDictionary<string, string> Bridges 
            { get => ContainerGroupIdentifiers.GroupIdentifiers.Get(ProgressionConstants.NamedIdentifiers.ERBridges);}
        #endregion

        #region Get Methods
        public virtual ERBridge GetBridge(string name) => 
            Get<ERBridge>(name, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.ERBridges);

        public virtual IDictionary<string, ERBridge> GetBridges() => 
            GetAll<ERBridge>(ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.ERBridges);
        #endregion

        #region Add Methods
        public void Add(ERBridge bridge) => 
            Add<ERBridge>(bridge, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.ERBridges);
        #endregion

        #region IEquatityComparer
        public bool Equals(ProgressionContainer x, ProgressionContainer y) =>
            x != null && y != null && x.ContainerType == y.ContainerType && base.Equals(x, y) &&
            x.ContainerGroupIdentifiers.Equals(y.ContainerGroupIdentifiers);

        public override bool Equals(object obj) => Equals(this, obj as ProgressionContainer);

        public int GetHashCode(ProgressionContainer obj)
        {
            int hash = base.GetHashCode(obj);
            if (!string.IsNullOrEmpty(obj.ContainerType))
                hash ^= obj.ContainerType.GetHashCode();
            if (obj.ContainerGroupIdentifiers != null)
                hash ^= obj.ContainerGroupIdentifiers.GetHashCode();

            return hash;
        }

        public override int GetHashCode() => GetHashCode(this);
        #endregion
    }
}
