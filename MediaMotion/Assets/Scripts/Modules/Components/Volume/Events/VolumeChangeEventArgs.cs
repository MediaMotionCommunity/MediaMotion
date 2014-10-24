using System;

namespace MediaMotion.Modules.Components.Volume.Events {
	public class VolumeChangeEventArgs : EventArgs {
		public int Volume { get; private set; }

		public VolumeChangeEventArgs(int NewVolume) {
			Volume = NewVolume;
		}
	}
}
