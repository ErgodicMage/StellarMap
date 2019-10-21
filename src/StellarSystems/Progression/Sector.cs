using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract (Name = ProgressionConstants.BodyType.Sector)]
    public class Sector : ProgressionContainer
    {
        #region Constructors
        public Sector()
        {            
        }
        
        public Sector(string name) : base(name, ProgressionConstants.ContainerTypes.Sector)
        {
            ContainerType = ProgressionConstants.ContainerTypes.Sector;
        }
        #endregion

        #region Public Properties
        [IgnoreDataMember]
        public IDictionary<string, string> Clusters { get { return ContainerGroupIdentifiers.GroupIdentifiers.Get(ProgressionConstants.NamedIdentifiers.Clusters); } }
        #endregion

        #region Get Methods
        public virtual Cluster GetCluster(string name) => Get<Cluster>(name, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Clusters);

        public virtual IDictionary<string, Cluster> GetClusters() => GetAll<Cluster>(ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Clusters);
        #endregion

        #region Add Methods
        public void Add(Cluster cluster) => Add<Cluster>(cluster, ContainerGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Clusters);
        #endregion

    }
}
