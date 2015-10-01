using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.Observers.Interfaces;
using MediaMotion.Modules.MediaViewer.SubModules.VideoPlayer.Observers;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Modules.MediaViewer.SubModules.VideoPlayer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class VideoPlayerModule : AModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="VideoPlayerModule"/> class.
		/// </summary>
		public VideoPlayerModule() {
			this.Name = "Video Player";
			this.Scene = "VideoPlayer";
			this.Description = "Watch your videos";
			this.SupportedExtensions = new string[] { ".avi", ".mkv", ".mp4" };
			this.SupportedAction = new ActionType[] {
				ActionType.Select,
				ActionType.Right,
				ActionType.Left
			};
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

			containerBuilderService.Register<ElementDrawObserver>().As<IElementDrawObserver>().SingleInstance = true;
			containerBuilderService.Register<ElementFactoryObserver>().As<IElementFactoryObserver>().SingleInstance = true;
			return (containerBuilderService.Build(container));
		}
	}
}
