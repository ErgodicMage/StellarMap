using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StellarMap.Core.Types
{
    [DataContract]
    public class GroupNamedIdentifiers : IEquatable<GroupNamedIdentifiers>
    {
        #region Constructors
        public GroupNamedIdentifiers(string name)
        {
            this.Name = name;
            GroupIdentifiers = new NestedDictionary<string, string, string>();
        }
        #endregion

        #region Public Properties
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember (Order = 2)]
        public NestedDictionary<string, string, string> GroupIdentifiers { get; set; }
        #endregion

        #region Indexer
        public Dictionary<string, string> this[string group] => GroupIdentifiers[group];

        public string this[string group, string property] => GroupIdentifiers[group][property];
        #endregion

        #region Add Methods
        public void Add(string group) => GroupIdentifiers.Add(group);

        public void Add(string group, string name, string identifier) => GroupIdentifiers.Add(group, name, identifier);

        public void AddGroup(string group, IEnumerable<KeyValuePair<string, string>> namedidentifiers) => GroupIdentifiers.AddToOuter(group, namedidentifiers);

        public void AddProperties(string group, IEnumerable<KeyValuePair<string, string>> namedidentifiers) => GroupIdentifiers.AddToInner(group, namedidentifiers);
        #endregion

        #region Remove Methods
        public void Remove(string group) => GroupIdentifiers.Remove(group);

        public void Remove(string group, string name) => GroupIdentifiers.Remove(group, name);
        #endregion

        #region Get and Set Methods
        public IDictionary<string, string> Get(string group) => GroupIdentifiers.Get(group);

        public string Get(string group, string name) => GroupIdentifiers.Get(group, name);

        public void Set(string group, string name, string identifier) => GroupIdentifiers.Set(group, name, identifier);
        #endregion

        #region ToString
        public override string ToString() => GroupIdentifiers.ToString();
        #endregion

        #region IEquatable
        public bool Equals(GroupNamedIdentifiers other) => GroupIdentifiers.Equals(other.GroupIdentifiers);

        public override bool Equals(object obj) => (obj is GroupNamedIdentifiers) && Equals(obj as GroupNamedIdentifiers);

        public override int GetHashCode() => GroupIdentifiers.GetHashCode();
        #endregion
    }
}
