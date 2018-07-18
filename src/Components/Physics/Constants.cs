using System;

namespace StellarMap.Physics
{
    /// <summary>
    /// General constants used.
    /// 
    /// </summary>
    public struct Constants
    {
        /// <summary>
        /// Speed of light in units of m/s.
        /// </summary>
        public static double SpeedofLight = 299792458;

        /// <summary>
        /// Gravitational Constant (G) in units of m3/kgs2.
        /// </summary>
        public static double G = 6.67408 * 1e-11;

        /// <summary>
        /// Light Year in units of m.
        /// </summary>
        public static double LightYear = 9.460530 * 1e15;

        /// <summary>
        /// Parsec in units of LightYear (ly).
        /// </summary>
        public static double Parsec = 3.261633;

        /// <summary>
        /// Parsec in units of m.
        /// </summary>
        public static double Parsecm = 3.085678 * 1e16;

        /// <summary>
        ///  Astronomical Unit (AU) in units of m.
        /// </summary>
        public static double AU = 1.495979 * 1e11;
    }
}
