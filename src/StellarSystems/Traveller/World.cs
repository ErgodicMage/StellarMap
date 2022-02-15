namespace StellarMap.Traveller;

[DataContract (Name = TravellerConstants.BodyType.World)]
public class World : StellarParentBody, IEqualityComparer<World>
{
    #region Constructor
    public World()
    {
    }

    public World(string name, Hex hex, string uwp = null) : base(name, TravellerConstants.BodyType.World)
    {
        HexNumber = hex;

        WorldGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-World");
        SetWorldProperty(TravellerConstants.PropertyNames.UWP, uwp);
    }
    #endregion

    #region Public Properties
    [DataMember(Order = 31)]
    public Hex HexNumber { get; set; }

    [IgnoreDataMember]
    public string UWP 
        { 
        get => GetWorldValue(TravellerConstants.PropertyNames.UWP); 
        set => SetWorldProperty(TravellerConstants.PropertyNames.UWP, value); 
    }

    [IgnoreDataMember]
    public string Base
    { 
        get => GetWorldValue(TravellerConstants.PropertyNames.Base); 
        set => SetWorldProperty(TravellerConstants.PropertyNames.Base, value); 
    }

    [IgnoreDataMember]
    public string Codes
    { 
        get => GetWorldValue(TravellerConstants.PropertyNames.Codes); 
        set => SetWorldProperty(TravellerConstants.PropertyNames.Codes, value); 
    }

    [IgnoreDataMember]
    public string Zone
    { 
        get => GetWorldValue(TravellerConstants.PropertyNames.Zone); 
        set => SetWorldProperty(TravellerConstants.PropertyNames.Zone, value); 
    }

    [IgnoreDataMember]
    public string PBG
    { 
        get => GetWorldValue(TravellerConstants.PropertyNames.PBG); 
        set => SetWorldProperty(TravellerConstants.PropertyNames.PBG, value); 
    }

    [IgnoreDataMember]
    public string Allegiance
    { 
        get => GetWorldValue(TravellerConstants.PropertyNames.Allegiance); 
        set => SetWorldProperty(TravellerConstants.PropertyNames.Allegiance, value); 
    }

    [IgnoreDataMember]
    public string StellarData
    {
        get => GetWorldValue(TravellerConstants.PropertyNames.StellarData);
        set => SetWorldProperty(TravellerConstants.PropertyNames.StellarData, value);
    }

    [DataMember(Order = 32)]
    public GroupNamedIdentifiers WorldGroupIdentifiers { get; set; }
    #endregion

    #region Set/Get World Properties
    public void SetWorldProperty(string propertyName, string propertyValue)
    {
        if (BasicProperties.ContainsKey(propertyName))
            BasicProperties[propertyName] = propertyValue;
        else
            BasicProperties.Add(propertyName, propertyValue);
    }

    public string GetWorldValue(string propertyName)
    {
        if (!BasicProperties.ContainsKey(propertyName))
            return string.Empty;
        return BasicProperties[propertyName];
    }
    #endregion

    #region IEqualityComparer
    public bool Equals(World x, World y) => WorldEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object obj) => WorldEqualityComparer.Comparer.Equals(this, obj as World);

    public int GetHashCode(World obj) => WorldEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => WorldEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class WorldEqualityComparer : IEqualityComparer<World>
{
    #region IEqualityComparer
    public bool Equals(World x, World y) =>
                StellarBodyEqualityComparer.Comparer.Equals(x, y) && x.HexNumber == y.HexNumber &&
                x.WorldGroupIdentifiers.Equals(y.WorldGroupIdentifiers);

    public int GetHashCode(World obj)
    {
        int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
        hash ^= obj.HexNumber.GetHashCode();
        if (obj.WorldGroupIdentifiers != null)
            hash ^= obj.WorldGroupIdentifiers.GetHashCode();

        return hash;
    }
    #endregion

    public static IEqualityComparer<World> Comparer { get; } = new WorldEqualityComparer();
}
