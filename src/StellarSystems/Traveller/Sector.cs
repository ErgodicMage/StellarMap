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
            string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
            foreach (string letter in letters)
                BasicProperties.Add(letter, string.Empty);
        }
        #endregion

        #region Public Properties
        [DataMember(Order = 11)]
        public GroupNamedIdentifiers SectorGroupIdentifiers { get; set; }

        [DataMember(Order = 12)]
        public IList<Route> Routes {get; set;} = new List<Route>();

        [DataMember(Order = 13)]
        public IList<Border> Borders {get; set;} = new List<Border>();

        [DataMember(Order = 14)]
        public IList<Allegiance> Allegiances {get; set;} = new List<Allegiance>();

        [DataMember(Order = 15)]
        public IList<Label> Labels {get; set;} = new List<Label>();
        #endregion

        #region Get Functions
        public virtual Subsector GetSubsector(string name) =>
            Get<Subsector>(name, SectorGroupIdentifiers, TravellerConstants.NamedIdentifiers.Subsector);

        public Subsector GetSubsectorByLetter(string letter)
        {
            string name = Properties["Basic", letter];
            return string.IsNullOrEmpty(name) ? null : GetSubsector(name);
        }
        #endregion

        #region Add Functions
        public void Add(Subsector subsector) =>
            Add<Subsector>(subsector, SectorGroupIdentifiers, TravellerConstants.NamedIdentifiers.Subsector);

        public void Add(string letter, Subsector subsector)
        {
            Add(subsector);
            Properties["Basic", letter] = subsector.Name;
        }
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
