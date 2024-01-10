namespace StellarMap.Core.Math;

public static class MathFunctions
{
    public static Point3d ConvertToCartesian(double distance, double rah, double ram, double ras, double decd, double decm, double decs)
    {
        double phi = rah * 15 + ram * 0.25 + ras * 0.0041666;
        double theta = System.Math.Abs(decd) + decm / 60 + decs / 3600;
        theta = decd < 1.0 ? -theta : theta;

        return new()
        {
            X = distance * System.Math.Cos(theta) * System.Math.Cos(phi),
            Y = distance * System.Math.Cos(theta) * System.Math.Sin(phi),
            Z = distance * System.Math.Sin(theta)
        };
    }

    public static double Distance(Point3d p1, Point3d p2)
        => System.Math.Sqrt(System.Math.Pow((p1.X - p2.X), 2) + System.Math.Pow((p1.Y - p2.Y), 2) + System.Math.Pow((p1.Z - p2.Z), 2));
}

