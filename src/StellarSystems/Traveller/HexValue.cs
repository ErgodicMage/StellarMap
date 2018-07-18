using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Text;

namespace StellarMap.Traveller
{
    [DataContract(Name = "HexValue")]
    public struct HexValue : IEquatable<HexValue>
    {
        #region Constructors
        public HexValue(string h)
        {
            Hex = string.Empty;
            SetHexValue(h);
        }
        #endregion

        [DataMember(Order = 1)]
        public string Hex { get; private set; }

        public string X { get { return Hex.Substring(0, 2); } }
        public string Y { get { return Hex.Substring(2, 2); } }

        #region Public Functions
        public void SetHexValue(string h)
        {
            // check range of 0101 - 3240
        }
        #endregion

        #region IEquatable Interface
        [Pure]
        public bool Equals(HexValue other) => this.Hex.Equals(other.Hex);

        [Pure]
        public override bool Equals(object obj) => obj is HexValue h && this.Equals(h);
        #endregion

        #region HashCode
        [Pure]
        public override int GetHashCode() => Hex.GetHashCode();
        #endregion
    }
}
