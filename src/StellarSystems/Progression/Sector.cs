﻿using System;
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
        }
        #endregion

        #region Public Properties
        public IDictionary<string, string> Clusters { get { return ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Clusters].Identifiers; } }
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
            ContainerType = ProgressionConstants.ContainerTypes.Sector;
        }

        protected override ObjectNamedIdentifiers GetObjectNamedIdentifiers(string name, bool create)
        {
            ObjectNamedIdentifiers identifiers = base.GetObjectNamedIdentifiers(name, create);

            if (identifiers == null && name == ProgressionConstants.BodyType.Cluster)
            {
                if (ContainerGroupIdentifiers.GroupIdentifiers.ContainsKey(ProgressionConstants.NamedIdentifiers.Clusters))
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Clusters];
                else if (create)
                {
                    ContainerGroupIdentifiers.Add(ProgressionConstants.NamedIdentifiers.Clusters);
                    identifiers = ContainerGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Clusters];
                }
            }

            return identifiers;
        }
        #endregion
    }
}
