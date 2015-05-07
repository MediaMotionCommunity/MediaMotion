using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Modules.VideoViewer.Observers;

namespace MediaMotion.Modules.VideoViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class VideoViewerModule : AModule {
		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure() {
			this.Name = "Video Viewer";
			this.Scene = "VideoViewer";
			this.Description = "Watch your movies and videos";
			// this.ElementFactoryObserver = new ElementFactoryObserver();
		}
	}
}
