﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;
using StellarMap.Math.Types;

namespace StellarMap.Progression
{
    [DataContract (Name = "Habitat")]
    public class Habitat : StellarBody
    {
        #region
        public Habitat()
        {

        }

        public Habitat(string name)
        {
            Name = name;
            Initialize();
        }
        #endregion

        #region Protected Methods
        protected override void Initialize()
        {
            base.Initialize();
        }
        #endregion
    }
}
