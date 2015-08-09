using System;
using System.Linq;
using MediaMotion.Core.Services.Playlist.Models.Abstracts;

namespace MediaMotion.Modules.MediaViewer.SubModules.MusicPlayer.Controllers {
	/// <summary>
	/// Slideshow Controller
	/// </summary>
	public class SlideshowController : ASlideshow<MusicPlayerModule, SlideshowTileController, SlideshowElementController> {
		/// <summary>
		/// Initializes the playlist.
		/// </summary>
		/// <returns>
		///   <c>true</c> if correctly initialized, <c>false</c> otherwise
		/// </returns>
		protected override bool InitPlaylist() {
			return (base.InitPlaylist() || this.playlistService.Configure(this.elementFactory.CreateFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), this.module.SupportedExtensions));
		}
	}
}