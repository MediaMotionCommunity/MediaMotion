using MediaMotion.Modules.Components.Volume.Events;

namespace MediaMotion.Modules.Components.Volume {
	/// <summary>
	/// Volume Model
	/// </summary>
	public class Volume : IVolume {
		/// <summary>
		/// Initializes a new instance of the <see cref="Volume"/> class.
		/// </summary>
		public Volume() {
			this.Step = 1;
			this.Sound = 50;
		}

		/// <summary>
		/// Event Handler Volume Change Event
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="VolumeChangeEventArgs"/> instance containing the event data.</param>
		public delegate void VolumeChangeHandler(object sender, VolumeChangeEventArgs e);

		/// <summary>
		/// Occurs when volume change.
		/// </summary>
		public event VolumeChangeHandler OnVolumeChange;

		/// <summary>
		/// Gets the volume.
		/// </summary>
		/// <value>
		/// The sound.
		/// </value>
		public int Sound { get; private set; }

		/// <summary>
		/// Gets the step.
		/// </summary>
		/// <value>
		/// The step.
		/// </value>
		public int Step { get; private set; }

		/// <summary>
		/// Volumes up.
		/// </summary>
		public void VolumeUp() {
			if (this.Sound < 100) {
				this.Sound += this.Step;

				this.OnVolumeChange(this, new VolumeChangeEventArgs(this.Sound));
			}
		}

		/// <summary>
		/// Volumes down.
		/// </summary>
		public void VolumeDown() {
			if (this.Sound > 0) {
				this.Sound -= this.Step;

				this.OnVolumeChange(this, new VolumeChangeEventArgs(this.Sound));
			}
		}
	}
}