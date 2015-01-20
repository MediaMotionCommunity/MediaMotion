using System;
using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Modules.Components.Playlist.Events {
	/// <summary>
	/// Event Handler Playlist Change Event
	/// </summary>
	public class PlaylistChangeEventArgs : EventArgs {
		/// <summary>
		/// Initializes a new instance of the <see cref="PlaylistChangeEventArgs"/> class.
		/// </summary>
		/// <param name="Elements">The elements.</param>
		public PlaylistChangeEventArgs(List<IElement> Elements) {
			this.Elements = Elements;
		}

		/// <summary>
		/// Gets the elements.
		/// </summary>
		/// <value>
		/// The elements.
		/// </value>
		public List<IElement> Elements { get; private set; }
	}
}
