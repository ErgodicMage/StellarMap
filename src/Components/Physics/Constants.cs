using System;

namespace StellarMap.Physics
{
    /// <summary>
    /// General constants used.
    /// 
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Speed of light in units of m/s.
        /// </summary>
        public const double SpeedofLight = 299792458;

        /// <summary>
        /// Gravitational Constant (G) in units of m3/kgs2.
        /// </summary>
        public const double G = 6.67408 * 1e-11;

        /// <summary>
        /// Light Year in units of m.
        /// </summary>
        public const double LightYear = 9.460530 * 1e15;

        /// <summary>
        /// Parsec in units of LightYear (ly).
        /// </summary>
        public const double Parsec = 3.261633;

        /// <summary>
        /// Parsec in units of m.
        /// </summary>
        public const double Parsecm = 3.085678 * 1e16;

        /// <summary>
        ///  Astronomical Unit (AU) in units of m.
        /// </summary>
        public const double AU = 1.495979 * 1e11;
    }
}
