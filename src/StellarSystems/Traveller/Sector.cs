using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Traveller
{
    [DataContract(Name = TravellerConstants.BodyType.Sector)]
    public class Sector : StellarParentBody, IEqualityComparer<Sector>
    {
        #region Constructors
        public Sector()
        {
        }

        public Sector(string name) : base(name, TravellerConstants.BodyType.Sector)
        {
            SectorGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-TravellerSector");

        }
        #endregion

        #region Public Properties
        [DataMember(Order = 12)]
        public GroupNamedIdentifiers SectorGroupIdentifiers { get; set; }
        #endregion

        #region Get Functions
        public virtual Subsector GetSubsector(string name) =>
            Get<Subsector>(name, SectorGroupIdentifiers, TravellerConstants.NamedIdentifiers.Subsector);
        #endregion

        #region Add Functions
        public void Add(Subsector subsector) =>
            Add<Subsector>(subsector, SectorGroupIdentifiers, TravellerConstants.NamedIdentifiers.Subsector);
        #endregion

        #region IEqualityComparer
        public bool Equals(Sector x, Sector y) => SectorEqualityComparer.Comparer.Equals(x, y);

        public override bool Equals(object obj) => SectorEqualityComparer.Comparer.Equals(this, obj as Sector);

        public int GetHashCode(Sector obj) => SectorEqualityComparer.Comparer.GetHashCode(obj);

        public override int GetHashCode() => SectorEqualityComparer.Comparer.GetHashCode(this);
        #endregion
    }

    public sealed class SectorEqualityComparer : IEqualityComparer<Sector>
    {
        #region IEqualityComparer
        public bool Equals(Sector x, Sector y) =>
                    StellarBodyEqualityComparer.Comparer.Equals(x, y) &&
                    x.SectorGroupIdentifiers.Equals(y.SectorGroupIdentifiers);

        public int GetHashCode(Sector obj)
        {
            int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
            if (obj.SectorGroupIdentifiers != null)
                hash ^= obj.SectorGroupIdentifiers.GetHashCode();

            return hash;
        }
        #endregion

        public static IEqualityComparer<Sector> Comparer { get; } = new SectorEqualityComparer();
    }
}
