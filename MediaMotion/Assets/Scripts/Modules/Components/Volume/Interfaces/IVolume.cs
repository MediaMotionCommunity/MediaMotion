namespace MediaMotion.Modules.Components.Volume {
	public interface IVolume {
		int Sound { get; }
		int Step { get; }

		void VolumeUp();
		void VolumeDown();
	}
}
