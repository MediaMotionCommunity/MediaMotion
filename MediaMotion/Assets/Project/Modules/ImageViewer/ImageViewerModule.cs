using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
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
			this.Container = container;
			// this.ElementFactoryObserver = new ElementFactoryObserver();
		}
	}
}
