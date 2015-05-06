using MediaMotion.Core.Models.Module.Abstracts;
using MediaMotion.Modules.PDFViewer.Observers;

namespace MediaMotion.Modules.PDFViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class PDFViewerModule : AModule {
		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure() {
			this.Configuration.Name = "PDF Viewer";
			this.Configuration.Scene = "PDFViewer";
			this.Configuration.Description = "Read your PDFs documents";
			this.Configuration.ElementFactoryObserver = new ElementFactoryObserver();
		}
	}
}
