using MediaMotion.Core.Models.Module.Abstracts;
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
			this.Configuration.Name = "Video Viewer";
			this.Configuration.Scene = "VideoViewer";
			this.Configuration.Description = "Watch your movies and videos";
			this.Configuration.ElementFactoryObserver = new ElementFactoryObserver();
		}
	}
}
