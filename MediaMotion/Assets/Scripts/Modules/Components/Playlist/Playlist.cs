using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Modules.Components.Playlist.Events;

namespace MediaMotion.Modules.Components.Playlist {
	/// <summary>
	/// Playlist Model
	/// </summary>
	public class Playlist : IPlaylist {
		/// <summary>
		/// The element list
		/// </summary>
		private List<IElement> ElementList;

		/// <summary>
		/// The current element
		/// </summary>
		private List<IElement>.Enumerator CurrentElement;

		/// <summary>
		/// Initializes a new instance of the <see cref="Playlist"/> class.
		/// </summary>
		/// <param name="Loop">if set to <c>true</c> [loop].</param>
		/// <param name="Random">if set to <c>true</c> [random].</param>
		public Playlist(bool Loop = true, bool Random = false) {
			this.Loop = Loop;
			this.Random = Random;
			this.ElementList = new List<IElement>();
			this.CurrentElement = this.ElementList.GetEnumerator();
		}

		/// <summary>
		/// Event Handler Playlist Element Change Event
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PlaylistElementChangeEventArgs"/> instance containing the event data.</param>
		public delegate void PlaylistElementChangeHandler(object sender, PlaylistElementChangeEventArgs e);

		/// <summary>
		/// Event Handler Playlist Change Event
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PlaylistChangeEventArgs"/> instance containing the event data.</param>
		public delegate void PlaylistChangeHandler(object sender, PlaylistChangeEventArgs e);

		/// <summary>
		/// Occurs when element change.
		/// </summary>
		public event PlaylistElementChangeHandler OnElementChange;

		/// <summary>
		/// Occurs when playlist change.
		/// </summary>
		public event PlaylistChangeHandler OnPlaylistChange;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylist" /> is random.
		/// </summary>
		/// <value>
		///   <c>true</c> if random; otherwise, <c>false</c>.
		/// </value>
		public bool Random { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylist" /> is loop.
		/// </summary>
		/// <value>
		///   <c>true</c> if loop; otherwise, <c>false</c>.
		/// </value>
		public bool Loop { get; set; }

		/// <summary>
		/// Currents this instance.
		/// </summary>
		/// <returns>
		/// The current element
		/// </returns>
		public IElement Current() {
			return (this.CurrentElement.Current);
		}

		/// <summary>
		/// Back to the previous element
		/// </summary>
		public void Prev() {
			IElement Previous = this.Current();

			this.CurrentElement.MoveNext();
			this.OnElementChange(this, new PlaylistElementChangeEventArgs(Previous, this.Current()));
		}

		/// <summary>
		/// Go to the next element
		/// </summary>
		public void Next() {
			IElement Previous = this.Current();

			this.CurrentElement.MoveNext();
			this.OnElementChange(this, new PlaylistElementChangeEventArgs(Previous, this.Current()));
		}

		/// <summary>
		/// Adds the specified elements.
		/// </summary>
		/// <param name="Elements">The elements.</param>
		public void Add(List<IElement> Elements) {
			this.ElementList.AddRange(Elements);

			this.OnPlaylistChange(this, new PlaylistChangeEventArgs(Elements));
		}

		/// <summary>
		/// Removes the specified elements.
		/// </summary>
		/// <param name="Elements">The elements.</param>
		public void Remove(List<IElement> Elements) {
			////this.ElementList.Remove(Elements);

			this.OnPlaylistChange(this, new PlaylistChangeEventArgs(Elements));
		}
	}
}