using System;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Modules.MediaPlayer.Events {
	public class MediaEventArgs : EventArgs {
		public IElement Element { get; private set; }

		public MediaEventArgs(IElement Element) {
			this.Element = Element;
		}
	}
}
