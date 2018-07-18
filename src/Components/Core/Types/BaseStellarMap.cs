using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;

namespace StellarMap.Core.Types
{
    [DataContract(Name = "BaseStellarMap")]
    public class BaseStellarMap : IStellarMap
    {
        #region Constuctors
        public BaseStellarMap()
        {
        }

        public BaseStellarMap(string name)
        {
            this.Name = name;
        }
        #endregion

        #region Public Properties
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public IDictionary<string, Star> Stars { get; set; }

        [DataMember(Order = 3)]
        public IDictionary<string, Planet> Planets { get; set; }

        [DataMember(Order = 4)]
        public IDictionary<string, Satellite> Satellites { get; set; }

        [DataMember(Order = 5)]
        public IDictionary<string, Asteroid> Asteroids { get; set; }

        [DataMember(Order = 6)]
        public IDictionary<string, Comet> Comets { get; set; }
        #endregion

        #region Static
        public static IStellarMap DefaultMap { get; } = new BaseStellarMap("Default");
        #endregion

        #region Public Get Functions
        public virtual T Get<T>(string id) where T : StellarBody
        {
            T t = null;

            if (!string.IsNullOrEmpty(id))
            {
                IDictionary<string, T> dict = GetDictionary<T>(false);

                if (dict != null && dict.ContainsKey(id))
                    t = dict[id];
            }

            return t;
        }

        public virtual void Get<T>(ICollection<string> identifiers, IDictionary<string, T> output) where T : StellarBody
        {
            foreach (string id in identifiers)
            {
                T t = Get<T>(id);
                if (t != null)
                    output.Add(id, t);
            }
        }

        public virtual Star GetStar(string id) => Get<Star>(id);

        public virtual IDictionary<string, Star> GetStars(ICollection<string> identifiers)
        {
            IDictionary<string, Star> stars = new Dictionary<string, Star>();

            Get<Star>(identifiers, stars);

            return stars;
        }

        public virtual Planet GetPlanet(string id) => Get<Planet>(id);

        public virtual IDictionary<string, Planet> GetPlanets(ICollection<string> identifiers)
        {
            IDictionary<string, Planet> planets = new Dictionary<string, Planet>();

            Get<Planet>(identifiers, planets);

            return planets;
        }

        public virtual Satellite GetSatellite(string id) => Get<Satellite>(id);

        public virtual IDictionary<string, Satellite> GetSatellites(ICollection<string> identifiers)
        {
            IDictionary<string, Satellite> satellites = new Dictionary<string, Satellite>();

            Get<Satellite>(identifiers, satellites);

            return satellites;
        }

        public virtual Asteroid GetAsteroid(string id) => Get<Asteroid>(id);

        public virtual IDictionary<string, Asteroid> GetAsteroids(ICollection<string> identifiers)
        {
            IDictionary<string, Asteroid> asteroids = new Dictionary<string, Asteroid>();

            Get<Asteroid>(identifiers, asteroids);

            return asteroids;
        }

        public virtual Comet GetComet(string id) => Get<Comet>(id);

        public virtual IDictionary<string, Comet> GetComets(ICollection<string> identifiers)
        {
            IDictionary<string, Comet> comets = new Dictionary<string, Comet>();

            Get<Comet>(identifiers, comets);

            return comets;
        }
        #endregion

        #region Public Add Functions
        public virtual void Add<T>(T t) where T : StellarBody
        {
            IDictionary<string, T> dict = GetDictionary<T>(true);

            if (string.IsNullOrEmpty(t.Identifier))
                t.Identifier = GenerateIdentifier<T>();

            if (!dict.ContainsKey(t.Identifier))
            {
                t.Map = this;
                dict.Add(t.Identifier, t);
            }
        }

        public virtual void Add<T>(ICollection<T> ts) where T : StellarBody
        {
            IDictionary<string, T> dict = GetDictionary<T>(false);

            foreach (T t in ts)
            {
                if (string.IsNullOrEmpty(t.Identifier))
                    t.Identifier = GenerateIdentifier<T>();

                if (!dict.ContainsKey(t.Identifier))
                {
                    t.Map = this;
                    dict.Add(t.Identifier, t);
                }
            }
        }

        public virtual void Add(Star star) => Add<Star>(star);

        public virtual void Add(ICollection<Star> stars) => Add<Star>(stars);

        public virtual void Add(Planet planet) => Add<Planet>(planet);

        public virtual void Add(ICollection<Planet> planets) => Add<Planet>(planets);

        public virtual void Add(Satellite satellite) => Add<Satellite>(satellite);

