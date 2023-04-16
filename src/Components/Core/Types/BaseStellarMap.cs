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
        MetaData["Storage"]?.Add("Type", "Base");
        MetaData["Storage"]?.Add("Version", "0.5");

        MetaData.Add("Basic");
        MetaData["Basic"]?.Add("Name", name);
    }
    #endregion

    #region Public Properties
    [IgnoreDataMember]
    public string Name { get => MetaData["Basic", "Name"]!;  
                            set => MetaData?.Set("Basic", "Name", value); }

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
    public virtual Result<T> Get<T>(string? id) where T : IStellarBody
    {
        Result guardResult = GuardClause.NullOrWhiteSpace(id);
        if (!guardResult.Success) return guardResult;

        var dictResult = GetDictionary<T>(false);

        if (dictResult.Success && dictResult.Value.ContainsKey(id))
                return Result<T>.Ok(dictResult.Value[id]);

        return Result.Error($"BaseStellarBody:Get {id} was not found");
    }

    public virtual Result Get<T>(ICollection<string>? identifiers, IDictionary<string, T> output) 
        where T : IStellarBody
    {
        Result guardResult = GuardClause.Null(identifiers).Null(output);
        if (!guardResult.Success) return guardResult;

        foreach (string id in identifiers!)
        {
            Result<T> result = Get<T>(id);
            if (result.Success)
                output.Add(id, result.Value);
        }

        return Result.Ok();
    }
    #endregion

    #region Public Add Functions
    public virtual Result Add<T>(T t) where T : IStellarBody
    {
        Result guardResult = GuardClause.Null(t);
        if (!guardResult.Success) return guardResult;

        var dictResult = GetDictionary<T>(true);

        if (!dictResult.Success) return dictResult;

        if (string.IsNullOrEmpty(t.Identifier))
            t.Identifier = GenerateIdentifier<T>();

        if (!dictResult.Success)
            return dictResult;

        if (dictResult.Value.ContainsKey(t.Identifier))
            return Result.Error($"BaseStellarMap:Add {t.Identifier} already exists in {nameof(T)}");

        t.Map = this;
        dictResult.Value.Add(t.Identifier, t);
        return Result.Ok();
    }

    public virtual Result Add<T>(ICollection<T> ts) where T : IStellarBody
    {
        Result guardResult = GuardClause.Null(ts);
        if (!guardResult.Success) return guardResult;

        var dictResult = GetDictionary<T>(false);
        if (!dictResult.Success) return dictResult;
        var dict = dictResult.Value;

        foreach (T t in ts)
        {
            if (t is null)
                continue;

            if (string.IsNullOrWhiteSpace(t.Identifier))
                t.Identifier = GenerateIdentifier<T>();

            if (dict!.ContainsKey(t.Identifier))
                continue;

            t.Map = this;
            dict.Add(t.Identifier, t);
        }

        return Result.Ok();
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

    public virtual Result<object> GetBody(string bodytype)
    {
        Result guardResult = GuardClause.NullOrWhiteSpace(bodytype);
        if (!guardResult.Success) return guardResult;

#pragma warning disable CS8604 // Possible null reference argument.
        return bodytype switch
        {
            Constants.BodyTypes.Star => Stars as object,
            Constants.BodyTypes.Planet => Planets as object,
            Constants.BodyTypes.DwarfPlanet => DwarfPlanets as object,
            Constants.BodyTypes.Satellite => Satellites as object,
            Constants.BodyTypes.Asteroid => Asteroids as object,
            Constants.BodyTypes.Comet => Comets as object,
            _ => Result.Error($"BaseStellarMap:GetBody can not get {bodytype}")
        };
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public virtual Result<Type> GetTypeOfBody(string bodytype)
    {
        Result guardResult = GuardClause.NullOrWhiteSpace(bodytype);
        if (!guardResult.Success) return guardResult;

        return bodytype switch
        {
            Constants.BodyTypes.Star => typeof(Dictionary<string, Star>),
            Constants.BodyTypes.Planet => typeof(Dictionary<string, Planet>),
            Constants.BodyTypes.DwarfPlanet => typeof(Dictionary<string, DwarfPlanet>),
            Constants.BodyTypes.Satellite => typeof(Dictionary<string, Satellite>),
            Constants.BodyTypes.Asteroid => typeof(Dictionary<string, Asteroid>),
            Constants.BodyTypes.Comet => typeof(Dictionary<string, Comet>),
            _ => Result.Error($"BaseStellarMap:GetTypeOfBody can not get Type {bodytype}")
        };
    }

    public virtual Result SetBody(string bodytype, object data)
    {
        Result guardResult = GuardClause.NullOrWhiteSpace(bodytype);
        if (!guardResult.Success) return guardResult;

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
    #endregion

    #region Protected Functions
    protected virtual Result<IDictionary<string, T>> GetDictionary<T>(bool create) where T : IStellarBody
    {
        IDictionary<string, T>? dict = default;
        Type dt = typeof(T);

        if (dt == typeof(Planet))
        {
            if (create && Planets is null)
                Planets = new Dictionary<string, Planet>();
            dict = Planets as IDictionary<string, T>;
        }
        else if (dt == typeof(Star))
        {
            if (create && Stars is null)
                Stars = new Dictionary<string, Star>();
            dict = Stars as IDictionary<string, T>;
        }
        else if (dt == typeof(DwarfPlanet))
        {
            if (create && DwarfPlanets is null)
                DwarfPlanets = new Dictionary<string, DwarfPlanet>();
            dict = DwarfPlanets as IDictionary<string, T>;
        } 
        else if (dt == typeof(Satellite))
        {
            if (create && Satellites is null)
                Satellites = new Dictionary<string, Satellite>();
            dict = Satellites as IDictionary<string, T>;
        }           
        else if (dt == typeof(Asteroid))
        {
            if (create && Asteroids is null)
                Asteroids = new Dictionary<string, Asteroid>();
            dict = Asteroids as IDictionary<string, T>;
        }
        else if (dt == typeof(Comet))
        {
            if (create && Comets is null)
                Comets = new Dictionary<string, Comet>();
            dict = Comets as IDictionary<string, T>;
        }

        return dict is not null ? Result.Ok() : Result.Error($"BaseStellarMap:GetDictionary can not get dictionary for {nameof(T)}");
    }
    #endregion

    #region IEqualityComparer
    public bool Equals(BaseStellarMap? x, BaseStellarMap? y) => BaseStellarMapEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => BaseStellarMapEqualityComparer.Comparer.Equals(obj as BaseStellarMap);

    public int GetHashCode(BaseStellarMap? obj) => BaseStellarMapEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => BaseStellarMapEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class BaseStellarMapEqualityComparer : IEqualityComparer<BaseStellarMap>
{
    #region IEqualityComparer
    public bool Equals(BaseStellarMap? x, BaseStellarMap? y)
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

    public int GetHashCode(BaseStellarMap? obj)
    {
        if (obj is null) return 0;

        int hash = 1345;
        if (obj.MetaData is not null)
            hash ^= obj.MetaData.GetHashCode();
        if (obj.Stars is not null)
            hash ^= obj.Stars.GetHashCode();
        if (obj.Planets is not null)
            hash ^= obj.Planets.GetHashCode();
        if (obj.DwarfPlanets is not null)
            hash ^= obj.DwarfPlanets.GetHashCode();
        if (obj.Satellites is not null)
            hash ^= obj.Satellites.GetHashCode();
        if (obj.Asteroids is not null)
            hash ^= obj.Asteroids.GetHashCode();
        if (obj.Comets is not null)
            hash ^= obj.Comets.GetHashCode();

        return hash;
    }
    #endregion

    public static BaseStellarMapEqualityComparer Comparer { get; } = new BaseStellarMapEqualityComparer();

    #region IsEqual
    public static bool IsEqual<T>(IDictionary<string, T>? thisObject, IDictionary<string, T>? otherObject) //where T : StellarBody
    {
        bool bRet = true;

        if (thisObject is null && otherObject is null)
            bRet = true;
        else if ((thisObject is null) || (otherObject is null))
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
                        if (thisEnumerator.Current.Value is not null && !thisEnumerator.Current.Value.Equals(otherEnumerator.Current.Value))
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
