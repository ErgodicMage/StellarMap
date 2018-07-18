using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    public class Cluster : ProgressionContainer
    {
        #region Constructors
        public Cluster()
        {
        }

        public Cluster(string name) : base(name)
        {
        }
        #endregion

        #region Public Properties
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
        protected override void Initialize()
        {
            base.Initialize();
            ContainerType = ProgressionConstants.ContainerTypes.Cluster;
        }

        protected override ObjectNamedIdentifiers GetObjectNamedIdentifiers(string name, bool create)
        {
            ObjectNamedIdentifiers identifiers = base.GetObjectNamedIdentifiers(name, create);

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
