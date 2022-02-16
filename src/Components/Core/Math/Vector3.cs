using System.Diagnostics.Contracts;
using System.Globalization;

namespace StellarMap.Core.Math;

/// <summary>
/// Represents a vector in 3 dimensional space
/// Note: Vector3d is inspired (borrowed) from the Math.Net.Spatial Vector3D class https://github.com/mathnet/mathnet-spatial/blob/master/src/Spatial/Euclidean/Vector3D.cs.
/// This version will work better with .net core and better serialization capabilities
/// </summary>
[DataContract]
public struct Vector3d : IEquatable<Vector3d>, IFormattable
{
    #region x, y, z
    /// <summary>
    /// The x component.
    /// </summary>
    [DataMember(Order = 1)]
    public readonly double x;

    /// <summary>
    /// The y component.
    /// </summary>
    [DataMember(Order = 2)]
    public readonly double y;

    /// <summary>
    /// The z component.
    /// </summary>
    [DataMember(Order = 3)]
    public readonly double z;

    #endregion

    #region Constructors
    /// <summary>
    /// Creates a Vector3d from 3 components
    /// </summary>
    /// <param name="px">The x component.</param>
    /// <param name="py">The y component.</param>
    /// <param name="pz">The z component.</param>
    public Vector3d(double px, double py, double pz)
    {
        this.x = px;
        this.y = py;
        this.z = pz;
    }

