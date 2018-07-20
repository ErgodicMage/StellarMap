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
    public abstract class StellarBody
    {
        #region Cosntructors
        public StellarBody()
        {
        }

        public StellarBody(string name, string bodytype)
        {
            Initialize();
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
        public IDictionary<string, GroupProperties> AllGroupProperties { get; set; }

        public IDictionary<string, string> BasicProperties { get { return AllGroupProperties["Basic"].Properties; } }

        public IStellarMap Map { get; set; }
        #endregion

        #region Protected Functions
        protected virtual void Initialize()
        {
            //if (!string.IsNullOrEmpty(Identifier))
            //    Identifier = Guid.NewGuid().ToString();
            if (AllGroupProperties == null)
                AllGroupProperties = new Dictionary<string, GroupProperties>();

            if (!AllGroupProperties.ContainsKey("Basic"))
            {
                GroupProperties basic = new GroupProperties("Basic");
                AllGroupProperties.Add("Basic", basic);
            }

            if (Map == null)
                Map = BaseStellarMap.DefaultMap;
        }
        #endregion
    }

    public abstract class StellarBodywithObjects : StellarBody
    {
        #region Constructors
        public StellarBodywithObjects() : base()
        {
        }

        public StellarBodywithObjects(string name, string bodytype) : base(name, bodytype)
        {
        }
        #endregion

        #region Get Methods
        public virtual T Get<T>(string name) where T : StellarBody
        {
            T t = null;

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

        public virtual IDictionary<string, T> GetAll<T>() where T : StellarBody
        {
            Type ty = typeof(T);
            BodyNamedIdentifiers identifiers = GetBodyNamedIdentifiers(ty.Name, false);

            if (identifiers == null)
                return null;

            IDictionary<string, T> all = new Dictionary<string, T>();

            foreach(KeyValuePair<string, string> kvp in identifiers.Identifiers)
            {
                T t = Map.Get<T>(kvp.Value);
                if (t != null)
                    all.Add(kvp.Key, t);
            }

            return all;
        }
        #endregion

        #region Add Methods
        public virtual void Add<T>(T t) where T : StellarBody
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
