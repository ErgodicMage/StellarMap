using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace StellarMap.Core.Types
{
    [DataContract (Name = "GroupProperties")]
    public class GroupProperties
    {
        #region Constructors
        public GroupProperties(string name)
        {
            Name = name;
            Properties = new Dictionary<string, string>();
        }
        #endregion

        #region Public Properties
        [DataMember (Order = 1)]
        public string Name { get; set; }

        [DataMember (Order = 2)]
        public IDictionary<string, string> Properties { get; set; }
        #endregion
    }
}
