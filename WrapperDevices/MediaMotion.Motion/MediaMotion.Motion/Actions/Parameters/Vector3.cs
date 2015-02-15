using System;

namespace MediaMotion.Motion.Actions.Parameters {
	/// <summary>
	/// 3D vector representation
	/// </summary>
	public class Vector3 : IVector3 {
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3"/> class.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <param name="z">The z.</param>
		public Vector3(float x, float y, float z) {
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3"/> class.
		/// </summary>
		/// <param name="v">The v.</param>
		public Vector3(IVector3 v) {
			this.X = v.X;
			this.Y = v.Y;
			this.Z = v.Z;
		}

		/// <summary>
		/// Gets or sets the x.
		/// </summary>
		/// <value>
		/// The x.
		/// </value>
		public float X { get; set; }

		/// <summary>
		/// Gets or sets the y.
		/// </summary>
		/// <value>
		/// The y.
		/// </value>
		public float Y { get; set; }

		/// <summary>
		/// Gets or sets the z.
		/// </summary>
		/// <value>
		/// The z.
		/// </value>
		public float Z { get; set; }

		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="v">The v.</param>
		/// <param name="scale">The scale.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Vector3 operator *(Vector3 v, float scale) {
			return new Vector3(
				v.X * scale,
				v.Y * scale,
				v.Z * scale);
		}

		/// <summary>
		/// Implements the operator /.
		/// </summary>
		/// <param name="v">The v.</param>
		/// <param name="scale">The scale.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Vector3 operator /(Vector3 v, float scale) {
			return new Vector3(
				v.X / scale,
				v.Y / scale,
				v.Z / scale);
		}

		/// <summary>
		/// Implements the operator +.
		/// </summary>
		/// <param name="v">The v.</param>
		/// <param name="b">The b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Vector3 operator +(Vector3 v, Vector3 b) {
			return new Vector3(
				v.X + b.X,
				v.Y + b.Y,
				v.Z + b.Z);
		}

		/// <summary>
		/// Implements the operator -.
		/// </summary>
		/// <param name="v">The v.</param>
		/// <param name="b">The b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Vector3 operator -(Vector3 v, Vector3 b) {
			return new Vector3(
				v.X - b.X,
				v.Y - b.Y,
				v.Z - b.Z);
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString() {
			return "Vector(X: " + Math.Round(this.X, 1)
				+ ", Y: " + Math.Round(this.Y, 1)
				+ ", Z: " + Math.Round(this.Z, 1) + ")";
		}
	}
}
