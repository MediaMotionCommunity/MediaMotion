using System;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Modules.Components.Playlist.Events {
	/// <summary>
	/// Event Handler Playlist Element Change Event
	/// </summary>
	public class PlaylistElementChangeEventArgs : EventArgs {
		/// <summary>
		/// Initializes a new instance of the <see cref="PlaylistElementChangeEventArgs"/> class.
		/// </summary>
		/// <param name="Previous">The previous.</param>
		/// <param name="Current">The current.</param>
		public PlaylistElementChangeEventArgs(IElement Previous, IElement Current) {
			this.Previous = Previous;
			this.Current = Current;
		}

		/// <summary>
		/// Gets the previous.
		/// </summary>
		/// <value>
		/// The previous.
		/// </value>
		public IElement Previous { get; private set; }

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>
		/// The current.
		/// </value>
		public IElement Current { get; private set; }
	}
}