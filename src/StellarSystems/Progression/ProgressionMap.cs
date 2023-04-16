namespace StellarMap.Progression;

[DataContract (Name = "ProgressionMap")]
public class ProgressionMap : BaseStellarMap, IEqualityComparer<ProgressionMap>
{
    #region Constructors
    public ProgressionMap()
    {
    }

    public ProgressionMap(string name) : base(name)
    {
        // reset Storage Metadata properties to 
        MetaData["Storage", "Type"] = "Progresssion";
        MetaData["Storage", "Version"] = "0.5";
    }
    #endregion

    #region Properties
    [DataMember (Order = 11)]
    public IDictionary<string, Habitat>? Habitats { get; set; }

    [DataMember (Order = 12)]
    public IDictionary<string, ERBridge>? Bridges { get; set; }

    [DataMember (Order = 13)]
    public IDictionary<string, StarSystem>? StarSystems { get; set; }

    [DataMember (Order = 14)]
    public IDictionary<string, Cluster>? Clusters { get; set; }

    [DataMember (Order = 15)]
    public IDictionary<string, Sector>? Sectors { get; set; }
    #endregion

    #region Public Methods
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
            case ProgressionConstants.BodyType.StarSystem:
                prefix = ProgressionConstants.BodyType.StarSystem;
                if (StarSystems is not null)
                    count = StarSystems.Count;
                break;
            case ProgressionConstants.BodyType.Cluster:
                prefix = ProgressionConstants.BodyType.Cluster;
                if (Clusters is not null)
                    count = Clusters.Count;
                break;
            case ProgressionConstants.BodyType.Sector:
                prefix = ProgressionConstants.BodyType.Sector;
                if (Sectors is not null)
                    count = Sectors.Count;
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
            case ProgressionConstants.BodyType.Habitat:
                prefix = ProgressionConstants.BodyType.Habitat;
                if (Habitats is not null)
                    count = Habitats.Count;
                break;
            case ProgressionConstants.BodyType.ERBridge:
                prefix = ProgressionConstants.BodyType.ERBridge;
                if (Bridges is not null)
                    count = Bridges.Count;
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
    #endregion

    #region Protected Methods
    protected override Result<IDictionary<string, T>> GetDictionary<T>(bool create)
    {
        IDictionary<string, T>? dict = default;
        Type dt = typeof(T);

        if (dt == typeof(ProgressionStar))
        {
            if (create && Stars == null)
                Stars = new Dictionary<string, Star>();
            dict = Stars as IDictionary<string, T>;
        }
        else if (dt == typeof(ERBridge))
        {
            if (create && Bridges == null)
                Bridges = new Dictionary<string, ERBridge>();
            dict = Bridges as IDictionary<string, T>;
        }
        else if (dt == typeof(Habitat))
        {
            if (create && Habitats == null)
                Habitats = new Dictionary<string, Habitat>();
            dict = Habitats as IDictionary<string, T>;
        }
        else if (dt == typeof(StarSystem))
        {
            if (create && StarSystems == null)
                StarSystems = new Dictionary<string, StarSystem>();
            dict = StarSystems as IDictionary<string, T>;
        }
        else if (dt == typeof(Cluster))
        {
            if (create && Clusters == null)
                Clusters = new Dictionary<string, Cluster>();
            dict = Clusters as IDictionary<string, T>;
        }
        else if (dt == typeof(Sector))
        {
            if (create && Sectors == null)
                Sectors = new Dictionary<string, Sector>();
            dict = Sectors as IDictionary<string, T>;
        }

        return dict is not null ? Result<IDictionary<string, T>>.Ok(dict) : base.GetDictionary<T>(create);
    }

    public override IList<string> GetBodyTypes()
    {
        IList<string> bodytypes = base.GetBodyTypes();

        bodytypes.Add(ProgressionConstants.BodyType.Habitat);
        bodytypes.Add(ProgressionConstants.BodyType.ERBridge);
        bodytypes.Add(ProgressionConstants.BodyType.StarSystem);
        bodytypes.Add(ProgressionConstants.BodyType.Cluster);
        bodytypes.Add(ProgressionConstants.BodyType.Sector);

        return bodytypes;
    }

