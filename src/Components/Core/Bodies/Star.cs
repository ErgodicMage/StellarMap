using System;
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
    public class Star : StellarBodywithObjects
    {
        #region Constructors
        public Star() : base()
        {
        }

        public Star(string name) : base(name, Constants.BodyTypes.Star)
        {
            //Initialize();
        }
        #endregion

        #region Public Properties
        [DataMember (Order = 11)]
        public GroupNamedIdentifiers StarGroupIdentifiers { get; set; }

        public IDictionary<string, string> Planets { get { return StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Planets].Identifiers; } }

        public IDictionary<string, string> Asteroids { get { return StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Asteroids].Identifiers; } }

        public IDictionary<string, string> Comets { get { return StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Comets].Identifiers; } }
        #endregion

        #region Get Functions
        public virtual Planet GetPlanet(string name) => Get<Planet>(name);

        public virtual IDictionary<string, Planet> GetPlanets() => GetAll<Planet>();

        public virtual Asteroid GetAsteroid(string name) => Get<Asteroid>(name);

        public virtual IDictionary<string, Asteroid> GetAsteroids() => GetAll<Asteroid>();

        public virtual Comet GetComet(string name) => Get<Comet>(name);

        public virtual IDictionary<string, Comet> GetComets() => GetAll<Comet>();
        #endregion

        #region Public Add Functions
        public void Add(Planet planet) => Add<Planet>(planet);

        public void Add(Asteroid asteroid) => Add<Asteroid>(asteroid);

        public void Add(Comet comet) => Add<Comet>(comet);
        #endregion

        #region Protected Functions
        protected override void Initialize()
        {
            base.Initialize();
            StarGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-Star");
            //StarGroupIdentifiers.Add(GroupNamedIdentifiers.Planets);
            //StarGroupIdentifiers.Add(GroupNamedIdentifiers.Asteroids);
            //StarGroupIdentifiers.Add(GroupNamedIdentifiers.Comets);
        }

        protected override ObjectNamedIdentifiers GetObjectNamedIdentifiers(string name, bool create)
        {
            ObjectNamedIdentifiers identifiers = null;

            switch (name)
            {
                case Constants.BodyTypes.Planet:
                    if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(Constants.NamedIdentifiers.Planets))
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Planets];
                    else if (create)
                    {
                        StarGroupIdentifiers.Add(Constants.NamedIdentifiers.Planets);
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Planets];
                    }
                    break;
                case Constants.BodyTypes.Asteroid:
                    if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(Constants.NamedIdentifiers.Asteroids))
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Asteroids];
                    else if (create)
                    {
                        StarGroupIdentifiers.Add(Constants.NamedIdentifiers.Asteroids);
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Asteroids];
                    }
                    break;
                case Constants.BodyTypes.Comet:
                    if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(Constants.NamedIdentifiers.Comets))
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Comets];
                    else if (create)
                    {
                        StarGroupIdentifiers.Add(Constants.NamedIdentifiers.Comets);
                        identifiers = StarGroupIdentifiers.GroupIdentifiers[Constants.NamedIdentifiers.Comets];
                    }
                    break;
            }
            return identifiers;
        }

        #endregion
    }
}
