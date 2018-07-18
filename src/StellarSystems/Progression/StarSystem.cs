using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    public class StarSystem : ProgressionContainer
    {
        #region Constructors
        public StarSystem()
        {
        }

        public StarSystem(string name) : base(name)
        {
        }
        #endregion

        #region Public Properties
        public IDictionary<string, string> Stars { get { return ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Stars].Identifiers; } }

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
        protected override void Initialize()
        {
            base.Initialize();
            ContainerType = ContainerTypes.StarSystem;
        }

        protected override ObjectNamedIdentifiers GetObjectNamedIdentifiers(string name, bool create)
        {
            ObjectNamedIdentifiers identifiers = base.GetObjectNamedIdentifiers(name, create);

            if (identifiers == null && name == "ProgressionStar")
            {
                if (ContainerGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionGroupNamedIdentifiers.Stars))
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Stars];
                else if (create)
                {
                    ContainerGroupIdentifiers.Add(ProgressionGroupNamedIdentifiers.Stars);
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Stars];
                }
            }

            return identifiers;
        }
        #endregion
    }
}
