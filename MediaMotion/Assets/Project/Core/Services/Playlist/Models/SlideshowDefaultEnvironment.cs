using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models {
	/// <summary>
	/// Slideshow default Environment
	/// </summary>
	public sealed class SlideshowDefaultEnvironment : MonoBehaviour, ISlideshowEnvironment {
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IFloor" /> is fullscreen.
		/// </summary>
		/// <value>
		///   <c>true</c> if fullscreen; otherwise, <c>false</c>.
		/// </value>
		public bool Fullscreen { get; set; }
	}
}
