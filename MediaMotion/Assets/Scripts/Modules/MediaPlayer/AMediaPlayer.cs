using MediaMotion.Modules.Components.Playlist;
using MediaMotion.Modules.MediaPlayer.Events;

namespace MediaMotion.Modules.MediaPlayer {
	abstract public class AMediaPlayer : IMediaPlayer {
		//
		// Attributes
		//
		private IPlaylist Manager;
		private int Volume;

		//
		// Delegate
		//
		public delegate void VolumeChangeHandler(object sender, VolumeChangeEventArgs e);
		public delegate void MediaHandle(object sender, MediaEvent e);

		//
		// Events
		//
		public event VolumeChangeHandler OnVolumeChange;
		public event MediaHandle OnMediaPlay;
		public event MediaHandle OnMediaPause;
		public event MediaHandle OnMediaStop;

		//
		// Construct
		//
		public AMediaPlayer(IPlaylist Manager = null) {
			if (Manager == null) {
				Manager = new Playlist();
			}
			this.Manager = Manager;
		}

		//
		// Lecture
		//
		public void Play() {
			OnMediaPlay(this, new MediaEvent(this.Current()));
		}

		public void Pause() {
			OnMediaPause(this, new MediaEvent(this.Current()));
		}

		public void Stop() {
			OnMediaStop(this, new MediaEvent(this.Current()));
		}
		
		//
		// Volume
		//
		public int GetVolume() {
			return (this.Volume);
		}

		public void VolumeUp() {
			this.Volume += 1;

			OnVolumeChange(this, new VolumeChangeEventArgs(this.Volume));
		}

		public void VolumeDown() {
			this.Volume -= 1;

			OnVolumeChange(this, new VolumeChangeEventArgs(this.Volume));
		}
		
		//
		// Playlist movement
		//
		public Element Current() {
			return (this.Manager.Current());
		}

		public void Prev() {
			this.Manager.Prev();
		}

		public void Next() {
			this.Manager.Next();
		}
		
		//
		// Playlist
		//
		public void Add(Element element) {
			this.Manager.Add(element);
		}

		public void Remove(Element element) {
			this.Manager.Remove(element);
		}
	}
}