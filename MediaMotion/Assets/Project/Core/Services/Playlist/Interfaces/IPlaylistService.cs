using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.Playlist.Interfaces {
	/// <summary>
	/// Playlist Service Interface
	/// </summary>
	public interface IPlaylistService {
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
		/// Gets the length.
		/// </summary>
		/// <value>
		/// The length.
		/// </value>
		int Length { get; }

		/// <summary>
		/// Configure the playlist
		/// </summary>
		/// <param name="element">The file or the directory.</param>
		/// <param name="filterExtension">The filter extension.</param>
		/// <returns><c>true</c> if the playlist is correctly configured otherwise, <c>false</c></returns>
		bool Configure(IElement element, string[] filterExtension);

		/// <summary>
		/// Current file in the list.
		/// </summary>
		/// <returns>The file</returns>
		IFile Current();

		/// <summary>
		/// Previous file in the list.
		/// </summary>
		/// <returns>The file</returns>
		IFile Previous();

		/// <summary>
		/// Next file in the list.
		/// </summary>
		/// <returns>The file</returns>
		IFile Next();
	}
}
