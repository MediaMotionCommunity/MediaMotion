using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Modules.MediaViewer.Services.Session.Interfaces;
using MediaMotion.Modules.MediaViewer.Services.VLC;
using MediaMotion.Modules.MediaViewer.SubModules.MusicPlayer;
using MediaMotion.Modules.MediaViewer.SubModules.VideoPlayer;

namespace MediaMotion.Modules.MediaViewer {
	/// <summary>
	/// Media viewer module
	/// </summary>
	public class MediaViewerModule : AAdvancedModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="MediaViewerModule"/> class.
		/// </summary>
		public MediaViewerModule() {
			this.Name = "Media Viewer";
			this.Description = "Watch and listen your media";
			this.Children = new IModule[] { new VideoPlayerModule(), new MusicPlayerModule() };
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		/// <param name="container">The container</param>
		public override void Configure(IContainer container) {
			this.Container = this.BuildContainer(container);
		}

		/// <summary>
		/// Builds the container.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <returns>
		///   The container
		/// </returns>
		private IContainer BuildContainer(IContainer container) {
			IContainerBuilderService containerBuilderService = container.Get<IContainerBuilderService>();

			containerBuilderService.Register<VLCService>(new VLCService()).As<IVLCService>().SingleInstance = true;
			return (containerBuilderService.Build(container));
		}
	}
}
