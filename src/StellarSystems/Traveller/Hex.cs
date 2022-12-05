namespace StellarMap.Traveller;

[DataContract(Name = "Hex")]
public sealed class Hex : IEqualityComparer<Hex>
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

    #region Properties
    [DataMember(Order = 1)]
    public byte X { get; private set; }

    [DataMember(Order = 2)]
    public byte Y { get; private set; }
    #endregion

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
    public override string ToString()
    {
        if (!IsValid())
            return "0000";
        return (X*100+Y).ToString("0000");
    }
    #endregion

    #region IEquatable Interface
    public bool Equals(Hex? x, Hex? y) =>HexEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => HexEqualityComparer.Comparer.Equals(this, obj as Hex);

    public int GetHashCode(Hex? obj) => HexEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => HexEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class HexEqualityComparer : IEqualityComparer<Hex>
{
    #region IEqualityComparer
    public bool Equals (Hex? x, Hex? y) => x is not null && y is not null && x.IsValid() && y.IsValid() && 
                                            x.X == y.X && x.Y == y.Y;                    

    public int GetHashCode(Hex? obj) => obj is null ? 0 : 1565 ^ obj.X.GetHashCode() ^ obj.Y.GetHashCode();
    #endregion

    public static HexEqualityComparer Comparer { get; } = new HexEqualityComparer();
}
