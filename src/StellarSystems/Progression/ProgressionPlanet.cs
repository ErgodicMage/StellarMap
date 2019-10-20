﻿using System;
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
            PlanetGroupIdentifiers.Name = "GroupIdentifiers-ProgressionPlanet";
        }
        #endregion

        #region Public Properties
        [IgnoreDataMember]
        public IDictionary<string, string> Habitats { get { return PlanetGroupIdentifiers.GroupIdentifiers[ProgressionConstants.NamedIdentifiers.Habitats].Identifiers; } }
        #endregion

        #region Get Methods
        public virtual Habitat GeHabitat(string name) => Get<Habitat>(name, PlanetGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);

        public virtual IDictionary<string, Habitat> GetHabitats() => GetAll<Habitat>(PlanetGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);
        #endregion

        #region Add Methods
        public void Add(Habitat habitat) => Add<Habitat>(habitat, PlanetGroupIdentifiers, ProgressionConstants.NamedIdentifiers.Habitats);
        #endregion

    }
}
