using System.IO;
using MediaMotion.Core.Services.FileSystem.Models.Abstracts;

namespace MediaMotion.Modules.PDFViewer.Models {
	/// <summary>
	/// Abstract Video model
	/// </summary>
	public class PDF : AFile, IPDF {
		/// <summary>
		/// Initializes a new instance of the <see cref="PDF" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public PDF(FileInfo fileInfo)
			: base(fileInfo) {
		}

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		public override string GetHumanTypeString() {
			return ("PDF file");
		}
	}
}
