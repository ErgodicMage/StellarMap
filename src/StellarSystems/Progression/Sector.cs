using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    public class Sector : ProgressionContainer
    {
        #region Constructors
        public Sector()
        {
        }

        public Sector(string name) : base(name)
        {
        }
        #endregion

        #region Public Properties
        public IDictionary<string, string> Clusters { get { return ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Clusters].Identifiers; } }
        #endregion

        #region Get Methods
        public virtual Cluster GetCluster(string name) => Get<Cluster>(name);

        public virtual IDictionary<string, Cluster> GetClusters() => GetAll<Cluster>();
        #endregion

        #region Add Methods
        public void Add(Cluster cluster) => Add<Cluster>(cluster);
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

            if (identifiers == null && name == "Sector")
            {
                if (ContainerGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionGroupNamedIdentifiers.Sectors))
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Sectors];
                else if (create)
                {
                    ContainerGroupIdentifiers.Add(ProgressionGroupNamedIdentifiers.Sectors);
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionGroupNamedIdentifiers.Sectors];
                }
            }

            return identifiers;
        }
        #endregion
    }
}
