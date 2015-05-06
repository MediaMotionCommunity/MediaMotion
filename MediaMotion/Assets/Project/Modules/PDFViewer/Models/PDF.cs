using System.IO;
using MediaMotion.Modules.PDFViewer.Models.Abstract;

namespace MediaMotion.Modules.PDFViewer.Models {
	/// <summary>
	/// PDF Model
	/// </summary>
	public class PDF : APDF {
		/// <summary>
		/// Initializes a new instance of the <see cref="PDF"/> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public PDF(FileInfo fileInfo)
			: base(fileInfo) {
		}
	}
}
