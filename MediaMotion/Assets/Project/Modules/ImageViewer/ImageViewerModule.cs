using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.Observers.Interfaces;
using MediaMotion.Modules.ImageViewer.Observers;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class ImageViewerModule : AModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageViewerModule"/> class.
		/// </summary>
		public ImageViewerModule() {
			this.Name = "Image Viewer";
			this.Scene = "ImageViewer";
			this.Description = "Display your picture in a wonderfull slideshow";
			this.SupportedExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".bmp", ".tiff" };
			this.SupportedAction = new ActionType[] {
				ActionType.Rotate,
				ActionType.Right,
				ActionType.Left,
				ActionType.ZoomIn,
				ActionType.ZoomOut, 
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
