using System.Globalization;

namespace StellarMap.Core.Math;

[DataContract (Name = "Point3d")]
public readonly record struct Point3d(double X, double Y, double Z)
{
    #region Static Points
    public static Point3d Origin => new(0, 0, 0);

    public static Point3d NaN => new(double.NaN, double.NaN, double.NaN);
    #endregion

    #region Functions
    public readonly SphericalPoint3d ToSphericalPoint()
    {
        var radius = System.Math.Sqrt(X * X + Y * Y + Z * Z);
        var inclination = System.Math.Acos(Z / radius);
        var azimuthal = System.Math.Sign(Y) * System.Math.Acos(X / radius);
        return new(radius, inclination, azimuthal);
    }

    public readonly Vector3d ToVector() => new(this.X, this.X, this.Z);

    /// <summary>
    public readonly Point3d Add(Vector3d addend) => new(this.X + addend.X, this.Y + addend.Y, this.Z + addend.Z);

    public readonly Point3d Subrtact(Vector3d vector) => new(this.X - vector.X, this.Y - vector.Y, this.Z - vector.Z);
    #endregion

    #region HashCode
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Z);
    #endregion

    #region Operators
    public static Point3d operator +(Point3d point, Vector3d vector) => new(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);

    public static Point3d operator -(Point3d point, Vector3d vector) => new(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
    #endregion

    #region IFormatable interface
    public override readonly string ToString() => ToString(null, CultureInfo.InvariantCulture);

    public readonly string ToString(IFormatProvider provider) => ToString(null, provider);

    public readonly string ToString(string? format, IFormatProvider? provider = null)
    {
        NumberFormatInfo numberFormatInfo = provider != null ? NumberFormatInfo.GetInstance(provider) : CultureInfo.InvariantCulture.NumberFormat;
        string separator = numberFormatInfo.NumberDecimalSeparator == "," ? ";" : ",";
        return string.Format("({0}{1} {2}{1} {3})", this.X.ToString(format, numberFormatInfo), separator, this.Y.ToString(format, numberFormatInfo), this.Z.ToString(format, numberFormatInfo));
    }
    #endregion

    #region Parse
    public static bool TryParse(string? text, out Point3d result)
    {
        if (TryParse(text, out var x, out var y, out var z))
        {
            result = new(x, y, z);
            return true;
        }
        result = Point3d.NaN;
        return false;
    }

    public static bool TryParse(string? text, out double x, out double y, out double z)
    {
        x = default;
        y = default;
        z = default;

        if (string.IsNullOrEmpty(text))
            return false;

        // right now this is elcheapo - only handles (1.1, 2.4, 3.2) format
        string? temp = text?.Replace("(", "").Replace(")", "");
        string[]? values = temp?.Split(",");

        if (values?.Length == 3 &&
            double.TryParse(values[0], out x) &&
            double.TryParse(values[1], out y) &&
            double.TryParse(values[2], out z))
            return true;

        return false;
    }

    #endregion
}