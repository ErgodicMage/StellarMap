namespace StellarMap.Progression;

[DataContract (Name = ProgressionConstants.BodyType.ERBridge)]
public class ERBridge : StellarBody, IEqualityComparer<ERBridge>
{
    #region Constructors
    public ERBridge()
    {            
    }
        
    public ERBridge(string type, StarSystem system1, StarSystem system2) : 
        base(string.Empty, ProgressionConstants.BodyType.ERBridge)
    {
        BodyType = ProgressionConstants.BodyType.ERBridge;
        BridgeType = type;
        Map = system1.Map;
        (Map as ProgressionMap)?.Add(this);

        Portals = new Portal[2];
        Portals[0].StarSystemIdentifier = system1.Identifier;
        Portals[0].ERBridgeIdentifier = this.Identifier;
        Portals[1].StarSystemIdentifier = system2.Identifier;
        Portals[1].ERBridgeIdentifier = this.Identifier;
        SetName();
    }
    #endregion

    #region Properties

    [DataMember (Order = 11)]
    public string BridgeType { get; set; }

    [DataMember (Order = 12)]
    public Portal[] Portals { get; init; }
    #endregion

    #region Get Methods
    public Result<StarSystem> GetStarSystem(int end)
    {
        Result guardResult = GuardClause.OutOfRange(end, 0, 1);
        if (!guardResult.Success) return guardResult;

        string? identifier = Portals?[end].StarSystemIdentifier;
        var map = Map as ProgressionMap;
        if (map is null) return Result.Error("ERBridge:GetStarSystem Map is not a ProgressionMap");

        return map.Get<StarSystem>(identifier);
    }

    public Result<StarSystem> GetStarSystem(string name)
    {
        Result guardResult = GuardClause.NullOrWhiteSpace(name);
        if (!guardResult.Success) return guardResult;

        var map = Map as ProgressionMap;
        if (map is null) return Result.Error("ERBridge:GetStarSystem Map is not a ProgressionMap");

        foreach (Portal p in Portals)
        {
            var system = map.Get<StarSystem>(p.StarSystemIdentifier);
            if (system.Success && system.Value.Name == name)
                return system;
        }

        return Result.Error($"ERBridge:GaetStarSystem can not find star system {name}");
    }

    public Result<IDictionary<string, StarSystem>> GetStarSystem()
    {
        var map = Map as ProgressionMap;
        if (map is null) return Result.Error("ERBridge:GetStarSystem Map is not a ProgressionMap");

        var systems = new Dictionary<string, StarSystem>();

        foreach (Portal p in Portals)
        {
            StarSystem s = map.Get<StarSystem>(p.StarSystemIdentifier);
            systems.Add(s.Name, s);
        }

        return systems;
    }
    #endregion

    #region Protected Methods
    protected void SetName()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Bridge: ");
        bool bFirst = true;
        foreach (Portal p in Portals)
        {
            StarSystem? system = (Map as ProgressionMap)?.Get<StarSystem>(p.StarSystemIdentifier);
            if (system is not null)
            {
                if (!bFirst)
                    sb.Append("-");
                sb.Append(system.Name);
                bFirst = false;
            }
        }
        Name = sb.ToString();
    }
    #endregion

    #region IEqualityComparer
    public bool Equals(ERBridge? x, ERBridge? y) => ERBridgeEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => ERBridgeEqualityComparer.Comparer.Equals(this, obj as ERBridge);

    public int GetHashCode(ERBridge? obj) => ERBridgeEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => ERBridgeEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class ERBridgeEqualityComparer : IEqualityComparer<ERBridge>
{
    #region IEqualityComparer
    public bool Equals(ERBridge? x, ERBridge? y)
    {
        bool bRet = true;

        if (x is null || y is null)
            bRet = false;
        else if (!ReferenceEquals(x, y))
        {
            bRet = StellarBodyEqualityComparer.Comparer.Equals(x, y) && 
                    x.BridgeType == y.BridgeType && x.Portals != null && y.Portals != null &&
                    x.Portals[0].Equals(y.Portals[0]) && x.Portals[1].Equals(y.Portals[1]);
        }

        return bRet;
    }

    public int GetHashCode(ERBridge? obj)
    {
        if (obj is null) return 0;

        int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
        if (!string.IsNullOrEmpty(obj.BridgeType))
            hash ^= obj.BridgeType.GetHashCode();
        if (obj.Portals is not null)
        {
            foreach (Portal p in obj.Portals)
                hash ^= p.GetHashCode();
        }

        return hash;
    }
    #endregion

    public static ERBridgeEqualityComparer Comparer { get; } = new ERBridgeEqualityComparer();
}
