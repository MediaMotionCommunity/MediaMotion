using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models {
	/// <summary>
	/// Slideshow Element
	/// </summary>
	public class SlideshowCamera<Module, Child> : ASlideshowAnimator<Module, Child>, ISlideshowCamera
		where Module : class, IModule
		where Child : SlideshowCamera<Module, Child> {
		/// <summary>
		/// Objects to focus.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="time"></param>
		public void ObjectToFocus(GameObject element, float time = 0.3f) {
			this.ObjectToFocus(element.transform.localScale, element.transform.localPosition, element.transform.localRotation, time);
		}

		/// <summary>
		/// Objects to focus.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="time">The time.</param>
		public void ObjectToFocus(Vector3 scale, Vector3 position, Quaternion rotation, float time = 0.3f) {
			this.Configure(default(Vector3), new Vector3(position.x, position.y, position.z + 4.0f), default(Quaternion), time);
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.Animate();
		}
	}

	/// <summary>
	/// Slideshow Element
	/// </summary>
	public class SlideshowCamera<Module> : SlideshowCamera<Module, SlideshowCamera<Module>>
		where Module : class, IModule {
	}
}
