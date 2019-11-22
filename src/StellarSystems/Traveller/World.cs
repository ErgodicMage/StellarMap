using System.Runtime.Serialization;

using StellarMap.Core.Bodies;

namespace StellarMap.Traveller
{
    [DataContract (Name = "World")]
    public class World : Planet
    {
        #region Constructor
        public World(string name) : base(name)
        {
        }
        #endregion
    }
}
