namespace MediaMotion.Modules.Components.Volume {
	public interface IVolume {
		int Sound { get; }
		int Step { get; }

		// Volume
		void VolumeUp();
		void VolumeDown();
	}
}
