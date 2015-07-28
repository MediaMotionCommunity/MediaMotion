using System.Collections;
using MediaMotion.Core.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Abstracts {
	/// <summary>
	/// Slideshow floor
	/// </summary>
	/// <typeparam name="Module">The type of the module.</typeparam>
	/// <typeparam name="Child">The type of the child.</typeparam>
	public class SlideshowBackground : ASlideshowEnvironment {
		/// <summary>
		/// Enters the fullscreen.
		/// </summary>
		public override void EnterFullscreen() {
			if (this.Fullscreen == false) {
				this.Colorate(new Color(0, 0, 0, 1));
			}
		}

		/// <summary>
		/// Leaves the fullscreen.
		/// </summary>
		public override void LeaveFullscreen() {
			if (this.Fullscreen == true) {
				this.Colorate(new Color(0, 0, 0, 0));
			}
		}

		/// <summary>
		/// Colorates the specified color.
		/// </summary>
		/// <param name="color">The color.</param>
		private void Colorate(Color color) {
			iTween.Stop(this.gameObject);
			iTween.ColorTo(this.gameObject, color, 1.5f);
		}
	}
}
