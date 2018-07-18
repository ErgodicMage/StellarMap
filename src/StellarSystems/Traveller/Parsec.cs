using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Traveller
{
    public class Parsec : StellarBodywithObjects
    {
        #region Constructors
        public Parsec()
        {
        }

        public Parsec(string name) : base(name)
        {
        }
        #endregion

        #region Public Properties
        #endregion

        #region Get Methods
        #endregion

        #region Add Methods
        #endregion

        #region Protected Methods
        protected override void Initialize()
        {
            base.Initialize();
        }
        #endregion
    }
}
