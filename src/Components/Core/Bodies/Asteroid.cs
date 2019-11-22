using System.Runtime.Serialization;

using StellarMap.Core.Types;

namespace StellarMap.Core.Bodies
{
    [DataContract (Name = Constants.BodyTypes.Asteroid)]
    public class Asteroid : StellarBody
    {
        #region Constructors
        public Asteroid()
        {            
        }

        public Asteroid(string name) : base(name, Constants.BodyTypes.Asteroid)
        {
        }
        #endregion
    }
}
