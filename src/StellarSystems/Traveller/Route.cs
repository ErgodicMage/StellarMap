namespace StellarMap.Traveller;

[DataContract (Name = TravellerConstants.BodyType.Route)]
public class Route : IEqualityComparer<Route>
{
    #region Constructor
    #endregion

    #region Properties
    [DataMember(Order = 11)]
    public Hex? Start {get; set;}

    [DataMember(Order = 12)]
    public Hex? End {get; set;}

    [DataMember(Order = 13)]
    public short StartOffsetX {get; set;}

    [DataMember(Order = 14)]
    public short StartOffsetY {get; set;}

    [DataMember(Order = 15)]
    public short EndOffsetX {get; set;}

    [DataMember(Order = 16)]
    public short EndOffsetY {get; set;}

    [DataMember(Order = 17)]
    public string? Color {get; set;}

    #endregion

    #region IEquatable Interface
    public bool Equals(Route? x, Route? y) =>RouteEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => RouteEqualityComparer.Comparer.Equals(this, obj as Route);

    public int GetHashCode(Route? obj) => RouteEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => RouteEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

    public sealed class RouteEqualityComparer : IEqualityComparer<Route>
{
    #region IEqualityComparer
    public bool Equals (Route? x, Route? y) => x is not null && y is not null &&
        x.Start is not null && y.Start is not null && x.End is not null && y.End is not null &&
        HexEqualityComparer.Comparer.Equals(x.Start, y.Start) && HexEqualityComparer.Comparer.Equals(x.End, y.End) &&
        x.StartOffsetX == y.StartOffsetX && x.StartOffsetY == y.StartOffsetY &&
        x.EndOffsetX == y.EndOffsetX && x.EndOffsetY == y.EndOffsetY && 
        x.Color is not null && y.Color is not null && x.Color == y.Color;

    public int GetHashCode(Route? obj)
    {
        if (obj is null) return 0;
        int hash = 1676;
        if (obj.Start != null)
            hash ^= obj.Start.GetHashCode();
        if (obj.End != null)
            hash ^= obj.End.GetHashCode();
        hash ^= obj.StartOffsetX.GetHashCode() ^ obj.StartOffsetY.GetHashCode();
        hash ^= obj.EndOffsetX.GetHashCode() ^ obj.EndOffsetY.GetHashCode();
        if (!string.IsNullOrEmpty(obj.Color))
            hash ^= obj.Color.GetHashCode();
        return hash;
    }
    #endregion

    public static RouteEqualityComparer Comparer { get; } = new RouteEqualityComparer();
}
