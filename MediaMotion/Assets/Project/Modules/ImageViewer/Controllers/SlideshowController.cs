using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer.Controllers {
	/// <summary>
	/// Slideshow Controller
	/// </summary>
	public class SlideshowController : ASlideshow<ImageViewerModule, SlideshowTileController> {
	}
}