﻿namespace StellarMap.Traveller;

[DataContract(Name = "TravellerMap")]
public class TravellerMap : BaseStellarMap, IEqualityComparer<TravellerMap>
{
    #region Constructors
    public TravellerMap()
    {
    }

    public TravellerMap(string name) : base(name)
    {
        MetaData["Storage", "Type"] = "Traveller";
        MetaData["Storage", "Version"] = "0.1";
    }
    #endregion

    #region Properties
    [DataMember(Order = 11)]
    public IDictionary<string, World>? Worlds { get; set; }

    [DataMember(Order = 12)]
    public IDictionary<string, Subsector>? Subsectors { get; set; }

    [DataMember(Order = 13)]
    public IDictionary<string, Sector>? Sectors { get; set; }
    #endregion

    #region Public methods
    public override string GenerateIdentifier<T>()
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
            case TravellerConstants.BodyType.World:
                prefix = TravellerConstants.BodyType.World;
                if (Worlds is not null)
                    count = Worlds.Count;
                break;
            case TravellerConstants.BodyType.Subsector:
                prefix = TravellerConstants.BodyType.Subsector;
                if (Subsectors is not null)
                    count = Subsectors.Count;
                break;
            case TravellerConstants.BodyType.Sector:
                prefix = TravellerConstants.BodyType.Sector;
                if (Sectors is not null)
                    count = Sectors.Count;
                break;
        }

        count++;

        StringBuilder sb = new StringBuilder();
        sb.Append(prefix);
        sb.Append("-");
        sb.Append(count.ToString("D5"));
        string id = sb.ToString();

        if (string.IsNullOrEmpty(id))
            id = base.GenerateIdentifier<T>();

        return id;
    }
    #endregion

    #region Protected Methods
    protected override IDictionary<string, T>? GetDictionary<T>(bool create)
    {
        IDictionary<string, T>? dict = default;
        Type dt = typeof(T);

        if (dt == typeof(World))
        {
            if (create && Worlds == null)
                Worlds = new Dictionary<string, World>();
            dict = Worlds as IDictionary<string, T>;
        }
        else if (dt == typeof(Subsector))
        {
            if (create && Subsectors == null)
                Subsectors = new Dictionary<string, Subsector>();
            dict = Subsectors as IDictionary<string, T>;
        }
        else if (dt == typeof(Sector))
        {
            if (create && Sectors == null)
                Sectors = new Dictionary<string, Sector>();
            dict = Sectors as IDictionary<string, T>;
        }

        if (dict == null)
            dict = base.GetDictionary<T>(create);

        return dict;
    }

    public override IList<string> GetBodyTypes()
    {
        IList<string> bodytypes = base.GetBodyTypes();

        bodytypes.Add(TravellerConstants.BodyType.World);
        bodytypes.Add(TravellerConstants.BodyType.Subsector);
        bodytypes.Add(TravellerConstants.BodyType.Sector);

        return bodytypes;
    }

    public override object? GetBody(string bodytype)
    {
        object? body = base.GetBody(bodytype);

        if (body == null)
        {
            switch (bodytype)
            {
                case TravellerConstants.BodyType.World:
                    body = Worlds;
                    break;
                case TravellerConstants.BodyType.Subsector:
                    body = Subsectors;
                    break;
                case TravellerConstants.BodyType.Sector:
                    body = Sectors;
                    break;
            }
        }

        return body;
    }

    public override Type? GetTypeOfBody(string bodytype)
    {
        Type? t = base.GetTypeOfBody(bodytype);

        if (t is null)
        {
            switch (bodytype)
            {
                case TravellerConstants.BodyType.World:
                    t = typeof(Dictionary<string, World>);
                    break;
                case TravellerConstants.BodyType.Subsector:
                    t = typeof(Dictionary<string, Subsector>);
                    break;
                case TravellerConstants.BodyType.Sector:
                    t = typeof(Dictionary<string, Sector>);
                    break;
            }
        }

        return t;
    }

    public override bool SetBody(string bodytype, object data)
    {
        bool bret = base.SetBody(bodytype, data);

        if (!bret)
        {
            switch (bodytype)
            {
                case TravellerConstants.BodyType.World:
                    Worlds = (Dictionary<string, World>)data;
                    bret = true;
                    break;
                case TravellerConstants.BodyType.Subsector:
                    Subsectors = (Dictionary<string, Subsector>)data;
                    bret = true;
                    break;
                case TravellerConstants.BodyType.Sector:
                    Sectors = (Dictionary<string, Sector>)data;
                    bret = true;
                    break;
            }
        }

        return bret;
    }
    #endregion

    #region IEqualityComparer
    public bool Equals(TravellerMap? x, TravellerMap? y) => TravellerMapEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => TravellerMapEqualityComparer.Comparer.Equals(this, obj as TravellerMap);

    public int GetHashCode(TravellerMap? obj) => TravellerMapEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => TravellerMapEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class TravellerMapEqualityComparer : IEqualityComparer<TravellerMap>
{
    #region IEqualityComparer
    public bool Equals(TravellerMap? x, TravellerMap? y)
    {
        bool bRet = true;

        if (x is null || y is null)
            bRet = false;
        else if (!ReferenceEquals(x, y))
        {
            bRet =  BaseStellarMapEqualityComparer.Comparer.Equals(x, y) &&
                    BaseStellarMapEqualityComparer.IsEqual<World>(x.Worlds, y.Worlds) &&
                    BaseStellarMapEqualityComparer.IsEqual<Subsector>(y.Subsectors, y.Subsectors) &&
                    BaseStellarMapEqualityComparer.IsEqual<Sector>(y.Sectors, y.Sectors);
        }

        return bRet;
    }

    public int GetHashCode(TravellerMap? obj)
    {
        if (obj is null) return 0;
        int hash = BaseStellarMapEqualityComparer.Comparer.GetHashCode(obj);
        if (obj.Worlds is not null)
            hash ^= obj.Worlds.GetHashCode();
        if (obj.Subsectors is not null)
            hash ^= obj.Subsectors.GetHashCode();
        if (obj.Sectors is not null)
            hash ^= obj.Sectors.GetHashCode();

        return hash;
    }
    #endregion

    public static TravellerMapEqualityComparer Comparer { get; } = new TravellerMapEqualityComparer();
}
