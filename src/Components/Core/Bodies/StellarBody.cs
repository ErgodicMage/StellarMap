using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using StellarMap.Core.Types;

namespace StellarMap.Core.Bodies
{
    [DataContract (Name = Constants.BodyTypes.StellarBody)]
    public abstract class StellarBody : IStellarBody, IEquatable<StellarBody>
    {
        #region Cosntructors
        public StellarBody()
        {            
        }
        
        public StellarBody(string name, string bodytype)
        {
            Properties = new GroupedProperties("Basic");

            if (Map == null)
                Map = BaseStellarMap.DefaultMap;

            Name = name;
            BodyType = bodytype;
        }
        #endregion

        #region Public Properties
        [DataMember (Order = 1)]
        public string Identifier { get; set; }

        [DataMember (Order = 2)]
        public string ParentIdentifier { get; set; }

        [DataMember (Order = 3)]
        public string Name { get; set; }

        [DataMember (Order = 4)]
        public string BodyType { get; set; }

        [DataMember (Order = 5)]
        public GroupedProperties Properties { get; set; }

        [IgnoreDataMember]
        public IDictionary<string, string> BasicProperties { get { return Properties.Get("Basic"); } }

        public IStellarMap Map { get; set; }
        #endregion

        #region IEquatable
        public bool Equals(StellarBody other)
        {
            bool bRet = true; 

            if (other == null)
                bRet = false;
            else if (!ReferenceEquals(this, other))
            {
                bRet = Name.Equals(other.Name) &&
                       Identifier.Equals(other.Identifier) &&
                       (ParentIdentifier == null ? true : ParentIdentifier.Equals(other.ParentIdentifier)) && 
                       BodyType.Equals(other.BodyType) &&
                       Properties.Equals(other.Properties);

            }

            return bRet;
        }

        public override bool Equals(object o) => Equals(o as StellarBody);

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            if (!string.IsNullOrEmpty(Name))
                hash = hash ^ Name.GetHashCode();
            if (!string.IsNullOrEmpty(Identifier))
                hash = hash ^ Identifier.GetHashCode();
            if (!string.IsNullOrEmpty(ParentIdentifier))
                hash = hash ^ ParentIdentifier.GetHashCode();
            if (!string.IsNullOrEmpty(BodyType))
                hash = hash ^ BodyType.GetHashCode();
            if (Properties != null)
                hash = hash ^ Properties.GetHashCode();

            return hash;
        }
        #endregion
    }

    public abstract class StellarParentBody : StellarBody, IStellarParentBody
    {
        #region Constructors
        public StellarParentBody()
        {

        }

        public StellarParentBody(string name, string bodytype) : base(name, bodytype)
        {
        }
        #endregion

        #region Get Methods
        public virtual T Get<T>(string name, GroupNamedIdentifiers groupIdentifiers, string groupName) where T : IStellarBody
        {
            T t = default(T);

            Type ty = typeof(T);
            Dictionary<string, string> identifiers = null;
            string id = string.Empty;

            if (groupIdentifiers.GroupIdentifiers.TryGetValue(groupName, out identifiers) && identifiers.TryGetValue(name, out id))
                t = Map.Get<T>(id);

            return t;

        }

        public virtual IDictionary<string, T> GetAll<T>(GroupNamedIdentifiers groupIdentifiers, string groupName) where T : IStellarBody
        {
            Type ty = typeof(T);
            Dictionary<string, string> identifiers = null;
            IDictionary<string, T> all = null;

            if (groupIdentifiers.GroupIdentifiers.TryGetValue(groupName, out identifiers))
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

                Type ty = typeof(T);
                Dictionary<string, string> identifiers = null;

                if (groupIdentifiers.GroupIdentifiers.TryGetValue(groupName, out identifiers))
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
}
