using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Traveller
{
    [DataContract (Name = TravellerConstants.BodyType.World)]
    public class World : StellarParentBody, IEqualityComparer<World>
    {
        #region Constructor
        public World()
        {
        }

        public World(string name, Hex hex, string uwp = null) : base(name, TravellerConstants.BodyType.World)
        {
            HexValue = hex;

            WorldGroupIdentifiers = new GroupNamedIdentifiers("GroupIdentifiers-World");
            if (uwp is null)
                BasicProperties.Add(TravellerConstants.PropertyNames.UWP, string.Empty);
            else
                BasicProperties.Add(TravellerConstants.PropertyNames.UWP, uwp);
        }
        #endregion

        #region Public Properties
        [DataMember(Order = 31)]
        public Hex HexValue { get; set; }

        [DataMember(Order = 32)]
        public GroupNamedIdentifiers WorldGroupIdentifiers { get; set; }
        #endregion

        #region IEqualityComparer

        public bool Equals(World x, World y) => WorldEqualityComparer.Comparer.Equals(x, y);

        public override bool Equals(object obj) => WorldEqualityComparer.Comparer.Equals(this, obj as World);

        public int GetHashCode(World obj) => WorldEqualityComparer.Comparer.GetHashCode(obj);

        public override int GetHashCode() => WorldEqualityComparer.Comparer.GetHashCode(this);
        #endregion
    }

    public sealed class WorldEqualityComparer : IEqualityComparer<World>
    {
        #region IEqualityComparer
        public bool Equals(World x, World y) =>
                    StellarBodyEqualityComparer.Comparer.Equals(x, y) && x.HexValue == y.HexValue &&
                    x.WorldGroupIdentifiers.Equals(y.WorldGroupIdentifiers);

        public int GetHashCode(World obj)
        {
            int hash = StellarBodyEqualityComparer.Comparer.GetHashCode(obj);
            hash ^= obj.HexValue.GetHashCode();
            if (obj.WorldGroupIdentifiers != null)
                hash ^= obj.WorldGroupIdentifiers.GetHashCode();

            return hash;
        }
        #endregion

        public static IEqualityComparer<World> Comparer { get; } = new WorldEqualityComparer();
    }
}
