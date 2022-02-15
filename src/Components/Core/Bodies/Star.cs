namespace StellarMap.Core.Bodies;

[DataContract (Name = Constants.BodyTypes.Star)]
public class Star : StellarParentBody,  IEqualityComparer<Star>
{
    #region Constructors
    public Star()
    {            
    }
        
    public Star(string name) : base(name, Constants.BodyTypes.Star)
    {
        StarGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-Star");
    }
    #endregion

    #region Public Properties
    [DataMember (Order = 11)]
    public GroupNamedIdentifiers StarGroupIdentifiers { get; set; }

    [IgnoreDataMember]
    public IDictionary<string, string> Planets 
        { get => StarGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Planets); }

    [IgnoreDataMember]
    public IDictionary<string, string> DwarfPlanets 
        { get => StarGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.DwarfPlanets); }

    [IgnoreDataMember]
    public IDictionary<string, string> Asteroids 
        { get => StarGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Asteroids); }

    [IgnoreDataMember]
    public IDictionary<string, string> Comets 
        { get => StarGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Comets); }
    #endregion

    #region Get Functions
    public virtual Planet GetPlanet(string name) => 
        Get<Planet>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.Planets);

    public virtual IDictionary<string, Planet> GetPlanets() => 
        GetAll<Planet>(StarGroupIdentifiers, Constants.NamedIdentifiers.Planets);

    public virtual DwarfPlanet GetDwarfPlanet(string name) => 
        Get<DwarfPlanet>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.DwarfPlanets);

    public virtual IDictionary<string, DwarfPlanet> GetDwarfPlanets() => 
        GetAll<DwarfPlanet>(StarGroupIdentifiers, Constants.NamedIdentifiers.DwarfPlanets);

    public virtual Asteroid GetAsteroid(string name) => 
        Get<Asteroid>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.Asteroids);

    public virtual IDictionary<string, Asteroid> GetAsteroids() => 
        GetAll<Asteroid>(StarGroupIdentifiers, Constants.NamedIdentifiers.Asteroids);

    public virtual Comet GetComet(string name) => 
        Get<Comet>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.Comets);

    public virtual IDictionary<string, Comet> GetComets() => 
        GetAll<Comet>(StarGroupIdentifiers, Constants.NamedIdentifiers.Comets);
    #endregion

    #region Public Add Functions
    public void Add(Planet planet) => 
        Add<Planet>(planet, StarGroupIdentifiers, Constants.NamedIdentifiers.Planets);

    public void Add(DwarfPlanet dwarf) => 
        Add<DwarfPlanet>(dwarf, StarGroupIdentifiers, Constants.NamedIdentifiers.DwarfPlanets);            

    public void Add(Asteroid asteroid) => 
        Add<Asteroid>(asteroid, StarGroupIdentifiers, Constants.NamedIdentifiers.Asteroids);

    public void Add(Comet comet) => 
        Add<Comet>(comet, StarGroupIdentifiers, Constants.NamedIdentifiers.Comets);
    #endregion

    #region IEqualityComparer
    public bool Equals(Star x, Star y) => StarEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object obj) => StarEqualityComparer.Comparer.Equals(this, obj as Star);

    public int GetHashCode(Star obj) => StarEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => StarEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class StarEqualityComparer : IEqualityComparer<Star>
{
    #region IEqualityComparer
    public bool Equals(Star x, Star y) =>
                StellarBodyEqualityComparer.Comparer.Equals(x, y) &&
                x.StarGroupIdentifiers.Equals(y.StarGroupIdentifiers);

    public int GetHashCode(Star obj)
    {
        int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
        if (obj.StarGroupIdentifiers != null)
            hash ^= obj.StarGroupIdentifiers.GetHashCode();

        return hash;
    }
    #endregion

    public static IEqualityComparer<Star> Comparer { get; } = new StarEqualityComparer();
}
