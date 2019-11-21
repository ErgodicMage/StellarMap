using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract (Name = "Portal")]
    public struct Portal : IEquatable<Portal>
    {
        [DataMember (Order = 1)]
        public string StarIdentifier { get; set; }

        [DataMember (Order = 2)]
        public string ERBridgeIdentifier { get; set; }

        [DataMember (Order = 3)]
        public Point3d Position { get; set; }

        #region IEquatable
        public bool Equals(Portal other) => StarIdentifier.Equals(other.StarIdentifier) && ERBridgeIdentifier.Equals(other.ERBridgeIdentifier) && Position.Equals(other.Position);

        public override bool Equals(object o) => o != null && o is Portal p && Equals(p);

        public override int GetHashCode() => base.GetHashCode();
        #endregion
    }
}
