using MediaMotion.Core.Models.Module;
using MediaMotion.Core.Models.Module.Abstracts;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;
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
			this.Configuration.Name = "Image Viewer";
			this.Configuration.Scene = "ImageViewer";
			this.Configuration.Description = "Display your picture in a wonderfull slideshow";
		}
	}
}
