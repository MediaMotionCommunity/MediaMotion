using System.Collections.Generic;
using MediaMotion.Core.Models.Interfaces;

namespace MediaMotion.Modules.Components.Playlist {
	public interface IPlaylist {
		bool Random { get; set; }
		bool Loop { get; set; }

		IElement Current();
		void Prev();
		void Next();

		void Add(List<IElement> Elements);
		void Remove(List<IElement> Elements);
	}
}