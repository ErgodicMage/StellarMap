using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace StellarMap.Core.Types
{
    [DataContract (Name = "GroupedProperties")]
    public sealed class GroupedProperties
    {
        #region Constructor
        public GroupedProperties()
        {
            PropertyGroups = new Dictionary<string, IDictionary<string, string>>();
        }

        public GroupedProperties(string initialGroup)
        {
            PropertyGroups = new Dictionary<string, IDictionary<string, string>>();
            PropertyGroups.Add(initialGroup, new Dictionary<string, string>());
        }
        #endregion

        #region Properties
        [DataMember(Order = 1)]
        public IDictionary<string, IDictionary<string, string>> PropertyGroups { get; set; }
        #endregion

        #region Indexer
        public IDictionary<string, string> this[string group] => PropertyGroups[group];

        public string this[string group, string property] => PropertyGroups[group][property];
        #endregion

        #region Add Methods
        public void AddGroup(string group)
        {
            if (!PropertyGroups.ContainsKey(group))
                PropertyGroups.Add(group, new Dictionary<string, string>());
        }

        public void AddProperty(string group, string property, string value)
        {
            if (PropertyGroups.ContainsKey(group))
            {
                var properties = PropertyGroups[group];
                if (!properties.ContainsKey(property))
                    properties.Add(property, value);
            }
        }
        #endregion

        #region Remove Methods
        public void RemoveGroup(string group)
        {
            if (PropertyGroups.ContainsKey(group))
                PropertyGroups.Remove(group);
        }

        public void RemoveProperty(string group, string property)
        {
            if (PropertyGroups.ContainsKey(group) && PropertyGroups[group].ContainsKey(property))
                PropertyGroups[group].Remove(property);
        }
        #endregion

        #region Get and Set Methods
        public IDictionary<string, string> Get(string group)
        {
            IDictionary<string, string> properties;
            if (!PropertyGroups.TryGetValue(group, out properties))
                properties = new Dictionary<string, string>();
            return properties;
        }

        public string Get(string group, string property)
        {
            string retValue = string.Empty;

            if (PropertyGroups.ContainsKey(group) && PropertyGroups[group].ContainsKey(property))
                retValue = PropertyGroups[group][property];

            return retValue;
        }

        public void Set(string group, string property, string value)
        {
            if (PropertyGroups.ContainsKey(group) && PropertyGroups[group].ContainsKey(property))
                PropertyGroups[group][property] = value;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            bool isFirst = true;

            foreach (string group in PropertyGroups.Keys)
            {
                if (isFirst)
                    isFirst = false;
                else
                    sb.AppendLine();

                sb.Append(group);
                sb.Append(":");

                var properties = PropertyGroups[group];
                foreach (var prop in properties)
                {
                    sb.AppendLine();
                    sb.Append('\t');
                    sb.Append(prop.Key);
                    sb.Append(" : ");
                    sb.Append(prop.Value);
                }
            }

            return sb.ToString();
        }
        #endregion
    }
}
