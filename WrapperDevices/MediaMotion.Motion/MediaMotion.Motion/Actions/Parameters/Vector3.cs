using System;

namespace MediaMotion.Motion.Actions.Parameters {

	/// <summary>
	/// 3D vector representation
	/// </summary>
	public class Vector3 {
		public double x, y, z;

		public Vector3(double x, double y, double z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3(Vector3 v) {
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}

		public static Vector3 operator *(Vector3 v, float scale) {
			return new Vector3(
				v.x * scale,
				v.y * scale,
				v.z * scale
			);
		}

		public static Vector3 operator /(Vector3 v, float scale) {
			return new Vector3(
				v.x / scale,
				v.y / scale,
				v.z / scale
			);
		}

		public static Vector3 operator +(Vector3 v, Vector3 b) {
			return new Vector3(
				v.x + b.x,
				v.y + b.y,
				v.z + b.z
			);
		}

		public static Vector3 operator -(Vector3 v, Vector3 b) {
			return new Vector3(
				v.x - b.x,
				v.y - b.y,
				v.z - b.z
			);
		}

		public override string ToString() {
			return "Vector(X: " + Math.Round(this.x, 1).ToString()
				+ ", Y: " + Math.Round(this.y, 1).ToString()
				+ ", Z: " + Math.Round(this.z, 1).ToString() + ")";
		}
	}
}
