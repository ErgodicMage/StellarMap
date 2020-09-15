using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Progression
{
    [DataContract(Name = ProgressionConstants.BodyType.StarSystem)]
    public class StarSystem : ProgressionContainer, IEqualityComparer<StarSystem>
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
        public IDictionary<string, string> Stars 
            { get => ContainerGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Stars); }

        [DataMember(Order = 21)]
        public IList<Portal> Portals { get; set; }
        #endregion

        #region Get Methods
        public virtual ProgressionStar GetStar(string name) => 
            (ProgressionStar)Get<Star>(name, ContainerGroupIdentifiers, Constants.NamedIdentifiers.Stars);

        public virtual IDictionary<string, Star> GetStars() => 
            GetAll<Star>(ContainerGroupIdentifiers, Constants.NamedIdentifiers.Stars);
        #endregion

        #region Add Methods
        public void Add(ProgressionStar star) => 
            Add<Star>(star, ContainerGroupIdentifiers, Constants.NamedIdentifiers.Stars);

        public void Add(Portal portal)
        {
            if (Portals == null)
                Portals = new List<Portal>();
            Portals.Add(portal);
        }
        #endregion

        #region IEqualityComparer
        public bool Equals(StarSystem x, StarSystem y) => StarSystemEqualityComparer.Comparer.Equals(x, y);

        public override bool Equals(object obj) => StarSystemEqualityComparer.Comparer.Equals(this, obj as StarSystem);

        public int GetHashCode(StarSystem obj) => StarSystemEqualityComparer.Comparer.GetHashCode(obj);

        public override int GetHashCode() => StarSystemEqualityComparer.Comparer.GetHashCode(this);
        #endregion
    }

    public sealed class StarSystemEqualityComparer : IEqualityComparer<StarSystem>
    {
        public bool Equals(StarSystem x, StarSystem y)
        {
            bool bRet = ProgressionContainerEqualityComparer.Comparer.Equals(x, y);

            if (bRet)
            {
                if (x.Portals == null && y.Portals == null)
                    bRet = true;
                else if (x.Portals == null || y.Portals == null)
                    bRet = false;
                else if (x.Portals.Count == y.Portals.Count)
                {
                    var thisPortals = x.Portals.GetEnumerator();
                    var otherPortals = y.Portals.GetEnumerator();

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

        public int GetHashCode(StarSystem obj)
        {
            int hash = ProgressionContainerEqualityComparer.Comparer.GetHashCode();
            if (obj.Portals != null)
            {
                foreach (Portal p in obj.Portals)
                    hash ^= p.GetHashCode();
            }

            return hash;
        }

        public static StarSystemEqualityComparer Comparer { get; } = new StarSystemEqualityComparer();
    }
}
