using System.IO;
using MediaMotion.Core.Services.FileSystem.Models.Abstracts;
using MediaMotion.Modules.ImageViewer.Models.Interfaces;

namespace MediaMotion.Modules.ImageViewer.Models.Abstract {
	/// <summary>
	/// Abstract Image Model
	/// </summary>
	public abstract class AImage : AFile, IImage {
		/// <summary>
		/// Initializes a new instance of the <see cref="AImage" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		/// <param name="resourceId">The resource identifier.</param>
		protected AImage(FileInfo fileInfo, string resourceId = "Image")
			: base(fileInfo, resourceId) {
		}

		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>
		/// The width.
		/// </value>
		public int Width { get; protected set; }

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>
		/// The height.
		/// </value>
		public int Height { get; protected set; }

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		public override string GetHumanTypeString() {
			return ("Image");
		}
	}
}
