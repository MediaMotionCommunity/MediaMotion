using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models {
	/// <summary>
	/// Slideshow animator
	/// </summary>
	public abstract class ASlideshowAnimator<Module, Child> : ASlideshowEnvironment<Module, Child>, ISlideshowAnimator
		where Module : class, IModule
		where Child : ASlideshowAnimator<Module, Child> {
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
		/// The time
		/// </summary>
		private float totalTime;

		/// <summary>
		/// The elapsed time
		/// </summary>
		private float elapsedTime;

		/// <summary>
		/// Gets a value indicating whether this instance is finnished.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is finnished; otherwise, <c>false</c>.
		/// </value>
		public bool IsFinnished {
			get {
				return (this.elapsedTime >= this.totalTime);
			}
		}

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

				this.totalTime = 0.0f;
				this.elapsedTime = 1.0f;
			}
		}

		/// <summary>
		/// Configure
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="time">The time.</param>
		public void Configure(Vector3 scale = default(Vector3), Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), float time = 0.3f) {
			lock (this.locker) {
				this.destinationScale = scale;
				this.destinationPosition = position;
				this.destinationRotation = rotation;

				if (this.destinationScale != default(Vector3)) {
					this.differenceScale = this.destinationScale - this.gameObject.transform.localScale;
				}
				if (this.destinationPosition != default(Vector3)) {
					this.differencePosition = this.destinationPosition - this.gameObject.transform.localPosition;
				}
				if (this.destinationRotation != default(Quaternion)) {
					this.differenceRotation = this.destinationRotation.eulerAngles - this.gameObject.transform.localRotation.eulerAngles;
					this.differenceRotation.x = Mathf.Repeat(this.differenceRotation.x + 180.0f, 360.0f) - 180.0f;
					this.differenceRotation.y = Mathf.Repeat(this.differenceRotation.y + 180.0f, 360.0f) - 180.0f;
					this.differenceRotation.z = Mathf.Repeat(this.differenceRotation.z + 180.0f, 360.0f) - 180.0f;
				}
				this.totalTime = time;
				this.elapsedTime = 0.0f;
			}
		}

		/// <summary>
		/// Animates the object.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the animation is terminated, <c>false otherwise</c>
		/// </returns>
		public bool Animate() {
			lock (this.locker) {
				if (this.gameObject != null) {
					if (!this.IsFinnished) {
						float coefficient = Time.deltaTime / this.totalTime;

						if (this.destinationScale != default(Vector3)) {
							this.gameObject.transform.localScale += this.differenceScale * coefficient;
						}
						if (this.destinationRotation != default(Quaternion)) {
							this.gameObject.transform.localRotation = Quaternion.Euler(this.gameObject.transform.localRotation.eulerAngles + (this.differenceRotation * coefficient));
						}
						if (this.destinationPosition != default(Vector3)) {
							this.gameObject.transform.localPosition += this.differencePosition * coefficient;
						}

						if ((this.elapsedTime += Time.deltaTime) >= this.totalTime) {
							if (this.destinationScale != default(Vector3)) {
								this.gameObject.transform.localScale = this.destinationScale;
							}
							if (this.destinationRotation != default(Quaternion)) {
								this.gameObject.transform.localRotation = this.destinationRotation;
							}
							if (this.destinationPosition != default(Vector3)) {
								this.gameObject.transform.localPosition = this.destinationPosition;
							}
							return (true);
						}
						return (false);
					}
					return (true);
				}
				return (false);
			}
		}
	}

	/// <summary>
	/// Slideshow animator
	/// </summary>
	public abstract class ASlideshowAnimator<Module> : ASlideshowAnimator<Module, ASlideshowAnimator<Module>>, ISlideshowAnimator
		where Module : class, IModule {
	}
}
