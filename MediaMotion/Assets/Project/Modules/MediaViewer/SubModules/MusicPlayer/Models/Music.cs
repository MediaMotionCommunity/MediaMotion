using System.IO;
using MediaMotion.Modules.MediaViewer.SubModules.MusicPlayer.Models.Abstract;

namespace MediaMotion.Modules.MediaViewer.SubModules.MusicPlayer.Models {
	/// <summary>
	/// Video Model
	/// </summary>
	public class Music : AMusic {
		/// <summary>
		/// Initializes a new instance of the <see cref="Video"/> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public Music(FileInfo fileInfo)
			: base(fileInfo) {
		}
	}
}
