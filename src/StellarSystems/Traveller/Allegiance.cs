namespace StellarMap.Traveller;

[DataContract (Name = TravellerConstants.BodyType.Allegiance)]
public class Allegiance : IEqualityComparer<Allegiance>
{
    #region Constructor
    #endregion
        
    #region Properties
    [DataMember(Order = 11)]
    public string Code {get; set;} = string.Empty;

    [DataMember(Order = 12)]
    public string Name {get; set;} = string.Empty;
    #endregion

    #region IEquatable Interface
    public bool Equals(Allegiance? x, Allegiance? y) =>AllegianceEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => AllegianceEqualityComparer.Comparer.Equals(this, obj as Allegiance);

    public int GetHashCode(Allegiance? obj) => AllegianceEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => AllegianceEqualityComparer.Comparer.GetHashCode(this);
    #endregion        
}

public sealed class AllegianceEqualityComparer : IEqualityComparer<Allegiance>
{
    #region IEqualityComparer
    public bool Equals (Allegiance? x, Allegiance? y) => 
        x is not null && y is not null &&
        !string.IsNullOrWhiteSpace(x.Code) && !string.IsNullOrWhiteSpace(x.Name) &&
        !string.IsNullOrWhiteSpace(y.Code) && !string.IsNullOrWhiteSpace(y.Name) &&
        x.Code == y.Code && x.Name == y.Name;

    public int GetHashCode(Allegiance? obj) => obj is null ? 0 : 4529 ^ obj.Code.GetHashCode() ^ obj.Name.GetHashCode();
    #endregion

    public static AllegianceEqualityComparer Comparer { get; } = new AllegianceEqualityComparer();
}