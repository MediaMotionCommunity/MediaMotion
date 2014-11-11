using System;
using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Modules.Components.Playlist.Events {
	public class PlaylistChangeEventArgs : EventArgs {
		public List<IElement> Elements { get; private set; }

		public PlaylistChangeEventArgs(List<IElement> Elements) {
			this.Elements = Elements;
		}
	}
}
