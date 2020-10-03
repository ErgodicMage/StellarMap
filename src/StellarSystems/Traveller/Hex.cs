using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

namespace StellarMap.Traveller
{
    [DataContract(Name = "Hex")]
    public sealed class Hex : IEquatable<Hex>
    {
        #region Constructors
        public Hex()
        {

        }

        public Hex(byte x, byte y)
        {
            X = 0;
            Y = 0;
            if (Hex.IsValid(x, y))
            {
                X = x;
                Y = y;
            }
        }

        public Hex(Hex other)
        {
            X = 0;
            Y = 0;
            if (other.IsValid())
            {
                X = other.X;
                Y = other.Y;
            }
        }
        public Hex(string h)
        {
            X = 0;
            Y = 0;
            SetHexValue(h);
        }
        #endregion

        [DataMember(Order = 1)]
        public byte X { get; private set; }

        [DataMember(Order = 2)]
        public byte Y { get; private set; }

        #region Public Functions
        public bool IsValid() => X >= 1 && X <= TravellerConstants.Sector.Width && Y >= 1 && Y <= TravellerConstants.Sector.Height;
        public static bool IsValid(byte x, byte y) => x >= 1 && x <= TravellerConstants.Sector.Width && y >= 1 && y <= TravellerConstants.Sector.Height;
        #endregion

        #region Set Function
        private void SetHexValue(string h)
        {
            if (!string.IsNullOrEmpty(h) && h.Length == 4)
            {
                string xportion = h.Substring(0, 2);
                string yportion = h.Substring(2, 2);

                if (byte.TryParse(xportion, out byte x))
                    X = x;
                if (byte.TryParse(yportion, out byte y))
                    Y = y;
            }
        }
        #endregion

        #region String Functions
        #endregion
        public override string ToString()
        {
            if (!IsValid())
                return "0000";
            return (X*100+Y).ToString("0000");
        }
        #region IEquatable Interface
        [Pure]
        public bool Equals(Hex other) => this.IsValid() && other.IsValid() && this.X == other.X && this.Y == other.Y;

        [Pure]
        public override bool Equals(object obj) => obj is Hex h && this.Equals(h);

        [Pure]
        public static bool operator == (Hex a, Hex b) => a.Equals(b);
        public static bool operator != (Hex a, Hex b) => !a.Equals(b);
        #endregion

        #region HashCode
        [Pure]
        public override int GetHashCode() => 1565 ^ X.GetHashCode() ^ Y.GetHashCode();
        #endregion
    }
}
