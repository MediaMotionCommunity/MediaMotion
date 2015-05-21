using System.IO;
using MediaMotion.Core.Services.FileSystem.Models.Abstracts;
using MediaMotion.Modules.PDFViewer.Models.Interfaces;

namespace MediaMotion.Modules.PDFViewer.Models.Abstract {
	/// <summary>
	/// Abstract Video model
	/// </summary>
	public abstract class APDF : AFile, IPDF {
		/// <summary>
		/// Initializes a new instance of the <see cref="APDF" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		protected APDF(FileInfo fileInfo)
			: base(fileInfo) {
		}

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		public override string GetHumanTypeString() {
			return ("Document file");
		}
	}
}
