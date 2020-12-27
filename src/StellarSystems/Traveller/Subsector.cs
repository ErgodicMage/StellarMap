using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Traveller
{
    [DataContract(Name = TravellerConstants.BodyType.Subsector)]
    public class Subsector : StellarParentBody, IEqualityComparer<Subsector>
    {
        #region Constructors
        public Subsector()
        {
        }

        public Subsector(string name) : base(name, TravellerConstants.BodyType.Subsector)
        {
            SubsectorGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-TravellerSubsector");

        }
        #endregion

        #region Public Properties
        [DataMember(Order = 11)]
        public GroupNamedIdentifiers SubsectorGroupIdentifiers { get; set; }

        [IgnoreDataMember]
        public IDictionary<string, string> Worlds
        { get => SubsectorGroupIdentifiers.GroupIdentifiers.Get(TravellerConstants.NamedIdentifiers.World); }
        #endregion

        #region Get Functions
        public virtual World GetWorld(string name) =>
            Get<World>(name, SubsectorGroupIdentifiers, TravellerConstants.NamedIdentifiers.World);
        #endregion

        #region Add Functions
        public void Add(World world) =>
            Add<World>(world, SubsectorGroupIdentifiers, TravellerConstants.NamedIdentifiers.World);
        #endregion

        #region IEqualityComparer
        public bool Equals(Subsector x, Subsector y) => SubsectorEqualityComparer.Comparer.Equals(x, y);

        public override bool Equals(object obj) => SubsectorEqualityComparer.Comparer.Equals(this, obj as Subsector);

        public int GetHashCode(Subsector obj) => SubsectorEqualityComparer.Comparer.GetHashCode(obj);

        public override int GetHashCode() => SubsectorEqualityComparer.Comparer.GetHashCode(this);
        #endregion
    }

    public sealed class SubsectorEqualityComparer : IEqualityComparer<Subsector>
    {
        #region IEqualityComparer
        public bool Equals(Subsector x, Subsector y) =>
                    StellarBodyEqualityComparer.Comparer.Equals(x, y) &&
                    x.SubsectorGroupIdentifiers.Equals(y.SubsectorGroupIdentifiers);

        public int GetHashCode(Subsector obj)
        {
            int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
            if (obj.SubsectorGroupIdentifiers != null)
                hash ^= obj.SubsectorGroupIdentifiers.GetHashCode();

            return hash;
        }
        #endregion

        public static IEqualityComparer<Subsector> Comparer { get; } = new SubsectorEqualityComparer();
    }
}
