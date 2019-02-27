using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract(Name = ProgressionConstants.BodyType.StarSystem)]
    public class StarSystem : ProgressionContainer
    {
        #region Constructors
        public StarSystem(string name) : base(name, ProgressionConstants.ContainerTypes.StarSystem)
        {
            ContainerType = ProgressionConstants.ContainerTypes.StarSystem;
        }
        #endregion

        #region Public Properties
        public IDictionary<string, string> Stars { get { return ContainerGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Stars].Identifiers; } }

        [DataMember(Order = 21)]
        public IList<Portal> Portals { get; set; }
        #endregion

        #region Get Methods
        public virtual ProgressionStar GetStar(string name) => (ProgressionStar)Get<Star>(name);

        public virtual IDictionary<string, Star> GetStars() => GetAll<Star>();
        #endregion

        #region Add Methods
        public void Add(ProgressionStar star) => Add<Star>(star);

        public void Add(Portal portal)
        {
            if (Portals == null)
                Portals = new List<Portal>();
            Portals.Add(portal);
        }
        #endregion

        #region Protected Methods
        protected override BodyNamedIdentifiers GetBodyNamedIdentifiers(string name, bool create)
        {
            BodyNamedIdentifiers identifiers = base.GetBodyNamedIdentifiers(name, create);

            if (identifiers == null && (name == Constants.BodyTypes.Star || name == ProgressionConstants.BodyType.ProgressionStar))
            {
                if (ContainerGroupIdentifiers.GroupIdentifiers.ContainsKey(Constants.NamedIdentifiers.Stars))
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Stars];
                else if (create)
                {
                    ContainerGroupIdentifiers.Add(Constants.NamedIdentifiers.Stars);
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Stars];
                }
            }

            return identifiers;
        }
        #endregion
    }
}