    public override Result<object> GetBody(string bodytype)
    {
        Result guardResult = GuardClause.NullOrWhiteSpace(bodytype);
        if (!guardResult.Success) return guardResult;

        var body = base.GetBody(bodytype);
        if (body.Success) return body;

#pragma warning disable CS8604 // Possible null reference argument.
        return bodytype switch
        {
            ProgressionConstants.BodyType.Habitat => Habitats as object,
            ProgressionConstants.BodyType.ERBridge => Bridges as object,
            ProgressionConstants.BodyType.StarSystem => StarSystems as object,
            ProgressionConstants.BodyType.Cluster => Clusters as object,
            ProgressionConstants.BodyType.Sector => Sectors as object,
            _ => Result.Error($"ProgressionBody:GetBody can not get {bodytype}"),
        };
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public override Result<Type> GetTypeOfBody(string bodytype)
    {
        Result<Type> t = base.GetTypeOfBody(bodytype);
        if (t.Success) return t;

        return bodytype switch
        {
            ProgressionConstants.BodyType.Habitat => typeof(Dictionary<string, Habitat>),
            ProgressionConstants.BodyType.ERBridge => typeof(Dictionary<string, ERBridge>),
            ProgressionConstants.BodyType.StarSystem => typeof(Dictionary<string, StarSystem>),
            ProgressionConstants.BodyType.Cluster => typeof(Dictionary<string, Cluster>),
            ProgressionConstants.BodyType.Sector => typeof(Dictionary<string, Sector>),
            _ => Result.Error($"ProgressionMap:GetTypeOfBody can not get Type {bodytype}")
        };
    }

    public override Result SetBody(string bodytype, object data)
    {
        Result guardResult = GuardClause.NullOrWhiteSpace(bodytype);
        if (!guardResult.Success) return guardResult;

        Result result = base.SetBody(bodytype, data);
        if (result.Success) return result;

        bool bret = false;
        switch (bodytype)
        {
            case ProgressionConstants.BodyType.Habitat:
                Habitats = (Dictionary<string, Habitat>)data;
                bret = true;
                break;
            case ProgressionConstants.BodyType.ERBridge:
                Bridges = (Dictionary<string, ERBridge>)data;
                bret = true;
                break;
            case ProgressionConstants.BodyType.StarSystem:
                StarSystems = (Dictionary<string, StarSystem>)data;
                bret = true;
                break;
            case ProgressionConstants.BodyType.Cluster:
                Clusters = (Dictionary<string, Cluster>)data;
                bret = true;
                break;
            case ProgressionConstants.BodyType.Sector:
                Sectors = (Dictionary<string, Sector>)data;
                bret = true;
                break;
        }

        return bret;
    }
    #endregion

    #region IEqualityComparer
    public bool Equals(ProgressionMap? x, ProgressionMap? y) => ProgressionMapEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => ProgressionMapEqualityComparer.Comparer.Equals(this, obj as ProgressionMap);

    public int GetHashCode(ProgressionMap? obj) => ProgressionMapEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => ProgressionMapEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class ProgressionMapEqualityComparer : IEqualityComparer<ProgressionMap>
{
    #region IEqualityComparer
    public bool Equals(ProgressionMap? x, ProgressionMap? y)
    {
        bool bRet = true;

        if (x is null || y is null)
            bRet = false;
        else if (!ReferenceEquals(x, y))
        {
            bRet = BaseStellarMapEqualityComparer.Comparer.Equals(x, y) &&
                    BaseStellarMapEqualityComparer.IsEqual<Habitat>(x.Habitats, y.Habitats) &&
                    BaseStellarMapEqualityComparer.IsEqual<ERBridge>(y.Bridges, y.Bridges) &&
                    BaseStellarMapEqualityComparer.IsEqual<StarSystem>(x.StarSystems, y.StarSystems) &&
                    BaseStellarMapEqualityComparer.IsEqual<Cluster>(x.Clusters, y.Clusters) &&
                    BaseStellarMapEqualityComparer.IsEqual<Sector>(x.Sectors, y.Sectors);
        }

        return bRet;
    }

    public int GetHashCode(ProgressionMap? obj)
    {
        if (obj is null) return 0;

        int hash = BaseStellarMapEqualityComparer.Comparer.GetHashCode(obj);
        if (obj.Habitats != null)
            hash ^= obj.Habitats.GetHashCode();
        if (obj.Bridges != null)
            hash ^= obj.Bridges.GetHashCode();
        if (obj.StarSystems != null)
            hash ^= obj.StarSystems.GetHashCode();
        if (obj.Clusters != null)
            hash ^= obj.Clusters.GetHashCode();
        if (obj.Sectors != null)
            hash ^= obj.Sectors.GetHashCode();

        return hash;
    }
    #endregion

    public static ProgressionMapEqualityComparer Comparer { get; } = new ProgressionMapEqualityComparer();
}
