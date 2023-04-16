namespace StellarMap.Core.Bodies;

[DataContract (Name = Constants.BodyTypes.DwarfPlanet)]
public class DwarfPlanet : StellarParentBody, IEqualityComparer<DwarfPlanet>
{
    #region Constructors
    public DwarfPlanet()
    {
    }

    public DwarfPlanet(string name) : base(name, Constants.BodyTypes.DwarfPlanet)
    {            
        DwarfPlanetGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-DwarfPlanet");
    }
    #endregion

    #region Public Properties
    [DataMember(Order = 11)]
    public GroupNamedIdentifiers DwarfPlanetGroupIdentifiers { get; set; }

    [IgnoreDataMember]
    public IDictionary<string, string>? Satellites 
        { get => DwarfPlanetGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Satellites).Value; }
    #endregion

    #region Get Functions
    public virtual Result<Satellite> GetSatellite(string name) => 
        Get<Satellite>(name, DwarfPlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);

    public virtual Result<IDictionary<string, Satellite>> GetSatellites() => 
        GetAll<Satellite>(DwarfPlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
    #endregion

    #region Public Add Functions
    public Result Add(Satellite satellite) => 
        Add<Satellite>(satellite, DwarfPlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
    #endregion

    #region IEqualityComparer
    public bool Equals(DwarfPlanet? x, DwarfPlanet? y) => DwarfPlanetEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => DwarfPlanetEqualityComparer.Comparer.Equals(this, obj as DwarfPlanet);

    public int GetHashCode(DwarfPlanet? obj) => DwarfPlanetEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => DwarfPlanetEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class DwarfPlanetEqualityComparer : IEqualityComparer<DwarfPlanet>
{
    #region IEqualityComparer
    public bool Equals(DwarfPlanet? x, DwarfPlanet? y) => 
                StellarBodyEqualityComparer.Comparer.Equals(x, y) && 
                x!.DwarfPlanetGroupIdentifiers.Equals(y!.DwarfPlanetGroupIdentifiers);


    public int GetHashCode(DwarfPlanet? obj)
    {
        if (obj is null) return 0;

        int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
        if (obj.DwarfPlanetGroupIdentifiers != null)
            hash ^= obj.DwarfPlanetGroupIdentifiers.GetHashCode();

        return hash;
    }
    #endregion

    public static DwarfPlanetEqualityComparer Comparer { get; } = new DwarfPlanetEqualityComparer();
}
