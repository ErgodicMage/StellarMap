namespace StellarMap.Core.Bodies;

[DataContract(Name = Constants.BodyTypes.Planet)]
public class Planet : StellarParentBody, IEqualityComparer<Planet>
{
    #region Cosntructors
    public Planet()
    {
    }

    public Planet(string name) : base(name, Constants.BodyTypes.Planet)
    {
        PlanetGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-Planet");
    }
    #endregion

    #region Public Properties
    [DataMember(Order = 11)]
    public GroupNamedIdentifiers PlanetGroupIdentifiers { get; set; }

    [IgnoreDataMember]
    public IDictionary<string, string>? Satellites 
        { get => PlanetGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Satellites); }
    #endregion

    #region Get Functions
    public virtual Satellite GetSatellite(string name) => 
        Get<Satellite>(name, PlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);

    public virtual IDictionary<string, Satellite> GetSatellites() => 
        GetAll<Satellite>(PlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
    #endregion

    #region Public Add Functions
    public void Add(Satellite satellite) => 
        Add<Satellite>(satellite, PlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
    #endregion

    #region IEqualityComparer
    public bool Equals(Planet x, Planet y) => PlanetEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object obj) => PlanetEqualityComparer.Comparer.Equals(this, obj as Planet);

    public int GetHashCode(Planet obj) => PlanetEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => PlanetEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class PlanetEqualityComparer : IEqualityComparer<Planet>
{
    #region IEqualityComparer
    public bool Equals (Planet x, Planet y) =>
                StellarBodyEqualityComparer.Comparer.Equals(x, y) &&
                x.PlanetGroupIdentifiers.Equals(y.PlanetGroupIdentifiers);

    public int GetHashCode(Planet obj)
    {
        int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
        if (obj.PlanetGroupIdentifiers != null)
            hash ^= obj.PlanetGroupIdentifiers.GetHashCode();

        return hash;
    }
    #endregion

    public static IEqualityComparer<Planet> Comparer { get; } = new PlanetEqualityComparer();
}
