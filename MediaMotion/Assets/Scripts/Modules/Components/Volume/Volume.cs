using MediaMotion.Modules.Components.Volume.Events;

namespace MediaMotion.Modules.Components.Volume {
	public class Volume : IVolume {
		public int Sound { get; private set; }
		public int Step { get; private set; }

		public delegate void VolumeChangeHandler(object sender, VolumeChangeEventArgs e);

		public event VolumeChangeHandler OnVolumeChange;

		public Volume() {
			this.Step = 1;
			this.Sound = 50;
		}

		public void VolumeUp() {
			if (this.Sound < 100) {
				this.Sound += this.Step;

				OnVolumeChange(this, new VolumeChangeEventArgs(this.Sound));
			}
		}

		public void VolumeDown() {
			if (this.Sound > 0) {
				this.Sound -= this.Step;

				OnVolumeChange(this, new VolumeChangeEventArgs(this.Sound));
			}
		}
	}
}