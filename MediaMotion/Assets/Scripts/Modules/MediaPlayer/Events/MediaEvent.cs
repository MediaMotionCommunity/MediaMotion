using System;

namespace MediaMotion.Modules.MediaPlayer.Events {
	public class MediaEventArgs : EventArgs {
		public Element Element { get; }

		public MediaEventArgs(Element Element) {
			this.Element = Element;
		}
	}
}
