using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers {
	/// <summary>
	/// Progress bar controller
	/// </summary>
	public class ProgressBarController : MonoBehaviour {
		/// <summary>
		/// The ratio
		/// </summary>
		private float ratio;

		/// <summary>
		/// Gets or sets the ratio.
		/// </summary>
		/// <value>
		/// The ratio.
		/// </value>
		public float Ratio {
			get {
				return (this.ratio);
			}
			set {
				float size = 5.0f * value;
				Vector3 position = this.transform.localPosition;
				Vector3 scale = this.transform.localScale;

				position.x = 5.0f - size;
				scale.y = size;
				
				this.transform.localPosition = position;
				this.transform.localScale = scale;

				this.ratio = value;
			}
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.Colorate((int)(Time.timeSinceLevelLoad * 10));
		}

		/// <summary>
		/// Gets the color.
		/// </summary>
		/// <param name="color">The color.</param>
		/// <returns>
		/// The color
		/// </returns>
		private float ToColor(int color) {
			return ((float)color / 255);
		}

		/// <summary>
		/// Computes the red value.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="amplitude">The amplitude.</param>
		/// <param name="minimal">The minimal.</param>
		/// <returns>
		/// The red value
		/// </returns>
		private int ComputeRedValue(int index, int amplitude, int minimal) {
			if (index < amplitude || (index >= (amplitude * 5))) {
				return (minimal + amplitude);
			}
			if (index >= amplitude && index < (amplitude * 2)) {
				return (minimal + (amplitude - (index % amplitude)));
			}
			if (index >= (amplitude * 4) && index < (amplitude * 5)) {
				return (minimal + (index % amplitude));
			}
			return (minimal);
		}

		/// <summary>
		/// Computes the green value.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="amplitude">The amplitude.</param>
		/// <param name="minimal">The minimal.</param>
		/// <returns>
		/// The green value
		/// </returns>
		private int ComputeGreenValue(int index, int amplitude, int minimal) {
			if (index >= amplitude && index < (amplitude * 3)) {
				return (minimal + amplitude);
			}
			if (index >= (amplitude * 3) && index < (amplitude * 4)) {
				return (minimal + (amplitude - (index % amplitude)));
			}
			if (index < amplitude) {
				return (minimal + (index % amplitude));
			}
			return (minimal);
		}

		/// <summary>
		/// Computes the blue value.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="amplitude">The amplitude.</param>
		/// <param name="minimal">The minimal.</param>
		/// <returns>
		/// The blue value
		/// </returns>
		private int ComputeBlueValue(int index, int amplitude, int minimal) {
			if (index >= (amplitude * 3) && index < (amplitude * 5)) {
				return (minimal + amplitude);
			}
			if (index >= (amplitude * 5) && index < (amplitude * 6)) {
				return (minimal + (amplitude - (index % amplitude)));
			}
			if (index >= (amplitude * 2) && index < (amplitude * 3)) {
				return (minimal + (index % amplitude));
			}
			return (minimal);
		}

		/// <summary>
		/// Computes the color.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="amplitude">The amplitude.</param>
		/// <param name="minimal">The minimal.</param>
		/// <param name="alpha">The alpha.</param>
		private void Colorate(int index, int amplitude = 50, int minimal = 50, int alpha = 255) {
			Renderer renderer = this.gameObject.GetComponent<Renderer>();

			if (renderer != null) {
				index %= (amplitude * 6);
				renderer.material.color = new Color(this.ToColor(this.ComputeRedValue(index, amplitude, minimal)), this.ToColor(this.ComputeGreenValue(index, amplitude, minimal)), this.ToColor(this.ComputeBlueValue(index, amplitude, minimal)), alpha);
			}
		}
	}
}
