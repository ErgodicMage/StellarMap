﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Core.Bodies
{
    [DataContract (Name = Constants.BodyTypes.Planet)]
    public class Planet : StellarParentBody
    {
        #region Cosntructors
        public Planet()
        {            
        }
        
        public Planet(string name) : base(name, Constants.BodyTypes.Planet)
        {
            PlanetGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-Planet");
        }
        #endregion

        #region Public Properties
        [DataMember(Order = 11)]
        public GroupNamedIdentifiers PlanetGroupIdentifiers { get; set; }

        [IgnoreDataMember]
        public IDictionary<string, string> Satellites { get { return PlanetGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Satellites].Identifiers; } }
        #endregion

        #region Get Functions
        public virtual Satellite GetSatellite(string name) => Get<Satellite>(name, PlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);

        public virtual IDictionary<string, Satellite> GetSatellites() => GetAll<Satellite>(PlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
        #endregion

        #region Public Add Functions
        public void Add(Satellite satellite) => Add<Satellite>(satellite, PlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
        #endregion

    }
}
