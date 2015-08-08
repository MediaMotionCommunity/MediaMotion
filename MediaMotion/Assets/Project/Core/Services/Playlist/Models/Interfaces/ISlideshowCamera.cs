using UnityEngine;
namespace MediaMotion.Core.Services.Playlist.Models.Interfaces {
	/// <summary>
	/// Slideshow camera interface
	/// </summary>
	public interface ISlideshowCamera : ISlideshowEnvironment {
		/// <summary>
		/// Objects to focus.
		/// </summary>
		/// <param name="element">The element.</param>
		void ObjectToFocus(GameObject element, float time = 0.3f);

		/// <summary>
		/// Objects to focus.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="time">The time.</param>
		void ObjectToFocus(Vector3 scale, Vector3 position, Quaternion rotation, float time = 0.3f);
	}
}
