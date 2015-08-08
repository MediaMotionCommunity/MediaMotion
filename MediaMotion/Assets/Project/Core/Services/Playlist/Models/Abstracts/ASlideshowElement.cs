using System.Collections;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models {
	/// <summary>
	/// Slideshow Element
	/// </summary>
	public abstract class ASlideshowElement<Module, Child> : ASlideshowAnimator<Module, Child>, ISlideshowElement
		where Module : class, IModule
		where Child : ASlideshowElement<Module, Child> {
		/// <summary>
		/// The destroy
		/// </summary>
		private bool disable;

		/// <summary>
		/// Animate to.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="disable">if set to <c>true</c> [disable].</param>
		/// <param name="time">The time.</param>
		public void AnimateTo(Vector3 scale, Vector3 position, Quaternion rotation, bool disable = false, float time = 0.3f) {
			this.Configure(scale, position, rotation, time);
			this.disable = disable;
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			if (!this.IsFinnished && this.Animate() && this.disable) {
				this.gameObject.SetActive(false);
			}
		}
	}

	/// <summary>
	/// Slideshow Element
	/// </summary>
	public abstract class ASlideshowElement<Module> : ASlideshowElement<Module, ASlideshowElement<Module>>, ISlideshowElement
		where Module : class, IModule {
	}
}
