using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Module.Abstracts;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;

namespace MediaMotion.Core.Services.Playlist {
	/// <summary>
	/// Playlist Service
	/// </summary>
	public class PlaylistService : IPlaylistService {
		/// <summary>
		/// The file system service
		/// </summary>
		private IFileSystemService fileSystemService;

		/// <summary>
		/// The file list
		/// </summary>
		private List<IFile> filesList;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlaylistService"/> class.
		/// </summary>
		/// <param name="fileSystem">The file system.</param>
		public PlaylistService(IFileSystemService fileSystem) {
			this.fileSystemService = fileSystem;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylistService" /> is random.
		/// </summary>
		/// <value>
		///   <c>true</c> if random; otherwise, <c>false</c>.
		/// </value>
		public bool Random { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylistService" /> is loop.
		/// </summary>
		/// <value>
		///   <c>true</c> if loop; otherwise, <c>false</c>.
		/// </value>
		public bool Loop { get; set; }

		/// <summary>
		/// Configures the directory.
		/// </summary>
		/// <param name="file">The file</param>
		public void Configure(IFile file, string[] filterExtension) {
			this.filesList = this.fileSystemService.GetContent(filterExtension, file.GetParent());
		}

		/// <summary>
		/// Current file in the list.
		/// </summary>
		/// <returns>
		/// The file
		/// </returns>
		public IFile Current() {
			return (default(IFile));
		}

		/// <summary>
		/// Previous file in the list.
		/// </summary>
		/// <returns>
		/// The file
		/// </returns>
		public IFile Previous() {
			return (default(IFile));
		}

		/// <summary>
		/// Next file in the list.
		/// </summary>
		/// <returns>
		/// The file
		/// </returns>
		public IFile Next() {
			return (default(IFile));
		}
	}
}
