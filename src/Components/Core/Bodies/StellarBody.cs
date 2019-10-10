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
    public abstract class StellarBody : IStellarBody
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
        public IDictionary<string, string> BasicProperties { get { return Properties.GetProperties("Basic"); } }

        public IStellarMap Map { get; set; }
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
        public virtual T Get<T>(string name) where T : IStellarBody
        {
            T t = default(T);

            Type ty = typeof(T);
            BodyNamedIdentifiers identifiers = GetBodyNamedIdentifiers(ty.Name, false);

            if (identifiers != null)
            {
                if (identifiers.Identifiers.ContainsKey(name))
                {
                    string id = identifiers.Identifiers[name];
                    t = Map.Get<T>(id);
                }
            }

            return t;

        }

        public virtual IDictionary<string, T> GetAll<T>() where T : IStellarBody
        {
            Type ty = typeof(T);
            BodyNamedIdentifiers identifiers = GetBodyNamedIdentifiers(ty.Name, false);

            if (identifiers == null)
                return null;

            IDictionary<string, T> all = new Dictionary<string, T>();

            foreach(var kvp in identifiers.Identifiers)
            {
                T t = Map.Get<T>(kvp.Value);
                if (t != null)
                    all.Add(kvp.Key, t);
            }

            return all;
        }
        #endregion

        #region Add Methods
        public virtual void Add<T>(T t) where T : IStellarBody
        {
            if (t != null)
            {
                t.ParentIdentifier = this.Identifier;

                T found = Map.Get<T>(t.Identifier);
                if (found == null)
                    Map.Add<T>(t);

                Type ty = typeof(T);
                BodyNamedIdentifiers identifiers = GetBodyNamedIdentifiers(ty.Name, true);

                if (identifiers != null)
                    identifiers.Add(t.Name, t.Identifier);
            }
        }
        #endregion

        #region Protected Methods
        protected virtual BodyNamedIdentifiers GetBodyNamedIdentifiers(string name, bool create)
        {
            return null;
        }
        #endregion
    }
}
