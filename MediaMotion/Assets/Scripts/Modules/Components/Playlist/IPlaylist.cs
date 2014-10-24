using System.Collections.Generic;

namespace MediaMotion.Modules.Components.Playlist {
	public interface IPlaylist {
		//
		// Properties
		//
		bool Random { get; set; }
		bool Loop { get; set; }

		//
		// Playlist
		//
		Element Current();
		void Prev();
		void Next();
		
		//
		// Playlist action
		//
		void Add(List<Element> Elements);
		void Remove(List<Element> Elements);
	}
}