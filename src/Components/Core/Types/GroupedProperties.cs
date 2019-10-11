using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace StellarMap.Core.Types
{
    [DataContract (Name = "GroupedProperties")]
    public class GroupedProperties
    {
        #region Constructors
        public GroupedProperties()
        {
            AllProperties = new Dictionary<string, IDictionary<string, string>>();
        }

        public GroupedProperties(string initialGroup)
        {
            AllProperties = new Dictionary<string, IDictionary<string, string>>();
            AllProperties.Add(initialGroup, new Dictionary<string, string>());
        }
        #endregion

        [DataMember (Order = 1)]
        public IDictionary<string, IDictionary<string, string>> AllProperties { get; set;}

        #region Indexers
        public IDictionary<string, string> this[string group] => AllProperties[group];

        public string this[string group, string property] => AllProperties[group][property];
        #endregion

        #region Add Methods
        public void AddGroup(string group)
        {
            if (!AllProperties.ContainsKey(group))
                AllProperties.Add(group, new Dictionary<string, string>());
        }

        public void AddGroup(string group, IDictionary<string, string> properties)
        {
            if (!AllProperties.ContainsKey(group))
                AllProperties.Add(group, properties);
        }

        public void AddProperty(string group, string property, string value)
        {
            IDictionary<string, string> properties;

            if (!AllProperties.TryGetValue(group, out properties))
            {
                properties = new Dictionary<string, string>();
                AllProperties.Add(group, properties);
            }
            properties.Add(property, value);
        }

        public void AddProperties(string group, IDictionary<string, string> properties)
        {
            if (!AllProperties.ContainsKey(group))
                AllProperties.Add(group, properties);
            else
            {
                var currentProperties = AllProperties[group];
                foreach (var kvp in properties)
                {
                    if (!currentProperties.ContainsKey(kvp.Key))
                        currentProperties.Add(kvp.Key, kvp.Value);
                }
            }
        }
        #endregion

        #region Remove Methods
        public void RemoveGroup(string group)
        {
            if (AllProperties.ContainsKey(group))
                AllProperties.Remove(group);
        }

        public void RemoveProperty(string group, string property)
        {
            if (AllProperties.ContainsKey(group) && AllProperties[group].ContainsKey(property))
                AllProperties[group].Remove(property);
        }
        #endregion

        #region Get Methods
        public IDictionary<string, string> GetProperties(string group)
        {
            IDictionary<string, string> properties;
            if (!AllProperties.TryGetValue(group, out properties))
                properties = new Dictionary<string, string>();
            return properties;
        }

        public string GetPropertyValue(string group, string property)
        {
            string propValue = string.Empty;
            if (AllProperties.ContainsKey(group) && AllProperties[group].ContainsKey(property))
                propValue = AllProperties[group][property];
            return propValue;
        }
        #endregion

        #region Set Methods
        public void SetGroupProperties(string group, IDictionary<string, string> properties)
        {
            if (AllProperties.ContainsKey(group))
                AllProperties[group] = properties;
        }

        public void SetProperty(string group, string property, string value)
        {
            if (AllProperties.ContainsKey(group) && AllProperties[group].ContainsKey(property))
                AllProperties[group][property] = value;
        }

        #endregion
    }
}
