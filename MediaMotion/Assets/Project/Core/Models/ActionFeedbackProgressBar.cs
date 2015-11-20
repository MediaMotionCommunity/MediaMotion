using System;
using UnityEngine;

namespace MediaMotion.Core.Models {
	/// <summary>
	/// Action Feedback Progress Bar
	/// </summary>
	public class ActionFeedbackProgressBar : MonoBehaviour {
		/// <summary>
		/// The start color
		/// </summary>
		private Color start = new Color(0.0275f, 0.0275f, 0.73922f);

		/// <summary>
		/// The end color
		/// </summary>
		private Color end = new Color(0.0275f, 0.73922f, 0.0275f);

		/// <summary>
		/// The cancel color
		/// </summary>
		private Color cancel = new Color(0.73922f, 0.0275f, 0.0275f);

		/// <summary>
		/// The action duration
		/// </summary>
		private float actionDuration;

		/// <summary>
		/// The duration
		/// </summary>
		private float duration;

		/// <summary>
		/// The ratio
		/// </summary>
		private float ratio;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionFeedbackProgressBar"/> is cancel.
		/// </summary>
		/// <value>
		///   <c>true</c> if cancel; otherwise, <c>false</c>.
		/// </value>
		public bool Cancel { get; set; }

		/// <summary>
		/// Initializes the specified action duration.
		/// </summary>
		/// <param name="actionDuration">Duration of the action.</param>
		/// <param name="delta">delta</param>
		public void Init(TimeSpan actionDuration, float delta) {
			this.actionDuration = ((float)actionDuration.Seconds + ((float)actionDuration.Milliseconds / 1000)) - delta;
			this.duration = 0.0f;
			this.ratio = 0.0f;
			this.Cancel = false;
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			if (this.duration < this.actionDuration) {
				if (!this.Cancel) {
					this.duration += Time.fixedDeltaTime;
				}
				this.ratio = this.duration / this.actionDuration;
				this.Resize();
				this.Colorate();
			}
		}

		/// <summary>
		/// Resizes this instance.
		/// </summary>
		private void Resize() {
			Vector3 scale = this.gameObject.transform.localScale;

			scale.y = this.ratio;
			this.gameObject.transform.localScale = scale;
		}

		/// <summary>
		/// Colorates this instance.
		/// </summary>
		private void Colorate() {
			Renderer renderer = this.gameObject.GetComponent<Renderer>();

			if (renderer != null) {
				Color color;

				if (this.Cancel) {
					color = this.cancel;
				} else {
					float r = this.end.r - this.start.r;
					float g = this.end.g - this.start.g;
					float b = this.end.b - this.start.b;

					color = new Color(this.start.r + (r * ratio), this.start.g + (g * ratio), this.start.b + (b * ratio));
				}
				renderer.material.color = color;
			}
		}
	}
}
