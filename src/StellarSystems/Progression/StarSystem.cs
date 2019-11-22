using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Progression
{
    [DataContract(Name = ProgressionConstants.BodyType.StarSystem)]
    public class StarSystem : ProgressionContainer, IEquatable<StarSystem>
    {
        #region Constructors
        public StarSystem()
        {            
        }
        
        public StarSystem(string name) : base(name, ProgressionConstants.ContainerTypes.StarSystem)
        {
            ContainerType = ProgressionConstants.ContainerTypes.StarSystem;
        }
        #endregion

        #region Public Properties
        [IgnoreDataMember]
        public IDictionary<string, string> Stars { get { return ContainerGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Stars); } }

        [DataMember(Order = 21)]
        public IList<Portal> Portals { get; set; }
        #endregion

        #region Get Methods
        public virtual ProgressionStar GetStar(string name) => (ProgressionStar)Get<Star>(name, ContainerGroupIdentifiers, Constants.NamedIdentifiers.Stars);

        public virtual IDictionary<string, Star> GetStars() => GetAll<Star>(ContainerGroupIdentifiers, Constants.NamedIdentifiers.Stars);
        #endregion

        #region Add Methods
        public void Add(ProgressionStar star) => Add<Star>(star, ContainerGroupIdentifiers, Constants.NamedIdentifiers.Stars);

        public void Add(Portal portal)
        {
            if (Portals == null)
                Portals = new List<Portal>();
            Portals.Add(portal);
        }
        #endregion

        #region IEquatable
        public bool Equals(StarSystem other)
        {
            bool bRet = true;

            if (other == null)
                bRet = false;

            else if (!ReferenceEquals(this, other))
            {
                if (!base.Equals(other))
                    bRet = false;
                else if (this.Portals == null && other.Portals == null)
                    bRet = true;
                else if (this.Portals == null || other.Portals == null)
                    bRet = false;
                else if (this.Portals.Count == other.Portals.Count)
                {
                    var thisPortals = Portals.GetEnumerator();
                    var otherPortals = other.Portals.GetEnumerator();

                    while (thisPortals.MoveNext() && otherPortals.MoveNext())
                    {
                        if (!thisPortals.Current.Equals(otherPortals.Current))
                        {
                            bRet = false;
                            break;
                        }
                    }
                }
                else
                    bRet = false;
            }

            return bRet;
        }

        public override bool Equals(object o) => Equals(o as StarSystem);

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            if (Portals != null)
            {
                foreach (Portal p in Portals)
                    hash = hash ^ p.GetHashCode();
            }

            return hash;
        }
        #endregion
    }
}
