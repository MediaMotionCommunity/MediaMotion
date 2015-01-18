using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Modules.Components.Playlist;
using MediaMotion.Modules.Components.Volume;
using MediaMotion.Modules.MediaPlayer.Events;

namespace MediaMotion.Modules.MediaPlayer {
	/// <summary>
	/// Media Player
	/// </summary>
	public abstract class AMediaPlayer : IMediaPlayer {
		/// <summary>
		/// The playlist
		/// </summary>
		protected Playlist Playlist;

		/// <summary>
		/// The volume
		/// </summary>
		protected Volume Volume;

		/// <summary>
		/// Event Handler Media Event
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="MediaEventArgs"/> instance containing the event data.</param>
		public delegate void MediaHandle(object sender, MediaEventArgs e);

		/// <summary>
		/// Occurs when playing.
		/// </summary>
		public event MediaHandle OnPlay;

		/// <summary>
		/// Occurs when pausing.
		/// </summary>
		public event MediaHandle OnPause;

		/// <summary>
		/// Occurs when stopping.
		/// </summary>
		public event MediaHandle OnStop;

		/// <summary>
		/// Occurs when [on element change].
		/// </summary>
		public event Playlist.PlaylistElementChangeHandler OnElementChange {
			add {
				this.Playlist.OnElementChange += value;
			}
			remove {
				this.Playlist.OnElementChange -= value;
			}
		}

		/// <summary>
		/// Occurs when [on playlist change].
		/// </summary>
		public event Playlist.PlaylistChangeHandler OnPlaylistChange {
			add {
				this.Playlist.OnPlaylistChange += value;
			}
			remove {
				this.Playlist.OnPlaylistChange -= value;
			}
		}

		/// <summary>
		/// Occurs when [on volume change].
		/// </summary>
		public event Volume.VolumeChangeHandler OnVolumeChange {
			add {
				this.Volume.OnVolumeChange += value;
			}
			remove {
				this.Volume.OnVolumeChange -= value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylist" /> is random.
		/// </summary>
		/// <value>
		///   <c>true</c> if random; otherwise, <c>false</c>.
		/// </value>
		public bool Random {
			get {
				return (this.Playlist.Random);
			}
			set {
				this.Playlist.Random = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylist" /> is loop.
		/// </summary>
		/// <value>
		///   <c>true</c> if loop; otherwise, <c>false</c>.
		/// </value>
		public bool Loop {
			get {
				return (this.Playlist.Loop);
			}
			set {
				this.Playlist.Loop = value;
			}
		}

		/// <summary>
		/// Gets the volume.
		/// </summary>
		/// <value>
		/// The sound.
		/// </value>
		public int Sound {
			get {
				return (this.Volume.Sound);
			}
			private set {
			}
		}

		/// <summary>
		/// Gets the step.
		/// </summary>
		/// <value>
		/// The step.
		/// </value>
		public int Step {
			get {
				return (this.Volume.Step);
			}
			private set {
			}
		}

		/// <summary>
		/// Plays this instance.
		/// </summary>
		public virtual void Play() {
			this.OnPlay(this, new MediaEventArgs(this.Current()));
		}

		/// <summary>
		/// Pauses this instance.
		/// </summary>
		public virtual void Pause() {
			this.OnPause(this, new MediaEventArgs(this.Current()));
		}

		/// <summary>
		/// Stops this instance.
		/// </summary>
		public virtual void Stop() {
			this.OnStop(this, new MediaEventArgs(this.Current()));
		}

		/// <summary>
		/// Currents this instance.
		/// </summary>
		/// <returns>
		/// The current element
		/// </returns>
		public IElement Current() {
			return (this.Playlist.Current());
		}

		/// <summary>
		/// Back to the previous element
		/// </summary>
		public void Prev() {
			this.Playlist.Prev();
		}

		/// <summary>
		/// Go to the next element
		/// </summary>
		public void Next() {
			this.Playlist.Next();
		}

		/// <summary>
		/// Adds the specified elements.
		/// </summary>
		/// <param name="Elements">The elements.</param>
		public void Add(List<IElement> Elements) {
			this.Playlist.Add(Elements);
		}

		/// <summary>
		/// Removes the specified elements.
		/// </summary>
		/// <param name="Elements">The elements.</param>
		public void Remove(List<IElement> Elements) {
			this.Playlist.Remove(Elements);
		}

		/// <summary>
		/// Volumes up.
		/// </summary>
		public void VolumeUp() {
			this.Volume.VolumeUp();
		}

		/// <summary>
		/// Volumes down.
		/// </summary>
		public void VolumeDown() {
			this.Volume.VolumeDown();
		}
	}
}