using System.Collections;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models {
	/// <summary>
	/// Slideshow Element
	/// </summary>
	public class SlideshowElement : MonoBehaviour, ISlideshowElement {
		/// <summary>
		/// Gets or sets the offset.
		/// </summary>
		/// <value>
		/// The offset.
		/// </value>
		public int Offset { get; set; }

		/// <summary>
		/// The dest scale
		/// </summary>
		private Vector3 destScale;

		/// <summary>
		/// To scale
		/// </summary>
		private Vector3 diffScale;

		/// <summary>
		/// The dest position
		/// </summary>
		private Vector3 destPosition;

		/// <summary>
		/// The difference position
		/// </summary>
		private Vector3 diffPosition;

		/// <summary>
		/// The dest rotation
		/// </summary>
		private Quaternion destRotation;

		/// <summary>
		/// The difference rotation
		/// </summary>
		private Vector3 diffRotation;

		/// <summary>
		/// The destroy
		/// </summary>
		private bool destroy;

		/// <summary>
		/// The time
		/// </summary>
		private float totalTime;

		/// <summary>
		/// The elapsed time
		/// </summary>
		private float elapsedTime;

		/// <summary>
		/// Transforms to.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="destroy">if set to <c>true</c> [destroy].</param>
		/// <param name="time">The time.</param>
		public void TransformTo(Vector3 scale, Vector3 position, Quaternion rotation, bool destroy = false, float time = 0.3f) {
			this.destScale = scale;
			this.destPosition = position;
			this.destRotation = rotation;

			this.diffScale = this.destScale - this.gameObject.transform.localScale;
			this.diffPosition = this.destPosition - this.gameObject.transform.localPosition;
			this.diffRotation = this.destRotation.eulerAngles - this.gameObject.transform.localRotation.eulerAngles;

			/* shortest rotation */
			this.diffRotation.x = Mathf.Repeat(this.diffRotation.x + 180.0f, 360.0f) - 180.0f;
			this.diffRotation.y = Mathf.Repeat(this.diffRotation.y + 180.0f, 360.0f) - 180.0f;
			this.diffRotation.z = Mathf.Repeat(this.diffRotation.z + 180.0f, 360.0f) - 180.0f;

			this.destroy = destroy;
			this.totalTime = time;
			this.elapsedTime = 0.0f;
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			if (this.elapsedTime < this.totalTime) {
				float coefficient = Time.deltaTime / this.totalTime;

				this.gameObject.transform.localScale += this.diffScale * coefficient;
				this.gameObject.transform.localRotation = Quaternion.Euler(this.gameObject.transform.localRotation.eulerAngles + this.diffRotation * coefficient);
				this.gameObject.transform.localPosition += this.diffPosition * coefficient;
				
				if ((this.elapsedTime += Time.deltaTime) >= this.totalTime) {
					if (this.destroy) {
						GameObject.Destroy(this.gameObject);
						return ;
					}
					this.gameObject.transform.localScale = this.destScale;
					this.gameObject.transform.localRotation = this.destRotation;
					this.gameObject.transform.localPosition = this.destPosition;
				}
			}
		}
	}
}
