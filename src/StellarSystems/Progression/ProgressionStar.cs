using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract(Name = ProgressionConstants.BodyType.ProgressionStar)]
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
        public IDictionary<string, string> Habitats { get { return StarGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Habitats].Identifiers; } }
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
            StarGroupIdentifiers.Name = "GroupIdentifiers-ProgressionStar";
        }

        protected override ObjectNamedIdentifiers GetObjectNamedIdentifiers(string name, bool create)
        {
            ObjectNamedIdentifiers identifiers = base.GetObjectNamedIdentifiers(name, create);

            if (identifiers == null)
            {
                switch (name)
                {
                    case ProgressionConstants.BodyType.Habitat:
                        if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionConstants.NamedIdentifiers.Habitats))
                            identifiers = StarGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Habitats];
                        else if (create)
                        {
                            StarGroupIdentifiers.Add(ProgressionConstants.NamedIdentifiers.Habitats);
                            identifiers = StarGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Habitats];
                        }
                        break;
                    case "ProgressionPlanet":
                        if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(Constants.NamedIdentifiers.Planets))
                            identifiers = StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Planets];
                        else if (create)
                        {
                            StarGroupIdentifiers.Add(Constants.NamedIdentifiers.Planets);
                            identifiers = StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Planets];
                        }
                        break;
                }
            }

            return identifiers;
        }
        #endregion
    }
}
