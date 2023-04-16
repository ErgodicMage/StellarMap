using System.Xml.Linq;

namespace StellarMap.Core.Bodies;

[DataContract (Name = Constants.BodyTypes.StellarBody)]
public abstract class StellarBody : IStellarBody, IEqualityComparer<StellarBody>
{
    #region Cosntructors
    protected StellarBody()
    {            
    }
        
    protected StellarBody(string name, string bodytype)
    {
        Properties = new GroupedProperties("Basic");
        Map ??= BaseStellarMap.DefaultMap;
        Name = name;
        BodyType = bodytype;
    }
    #endregion

    #region Public Properties
    [DataMember (Order = 1)]
    public string? Identifier { get; set; }

    [DataMember (Order = 2)]
    public string? ParentIdentifier { get; set; }

    [DataMember (Order = 3)]
    public string Name { get; set; }

    [DataMember (Order = 4)]
    public string BodyType { get; set; }

    [DataMember (Order = 5)]
    public GroupedProperties Properties { get; set; }

    [IgnoreDataMember]
    public IDictionary<string, string> BasicProperties { get => Properties.Get("Basic").Value; }

    public IStellarMap Map { get; set; }
    #endregion

    #region IEqualityComparer
    public bool Equals(StellarBody? x, StellarBody? y) => StellarBodyEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object? obj) => StellarBodyEqualityComparer.Comparer.Equals(this, obj as StellarBody);

    public int GetHashCode(StellarBody? obj) => StellarBodyEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => StellarBodyEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class StellarBodyEqualityComparer : IEqualityComparer<StellarBody>
{
    #region IEqualityComparer
    public bool Equals(StellarBody? x, StellarBody? y)
    {
        bool bRet = true;

        if (x is null || y is null)
            bRet = false;
        else if (x.Name is null || y.Name is null || x.Identifier is null || y.Identifier is null)
            bRet = false;
        else if (!ReferenceEquals(x, y))
        {
            bRet = x.Name == y.Name && x.Identifier == y.Identifier &&
                    (x.ParentIdentifier == null || x.ParentIdentifier.Equals(y.ParentIdentifier)) &&
                    x.BodyType == y.BodyType &&
                    x.Properties.Equals(y.Properties);
        }

        return bRet;
    }

    public int GetHashCode(StellarBody? obj)
    {
        if (obj is null) return 0;

        int hash = 1;
        if (obj.Name is not null)
            hash ^= obj.Name.GetHashCode();
        if (obj.Identifier is not null)
            hash ^= obj.Identifier.GetHashCode();
        if (obj.ParentIdentifier is not null)
            hash ^= obj.ParentIdentifier.GetHashCode();
        if (obj.BodyType is not null)
            hash ^= obj.BodyType.GetHashCode();
        if (obj.Properties is not null)
            hash ^= obj.Properties.GetHashCode();

        return hash;
    }
    #endregion

    public static StellarBodyEqualityComparer Comparer { get; } = new StellarBodyEqualityComparer();
}

public abstract class StellarParentBody : StellarBody, IStellarParentBody
{
    #region Constructors
    protected StellarParentBody()
    {
    }

    protected StellarParentBody(string name, string bodytype) : base(name, bodytype)
    {
    }
    #endregion

    #region Get Methods
    public virtual Result<T> Get<T>(string name, GroupNamedIdentifiers groupIdentifiers, string groupName) where T : IStellarBody
    {
        Result guardResult = GuardClause.NullOrWhiteSpace(name).Null(groupIdentifiers).NullOrWhiteSpace(groupName);
        if (!guardResult.Success) return guardResult;

        if (groupIdentifiers.GroupIdentifiers.TryGetValue(groupName, out Dictionary<string, string>? identifiers) && 
                identifiers.TryGetValue(name, out string? id))
            return Map.Get<T>(id);

        return Result<T>.Error($"StellarParentBody:Get could not get {name} from {groupName}");
    }

    public virtual Result<IDictionary<string, T>> GetAll<T>(GroupNamedIdentifiers? groupIdentifiers, string groupName) where T : IStellarBody
    {
        Result guardResult = GuardClause.Null(groupIdentifiers).NullOrWhiteSpace(groupName);
        if (!guardResult.Success) return guardResult;

        if (!groupIdentifiers!.GroupIdentifiers.TryGetValue(groupName, out Dictionary<string, string>? identifiers))
            return Result.Error($"StellarParentBody:GetAll could not get identifiers for {groupName}");

        IDictionary<string, T> all = new Dictionary<string, T>();

        foreach (var kvp in identifiers)
        {
            T? t = Map.Get<T>(kvp.Value);
            if (t is not null)
                all.Add(kvp.Key, t);
        }

        return Result<IDictionary<string, T>>.Ok(all);
    }
    #endregion

    #region Add Methods
    public virtual Result Add<T>(T t, GroupNamedIdentifiers groupIdentifiers, string groupName) where T : IStellarBody
    {
        Result guardResult = GuardClause.Null(t).NullOrEmpty(t.Name).Null(groupIdentifiers).NullOrWhiteSpace(groupName);
        if (!guardResult.Success) return guardResult;

        t.ParentIdentifier = this.Identifier;

        T? found = Map.Get<T>(t.Identifier);
        if (found is null)
            Map.Add<T>(t);

        if (groupIdentifiers.GroupIdentifiers.TryGetValue(groupName, out Dictionary<string, string>? identifiers) && !identifiers.ContainsKey(t.Name))
        {
            identifiers.Add(t.Name, t.Identifier!);
            return Result.Ok();
        }

        Result result = groupIdentifiers.Add(groupName);
        if (result.Success)
        {
            if (groupIdentifiers.GroupIdentifiers.ContainsKey(t.Name))
                result = Result.Error($"StellarBody:Add duplicate name {t.Name} in {groupName}");
            else
                groupIdentifiers.GroupIdentifiers[groupName].Add(t.Name, t.Identifier!);
        }
        return result;
    }
    #endregion
}
