using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Modules.ImageViewer.Observers;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class ImageViewerModule : AModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageViewerModule"/> class.
		/// </summary>
		public ImageViewerModule()
			: base() {
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure() {
			this.Name = "Image Viewer";
			this.Scene = "ImageViewer";
			this.Description = "Display your picture in a wonderfull slideshow";
			// this.ElementFactoryObserver = new ElementFactoryObserver();
		}
	}
}
