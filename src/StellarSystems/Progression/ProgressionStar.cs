using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Bodies;

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
            StarGroupIdentifiers.Name = "GroupIdentifiers-ProgressionStar";
        }
        #endregion

        #region Public Properties
        [IgnoreDataMember]
        public IDictionary<string, string> Habitats { get { return StarGroupIdentifiers.GroupIdentifiers.Get(ProgressionConstants.NamedIdentifiers.Habitats); } }
        #endregion

        #region Get Methods
        public virtual Habitat GeHabitat(string name) => Get<Habitat>(name, StarGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);

        public virtual IDictionary<string, Habitat> GetHabitats() => GetAll<Habitat>(StarGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);

        #endregion

        #region Add Methods
        public void Add(Habitat habitat) => Add<Habitat>(habitat, StarGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);
        #endregion

        #region Public Methods
        #endregion

    }
}
