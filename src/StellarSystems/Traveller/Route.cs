using System.Runtime.Serialization;

namespace StellarMap.Traveller
{
    [DataContract (Name = TravellerConstants.BodyType.Route)]
    public class Route
    {
        #region Constructor
        #endregion

        #region Properties
        [DataMember(Order = 11)]
        public Hex Start {get; set;}

        [DataMember(Order = 12)]
        public Hex End {get; set;}

        [DataMember(Order = 13)]
        public short StartOffsetX {get; set;}

        [DataMember(Order = 14)]
        public short StartOffsetY {get; set;}

        [DataMember(Order = 15)]
        public short EndOffsetX {get; set;}

        [DataMember(Order = 16)]
        public short EndOffsetY {get; set;}

        [DataMember(Order = 17)]
        public string Color {get; set;}

        #endregion
    }
}