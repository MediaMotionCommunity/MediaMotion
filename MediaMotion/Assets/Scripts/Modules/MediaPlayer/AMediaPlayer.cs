using System.Collections.Generic;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Modules.Components.Playlist;
using MediaMotion.Modules.Components.Volume;
using MediaMotion.Modules.MediaPlayer.Events;

namespace MediaMotion.Modules.MediaPlayer {
	abstract public class AMediaPlayer : IMediaPlayer {
		public bool Random {
			get {
				return (this.Playlist.Random);
			}
			set {
				this.Playlist.Random = value;
			}
		}

		public bool Loop {
			get {
				return (this.Playlist.Loop);
			}
			set {
				this.Playlist.Loop = value;
			}
		}

		public int Sound {
			get {
				return (this.Volume.Sound);
			}
			private set {
			}
		}

		public int Step {
			get {
				return (this.Volume.Step);
			}
			private set {
			}
		}

		protected Playlist Playlist;
		protected Volume Volume;

		public delegate void MediaHandle(object sender, MediaEventArgs e);

		public event MediaHandle OnPlay;
		public event MediaHandle OnPause;
		public event MediaHandle OnStop;

		public event Playlist.PlaylistElementChangeHandler OnElementChange {
			add {
				this.Playlist.OnElementChange += value;
			}
			remove {
				this.Playlist.OnElementChange -= value;
			}
		}

		public event Playlist.PlaylistChangeHandler OnPlaylistChange {
			add {
				this.Playlist.OnPlaylistChange += value;
			}
			remove {
				this.Playlist.OnPlaylistChange -= value;
			}
		}

		public event Volume.VolumeChangeHandler OnVolumeChange {
			add {
				this.Volume.OnVolumeChange += value;
			}
			remove {
				this.Volume.OnVolumeChange -= value;
			}
		}

		virtual public void Play() {
			OnPlay(this, new MediaEventArgs(this.Current()));
		}

		virtual public void Pause() {
			OnPause(this, new MediaEventArgs(this.Current()));
		}

		virtual public void Stop() {
			OnStop(this, new MediaEventArgs(this.Current()));
		}

		public IElement Current() {
			return (this.Playlist.Current());
		}

		public void Prev() {
			this.Playlist.Prev();
		}

		public void Next() {
			this.Playlist.Next();
		}

		public void Add(List<IElement> Elements) {
			this.Playlist.Add(Elements);
		}

		public void Remove(List<IElement> Elements) {
			this.Playlist.Remove(Elements);
		}

		public void VolumeUp() {
			this.Volume.VolumeUp();
		}

		public void VolumeDown() {
			this.Volume.VolumeDown();
		}
	}
}