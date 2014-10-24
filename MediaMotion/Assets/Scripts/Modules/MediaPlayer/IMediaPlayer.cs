using System;
using MediaMotion.Modules.Components.Playlist;
using MediaMotion.Modules.Components.Volume;

namespace MediaMotion.Modules.MediaPlayer {
	public interface IMediaPlayer : IModule, IPlaylist, IVolume {
		//
		// Lecture
		//
		void Play();
		void Pause();
		void Stop();
	}
}