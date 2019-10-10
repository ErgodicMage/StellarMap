using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract (Name = ProgressionConstants.BodyType.Cluster)]
    public class Cluster : ProgressionContainer
    {
        #region Constructors
        public Cluster()
        {    
        }
        
        public Cluster(string name) : base(name, ProgressionConstants.ContainerTypes.Cluster)
        {
            ContainerType = ProgressionConstants.ContainerTypes.Cluster;
        }
        #endregion

        #region Public Properties
        [IgnoreDataMember]
        public IDictionary<string, string> StarSystems { get { return ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.StarSystems].Identifiers; } }
        #endregion

        #region Get Methods
        public virtual StarSystem GetStarSystem(string name) => Get<StarSystem>(name);

        public virtual IDictionary<string, StarSystem> GetStarSystems() => GetAll<StarSystem>();
        #endregion

        #region Add Methods
        public void Add(StarSystem system) => Add<StarSystem>(system);
        #endregion

        #region Protected Methods
        protected override BodyNamedIdentifiers GetBodyNamedIdentifiers(string name, bool create)
        {
            BodyNamedIdentifiers identifiers = base.GetBodyNamedIdentifiers(name, create);

            if (identifiers == null)
            {
                if (name == ProgressionConstants.BodyType.StarSystem)
                {
                    if (ContainerGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionConstants.NamedIdentifiers.StarSystems))
                        identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.StarSystems];
                    else if (create)
                    {
                        ContainerGroupIdentifiers.Add(ProgressionConstants.NamedIdentifiers.StarSystems);
                        identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.StarSystems];
                    }
                }
            }

            return identifiers;
        }
        #endregion
    }
}
