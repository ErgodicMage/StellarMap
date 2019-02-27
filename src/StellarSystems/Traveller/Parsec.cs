using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using StellarMap.Core.Bodies;
using StellarMap.Core.Types;

namespace StellarMap.Traveller
{
    [DataContract (Name = "Parsec")]
    public class Parsec : StellarParentBody
    {
        #region Constructors
        public Parsec(string name) : base(name, "Parsec")
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
        #endregion
    }
}
