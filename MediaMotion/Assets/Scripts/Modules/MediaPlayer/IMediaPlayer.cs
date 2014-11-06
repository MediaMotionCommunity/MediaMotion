using System;
using MediaMotion.Modules.Components.Playlist;
using MediaMotion.Modules.Components.Volume;

namespace MediaMotion.Modules.MediaPlayer {
	public interface IMediaPlayer : IPlaylist, IVolume {
		void Play();
		void Pause();
		void Stop();
	}
}