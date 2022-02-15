namespace StellarMap.Traveller;

[DataContract (Name = TravellerConstants.BodyType.Border)]
public class Border : IEqualityComparer<Border>
{
    #region Constructor        
    #endregion

    #region Properties
    [DataMember(Order = 11)]
    public IList<Hex> Positions {get; set;} = new List<Hex>();

    [DataMember(Order = 12)]
    public string Color {get; set;}
    #endregion

    #region IEquatable Interface
    public bool Equals(Border x, Border y) => BorderEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object obj) => BorderEqualityComparer.Comparer.Equals(this, obj as Border);

    public int GetHashCode(Border obj) => BorderEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => BorderEqualityComparer.Comparer.GetHashCode(this);
    #endregion   
}

public sealed class BorderEqualityComparer : IEqualityComparer<Border>
{
    #region IEqualityComparer
    public bool Equals (Border x, Border y)
    {
        bool retValue = true;
        if (x is null || y is null)
            retValue = false;
        else if (!ReferenceEquals(x, y))
        {
            if (x.Positions.Count != y.Positions.Count)
                retValue = false;
            else if (x.Color != y.Color)
                retValue = false;
            else
            {
                for (int i = 0; i < x.Positions.Count; i++)
                    if (x.Positions[i] != y.Positions[i])
                    {
                        retValue = false;
                        break;
                    }
            }
        }

        return retValue;
    }                   

    public int GetHashCode(Border obj)
    {
        int hash = 9860;
        foreach (Hex h in obj.Positions)
            hash ^= h.GetHashCode();
        hash ^= obj.Color.GetHashCode();

        return hash;
    } 
    #endregion

    public static IEqualityComparer<Border> Comparer { get; } = new BorderEqualityComparer();
}
