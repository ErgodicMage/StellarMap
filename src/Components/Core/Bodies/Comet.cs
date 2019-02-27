using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Types;

namespace StellarMap.Core.Bodies
{
    [DataContract (Name = Constants.BodyTypes.Comet)]
    public class Comet : StellarBody
    {
        #region Constructors
        public Comet(string name) : base(name, Constants.BodyTypes.Comet)
        {
        }
        #endregion
    }
}
