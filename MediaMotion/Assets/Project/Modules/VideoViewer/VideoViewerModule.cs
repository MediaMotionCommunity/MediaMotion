using MediaMotion.Core.Models.Module.Abstracts;

namespace MediaMotion.Modules.VideoViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class VideoViewerModule : AModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="VideoViewerModule"/> class.
		/// </summary>
		public VideoViewerModule()
			: base() {
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure() {
			this.Configuration.Name = "Video Viewer";
			this.Configuration.Scene = "VideoViewer";
			this.Configuration.Description = "Watch your movies and videos";
		}

	}
}
