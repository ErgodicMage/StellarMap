using System.Linq;
using System.Reflection;

namespace StellarMap.Core.Types;

[DataContract(Name = "BaseStellarMap")]
public class BaseStellarMap : IStellarMap, IEqualityComparer<BaseStellarMap>
{
    #region Constuctors
    public BaseStellarMap()
    {
    }

    public BaseStellarMap(string name)
    {
        MetaData = new GroupedProperties("Storage");
        MetaData["Storage"].Add("Type", "Base");
        MetaData["Storage"].Add("Version", "0.5");

        MetaData.Add("Basic");
        MetaData["Basic"].Add("Name", name);
    }
    #endregion

    #region Public Properties
    [IgnoreDataMember]
    public string Name { get => MetaData["Basic", "Name"];  
                            set => MetaData.Set("Basic", "Name", value); }

    [DataMember(Order = 1)]
    public GroupedProperties MetaData { get; set; }

    [DataMember(Order = 2)]
    public IDictionary<string, Star>? Stars { get; set; }

    [DataMember(Order = 3)]
    public IDictionary<string, Planet>? Planets { get; set; }

    [DataMember(Order = 4)]
    public IDictionary<string, DwarfPlanet>? DwarfPlanets { get; set; }

    [DataMember(Order = 5)]
    public IDictionary<string, Satellite>? Satellites { get; set; }

    [DataMember(Order = 6)]
    public IDictionary<string, Asteroid>? Asteroids { get; set; }

    [DataMember(Order = 7)]
    public IDictionary<string, Comet>? Comets { get; set; }
    #endregion

    #region Static
    public static IStellarMap DefaultMap { get; } = new BaseStellarMap("Default");
    #endregion

    #region Public Get Functions
    public virtual T? Get<T>(string id) where T : IStellarBody
    {
        T? t = default;

        if (!string.IsNullOrEmpty(id))
        {
            IDictionary<string, T>? dict = GetDictionary<T>(false);

            if (dict is not null && dict.ContainsKey(id))
                t = dict[id];
        }

        return t;
    }

    public virtual void Get<T>(ICollection<string> identifiers, IDictionary<string, T> output) 
        where T : IStellarBody
    {
        foreach (string id in identifiers)
        {
            T? t = Get<T>(id);
            if (t is not null)
                output.Add(id, t);
        }
    }
    #endregion

    #region Public Add Functions
    public virtual void Add<T>(T t) where T : IStellarBody
    {
        IDictionary<string, T>? dict = GetDictionary<T>(true);

        if (string.IsNullOrEmpty(t.Identifier))
            t.Identifier = GenerateIdentifier<T>();

        if (dict is not null && !dict.ContainsKey(t.Identifier))
        {
            t.Map = this;
            dict.Add(t.Identifier, t);
        }
    }

    public virtual void Add<T>(ICollection<T> ts) where T : IStellarBody
    {
        IDictionary<string, T>? dict = GetDictionary<T>(false);

        foreach (T t in ts)
        {
            if (string.IsNullOrEmpty(t.Identifier))
                t.Identifier = GenerateIdentifier<T>();

            if (dict is not null && !dict.ContainsKey(t.Identifier))
            {
                t.Map = this;
                dict.Add(t.Identifier, t);
            }
        }
    }
    #endregion

    #region Public Methods
    public virtual string GenerateIdentifier<T>() where T : IStellarBody
    {
        Type dt = typeof(T);
        int count = 0;
        string prefix = string.Empty;

        switch (dt.Name)
        {
            case Constants.BodyTypes.Planet:
                prefix = Constants.BodyTypes.Planet;
                if (Planets is not null)
                    count = Planets.Count;
                break;
            case Constants.BodyTypes.DwarfPlanet:
                prefix = Constants.BodyTypes.DwarfPlanet;
                if (DwarfPlanets is not null)
                    count = DwarfPlanets.Count;
                break;
            case Constants.BodyTypes.Star:
                prefix = Constants.BodyTypes.Star;
                if (Stars is not null)
                    count = Stars.Count;
                break;
            case Constants.BodyTypes.Satellite:
                prefix = Constants.BodyTypes.Satellite;
                if (Satellites is not null)
                    count = Satellites.Count;
                break;
            case Constants.BodyTypes.Asteroid:
                prefix = Constants.BodyTypes.Asteroid;
                if (Asteroids is not null)
                    count = Asteroids.Count;
                break;
            case Constants.BodyTypes.Comet:
                prefix = Constants.BodyTypes.Comet;
                if (Comets is not null)
                    count = Comets.Count;
                break;
        }

        count++;

        StringBuilder sb = new StringBuilder();
        sb.Append(prefix);
        sb.Append("-");
        sb.Append(count.ToString("D5"));
        string id = sb.ToString();

        return id;
    }

