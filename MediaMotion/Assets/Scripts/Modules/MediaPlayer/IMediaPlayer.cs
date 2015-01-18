using System;
using MediaMotion.Modules.Components.Playlist;
using MediaMotion.Modules.Components.Volume;

namespace MediaMotion.Modules.MediaPlayer {
	/// <summary>
	/// Media Player Interface
	/// </summary>
	public interface IMediaPlayer : IPlaylist, IVolume {
		/// <summary>
		/// Plays this instance.
		/// </summary>
		void Play();

		/// <summary>
		/// Pauses this instance.
		/// </summary>
		void Pause();

		/// <summary>
		/// Stops this instance.
		/// </summary>
		void Stop();
	}
}