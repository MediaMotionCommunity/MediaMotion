using System;

namespace MediaMotion.Modules.MediaPlayer.Events {
	public class VolumeChangeEventArgs : EventArgs {
		public int Volume { get; private set; }

		public VolumeChangeEventArgs(int NewVolume) {
			Volume = NewVolume;
		}
	}
}
