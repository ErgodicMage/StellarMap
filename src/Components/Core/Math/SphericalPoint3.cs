namespace StellarMap.Core.Math;

[DataContract(Name = "SphericalPoint3d")]
public readonly record struct SphericalPoint3d(double Radius, double Inclination, double Azimuth)
{
    #region Static SphericalPoint3d
    /// <summary>
    /// Gets a point at the origin
    /// </summary>
    public static SphericalPoint3d Origin { get; } = new(0, 0, 0);

    /// <summary>
    /// Gets a point where all values are NAN
    /// </summary>
    public static SphericalPoint3d NaN { get; } = new(double.NaN, double.NaN, double.NaN);
    #endregion

    #region Conversion
    public readonly Point3d ToPoint()
    {
        var x = Radius * System.Math.Sin(Inclination) * System.Math.Cos(Azimuth);
        var y = Radius * System.Math.Sin(Inclination) * System.Math.Sin(Azimuth);
        var z = Radius * System.Math.Cos(Inclination);
        return new(x, y, z);
    }
    #endregion
}
