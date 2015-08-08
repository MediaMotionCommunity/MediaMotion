using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Abstracts {
	/// <summary>
	/// Slideshow environment abstract
	/// </summary>
	public class ASlideshowEnvironment<Module, Child> : AScript<Module, Child>, ISlideshowEnvironment
		where Module : class, IModule
		where Child : ASlideshowEnvironment<Module, Child> {
		/// <summary>
		/// The fullscreen
		/// </summary>
		private bool fullscreen;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IFloor" /> is fullscreen.
		/// </summary>
		/// <value>
		///   <c>true</c> if fullscreen; otherwise, <c>false</c>.
		/// </value>
		public bool Fullscreen {
			get {
				return (this.fullscreen);
			}
			set {
				switch (value) {
					case true:
						this.EnterFullscreen();
						break;
					case false:
						this.LeaveFullscreen();
						break;
				}
				this.fullscreen = value;
			}
		}

		/// <summary>
		/// Enters the fullscreen.
		/// </summary>
		public virtual void EnterFullscreen() {
		}

		/// <summary>
		/// Leaves the fullscreen.
		/// </summary>
		public virtual void LeaveFullscreen() {
		}
	}
}
