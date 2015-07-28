using System.Collections;
using MediaMotion.Core.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Abstracts {
	/// <summary>
	/// Slideshow floor
	/// </summary>
	/// <typeparam name="Module">The type of the module.</typeparam>
	/// <typeparam name="Child">The type of the child.</typeparam>
	public class SlideshowFloor : ASlideshowEnvironment {
		/// <summary>
		/// Enters the fullscreen.
		/// </summary>
		public override void EnterFullscreen() {
			if (this.Fullscreen == false) {
				this.Rotate(new Vector3(-45.0f, 0.0f, 0.0f));
			}
		}

		/// <summary>
		/// Leaves the fullscreen.
		/// </summary>
		public override void LeaveFullscreen() {
			if (this.Fullscreen == true) {
				this.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
			}
		}

		/// <summary>
		/// Rotates the specified angles.
		/// </summary>
		/// <param name="angles">The angles.</param>
		private void Rotate(Vector3 angles) {
			iTween.Stop(this.gameObject);
			iTween.RotateTo(this.gameObject, angles, 10.0f);
		}
	}
}
