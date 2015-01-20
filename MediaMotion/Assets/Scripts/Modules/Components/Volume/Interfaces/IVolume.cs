namespace MediaMotion.Modules.Components.Volume {
	/// <summary>
	/// Volume Interface
	/// </summary>
	public interface IVolume {
		/// <summary>
		/// Gets the volume.
		/// </summary>
		/// <value>
		/// The sound.
		/// </value>
		int Sound { get; }

		/// <summary>
		/// Gets the step.
		/// </summary>
		/// <value>
		/// The step.
		/// </value>
		int Step { get; }

		/// <summary>
		/// Volumes up.
		/// </summary>
		void VolumeUp();

		/// <summary>
		/// Volumes down.
		/// </summary>
		void VolumeDown();
	}
}
