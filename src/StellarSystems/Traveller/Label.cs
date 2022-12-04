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
    public string Text {get; set;} = string.Empty;
    #endregion

    #region IEquatable Interface
    public bool Equals(Label x, Label y) =>LabelEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object obj) => LabelEqualityComparer.Comparer.Equals(this, obj as Label);

    public int GetHashCode(Label obj) => LabelEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => LabelEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class LabelEqualityComparer : IEqualityComparer<Label>
{
    #region IEqualityComparer
    public bool Equals (Label x, Label y) => x != null && y != null && 
                    x.Position != null && y.Position != null && x.Position.IsValid() && y.Position.IsValid() && 
                    x.Text != null && y.Text != null && x.Text == y.Text;                 

    public int GetHashCode(Label obj) => 9871 ^ obj.Position.GetHashCode() ^ obj.Text.GetHashCode();

    #endregion

    public static IEqualityComparer<Label> Comparer { get; } = new LabelEqualityComparer();
}    
