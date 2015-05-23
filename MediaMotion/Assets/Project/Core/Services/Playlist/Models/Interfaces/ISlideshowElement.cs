using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Interfaces {
	/// <summary>
	/// Slideshow Element Interface
	/// </summary>
	public interface ISlideshowElement {
		/// <summary>
		/// Gets or sets the offset.
		/// </summary>
		/// <value>
		/// The offset.
		/// </value>
		int Offset { get; set; }

		/// <summary>
		/// Transforms to.
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="destroy">if set to <c>true</c> [destroy].</param>
		/// <param name="time">The time.</param>
		void TransformTo(Vector3 scale, Vector3 position, Quaternion rotation, bool destroy = false, float time = 0.3f);
	}
}
