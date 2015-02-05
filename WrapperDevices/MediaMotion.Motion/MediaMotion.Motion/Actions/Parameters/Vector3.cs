using System;

namespace MediaMotion.Motion.Actions.Parameters {
	/// <summary>
	/// 3D vector representation
	/// </summary>
	public class Vector3 : IVector3 {
		public Vector3(double x, double y, double z) {
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public Vector3(IVector3 v) {
			this.X = v.X;
			this.Y = v.Y;
			this.Z = v.Z;
		}

		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public static Vector3 operator *(Vector3 v, float scale) {
			return new Vector3(
				v.X * scale,
				v.Y * scale,
				v.Z * scale);
		}

		public static Vector3 operator /(Vector3 v, float scale) {
			return new Vector3(
				v.X / scale,
				v.Y / scale,
				v.Z / scale);
		}

		public static Vector3 operator +(Vector3 v, Vector3 b) {
			return new Vector3(
				v.X + b.X,
				v.Y + b.Y,
				v.Z + b.Z);
		}

		public static Vector3 operator -(Vector3 v, Vector3 b) {
			return new Vector3(
				v.X - b.X,
				v.Y - b.Y,
				v.Z - b.Z);
		}

		public override string ToString() {
			return "Vector(X: " + Math.Round(this.X, 1)
				+ ", Y: " + Math.Round(this.Y, 1)
				+ ", Z: " + Math.Round(this.Z, 1) + ")";
		}
	}
}
