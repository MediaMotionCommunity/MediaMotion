using System.IO;
using MediaMotion.Modules.MediaViewer.Models.Abstracts;
using MediaMotion.Modules.MediaViewer.SubModules.VideoPlayer.Models.Interfaces;

namespace MediaMotion.Modules.MediaViewer.SubModules.VideoPlayer.Models.Abstract {
	/// <summary>
	/// Abstract Video model
	/// </summary>
	public abstract class AVideo : AMedia, IVideo {
		/// <summary>
		/// Initializes a new instance of the <see cref="AVideo" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		protected AVideo(FileInfo fileInfo)
			: base(fileInfo) {
		}

		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>
		/// The width.
		/// </value>
		public int Width { get; private set; }

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>
		/// The height.
		/// </value>
		public int Height { get; private set; }

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		public override string GetHumanTypeString() {
			return ("Video");
		}
	}
}
