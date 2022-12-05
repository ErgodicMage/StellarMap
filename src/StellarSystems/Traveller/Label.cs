namespace StellarMap.Traveller;

[DataContract (Name = TravellerConstants.BodyType.Label)]
public class Label : IEqualityComparer<Label>
{
    #region Constructor
    #endregion

    #region Properties
    [DataMember(Order = 11)]
    public Hex? Position {get; set;}

    [DataMember(Order = 12)]
    public string? Text {get; set;}
    #endregion

    #region IEquatable Interface
    public bool Equals(Label? x, Label? y) =>LabelEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => LabelEqualityComparer.Comparer.Equals(this, obj as Label);

    public int GetHashCode(Label? obj) => LabelEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => LabelEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class LabelEqualityComparer : IEqualityComparer<Label>
{
    #region IEqualityComparer
    public bool Equals (Label? x, Label? y) => 
        x is not null && y is not null && 
        x.Position is not null && y.Position is not null && 
        !string.IsNullOrEmpty(x.Text) && !string.IsNullOrEmpty(y.Text) &&
        x.Position.IsValid() && y.Position.IsValid() && 
        x.Text == y.Text;

    public int GetHashCode(Label? obj)
    {
        if (obj is null) return 0;
        int hash = 9871;
        if (obj.Position is not null)
            hash ^= obj.Position.GetHashCode();
        if (!string.IsNullOrWhiteSpace(obj.Text))
            hash ^= obj.Text.GetHashCode();
        return hash;
    }

    #endregion

    public static LabelEqualityComparer Comparer { get; } = new LabelEqualityComparer();
}    
