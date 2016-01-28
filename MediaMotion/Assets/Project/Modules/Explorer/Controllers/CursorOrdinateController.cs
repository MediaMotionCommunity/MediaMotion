using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Cursor ordinate controller
	/// </summary>
	public class CursorOrdinateController : MonoBehaviour {
		/// <summary>
		/// The i
		/// </summary>
		private float i;

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start() {
			this.i = -0.001f;
		}

		/// <summary>
		/// Scales this instance.
		/// </summary>
		private void Scale() {
			Vector3 scale = this.gameObject.transform.localScale;

			if (scale.x > 0.15f || scale.x < 0.02f) {
				this.i *= -1.0f;
			}
			scale.x += this.i;
			scale.z = scale.x;
			this.gameObject.transform.localScale = scale;
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.Scale();
		}
	}
}
