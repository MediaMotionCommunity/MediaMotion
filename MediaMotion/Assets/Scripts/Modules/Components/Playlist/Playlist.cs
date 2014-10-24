using System.Collections.Generic;
using MediaMotion.Modules.Components.Playlist.Events;

namespace MediaMotion.Modules.Components.Playlist {
	public class Playlist : IPlaylist {
		//
		// Attributes
		//
		private List<Element> ElementList;
		private List<Element>.Enumerator CurrentElement;

		//
		// Delegate
		//
		public delegate void PlaylistElementChangeHandler(object sender, PlaylistElementChangeEventArgs e);
		public delegate void PlaylistChangeHandler(object sender, PlaylistChangeEventArgs e);

		//
		// Event
		//
		public event PlaylistElementChangeHandler OnElementChange;
		public event PlaylistChangeHandler OnPlaylistChange;

		//
		// Properties
		//
		public bool Random { get; set; }
		public bool Loop { get; set; }

		public Playlist(bool Loop = true, bool Random = false) {
			this.Loop = Loop;
			this.Random = Random;
			this.CurrentElement = this.ElementList.GetEnumerator();
		}

		//
		// Playlist
		//
		public Element Current() {
			return (this.CurrentElement.Current);
		}

		public void Prev() {
			Element Previous = this.Current();

			this.CurrentElement.MoveNext();
			this.OnElementChange(this, new PlaylistElementChangeEventArgs(Previous, this.Current()));
		}

		public void Next() {
			Element Previous = this.Current();

			this.CurrentElement.MoveNext();
			this.OnElementChange(this, new PlaylistElementChangeEventArgs(Previous, this.Current()));
		}
		
		//
		// Playlist action
		//
		public void Add(List<Element> Elements) {
			this.ElementList.Add(Elements);

			this.OnElementChange(this, new PlaylistChangeEventArgs(Elements));
		}
		
		public void Remove(List<Element> Elements) {
			this.ElementList.Remove(Elements);

			this.OnElementChange(this, new PlaylistChangeEventArgs(Elements));
		}
	}
}