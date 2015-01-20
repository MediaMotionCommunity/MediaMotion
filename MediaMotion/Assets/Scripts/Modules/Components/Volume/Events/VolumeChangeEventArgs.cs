using System;

namespace MediaMotion.Modules.Components.Volume.Events {
	/// <summary>
	/// Volume Change Event Args
	/// </summary>
	public class VolumeChangeEventArgs : EventArgs {
		/// <summary>
		/// Initializes a new instance of the <see cref="VolumeChangeEventArgs"/> class.
		/// </summary>
		/// <param name="NewVolume">The new volume.</param>
		public VolumeChangeEventArgs(int NewVolume) {
			this.Volume = NewVolume;
		}

		/// <summary>
		/// Gets the volume.
		/// </summary>
		/// <value>
		/// The volume.
		/// </value>
		public int Volume { get; private set; }
	}
}
