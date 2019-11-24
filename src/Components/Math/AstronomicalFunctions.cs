using StellarMap.Math.Types;

namespace StellarMap.Math
{
    public static class AstronomicalFunctions
    {
        public static Point3d ConvertToCartesian(double distance, double rah, double ram, double ras, double decd, double decm, double decs)
        {
            double phi = rah * 15 + ram * 0.25 + ras * 0.0041666;

            double theta = System.Math.Abs(decd) + decm / 60 + decs / 3600;
            if (decd < 1.0)
                theta *= -1.0;

            double x = distance * System.Math.Cos(theta) * System.Math.Cos(phi);
            double y = distance * System.Math.Cos(theta) * System.Math.Sin(phi);
            double z = distance * System.Math.Sin(theta);

            Point3d retPoint = new Point3d(x, y, z);

            return retPoint;
        }
    }
}
