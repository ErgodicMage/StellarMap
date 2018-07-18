using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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

        public Asteroid(string name) : base(name)
        {
            //Initialize();
        }
        #endregion

        #region Protected Functions
        protected override void Initialize()
        {
            base.Initialize();
        }
        #endregion
    }
}
