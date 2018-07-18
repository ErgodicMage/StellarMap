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
        public Comet()
        {

        }

        public Comet(string name) : base(name)
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
