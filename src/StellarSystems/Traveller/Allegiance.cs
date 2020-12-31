using System.Runtime.Serialization;

namespace StellarMap.Traveller
{
    [DataContract (Name = TravellerConstants.BodyType.Allegiance)]
    public class Allegiance
    {
        #region Constructor
        #endregion
        
        #region Properties
        [DataMember(Order = 11)]
        public string Code {get; set;}

        [DataMember(Order = 12)]
        public string Name {get; set;}
        #endregion
    }
}