    public virtual IList<string> GetBodyTypes()
    {
        return new List<string>()
        {
            Constants.BodyTypes.Star,
            Constants.BodyTypes.Planet,
            Constants.BodyTypes.DwarfPlanet,                
            Constants.BodyTypes.Satellite,
            Constants.BodyTypes.Asteroid,
            Constants.BodyTypes.Comet
        };
    }

    public virtual object? GetBody(string bodytype)
    {
        return bodytype switch
        {
            Constants.BodyTypes.Star => Stars as object,
            Constants.BodyTypes.Planet => Planets as object,
            Constants.BodyTypes.DwarfPlanet => DwarfPlanets as object,
            Constants.BodyTypes.Satellite => Satellites as object,
            Constants.BodyTypes.Asteroid => Asteroids as object,
            Constants.BodyTypes.Comet => Comets as object,
            _ => null
        };
    }

    public virtual Type? GetTypeOfBody(string bodytype)
    {
        return bodytype switch
        {
            Constants.BodyTypes.Star => typeof(Dictionary<string, Star>),
            Constants.BodyTypes.Planet => typeof(Dictionary<string, Planet>),
            Constants.BodyTypes.DwarfPlanet => typeof(Dictionary<string, DwarfPlanet>),
            Constants.BodyTypes.Satellite => typeof(Dictionary<string, Satellite>),
            Constants.BodyTypes.Asteroid => typeof(Dictionary<string, Asteroid>),
            Constants.BodyTypes.Comet => typeof(Dictionary<string, Comet>),
            _ => null
        };
    }

    public virtual bool SetBody(string bodytype, object data)
    {
#pragma warning disable S125
        // Note this is some whacky syntax, not sure about realing using it since it's so obstuse
        // decided not to use it.
        //var bret = bodytype switch
        //{
        //    Constants.BodyTypes.Star => ((Func<bool>)(() => { Stars = (Dictionary<string, Star>)data; return true;}))(),
        //    Constants.BodyTypes.Planet => ((Func<bool>)(() => { Planets = (Dictionary<string, Planet>)data; return true; }))(),
        //    Constants.BodyTypes.DwarfPlanet => ((Func<bool>)(() => { DwarfPlanets = (Dictionary<string, DwarfPlanet>)data; return true; }))(),
        //    Constants.BodyTypes.Satellite => ((Func<bool>)(() => { Satellites = (Dictionary<string, Satellite>)data; return true; }))(),
        //    Constants.BodyTypes.Asteroid => ((Func<bool>)(() => { Asteroids = (Dictionary<string, Asteroid>)data; return true; }))(),
        //    Constants.BodyTypes.Comet => ((Func<bool>)(() => { Comets = (Dictionary<string, Comet>)data; return true; }))(),
        //    _ => false
        //};
#pragma warning restore S125

        // Note this was the original switch statement, though longer it's not as obtuse
        bool bret = false;
        switch (bodytype)
        {
            case Constants.BodyTypes.Star:
                Stars = (Dictionary<string, Star>)data;
                bret = true;
                break;
            case Constants.BodyTypes.Planet:
                Planets = (Dictionary<string, Planet>)data;
                bret = true;
                break;
            case Constants.BodyTypes.DwarfPlanet:
                DwarfPlanets = (Dictionary<string, DwarfPlanet>)data;
                bret = true;
                break;
            case Constants.BodyTypes.Satellite:
                Satellites = (Dictionary<string, Satellite>)data;
                bret = true;
                break;
            case Constants.BodyTypes.Asteroid:
                Asteroids = (Dictionary<string, Asteroid>)data;
                bret = true;
                break;
            case Constants.BodyTypes.Comet:
                Comets = (Dictionary<string, Comet>)data;
                bret = true;
                break;
        }

        return bret;
    }

    //public virtual void SetMap() => MapSetter.SetMap(this, this);
    #endregion

