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
    public IDictionary<string, string> BasicProperties { get => Properties.Get("Basic"); }

    public IStellarMap Map { get; set; }
    #endregion

    #region IEqualityComparer
    public bool Equals(StellarBody x, StellarBody y) => StellarBodyEqualityComparer.Comparer.Equals(x, y);

    public override bool Equals(object obj) => StellarBodyEqualityComparer.Comparer.Equals(this, obj as StellarBody);

    public int GetHashCode(StellarBody obj) => StellarBodyEqualityComparer.Comparer.GetHashCode(obj);

    public override int GetHashCode() => StellarBodyEqualityComparer.Comparer.GetHashCode(this);
    #endregion
}

public sealed class StellarBodyEqualityComparer : IEqualityComparer<StellarBody>
{
    #region IEqualityComparer
    public bool Equals(StellarBody x, StellarBody y)
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

    public int GetHashCode(StellarBody obj)
    {
        int hash = 1;
        if (!string.IsNullOrEmpty(obj.Name))
            hash ^= obj.Name.GetHashCode();
        if (!string.IsNullOrEmpty(obj.Identifier))
            hash ^= obj.Identifier.GetHashCode();
        if (!string.IsNullOrEmpty(obj.ParentIdentifier))
            hash ^= obj.ParentIdentifier.GetHashCode();
        if (!string.IsNullOrEmpty(obj.BodyType))
            hash ^= obj.BodyType.GetHashCode();
        if (obj.Properties != null)
            hash ^= obj.Properties.GetHashCode();

        return hash;
    }
    #endregion

    public static IEqualityComparer<StellarBody> Comparer { get; } = new StellarBodyEqualityComparer();
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
    public virtual T Get<T>(string name, GroupNamedIdentifiers groupIdentifiers, string groupName) where T : IStellarBody
    {
        T t = default;

        if (groupIdentifiers.GroupIdentifiers.TryGetValue(groupName, out Dictionary<string, string> identifiers) && 
                identifiers.TryGetValue(name, out string id))
            t = Map.Get<T>(id);

        return t;

    }

    public virtual IDictionary<string, T> GetAll<T>(GroupNamedIdentifiers groupIdentifiers, string groupName) where T : IStellarBody
    {
        IDictionary<string, T> all = null;

        if (groupIdentifiers.GroupIdentifiers.TryGetValue(groupName, out Dictionary<string, string> identifiers))
        {
            all = new Dictionary<string, T>();

            foreach (var kvp in identifiers)
            {
                T t = Map.Get<T>(kvp.Value);
                if (t != null)
                    all.Add(kvp.Key, t);
            }
        }

        return all;
    }
    #endregion

    #region Add Methods
    public virtual void Add<T>(T t, GroupNamedIdentifiers groupIdentifiers, string groupName) where T : IStellarBody
    {
        if (t != null)
        {
            t.ParentIdentifier = this.Identifier;

            T found = Map.Get<T>(t.Identifier);
            if (found == null)
                Map.Add<T>(t);

            if (groupIdentifiers.GroupIdentifiers.TryGetValue(groupName, out Dictionary<string, string> identifiers))
                identifiers.Add(t.Name, t.Identifier);
            else
            {
                groupIdentifiers.Add(groupName);
                groupIdentifiers.GroupIdentifiers[groupName].Add(t.Name, t.Identifier);
            }
        }
    }
    #endregion
}
