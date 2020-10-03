using System;
using System.Collections.Generic;
using System.Text;

namespace StellarMap.Traveller
{
    public static class TravellerConstants
    {
        public static class Sector
        {
            public const byte Height = 40;
            public const byte Width = 32;
        }

        public static class Subsector
        {
            public const byte Height = 10;
            public const byte Width = 8;
        }

        public static class BodyType
        {
            public const string Sector = "Sector";
            public const string Subsector = "Subsector";
            public const string World = "World";
        }

        public static class PropertyNames
        {
            public const string UWP = "UWP";
        }
    }
}