    #region Protected Functions
    protected virtual IDictionary<string, T>? GetDictionary<T>(bool create) where T : IStellarBody
    {
        IDictionary<string, T>? dict = default;
        Type dt = typeof(T);

        if (dt == typeof(Planet))
        {
            if (create && Planets is null)
                Planets = new Dictionary<string, Planet>();
            dict = (IDictionary<string, T>)Planets;
        }
        else if (dt == typeof(Star))
        {
            if (create && Stars is null)
                Stars = new Dictionary<string, Star>();
            dict = (IDictionary<string, T>)Stars;
        }
        else if (dt == typeof(DwarfPlanet))
        {
            if (create && DwarfPlanets is null)
                DwarfPlanets = new Dictionary<string, DwarfPlanet>();
            dict = (IDictionary<string, T>)DwarfPlanets;
        } 
        else if (dt == typeof(Satellite))
        {
            if (create && Satellites is null)
                Satellites = new Dictionary<string, Satellite>();
            dict = (IDictionary<string, T>)Satellites;
        }           
        else if (dt == typeof(Asteroid))
        {
            if (create && Asteroids is null)
                Asteroids = new Dictionary<string, Asteroid>();
            dict = (IDictionary<string, T>)Asteroids;
        }
        else if (dt == typeof(Comet))
        {
            if (create && Comets is null)
                Comets = new Dictionary<string, Comet>();
            dict = (IDictionary<string, T>)Comets;
        }

        return dict;
    }
    #endregion

    #region IEqualityComparer
    public bool Equals(BaseStellarMap x, BaseStellarMap y) => BaseStellarMapEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object obj) => BaseStellarMapEqualityComparer.Comparer.Equals(obj as BaseStellarMap);

    public int GetHashCode(BaseStellarMap obj) => BaseStellarMapEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => BaseStellarMapEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class BaseStellarMapEqualityComparer : IEqualityComparer<BaseStellarMap>
{
    #region IEqualityComparer
    public bool Equals(BaseStellarMap x, BaseStellarMap y)
    {
        bool bRet = true;

        if (x is null || y is null)
            bRet = false;
        else if (!ReferenceEquals(x, y))
        {
            bRet = x.MetaData != null &&
                   x.MetaData.Equals(y.MetaData) &&
                   IsEqual<Planet>(x.Planets, y.Planets) &&
                   IsEqual<Star>(x.Stars, y.Stars) &&
                   IsEqual<DwarfPlanet>(x.DwarfPlanets, y.DwarfPlanets) &&
                   IsEqual<Satellite>(x.Satellites, y.Satellites) &&
                   IsEqual<Asteroid>(x.Asteroids, y.Asteroids) &&
                   IsEqual<Comet>(x.Comets, y.Comets);
        }

        return bRet;
    }

    public int GetHashCode(BaseStellarMap obj)
    {
        int hash = 1345;
        if (obj.MetaData != null)
            hash ^= obj.MetaData.GetHashCode();
        if (obj.Stars != null)
            hash ^= obj.Stars.GetHashCode();
        if (obj.Planets != null)
            hash ^= obj.Planets.GetHashCode();
        if (obj.DwarfPlanets != null)
            hash ^= obj.DwarfPlanets.GetHashCode();
        if (obj.Satellites != null)
            hash ^= obj.Satellites.GetHashCode();
        if (obj.Asteroids != null)
            hash ^= obj.Asteroids.GetHashCode();
        if (obj.Comets != null)
            hash ^= obj.Comets.GetHashCode();

        return hash;
    }
    #endregion

    public static IEqualityComparer<BaseStellarMap> Comparer { get; } = new BaseStellarMapEqualityComparer();

    #region IsEqual
    public static bool IsEqual<T>(IDictionary<string, T> thisObject, IDictionary<string, T> otherObject) //where T : StellarBody
    {
        bool bRet = true;

        if (thisObject == null && otherObject == null)
            bRet = true;
        else if ((thisObject == null) || (otherObject == null))
            bRet = false;
        else if (!ReferenceEquals(thisObject, otherObject))
        {
            if (thisObject.Count == otherObject.Count)
            {
                var thisEnumerator = thisObject.GetEnumerator();
                var otherEnumerator = otherObject.GetEnumerator();

                while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
                {
                    if (thisEnumerator.Current.Key.Equals(otherEnumerator.Current.Key))
                    {
                        if (!thisEnumerator.Current.Value.Equals(otherEnumerator.Current.Value))
                            bRet = false;
                    }
                    else
                        bRet = false;

                    if (!bRet)
                        break;
                }
            }
            else
                bRet = false;
        }

        return bRet;
    }
    #endregion
}
