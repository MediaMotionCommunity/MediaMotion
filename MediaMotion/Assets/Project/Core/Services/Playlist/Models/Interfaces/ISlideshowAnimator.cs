using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Interfaces {
	/// <summary>
	/// Slideshow animator interface
	/// </summary>
	public interface ISlideshowAnimator {
		/// <summary>
		/// Gets a value indicating whether this instance is finnished.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is finnished; otherwise, <c>false</c>.
		/// </value>
		bool IsFinnished { get; }

		/// <summary>
		/// Reloads the animation.
		/// </summary>
		void Reload();

		/// <summary>
		/// Configure the animation
		/// </summary>
		/// <param name="scale">The scale.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="time">The time.</param>
		void Configure(Vector3 scale = default(Vector3), Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), float time = 0.3f);

		/// <summary>
		/// Animates the object.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the animation is terminated, <c>false otherwise</c>
		/// </returns>
		bool Animate();
	}
}
