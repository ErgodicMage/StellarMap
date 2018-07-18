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
    [DataContract (Name = "Star")]
    public class Star : StellarBodywithObjects
    {
        #region Properties Constants
        public static string Designation = "Designation";
        public static string ProperName = "ProperName";
        public static string StellarClass = "StellarClass";
        #endregion

        #region Constructors
        public Star() : base()
        {
        }

        public Star(string name) : base(name)
        {
            //Initialize();
        }
        #endregion

        #region Public Properties
        [DataMember (Order = 11)]
        public GroupNamedIdentifiers StarGroupIdentifiers { get; set; }

        public IDictionary<string, string> Planets { get { return StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Planets].Identifiers; } }

        public IDictionary<string, string> Asteroids { get { return StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Asteroids].Identifiers; } }

        public IDictionary<string, string> Comets { get { return StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Comets].Identifiers; } }
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

            if (name == "Planet")
            {
                if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(GroupNamedIdentifiers.Planets))
                    identifiers = StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Planets];
                else if (create)
                {
                    StarGroupIdentifiers.Add(GroupNamedIdentifiers.Planets);
                    identifiers = StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Planets];
                }
            }
            else if (name == "Asteroid")
            {
                if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(GroupNamedIdentifiers.Asteroids))
                    identifiers = StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Asteroids];
                else if (create)
                {
                    StarGroupIdentifiers.Add(GroupNamedIdentifiers.Asteroids);
                    identifiers = StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Asteroids];
                }
            }
            else if (name == "Comet")
            {
                if (StarGroupIdentifiers.GroupIdentifiers.ContainsKey(GroupNamedIdentifiers.Comets))
                    identifiers = StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Comets];
                else if (create)
                {
                    StarGroupIdentifiers.Add(GroupNamedIdentifiers.Comets);
                    identifiers = StarGroupIdentifiers.GroupIdentifiers[GroupNamedIdentifiers.Comets];
                }
            }

            return identifiers;
        }

        #endregion
    }
}
