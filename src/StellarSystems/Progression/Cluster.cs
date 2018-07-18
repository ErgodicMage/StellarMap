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
        public IDictionary<string, string> StarSystems { get { return ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.StarSystem].Identifiers; } }
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
            ContainerType = ContainerTypes.Cluster;
        }

        protected override ObjectNamedIdentifiers GetObjectNamedIdentifiers(string name, bool create)
        {
            ObjectNamedIdentifiers identifiers = base.GetObjectNamedIdentifiers(name, create);

            if (identifiers == null)
            {
                if (name == "StarSystem")
                {
                    if (ContainerGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionGroupNamedIdentifiers.StarSystem))
                        identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.StarSystem];
                    else if (create)
                    {
                        ContainerGroupIdentifiers.Add(ProgressionGroupNamedIdentifiers.StarSystem);
                        identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.StarSystem];
                    }
                }
                else if (name == "Cluster")
                {
                    if (ContainerGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionGroupNamedIdentifiers.Clusters))
                        identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Clusters];
                    else if (create)
                    {
                        ContainerGroupIdentifiers.Add(ProgressionGroupNamedIdentifiers.Clusters);
                        identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Clusters];
                    }
                }
            }

            return identifiers;
        }
        #endregion
    }
}
