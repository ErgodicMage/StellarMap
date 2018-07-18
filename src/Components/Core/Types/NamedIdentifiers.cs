using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace StellarMap.Core.Types
{
    [DataContract]
    public class ObjectNamedIdentifiers
    {
        #region Constructors
        public ObjectNamedIdentifiers()
        {

        }

        public ObjectNamedIdentifiers(string name)
        {
            this.Name = name;
            Initialize();
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

        #region Protected Functions
        protected virtual void Initialize()
        {
            Identifiers = new Dictionary<string, string>();
        }
        #endregion
    }

    [DataContract]
    public class GroupNamedIdentifiers
    {
        #region Default Group Names
        public static string Planets = "Planets";
        public static string Stars = "Stars";
        public static string Satellites = "Satellites";
        public static string Asteroids = "Asteroids";
        public static string Comets = "Comets";
        #endregion

        #region Constructors
        public GroupNamedIdentifiers()
        {
        }

        public GroupNamedIdentifiers(string name)
        {
            this.Name = name;
            Initialize();
        }
        #endregion

        #region Public Properties
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember (Order = 2)]
        public IDictionary<string, ObjectNamedIdentifiers> GroupIdentifiers { get; set; }
        #endregion

        #region Public Functions
        public void Add(string name)
        {
            if (!GroupIdentifiers.ContainsKey(name))
                GroupIdentifiers.Add(name, new ObjectNamedIdentifiers(name));
        }

        public void Add(ObjectNamedIdentifiers identifiers, bool addto = false)
        {
            Add(identifiers.Name, identifiers.Identifiers, addto);
        }

        public void Add(string name, IDictionary<string, string> identifiers, bool addto = false)
        {
            if (!GroupIdentifiers.ContainsKey(name))
            {
                ObjectNamedIdentifiers obj = new ObjectNamedIdentifiers(name);
                GroupIdentifiers.Add(name, obj);
                foreach (KeyValuePair<string, string> kvp in identifiers)
                    obj.Identifiers.Add(kvp);
            }
            else if (addto)
            {
                ObjectNamedIdentifiers obj = GroupIdentifiers[name];
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

        #region Protected Functions
        protected virtual void Initialize()
        {
            GroupIdentifiers = new Dictionary<string, ObjectNamedIdentifiers>();
        }
        #endregion
    }
}
