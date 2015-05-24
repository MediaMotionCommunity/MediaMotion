using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Interfaces {
	/// <summary>
	/// Slideshow Element Interface
	/// </summary>
	public interface ISlideshowElement {
		/// <summary>
		/// Reload the script
		/// </summary>
		void Reload();

		/// <summary>
		/// Animate to.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="destroy">if set to <c>true</c> [destroy].</param>
		/// <param name="time">The time.</param>
		void AnimateTo(Vector3 scale, Vector3 position, Quaternion rotation, bool destroy = false, float time = 0.3f);
	}
}