        public virtual void Add(ICollection<Satellite> satellites) => Add<Satellite>(satellites);

        public virtual void Add(Asteroid asteroid) => Add<Asteroid>(asteroid);

        public virtual void Add(ICollection<Asteroid> asteroids) => Add<Asteroid>(asteroids);

        public virtual void Add(Comet comet) => Add<Comet>(comet);

        public virtual void Add(ICollection<Comet> comets) => Add<Comet>(comets);
        #endregion

        #region Public Methods
        public virtual string GenerateIdentifier<T>() where T : StellarBody
        {
            return Guid.NewGuid().ToString();
        }

        public virtual IList<string> GetBodyTypes()
        {
            IList<string> bodytypes = new List<string>()
            {
                Constants.BodyTypes.Star,
                Constants.BodyTypes.Planet,
                Constants.BodyTypes.Satellite,
                Constants.BodyTypes.Asteroid,
                Constants.BodyTypes.Comet
            };

            return bodytypes;
        }

        public virtual object GetBody(string bodytype)
        {
            object body = null;

            switch (bodytype)
            {
                case Constants.BodyTypes.Star:
                    body = Stars;
                    break;
                case Constants.BodyTypes.Planet:
                    body = Planets;
                    break;
                case Constants.BodyTypes.Satellite:
                    body = Satellites;
                    break;
                case Constants.BodyTypes.Asteroid:
                    body = Asteroids;
                    break;
                case Constants.BodyTypes.Comet:
                    body = Comets;
                    break;
            }

            return body;
        }

        public virtual Type GetTypeOfBody(string bodytype)
        {
            Type t = null;

            switch (bodytype)
            {
                case Constants.BodyTypes.Star:
                    t = typeof(Dictionary<string, Star>);
                    break;
                case Constants.BodyTypes.Planet:
                    t = typeof(Dictionary<string, Planet>);
                    break;
                case Constants.BodyTypes.Satellite:
                    t = typeof(Dictionary<string, Satellite>);
                    break;
                case Constants.BodyTypes.Asteroid:
                    t = typeof(Dictionary<string, Asteroid>);
                    break;
                case Constants.BodyTypes.Comet:
                    t = typeof(Dictionary<string, Comet>);
                    break;
            }

            return t;
        }

        public virtual bool SetBody(string bodytype, object data)
        {
            bool bret = false;
            switch (bodytype)
            {
                case Constants.BodyTypes.Star:
                    Stars = (Dictionary<string, Star>)data;
                    bret = true;
                    break;
                case Constants.BodyTypes.Planet:
                    Planets = (Dictionary<string, Planet>)data;
                    bret = true;
                    break;
                case Constants.BodyTypes.Satellite:
                    Satellites = (Dictionary<string, Satellite>)data;
                    bret = true;
                    break;
                case Constants.BodyTypes.Asteroid:
                    Asteroids = (Dictionary<string, Asteroid>)data;
                    bret = true;
                    break;
                case Constants.BodyTypes.Comet:
                    Comets = (Dictionary<string, Comet>)data;
                    bret = true;
                    break;
            }

            return bret;
        }
        #endregion

        #region Protected Functions
        protected virtual IDictionary<string, T> GetDictionary<T>(bool create) where T : StellarBody
        {
            IDictionary<string, T> dict = null;
            Type dt = typeof(T);

            if (dt == typeof(Planet))
            {
                if (create && Planets == null)
                    Planets = new Dictionary<string, Planet>();
                dict = (IDictionary<string, T>)Planets;
            }
            else if (dt == typeof(Star))
            {
                if (create && Stars == null)
                    Stars = new Dictionary<string, Star>();
                dict = (IDictionary<string, T>)Stars;
            }
            //else if (dt == typeof(StarSystem))
            //{
            //    if (create && StarSystems == null)
            //        StarSystems = new Dictionary<string, StarSystem>();
            //    dict = (IDictionary<string, T>)StarSystems;
            //}
            else if (dt == typeof(Satellite))
            {
                if (create && Satellites == null)
                    Satellites = new Dictionary<string, Satellite>();
                dict = (IDictionary<string, T>)Satellites;
            }
            else if (dt == typeof(Asteroid))
            {
                if (create && Asteroids == null)
                    Asteroids = new Dictionary<string, Asteroid>();
                dict = (IDictionary<string, T>)Asteroids;
            }
            else if (dt == typeof(Comet))
            {
                if (create && Comets == null)
                    Comets = new Dictionary<string, Comet>();
                dict = (IDictionary<string, T>)Comets;
            }

            return dict;
        }
        #endregion
    }
}
