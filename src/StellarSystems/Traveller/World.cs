using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

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
