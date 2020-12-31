using System.Runtime.Serialization;

namespace StellarMap.Traveller
{
    [DataContract (Name = TravellerConstants.BodyType.Label)]
    public class Label
    {
        #region Constructor
        #endregion

        #region Properties
        [DataMember(Order = 11)]
        public Hex Position {get; set;}

        [DataMember(Order = 12)]
        public string Text {get; set;}
        #endregion
    }
}