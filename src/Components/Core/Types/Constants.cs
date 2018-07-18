using System;
using System.Collections.Generic;
using System.Text;

namespace StellarMap.Core.Types
{
    public static class Constants
    {
        public static class BodyTypes
        {
            public const string StellarBody = "StellarBody";
            public const string Star = "Star";
            public const string Planet = "Planet";
            public const string Satellite = "Satellite";
            public const string Asteroid = "Asteroid";
            public const string Comet = "Comet";
        }

        public static class NamedIdentifiers
        {
            public const string Planets = "Planets";
            public const string Stars = "Stars";
            public const string Satellites = "Satellites";
            public const string Asteroids = "Asteroids";
            public const string Comets = "Comets";
        }

        public static class PropertyNames
        {
            public const string Designation = "Designation";
            public const string StellarClass = "StellarClass";
        }
    }
}
