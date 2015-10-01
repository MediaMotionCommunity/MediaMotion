using System.IO;
using MediaMotion.Core.Services.FileSystem.Models.Abstracts;
using MediaMotion.Modules.MediaViewer.Models.Interfaces;

namespace MediaMotion.Modules.MediaViewer.Models.Abstracts {
	public class AMedia : AFile, IMedia {
		/// <summary>
		/// Initializes a new instance of the <see cref="AMedia"/> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		protected AMedia(FileInfo fileInfo)
			: base(fileInfo) {
		}

		/// <summary>
		/// Gets the duration.
		/// </summary>
		/// <value>
		/// The duration.
		/// </value>
		public int Duration { get; protected set; }

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>
		/// the type that a human can read and understand
		/// </returns>
		public override string GetHumanTypeString() {
			return ("Media");
		}
	}
}
