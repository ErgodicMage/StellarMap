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
    [DataContract (Name = Constants.BodyTypes.Star)]
    public class Star : StellarParentBody
    {
        #region Constructors
        public Star()
        {            
        }
        
        public Star(string name) : base(name, Constants.BodyTypes.Star)
        {
            StarGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-Star");
        }
        #endregion

        #region Public Properties
        [DataMember (Order = 11)]
        public GroupNamedIdentifiers StarGroupIdentifiers { get; set; }

        [IgnoreDataMember]
        public IDictionary<string, string> Planets { get { return StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Planets].Identifiers; } }

        [IgnoreDataMember]
        public IDictionary<string, string> Asteroids { get { return StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Asteroids].Identifiers; } }

        [IgnoreDataMember]
        public IDictionary<string, string> Comets { get { return StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Comets].Identifiers; } }
        #endregion

        #region Get Functions
        public virtual Planet GetPlanet(string name) => Get<Planet>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.Planets);

        public virtual IDictionary<string, Planet> GetPlanets() => GetAll<Planet>(StarGroupIdentifiers, Constants.NamedIdentifiers.Planets);

        public virtual Asteroid GetAsteroid(string name) => Get<Asteroid>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.Asteroids);

        public virtual IDictionary<string, Asteroid> GetAsteroids() => GetAll<Asteroid>(StarGroupIdentifiers, Constants.NamedIdentifiers.Asteroids);

        public virtual Comet GetComet(string name) => Get<Comet>(name, StarGroupIdentifiers, Constants.NamedIdentifiers.Comets);

        public virtual IDictionary<string, Comet> GetComets() => GetAll<Comet>(StarGroupIdentifiers, Constants.NamedIdentifiers.Comets);
        #endregion

        #region Public Add Functions
        public void Add(Planet planet) => Add<Planet>(planet, StarGroupIdentifiers, Constants.NamedIdentifiers.Planets);

        public void Add(Asteroid asteroid) => Add<Asteroid>(asteroid, StarGroupIdentifiers, Constants.NamedIdentifiers.Asteroids);

        public void Add(Comet comet) => Add<Comet>(comet, StarGroupIdentifiers, Constants.NamedIdentifiers.Comets);
        #endregion

    }
}