    /// <summary>
    /// Creates a new Vector3d from another.
    /// </summary>
    /// <param name="p">The vector to create from</param>
    public Vector3d(Vector3d p)
    {
        this.x = p.x;
        this.y = p.y;
        this.z = p.z;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Returns the X component of the vector.
    /// </summary>
    public double X { get { return x; } }

    /// <summary>
    /// Returns the Y component of the vector.
    /// </summary>
    public double Y { get { return y; } }

    /// <summary>
    /// Returns the Z component of the vector.
    /// </summary>
    public double Z { get { return z; } }

    #endregion

    #region Static Vector3ds
    public static Vector3d NullVector => new Vector3d(0, 0, 0);
    /// <summary>
    /// Gets an invalid vector with no values
    /// </summary>
    public static Vector3d NaN => new Vector3d(double.NaN, double.NaN, double.NaN);
    #endregion

    #region Functions
    /// <summary>
    /// Gets the Euclidean Norm.
    /// </summary>
    [Pure]
    public double Length => System.Math.Sqrt((this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z));

    /// <summary>
    /// Returns a point equivalent to the vector
    /// </summary>
    /// <returns>A point</returns>
    public Point3d ToPoint3d() => new Point3d(this.x, this.y, this.z);

    /// <summary>
    /// Add this vector with another
    /// </summary>
    /// <param name="addend">The vector to add</param>
    /// <returns>A new summed vector</returns>
    [Pure]
    public Vector3d Add(Vector3d addend) => new Vector3d(this.x + addend.x, this.y + addend.y, this.z + addend.z);

    /// <summary>
    /// Subtract a vector with this.
    /// </summary>
    /// <param name="subtrahend">The vector to subtract</param>
    /// <returns>A new difference vector</returns>
    [Pure]
    public Vector3d Subtract(Vector3d subtrahend) => new Vector3d(this.x - subtrahend.x, this.y - subtrahend.y, this.z - subtrahend.z);

    /// <summary>
    /// Inverses the direction of the vector, equivalent to multiplying by -1
    /// </summary>
    /// <returns>A <see cref="Vector3d"/> pointing in the opposite direction.</returns>
    [Pure]
    public Vector3d Negate() => new Vector3d(-1 * this.X, -1 * this.Y, -1 * this.Z);

    /// <summary>
    /// Scales a vector by a factor, in other words multiplies the vector by a scalar value
    /// </summary>
    /// <param name="factor"></param>
    /// <returns>A new scaled vector</returns>
    [Pure]
    public Vector3d Scale(double factor) => new Vector3d(factor * this.x, factor * this.y, factor * this.z);

    /// <summary>
    /// Returns the dot product of two vectors.
    /// </summary>
    /// <param name="v">The second vector.</param>
    /// <returns>The dot product.</returns>
    [Pure]
    public double DotProduct(Vector3d v) => (this.x * v.x) + (this.y * v.y) + (this.z * v.z);

    /// <summary>
    /// Returns the cross product of this vector and <paramref name="other"/>
    /// </summary>
    /// <param name="other">A vector</param>
    /// <returns>A new vector with the cross product result</returns>
    [Pure]
    public Vector3d CrossProduct(Vector3d other)
    {
        double xx = (this.y * other.z) - (this.z * other.y);
        double yy = (this.z * other.x) - (this.x * other.z);
        double zz = (this.x * other.y) - (this.y * other.x);
        return new Vector3d(xx, yy, zz);
    }

    #endregion

    #region IEquatable<Vector3d> interface
    /// <summary>
    /// Returns a value to indicate if a pair of vectors are equal
    /// </summary>
    /// <param name="other">The vector to compare against.</param>
    /// <param name="tolerance">A tolerance (epsilon) to adjust for floating point error</param>
    /// <returns>True if the vectors are equal; otherwise false</returns>
    [Pure]
    public bool Equals(Vector3d other, double tolerance)
    {
        if (tolerance < 0)
        {
            throw new ArgumentException("epsilon < 0");
        }

        return System.Math.Abs(other.x - this.x) < tolerance &&
                System.Math.Abs(other.y - this.y) < tolerance &&
                System.Math.Abs(other.z - this.z) < tolerance;
    }

    /// <inheritdoc />
    [Pure]
    public bool Equals(Vector3d other) => this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z);

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object obj) => obj is Vector3d p && this.Equals(p);
    #endregion

    #region IFormatable interface
    /// <inheritdoc />
    [Pure]
    public override string ToString() => ToString(null, CultureInfo.InvariantCulture);

    /// <summary>
    /// Returns a string representation of this instance using the provided <see cref="IFormatProvider"/>
    /// </summary>
    /// <param name="provider">A <see cref="IFormatProvider"/></param>
    /// <returns>The string representation of this instance.</returns>
    [Pure]
    public string ToString(IFormatProvider provider) => ToString(null, provider);

    /// <inheritdoc />
    [Pure]
    public string ToString(string format, IFormatProvider provider = null)
    {
        NumberFormatInfo numberFormatInfo = provider != null ? NumberFormatInfo.GetInstance(provider) : CultureInfo.InvariantCulture.NumberFormat;
        string separator = numberFormatInfo.NumberDecimalSeparator == "," ? ";" : ",";
        return string.Format("({0}{1} {2}{1} {3})", this.x.ToString(format, numberFormatInfo), separator, this.y.ToString(format, numberFormatInfo), this.z.ToString(format, numberFormatInfo));
    }
    #endregion

    #region HashCode
    /// <inheritdoc />
    [Pure]
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 103841;
            // Suitable nullity checks etc, of course :)
            hash = hash * 49627 + x.GetHashCode();
            hash = hash * 49627 + y.GetHashCode();
            hash = hash * 49627 + z.GetHashCode();
            return hash;
        }
    }
    #endregion

    #region Operators
    /// <summary>
    /// Returns a value that indicates whether each pair of elements in two specified vectors is equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are the same; otherwise false.</returns>
    [Pure]
    public static bool operator ==(Vector3d left, Vector3d right) => left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether any pair of elements in two specified vectors is not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are different; otherwise false.</returns>
    [Pure]
    public static bool operator !=(Vector3d left, Vector3d right) => !left.Equals(right);

    /// <summary>
    /// Adds two vectors
    /// </summary>
    /// <param name="left">The first vector</param>
    /// <param name="right">The second vector</param>
    /// <returns>A new summed vector</returns>
    [Pure]
    public static Vector3d operator +(Vector3d left, Vector3d right) => new Vector3d(left.x + right.x, left.y + right.y, left.z + right.z);

    /// <summary>
    /// Subtracts two vectors
    /// </summary>
    /// <param name="left">The first vector</param>
    /// <param name="right">The second vector</param>
    /// <returns>A new difference vector</returns>
    [Pure]
    public static Vector3d operator -(Vector3d left, Vector3d right) => new Vector3d(left.x - right.x, left.y - right.y, left.z - right.z);

    #endregion

    #region Parse
    #endregion

}

