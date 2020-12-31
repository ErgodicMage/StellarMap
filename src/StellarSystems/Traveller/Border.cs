using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StellarMap.Traveller
{
    [DataContract (Name = TravellerConstants.BodyType.Border)]
    public class Border
    {
        #region Constructor        
        #endregion

        #region Properties
        [DataMember(Order = 11)]
        public IList<Hex> Positions {get; set;}

        [DataMember(Order = 12)]
        public string Color {get; set;}
        #endregion
        
    }
}