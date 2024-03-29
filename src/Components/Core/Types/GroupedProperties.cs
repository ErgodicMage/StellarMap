namespace StellarMap.Core.Types;

[DataContract (Name = "GroupedProperties")]
public sealed class GroupedProperties : IEquatable<GroupedProperties>
{
    #region Constructor
    public GroupedProperties()
    {
        PropertyGroups = new NestedDictionary<string, string, string>();
    }

    public GroupedProperties(string initialGroup)
    {
        PropertyGroups = new NestedDictionary<string, string, string>
        {
            { initialGroup, new Dictionary<string, string>() }
        };
    }
    #endregion

    #region Properties
    [DataMember(Order = 1)]
    public NestedDictionary<string, string, string> PropertyGroups { get; set; }
    #endregion

    #region Indexer
    public Dictionary<string, string>? this[string group] => PropertyGroups[group];

    public string? this[string group, string property]
    {
        get => PropertyGroups[group][property];
        set => PropertyGroups.Set(group, property, value!);
    }
    #endregion

    #region Add Methods
    public void Add(string group) => PropertyGroups.Add(group);

    public void Add(string group, string property, string value) => 
        PropertyGroups.Add(group, property, value);

    public void AddGroup(string group, IEnumerable<KeyValuePair<string, string>> properties) => 
        PropertyGroups.AddToOuter(group, properties);

    public void AddProperties(string group, IEnumerable<KeyValuePair<string, string>> properties) => 
        PropertyGroups.AddToInner(group, properties);
    #endregion

    #region Remove Methods
    public void Remove(string group) => PropertyGroups.Remove(group);

    public void Remove(string group, string property) => PropertyGroups.Remove(group, property);
    #endregion

    #region Get and Set Methods
    public IDictionary<string, string>? Get(string group) => PropertyGroups.Get(group);

    public string? Get(string group, string property) => PropertyGroups.Get(group, property);

    public void Set(string group, string property, string value) => PropertyGroups.Set(group, property, value);
    #endregion

    #region ToString
    public override string ToString() => PropertyGroups.ToString();
    #endregion

    #region IEquatable
    public bool Equals(GroupedProperties? other) => other is not null && PropertyGroups.Equals(other.PropertyGroups);

    public override bool Equals(object? obj) => (obj is GroupedProperties) && Equals(obj as GroupedProperties);

    public override int GetHashCode() => PropertyGroups.GetHashCode();
    #endregion
}
