using System;
using System.Collections.Generic;

namespace MediaMotion.Modules.Components.Playlist.Events {
	public class PlaylistChangeEventArgs : EventArgs {
		public List<Element> Elements { get; }

		public PlaylistChangeEventArgs(List<Element> Elements) {
			this.Elements = Elements;
		}
	}
}
