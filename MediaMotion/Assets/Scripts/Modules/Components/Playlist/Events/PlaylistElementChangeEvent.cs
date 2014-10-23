using System;

namespace MediaMotion.Modules.Components.Playlist.Events {
	public class PlaylistElementChangeEvent : EventArgs {
		public Element Previous { get; private set; }
		public Element Current { get; private set; }

		PlaylistElementChangeEvent(Element Previous, Element Current) {
			this.Previous = Previous;
			this.Current = Current;
		}
	}
}