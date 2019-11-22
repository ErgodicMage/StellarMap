using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Bodies;

namespace StellarMap.Core.Types
{
    [DataContract(Name = "BaseStellarMap")]
    public class BaseStellarMap : IStellarMap, IEquatable<BaseStellarMap>
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
        public virtual T Get<T>(string id) where T : IStellarBody
        {
            T t = default(T);

            if (!string.IsNullOrEmpty(id))
            {
                IDictionary<string, T> dict = GetDictionary<T>(false);

                if (dict != null && dict.ContainsKey(id))
                    t = dict[id];
            }

            return t;
        }

        public virtual void Get<T>(ICollection<string> identifiers, IDictionary<string, T> output) where T : IStellarBody
        {
            foreach (string id in identifiers)
            {
                T t = Get<T>(id);
                if (t != null)
                    output.Add(id, t);
            }
        }
        #endregion

        #region Public Add Functions
        public virtual void Add<T>(T t) where T : IStellarBody
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

        public virtual void Add<T>(ICollection<T> ts) where T : IStellarBody
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
        #endregion

        #region Public Methods
        public virtual string GenerateIdentifier<T>() where T : IStellarBody
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

        public virtual void SetMap()
        {
            // can't use casting with dictionaries the way I want to object as Dictionary<string, StellarBody> so have to do it the hard way

            if (Stars != null)
            {
                foreach (var value in Stars.Values)
                    value.Map = this;
            }

            if (Planets != null)
            {
                foreach (var value in Planets.Values)
                    value.Map = this;
            }

            if (Satellites != null)
            {
                foreach (var value in Satellites.Values)
                    value.Map = this;
            }

            if (Asteroids != null)
            {
                foreach (var value in Asteroids.Values)
                    value.Map = this;
            }

            if (Comets != null)
            {
                foreach (var value in Comets.Values)
                    value.Map = this;
            }
        }
        #endregion

        #region Protected Functions
        protected virtual IDictionary<string, T> GetDictionary<T>(bool create) where T : IStellarBody
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

        #region IEquatable
        public bool Equals(BaseStellarMap other)
        {
            bool bRet = true;

            if (other == null)
                bRet = false;
            else if (!ReferenceEquals(this, other))
            {
                bRet = Name.Equals(other.Name) &&
                       IsEqual<Planet>(this.Planets, other.Planets) &&
                       IsEqual<Star>(this.Stars, other.Stars) &&
                       IsEqual<Satellite>(this.Satellites, other.Satellites) &&
                       IsEqual<Asteroid>(this.Asteroids, other.Asteroids) &&
                       IsEqual<Comet>(this.Comets, other.Comets);
            }

            return bRet;
        }

        public override bool Equals(object obj) => Equals(obj as BaseStellarMap);

        public override int GetHashCode()
        {
            int hash = Name.GetHashCode();
            if (Stars != null)
                hash = hash ^ Stars.GetHashCode();
            if (Planets != null)
                hash = hash ^ Planets.GetHashCode();
            if (Satellites != null)
                hash = hash ^ Satellites.GetHashCode();
            if (Asteroids != null)
                hash = hash ^ Asteroids.GetHashCode();
            if (Comets != null)
                hash = hash ^ Comets.GetHashCode();

            return hash;
        }

        protected static bool IsEqual<T>(IDictionary<string, T> thisObject, IDictionary<string, T> otherObject) //where T : StellarBody
        {
            bool bRet = true;

            if (thisObject == null && otherObject == null)
                bRet = true;
            else if ((thisObject == null) || (otherObject == null))
                bRet = false;
            else if (!ReferenceEquals(thisObject, otherObject))
            {
                if (thisObject.Count == otherObject.Count)
                {
                    var thisEnumerator = thisObject.GetEnumerator();
                    var otherEnumerator = otherObject.GetEnumerator();

                    while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
                    {
                        if (thisEnumerator.Current.Key.Equals(otherEnumerator.Current.Key))
                        {
                            if (!thisEnumerator.Current.Value.Equals(otherEnumerator.Current.Value))
                                bRet = false;
                        }
                        else
                            bRet = false;

                        if (!bRet)
                            break;
                    }
                }
                else
                    bRet = false;
            }

            return bRet;
        }
        #endregion
    }
}
