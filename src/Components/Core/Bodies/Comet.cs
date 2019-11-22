using System.Runtime.Serialization;

using StellarMap.Core.Types;

namespace StellarMap.Core.Bodies
{
    [DataContract (Name = Constants.BodyTypes.Comet)]
    public class Comet : StellarBody
    {
        #region Constructors
        public Comet() : base()
        {            
        }

        public Comet(string name) : base(name, Constants.BodyTypes.Comet)
        {
        }
        #endregion
    }
}
