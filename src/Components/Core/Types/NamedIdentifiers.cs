using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace StellarMap.Core.Types
{
    [DataContract]
    public class BodyNamedIdentifiers
    {
        #region Constructor
        public BodyNamedIdentifiers(string name)
        {
            this.Name = name;
            Identifiers = new Dictionary<string, string>();
        }

        #endregion

        #region Public Properties
        [DataMember (Order = 1)]
        public string Name { get; set; }

        [DataMember (Order = 2)]
        public IDictionary<string, string> Identifiers { get; set; }
        #endregion

        #region Public Functions
        public void Add(string name, string identifier)
        {
            Identifiers.Add(name, identifier);
        }

        public void RemoveByName(string name)
        {
            if (Identifiers.ContainsKey(name))
                Identifiers.Remove(name);
        }

        public void RemoveByIdentifier(string identifier)
        {
            foreach(KeyValuePair<string, string> kvp in Identifiers)
            {
                if (kvp.Value == identifier)
                {
                    Identifiers.Remove(kvp);
                    break;
                }
            }
        }
        #endregion
    }

    [DataContract]
    public class GroupNamedIdentifiers
    {
        #region Constructors
        public GroupNamedIdentifiers(string name)
        {
            this.Name = name;
            GroupIdentifiers = new Dictionary<string, BodyNamedIdentifiers>();
        }
        #endregion

        #region Public Properties
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember (Order = 2)]
        public IDictionary<string, BodyNamedIdentifiers> GroupIdentifiers { get; set; }
        #endregion

        #region Public Functions
        public void Add(string name)
        {
            if (!GroupIdentifiers.ContainsKey(name))
                GroupIdentifiers.Add(name, new BodyNamedIdentifiers(name));
        }

        public void Add(BodyNamedIdentifiers identifiers, bool addto = false)
        {
            Add(identifiers.Name, identifiers.Identifiers, addto);
        }

        public void Add(string name, IDictionary<string, string> identifiers, bool addto = false)
        {
            if (!GroupIdentifiers.ContainsKey(name))
            {
                BodyNamedIdentifiers obj = new BodyNamedIdentifiers(name);
                GroupIdentifiers.Add(name, obj);
                foreach (KeyValuePair<string, string> kvp in identifiers)
                    obj.Identifiers.Add(kvp);
            }
            else if (addto)
            {
                BodyNamedIdentifiers obj = GroupIdentifiers[name];
                if (obj != null)
                {
                    foreach (KeyValuePair<string, string> kvp in identifiers)
                    {
                        if (!obj.Identifiers.ContainsKey(kvp.Key))
                            obj.Identifiers.Add(kvp);
                    }
                }
            }
        }

        public void RemoveGroup(string name)
        {
            if (GroupIdentifiers.ContainsKey(name))
                GroupIdentifiers.Remove(name);
        }
        #endregion
    }
}
