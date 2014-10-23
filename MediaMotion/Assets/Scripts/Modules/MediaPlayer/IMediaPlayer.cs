using System;

namespace MediaMotion.Modules.MediaPlayer {
	public interface IMediaPlayer : IModule {
		//
		// Lecture
		//
		void Play();
		void Pause();
		void Stop();

		//
		// Volume
		//
		int GetVolume();
		void VolumeUp();
		void VolumeDown();
		
		//
		// Playlist
		//
		Element Current();
		void Prev();
		void Next();

		//
		// Playlist action
		//
		void Add(Element element);
		void Remove(Element element);
	}
}