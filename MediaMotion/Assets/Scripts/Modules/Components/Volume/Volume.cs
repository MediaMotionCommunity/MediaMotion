using MediaMotion.Modules.Components.Volume.Events;

namespace MediaMotion.Modules.Components.Volume {
	public class Volume : IVolume {
		//
		// Properties
		//
		public int Sound { get; private set; }
		public int Step { get; private set; }

		//
		// Delegates
		//
		public delegate void VolumeChangeHandler(object sender, VolumeChangeEventArgs e);

		//
		// Events
		//
		public event VolumeChangeHandler OnVolumeChange;

		//
		// Construct
		//
		public Volume() {
			this.Step = 1;
			this.Sound = 50;
		}

		//
		// Volume
		//
		public void VolumeUp() {
			if (this.Sound < 100) {
				this.Sound += this.Step;

				OnVolumeChange (this, new VolumeChangeEventArgs (this.Sound));
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