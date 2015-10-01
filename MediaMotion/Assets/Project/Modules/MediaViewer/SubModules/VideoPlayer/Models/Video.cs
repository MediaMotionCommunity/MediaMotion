using System.IO;
using MediaMotion.Modules.MediaViewer.SubModules.VideoPlayer.Models.Abstract;

namespace MediaMotion.Modules.MediaViewer.SubModules.VideoPlayer.Models {
	/// <summary>
	/// Video Model
	/// </summary>
	public class Video : AVideo {
		/// <summary>
		/// Initializes a new instance of the <see cref="Video"/> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public Video(FileInfo fileInfo)
			: base(fileInfo) {
		}
	}
}
