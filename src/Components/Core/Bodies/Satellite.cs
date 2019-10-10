using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Types;

namespace StellarMap.Core.Bodies
{
    [DataContract (Name = Constants.BodyTypes.Satellite)]
    public class Satellite : StellarBody
    {
        #region Constructors
        public Satellite()
        {
        }

        public Satellite(string name) : base(name, Constants.BodyTypes.Satellite)
        {
        }
        #endregion
    }
}
