using System;

namespace MediaMotion.Modules.Components.Playlist.Events {
	public class PlaylistElementChangeEventArgs : EventArgs {
		public Element Previous { get; private set; }
		public Element Current { get; private set; }

		PlaylistElementChangeEventArgs(Element Previous, Element Current) {
			this.Previous = Previous;
			this.Current = Current;
		}
	}
}