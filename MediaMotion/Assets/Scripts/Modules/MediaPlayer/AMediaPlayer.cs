using System.Collections.Generic;
using MediaMotion.Modules.Components.Playlist;
using MediaMotion.Modules.Components.Volume;
using MediaMotion.Modules.MediaPlayer.Events;

namespace MediaMotion.Modules.MediaPlayer {
	abstract public class AMediaPlayer : IMediaPlayer {
		//
		// Playlist properties proxy
		//
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

		//
		// Volume properties proxy
		//
		public int Sound {
			get {
				return (this.Playlist.Loop);
			}
			private set;
		}

		public int Step { 
			get {
				return (this.Playlist.Loop);
			}
			private set;
		}

		//
		// Attributes
		//
		protected Playlist Playlist;
		protected Volume Volume;

		//
		// Delegate
		//
		public delegate void MediaHandle(object sender, MediaEventArgs e);

		//
		// Events
		//
		public event MediaHandle OnPlay;
		public event MediaHandle OnPause;
		public event MediaHandle OnStop;
		
		//
		// Playlist events proxy
		//
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

		//
		// Volume events proxy
		//
		public event Volume.VolumeChangeHandler OnVolumeChange {
			add {
				this.Volume.OnVolumeChange += value;
			}
			remove {
				this.Volume.OnVolumeChange -= value;
			}
		}

		//
		// Lecture
		//
		public void Play() {
			OnPlay(this, new MediaEventArgs(this.Current()));
		}

		public void Pause() {
			OnPause(this, new MediaEventArgs(this.Current()));
		}

		public void Stop() {
			OnStop(this, new MediaEventArgs(this.Current()));
		}
		
		//
		// Playlist
		//
		public Element Current() {
			return (this.Playlist.Current());
		}

		public void Prev() {
			this.Playlist.Prev();
		}

		public void Next() {
			this.Playlist.Next();
		}
		
		//
		// Playlist action
		//
		public void Add(List<Element> Elements) {
			this.Playlist.Add(Elements);
		}

		public void Remove(List<Element> Elements) {
			this.Playlist.Remove(Elements);
		}
		
		// Volume
		public void VolumeUp() {
			this.Volume.VolumeUp();
		}

		public void VolumeDown() {
			this.Volume.VolumeDown();
		}
	}
}