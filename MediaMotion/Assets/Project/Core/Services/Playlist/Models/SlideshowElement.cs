using System.Collections;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models {
	/// <summary>
	/// Slideshow Element
	/// </summary>
	public class SlideshowElement : MonoBehaviour, ISlideshowElement {
		/// <summary>
		/// The locker
		/// </summary>
		private readonly object locker = new object();

		/// <summary>
		/// The destination scale
		/// </summary>
		private Vector3 destinationScale;

		/// <summary>
		/// The difference scale
		/// </summary>
		private Vector3 differenceScale;

		/// <summary>
		/// The destination position
		/// </summary>
		private Vector3 destinationPosition;

		/// <summary>
		/// The difference position
		/// </summary>
		private Vector3 differencePosition;

		/// <summary>
		/// The destination rotation
		/// </summary>
		private Quaternion destinationRotation;

		/// <summary>
		/// The difference rotation
		/// </summary>
		private Vector3 differenceRotation;

		/// <summary>
		/// The destroy
		/// </summary>
		private bool disable;

		/// <summary>
		/// The time
		/// </summary>
		private float totalTime;

		/// <summary>
		/// The elapsed time
		/// </summary>
		private float elapsedTime;

		/// <summary>
		/// Reload the script
		/// </summary>
		public void Reload() {
			lock (this.locker) {
				this.destinationScale = default(Vector3);
				this.destinationPosition = default(Vector3);
				this.destinationRotation = default(Quaternion);

				this.differenceScale = default(Vector3);
				this.differencePosition = default(Vector3);
				this.differenceRotation = default(Vector3);

				this.disable = default(bool);
				this.totalTime = 0.0f;
				this.elapsedTime = 1.0f;
			}
		}

		/// <summary>
		/// Transforms to.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="disable">if set to <c>true</c> [destroy].</param>
		/// <param name="time">The time.</param>
		public void AnimateTo(Vector3 scale, Vector3 position, Quaternion rotation, bool disable = false, float time = 0.3f) {
			lock (this.locker) {
				this.destinationScale = scale;
				this.destinationPosition = position;
				this.destinationRotation = rotation;

				this.differenceScale = this.destinationScale - this.gameObject.transform.localScale;
				this.differencePosition = this.destinationPosition - this.gameObject.transform.localPosition;
				this.differenceRotation = this.destinationRotation.eulerAngles - this.gameObject.transform.localRotation.eulerAngles;

				/* shortest rotation */
				this.differenceRotation.x = Mathf.Repeat(this.differenceRotation.x + 180.0f, 360.0f) - 180.0f;
				this.differenceRotation.y = Mathf.Repeat(this.differenceRotation.y + 180.0f, 360.0f) - 180.0f;
				this.differenceRotation.z = Mathf.Repeat(this.differenceRotation.z + 180.0f, 360.0f) - 180.0f;

				this.disable = disable;
				this.totalTime = time;
				this.elapsedTime = 0.0f;
			}
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			lock (this.locker) {
				if (this.elapsedTime < this.totalTime) {
					float coefficient = Time.deltaTime / this.totalTime;

					this.gameObject.transform.localScale += this.differenceScale * coefficient;
					this.gameObject.transform.localRotation = Quaternion.Euler(this.gameObject.transform.localRotation.eulerAngles + (this.differenceRotation * coefficient));
					this.gameObject.transform.localPosition += this.differencePosition * coefficient;

					if ((this.elapsedTime += Time.deltaTime) >= this.totalTime) {
						this.gameObject.transform.localScale = this.destinationScale;
						this.gameObject.transform.localRotation = this.destinationRotation;
						this.gameObject.transform.localPosition = this.destinationPosition;
						if (this.disable) {
							this.gameObject.SetActive(false);
						}
					}
				}
			}
		}
	}
}
