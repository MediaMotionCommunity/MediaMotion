using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Modules.ImageViewer.Observers;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class ImageViewerModule : AModule {
		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure(IContainer container) {
			this.Name = "Image Viewer";
			this.Scene = "ImageViewer";
			this.Description = "Display your picture in a wonderfull slideshow";
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

			containerBuilderService.Register<ElementFactoryObserver>().As<IElementFactoryObserver>().SingleInstance = true;

			return (containerBuilderService.Build(container));
		}
	}
}
