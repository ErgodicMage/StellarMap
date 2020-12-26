using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Globalization;

namespace StellarMap.Math.Types
{
    /// <summary>
    /// Represents a point in 3 dimensional space
    /// Note: Point3d is inspired (borrowed) from the Math.Net.Spatial Point3D class https://github.com/mathnet/mathnet-spatial/blob/master/src/Spatial/Euclidean/Point3D.cs.
    /// This version should work better with .net core and better serialization capabilities
    /// </summary>
    [DataContract (Name = "Point3d")]
    public struct Point3d : IEquatable<Point3d>, IFormattable
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
        /// Creates a Point3d from 3 components
        /// </summary>
        /// <param name="px">The x component.</param>
        /// <param name="py">The y component.</param>
        /// <param name="pz">The z component.</param>
        public Point3d(double px, double py, double pz)
        {
            this.x = px;
            this.y = py;
            this.z = pz;
        }

        /// <summary>
        /// Creates a new Point3d from another.
        /// </summary>
        /// <param name="p">The point to create from</param>
        public Point3d(Point3d p)
        {
            this.x = p.x;
            this.y = p.y;
            this.z = p.z;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns the X component of the point.
        /// </summary>
        public double X { get { return x; } }

        /// <summary>
        /// Returns the Y component of the point.
        /// </summary>
        public double Y { get { return y; } }

        /// <summary>
        /// Returns the Z component of the point.
        /// </summary>
        public double Z { get { return z; } }

        #endregion

        #region Static Point3ds
        /// <summary>
        /// Gets a point at the origin
        /// </summary>
        public static Point3d Origin { get; } = new Point3d(0, 0, 0);

        /// <summary>
        /// Gets a point where all values are NAN
        /// </summary>
        public static Point3d NaN { get; } = new Point3d(double.NaN, double.NaN, double.NaN);
        #endregion

        #region Functions
        /// <summary>
        /// Creates a new vector from the point, assuming zero origin
        /// </summary>
        /// <returns>A new vector from the origin to the point</returns>
        [Pure]
        public Vector3d ToVector3d()
        {
            return new Vector3d(this.x, this.y, this.z);
        }

        /// <summary>
        /// Adds a point and a vector together
        /// </summary>
        /// <param name="addend">The vector to add</param>
        /// <returns>A new point at the summed location</returns>
        [Pure]
        public Point3d Add(Vector3d addend)
        {
            return new Point3d(this.x + addend.x, this.y + addend.y, this.z + addend.z);
        }

        /// <summary>
        /// Subtracts the first point from the vector
        /// </summary>
        /// <param name="vector">The vector to subtract</param>
        /// <returns>A new point at the difference</returns>
        [Pure]
        public Point3d Subrtact(Vector3d vector)
        {
            return new Point3d(this.x - vector.x, this.y - vector.y, this.z - vector.z);
        }
        #endregion

        #region IEquatable<Point3d> interface
        /// <summary>
        /// Returns a value to indicate if a pair of points are equal
        /// </summary>
        /// <param name="other">The point to compare against.</param>
        /// <param name="tolerance">A tolerance (epsilon) to adjust for floating point error</param>
        /// <returns>True if the points are equal; otherwise false</returns>
        [Pure]
        public bool Equals(Point3d other, double tolerance)
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
        public bool Equals(Point3d other) => this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z);

        /// <inheritdoc />
        [Pure]
        public override bool Equals(object obj) => obj is Point3d p && this.Equals(p);
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
                int hash = 81173;
                // Suitable nullity checks etc, of course :)
                hash = hash * 96527 + x.GetHashCode();
                hash = hash * 96527 + y.GetHashCode();
                hash = hash * 96527 + z.GetHashCode();
                return hash;
            }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Adds a point and a vector together
        /// </summary>
        /// <param name="point">A point</param>
        /// <param name="vector">A vector</param>
        /// <returns>A new point at the summed location</returns>
        [Pure]
        public static Point3d operator +(Point3d point, Vector3d vector) => new Point3d(point.x + vector.x, point.y + vector.y, point.z + vector.z);

        /// <summary>
        /// Subtracts a vector from a point
        /// </summary>
        /// <param name="point">A point</param>
        /// <param name="vector">A vector</param>
        /// <returns>A new point at the difference</returns>
        [Pure]
        public static Point3d operator -(Point3d point, Vector3d vector) => new Point3d(point.x - vector.x, point.y - vector.y, point.z - vector.z);

        /// <summary>
        /// Returns a value that indicates whether each pair of elements in two specified points is equal.
        /// </summary>
        /// <param name="left">The first point to compare</param>
        /// <param name="right">The second point to compare</param>
        /// <returns>True if the points are the same; otherwise false.</returns>
        [Pure]
        public static bool operator ==(Point3d left, Point3d right) => left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether any pair of elements in two specified points is not equal.
        /// </summary>
        /// <param name="left">The first point to compare</param>
        /// <param name="right">The second point to compare</param>
        /// <returns>True if the points are different; otherwise false.</returns>
        [Pure]
        public static bool operator !=(Point3d left, Point3d right) => !left.Equals(right);

        #endregion

        #region Parse
        public static bool TryParse(string text, out Point3d result) => TryParse(text, null, out result);

        public static bool TryParse(string text, IFormatProvider formatProvider, out Point3d result)
        {
            bool retValue = false;
            result = Point3d.NaN;

            if (TryParse(text, formatProvider, out var x, out var y, out var z))
            {
                result = new Point3d(x, y, z);
                retValue = true;
            }
            return retValue;
        }

        public static bool TryParse(string text, IFormatProvider formatProvider, out double x, out double y, out double z)
        {
            bool retValue = false;
            x = default;
            y = default;
            z = default;

            // right now this is elcheapo - only handles (1.1, 2.4, 3.2) format
            string temp = text.Replace("(", "").Replace(")", "");
            string[] values = temp.Split(",");

            if (values.Length == 3 && 
                double.TryParse(values[0], out x) && 
                double.TryParse(values[1], out y) && 
                double.TryParse(values[2], out z))
                    retValue = true;

            return retValue;
        }

        #endregion

    }
}
