using Newtonsoft.Json.Linq;

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
    public IDictionary<string, string>? Planets 
        { get => StarGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Planets).Value; }

    [IgnoreDataMember]
    public IDictionary<string, string>? DwarfPlanets 
        { get => StarGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.DwarfPlanets).Value; }

    [IgnoreDataMember]
    public IDictionary<string, string>? Asteroids 
        { get => StarGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Asteroids).Value; }

    [IgnoreDataMember]
    public IDictionary<string, string>? Comets 
        { get => StarGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Comets).Value; }
    #endregion

    #region Get Functions
    public virtual Result<Planet> GetPlanet(string name) => 
        Get<Planet>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.Planets);

    public virtual Result<IDictionary<string, Planet>> GetPlanets() => 
        GetAll<Planet>(StarGroupIdentifiers, Constants.NamedIdentifiers.Planets);

    public virtual Result<DwarfPlanet> GetDwarfPlanet(string name) => 
        Get<DwarfPlanet>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.DwarfPlanets);

    public virtual Result<IDictionary<string, DwarfPlanet>> GetDwarfPlanets() => 
        GetAll<DwarfPlanet>(StarGroupIdentifiers, Constants.NamedIdentifiers.DwarfPlanets);

    public virtual Result<Asteroid> GetAsteroid(string name) => 
        Get<Asteroid>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.Asteroids);

    public virtual Result<IDictionary<string, Asteroid>> GetAsteroids() => 
        GetAll<Asteroid>(StarGroupIdentifiers, Constants.NamedIdentifiers.Asteroids);

    public virtual Result<Comet> GetComet(string name) => 
        Get<Comet>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.Comets);

    public virtual Result<IDictionary<string, Comet>> GetComets() => 
        GetAll<Comet>(StarGroupIdentifiers, Constants.NamedIdentifiers.Comets);
    #endregion

    #region Public Add Functions
    public Result Add(Planet planet) => 
        Add<Planet>(planet, StarGroupIdentifiers, Constants.NamedIdentifiers.Planets);

    public Result Add(DwarfPlanet dwarf) => 
        Add<DwarfPlanet>(dwarf, StarGroupIdentifiers, Constants.NamedIdentifiers.DwarfPlanets);            

    public Result Add(Asteroid asteroid) => 
        Add<Asteroid>(asteroid, StarGroupIdentifiers, Constants.NamedIdentifiers.Asteroids);

    public Result Add(Comet comet) => 
        Add<Comet>(comet, StarGroupIdentifiers, Constants.NamedIdentifiers.Comets);
    #endregion

    #region IEqualityComparer
    public bool Equals(Star? x, Star? y) => StarEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => StarEqualityComparer.Comparer.Equals(this, obj as Star);

    public int GetHashCode(Star? obj) => StarEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => StarEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class StarEqualityComparer : IEqualityComparer<Star>
{
    #region IEqualityComparer
    public bool Equals(Star? x, Star? y) =>
                StellarBodyEqualityComparer.Comparer.Equals(x, y) &&
                x!.StarGroupIdentifiers.Equals(y!.StarGroupIdentifiers);

    public int GetHashCode(Star? obj)
    {
        if (obj is null) return 0;

        int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
        if (obj.StarGroupIdentifiers is not null)
            hash ^= obj.StarGroupIdentifiers.GetHashCode();

        return hash;
    }
    #endregion

    public static StarEqualityComparer Comparer { get; } = new StarEqualityComparer();
}
