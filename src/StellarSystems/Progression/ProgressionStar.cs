using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract(Name = "ProgressionStar")]
    public class ProgressionStar : Star
    {
        #region Consturctors
        public ProgressionStar()
        {

        }

        public ProgressionStar(string name) : base(name)
        {
        }
        #endregion

        #region Public Properties
        public IDictionary<string, string> Habitats { get { return StarGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Habitats].Identifiers; } }
        #endregion

        #region Get Methods
        public virtual Habitat GeHabitat(string name) => Get<Habitat>(name);

        public virtual IDictionary<string, Habitat> GetHabitats() => GetAll<Habitat>();

        #endregion

        #region Add Methods
        public void Add(Habitat habitat) => Add<Habitat>(habitat);
        #endregion

        #region Public Methods
        #endregion

        #region Protected Methods
        protected override void Initialize()
        {
            base.Initialize();
            StarGroupIdentifiers.Name = "GroupIdentifiers-ProgressionStarSystem";
        }

        protected override ObjectNamedIdentifiers GetObjectNamedIdentifiers(string name, bool create)
        {
            ObjectNamedIdentifiers identifiers = base.GetObjectNamedIdentifiers(name, create);

            if (identifiers == null)
            {
                if (name == "Habitat")
                {
                    if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionGroupNamedIdentifiers.Habitats))
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Habitats];
                    else if (create)
                    {
                        StarGroupIdentifiers.Add(ProgressionGroupNamedIdentifiers.Habitats);
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Habitats];
                    }
                }
                else if (name == "ProgressionPlanet")
                {
                    if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(GroupNamedIdentifiers.Planets))
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Planets];
                    else if (create)
                    {
                        StarGroupIdentifiers.Add(GroupNamedIdentifiers.Planets);
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Planets];
                    }
                }
            }

            return identifiers;
        }
        #endregion
    }
}
