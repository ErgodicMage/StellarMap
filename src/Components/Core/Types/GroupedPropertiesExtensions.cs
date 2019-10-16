using System;
using System.Collections.Generic;
using System.Text;

namespace StellarMap.Core.Types
{
    public static class GroupedPropertiesExtensions
    {
        public static void AddGroup(this GroupedProperties groupedProperties, string group, IEnumerable<KeyValuePair<string, string>> properties)
        {
            if (!groupedProperties.PropertyGroups.ContainsKey(group))
            {
                IDictionary<string, string> newProperties = new Dictionary<string, string>();
                foreach (var prop in properties)
                    newProperties.Add(prop);

                groupedProperties.PropertyGroups.Add(group, newProperties);
            }
        }

        public static void AddProperties(this GroupedProperties groupedProperties, string group, IEnumerable<KeyValuePair<string, string>> properties)
        {
            if (groupedProperties.PropertyGroups.ContainsKey(group))
            {
                IDictionary<string, string> currentProperties = groupedProperties.PropertyGroups[group];
                foreach (var prop in properties)
                {
                    if (!currentProperties.ContainsKey(prop.Key))
                        currentProperties.Add(prop);
                }
            }
        }
    }
}
