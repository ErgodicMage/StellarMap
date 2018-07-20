using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract(Name = ProgressionConstants.BodyType.ProgressionPlanet)]
    public class ProgressionPlanet : Planet
    {
        #region Constructors
        public ProgressionPlanet()
        {
        }

        public ProgressionPlanet(string name) : base(name)
        {
        }
        #endregion

        #region Public Properties
        public IDictionary<string, string> Habitats { get { return PlanetGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Habitats].Identifiers; } }
        #endregion

        #region Get Methods
        public virtual Habitat GeHabitat(string name) => Get<Habitat>(name);

        public virtual IDictionary<string, Habitat> GetHabitats() => GetAll<Habitat>();
        #endregion

        #region Add Methods
        public void Add(Habitat habitat) => Add<Habitat>(habitat);
        #endregion

        #region Protected Methods
        protected override void Initialize()
        {
            base.Initialize();
            PlanetGroupIdentifiers.Name = "GroupIdentifiers-ProgressionPlanet";
        }

        protected override BodyNamedIdentifiers GetBodyNamedIdentifiers(string name, bool create)
        {
            BodyNamedIdentifiers identifiers = base.GetBodyNamedIdentifiers(name, create);

            if (identifiers == null && name == ProgressionConstants.BodyType.Habitat)
            {
                if (PlanetGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionConstants.NamedIdentifiers.Habitats))
                    identifiers = PlanetGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Habitats];
                else if (create)
                {
                    PlanetGroupIdentifiers.Add(ProgressionConstants.NamedIdentifiers.Habitats);
                    identifiers = PlanetGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Habitats];
                }
            }

            return identifiers;
        }
        #endregion
    }
}
