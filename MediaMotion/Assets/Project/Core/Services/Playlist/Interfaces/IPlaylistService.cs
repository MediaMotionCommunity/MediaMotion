using System;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.Playlist.Interfaces {
	/// <summary>
	/// Playlist Service Interface
	/// </summary>
	public interface IPlaylistService : IResetable {
		/// <summary>
		/// Gets a value indicating whether this instance is configured.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is configured; otherwise, <c>false</c>.
		/// </value>
		bool IsConfigured { get; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylistService"/> is random.
		/// </summary>
		/// <value>
		///   <c>true</c> if random; otherwise, <c>false</c>.
		/// </value>
		bool Random { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylistService"/> is loop.
		/// </summary>
		/// <value>
		///   <c>true</c> if loop; otherwise, <c>false</c>.
		/// </value>
		bool Loop { get; set; }

		/// <summary>
		/// Gets the elements.
		/// </summary>
		/// <value>
		/// The elements.
		/// </value>
		IComparable[] Elements { get; }

		/// <summary>
		/// Gets the length.
		/// </summary>
		/// <value>
		/// The length.
		/// </value>
		int Length { get; }

		/// <summary>
		/// Gets the index.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		int Index { get; }

		/// <summary>
		/// Configure the playlist
		/// </summary>
		/// <param name="element">The file or the directory.</param>
		/// <param name="filterExtension">The filter extension.</param>
		/// <returns>
		///   <c>true</c> if the playlist is correctly configured otherwise, <c>false</c>
		/// </returns>
		bool Configure(IElement element, string[] filterExtension);

		/// <summary>
		/// Configure the playlist
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <param name="element">The element.</param>
		/// <returns>
		///   <c>true</c> if the playlist is correctly configured otherwise, <c>false</c>
		/// </returns>
		bool Configure(IComparable[] elements, IComparable element = null);

		/// <summary>
		/// Current file in the list.
		/// </summary>
		/// <returns>
		///   The element or <c>null</c>
		/// </returns>
		IComparable Current();

		/// <summary>
		/// Previous file in the list.
		/// </summary>
		/// <returns>
		///   The element or <c>null</c>
		/// </returns>
		IComparable Previous();

		/// <summary>
		/// Next file in the list.
		/// </summary>
		/// <returns>
		///   The element or <c>null</c>
		/// </returns>
		IComparable Next();

		/// <summary>
		/// Peeks the specified offset.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		///   The element or <c>null</c>
		/// </returns>
		IComparable Peek(int offset = 0);
	}
}
