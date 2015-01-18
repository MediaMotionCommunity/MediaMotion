using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Modules.Components.Playlist {
	/// <summary>
	/// Playlist Interface
	/// </summary>
	public interface IPlaylist {
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylist"/> is random.
		/// </summary>
		/// <value>
		///   <c>true</c> if random; otherwise, <c>false</c>.
		/// </value>
		bool Random { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylist"/> is loop.
		/// </summary>
		/// <value>
		///   <c>true</c> if loop; otherwise, <c>false</c>.
		/// </value>
		bool Loop { get; set; }

		/// <summary>
		/// Currents this instance.
		/// </summary>
		/// <returns>The current element</returns>
		IElement Current();

		/// <summary>
		/// Back to the previous element
		/// </summary>
		void Prev();

		/// <summary>
		/// Go to the next element
		/// </summary>
		void Next();

		/// <summary>
		/// Adds the specified elements.
		/// </summary>
		/// <param name="Elements">The elements.</param>
		void Add(List<IElement> Elements);

		/// <summary>
		/// Removes the specified elements.
		/// </summary>
		/// <param name="Elements">The elements.</param>
		void Remove(List<IElement> Elements);
	}
}