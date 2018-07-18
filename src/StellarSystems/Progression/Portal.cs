using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract (Name = "Portal")]
    public struct Portal
    {
        [DataMember (Order = 1)]
        public string StarIdentifier { get; set; }

        [DataMember (Order = 2)]
        public string ERBridgeIdentifier { get; set; }

        [DataMember (Order = 3)]
        public Point3d Position { get; set; }
    }
}
