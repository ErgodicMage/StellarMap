using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Types;

namespace StellarMap.Core.Bodies
{
    [DataContract(Name = Constants.BodyTypes.Planet)]
    public class Planet : StellarParentBody, IEquatable<Planet>
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
        public IDictionary<string, string> Satellites 
            { get => PlanetGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Satellites); }
        #endregion

        #region Get Functions
        public virtual Satellite GetSatellite(string name) => 
            Get<Satellite>(name, PlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);

        public virtual IDictionary<string, Satellite> GetSatellites() => 
            GetAll<Satellite>(PlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
        #endregion

        #region Public Add Functions
        public void Add(Satellite satellite) => 
            Add<Satellite>(satellite, PlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
        #endregion

        #region IEquatable
        public bool Equals(Planet other) => 
            other!=null && base.Equals(other as StellarParentBody) && 
            PlanetGroupIdentifiers.Equals(other.PlanetGroupIdentifiers);

        public override bool Equals(object obj) => Equals(obj as Planet);

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            if (PlanetGroupIdentifiers != null)
                hash = hash ^ PlanetGroupIdentifiers.GetHashCode();

            return hash;
        }
        #endregion
    }
}
