using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using MediaMotion.Modules.MediaViewer.Models.Interfaces;
using MediaMotion.Modules.MediaViewer.Services.Session.Interfaces;
using MediaMotion.Modules.MediaViewer.Services.VLC.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.MediaViewer.SubModules.VideoPlayer.Controllers {
	/// <summary>
	/// Slideshow tile controller
	/// </summary>
	public sealed class SlideshowTileController : ASlideshowTile<VideoPlayerModule, SlideshowTileController> {
		/// <summary>
		/// The player
		/// </summary>
		private IPlayer player;

		/// <summary>
		/// The progress bar
		/// </summary>
		private ProgressBarController progressBar;

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="vlcService">The VLC service.</param>
 		public void Init(IVLCService vlcService) {
			this.player = vlcService.GetPlayer((IMedia)this.element);
			this.progressBar = this.gameObject.transform.Find("ProgressBar").gameObject.AddComponent<ProgressBarController>();
			this.progressBar.enabled = false;
		}

		/// <summary>
		/// Called when [destroy].
		/// </summary>
		public override void OnDestroy() {
			base.OnDestroy();
			if (this.player != null) {
				this.player.Dispose();
				this.player = null;
			}
		}

		/// <summary>
		/// Selects this instance.
		/// </summary>
		public override void EnterFullscreen() {
			if (this.player != null) {
				this.player.IsPlaying = !this.player.IsPlaying;
				this.progressBar.enabled = true;
			}
		}

		/// <summary>
		/// Unselects this instance.
		/// </summary>
		public override void LeaveFullscreen() {
			if (this.player != null) {
				this.player.Stop();
				this.progressBar.enabled = false;
			}
		}

		/// <summary>
		/// Texture2D loading process.
		/// </summary>
		protected override void Texture2DLoadingProcess() {
			if (this.IsTexture2DReady()) {
				if (this.Fullscreen || !this.Texture2DApplied) {
					this.ApplyTexture2D();
				}
			} else {
				this.LoadTexture2D();
				this.ScaleTexture2D(2.6f, 1.1f);
			}
		}

		/// <summary>
		/// Apply the texture 2D.
		/// </summary>
		protected override void ApplyTexture2D() {
			if (this.player != null && this.texture2D != null && this.gameObject.GetComponent<Renderer>() != null) {
				this.texture2D.SetPixels32((Color32[])this.player.Texture.Obj, 0);
				this.texture2D.Apply();
				this.progressBar.Ratio = (this.player.Duration > 0) ? (((float)this.player.Time) / (float)this.player.Duration) : (0.0f);
				this.Texture2DApplied = true;
			}
		}

		/// <summary>
		/// Loads the texture2d.
		/// </summary>
		protected override void LoadTexture2D() {
			if (this.player != null) {
				this.texture2D = new Texture2D(this.player.Media.Width, this.player.Media.Height, TextureFormat.ARGB32, false);
				this.texture2D.wrapMode = TextureWrapMode.Clamp;
				this.texture2D.filterMode = FilterMode.Trilinear;
				this.player.SetTexture(this.texture2D.GetPixels32(0));
				this.gameObject.GetComponent<Renderer>().material.mainTexture = this.texture2D;
				this.oppositeYScale = true;
			}
		}
	}
}
