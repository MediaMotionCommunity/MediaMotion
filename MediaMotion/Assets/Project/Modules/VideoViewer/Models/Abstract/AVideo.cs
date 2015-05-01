using MediaMotion.Modules.VideoViewer.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Abstracts;
using System.IO;

namespace MediaMotion.Modules.VideoViewer.Models.Abstract {
	/// <summary>
	/// Abstract Video model
	/// </summary>
	public abstract class AVideo : AFile, IVideo {
		/// <summary>
		/// Initializes a new instance of the <see cref="AVideo"/> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		/// <param name="resourceId">The resource Id.</param>
		protected AVideo(FileInfo fileInfo, string resourceId = "Video")
			: base(fileInfo, resourceId) {
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
		/// <returns>the type that a human can read and understand</returns>
		public override string GetHumanTypeString() {
			return ("Video");
		}
	}
}
