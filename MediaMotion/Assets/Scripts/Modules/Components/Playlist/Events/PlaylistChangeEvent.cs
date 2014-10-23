using System;
using System.Collections.Generic;

namespace MediaMotion.Modules.Components.Playlist.Events {
	public class PlaylistChangeEvent : EventArgs {
		public List<Element> NewElements { get; }

		public PlaylistChangeEvent(List<Element> NewElements) {
			this.NewElements = NewElements;
		}
	}
}
