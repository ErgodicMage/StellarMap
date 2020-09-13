using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Types;

namespace StellarMap.Core.Bodies
{
    [DataContract (Name = Constants.BodyTypes.DwarfPlanet)]
    public class DwarfPlanet : StellarParentBody, IEquatable<DwarfPlanet>, IEqualityComparer<DwarfPlanet>
    {
        #region Constructors
        public DwarfPlanet()
        {
        }

        public DwarfPlanet(string name) : base(name, Constants.BodyTypes.DwarfPlanet)
        {            
            DwarfPlanetGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-DwarfPlanet");
        }
        #endregion

        #region Public Properties
        [DataMember(Order = 11)]
        public GroupNamedIdentifiers DwarfPlanetGroupIdentifiers { get; set; }

        [IgnoreDataMember]
        public IDictionary<string, string> Satellites 
            { get => DwarfPlanetGroupIdentifiers.GroupIdentifiers.Get(Constants.NamedIdentifiers.Satellites); }
        #endregion

        #region Get Functions
        public virtual Satellite GetSatellite(string name) => 
            Get<Satellite>(name, DwarfPlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);

        public virtual IDictionary<string, Satellite> GetSatellites() => 
            GetAll<Satellite>(DwarfPlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
        #endregion

        #region Public Add Functions
        public void Add(Satellite satellite) => 
            Add<Satellite>(satellite, DwarfPlanetGroupIdentifiers, Constants.NamedIdentifiers.Satellites);
        #endregion

        #region IEquatable
        public bool Equals(DwarfPlanet other) => 
            other!=null && base.Equals(other as StellarParentBody) && 
            DwarfPlanetGroupIdentifiers.Equals(other.DwarfPlanetGroupIdentifiers);

        public override bool Equals(object obj) => Equals(obj as DwarfPlanet);

        public bool Equals(DwarfPlanet x, DwarfPlanet y) => x.Equals(y);

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            if (DwarfPlanetGroupIdentifiers != null)
                hash ^= DwarfPlanetGroupIdentifiers.GetHashCode();

            return hash;
        }

        public int GetHashCode(DwarfPlanet obj) => obj.GetHashCode();
        #endregion
    }
}
