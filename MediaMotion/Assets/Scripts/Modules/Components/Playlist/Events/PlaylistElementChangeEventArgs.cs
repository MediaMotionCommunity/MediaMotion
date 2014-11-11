using System;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Modules.Components.Playlist.Events {
	public class PlaylistElementChangeEventArgs : EventArgs {
		private IElement Previous1;
		private IElement element;

		public IElement Previous { get; private set; }
		public IElement Current { get; private set; }

		public PlaylistElementChangeEventArgs(IElement Previous, IElement Current) {
			this.Previous = Previous;
			this.Current = Current;
		}
	}
}