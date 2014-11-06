using System.Collections.Generic;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Modules.Components.Playlist.Events;

namespace MediaMotion.Modules.Components.Playlist {
	public class Playlist : IPlaylist {
		private List<IElement> ElementList;
		private List<IElement>.Enumerator CurrentElement;

		public delegate void PlaylistElementChangeHandler(object sender, PlaylistElementChangeEventArgs e);
		public delegate void PlaylistChangeHandler(object sender, PlaylistChangeEventArgs e);

		public event PlaylistElementChangeHandler OnElementChange;
		public event PlaylistChangeHandler OnPlaylistChange;

		public bool Random { get; set; }
		public bool Loop { get; set; }

		public Playlist(bool Loop = true, bool Random = false) {
			this.Loop = Loop;
			this.Random = Random;
			this.CurrentElement = this.ElementList.GetEnumerator();
		}

		public IElement Current() {
			return (this.CurrentElement.Current);
		}

		public void Prev() {
			IElement Previous = this.Current();

			this.CurrentElement.MoveNext();
			this.OnElementChange(this, new PlaylistElementChangeEventArgs(Previous, this.Current()));
		}

		public void Next() {
			IElement Previous = this.Current();

			this.CurrentElement.MoveNext();
			this.OnElementChange(this, new PlaylistElementChangeEventArgs(Previous, this.Current()));
		}

		public void Add(List<IElement> Elements) {
			this.ElementList.AddRange(Elements);

			this.OnPlaylistChange(this, new PlaylistChangeEventArgs(Elements));
		}

		public void Remove(List<IElement> Elements) {
			//this.ElementList.Remove(Elements);

			this.OnPlaylistChange(this, new PlaylistChangeEventArgs(Elements));
		}
	}
}