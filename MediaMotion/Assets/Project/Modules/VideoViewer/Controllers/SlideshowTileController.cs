using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using MediaMotion.Modules.VideoViewer.Models.Interfaces;
using MediaMotion.Modules.VideoViewer.Services.Session.Interfaces;
using MediaMotion.Modules.VideoViewer.Services.VLC.Models.Interfaces;
using UnityEngine;


namespace MediaMotion.Modules.VideoViewer.Controllers {
	/// <summary>
	/// SlideshowTileController
	/// </summary>
	sealed public class SlideshowTileController : ASlideshowTile<VideoViewerModule, SlideshowTileController> {
		/// <summary>
		/// The player
		/// </summary>
		private IPlayer player;

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Init(IVLCService vlcService) {
			this.player = vlcService.GetPlayer((IVideo)this.file);
		}

		/// <summary>
		/// Called when [destroy].
		/// </summary>
		public void OnDestroy() {
			if (this.player != null) {
				this.player.Dispose();
				this.player = null;
			}
		}

		/// <summary>
		/// Selects this instance.
		/// </summary>
		public override void Select() {
			if (this.player != null) {
				base.Select();
				this.player.IsPlaying = !this.player.IsPlaying;
			}
		}

		/// <summary>
		/// Unselects this instance.
		/// </summary>
		public override void Unselect() {
			if (this.player != null) {
				base.Unselect();
				this.player.Stop();
			}
		}

		/// <summary>
		/// Texture2D loading process.
		/// </summary>
		protected override void Texture2DLoadingProcess() {
			if (this.IsTexture2DReady()) {
				if (this.selected || !this.Texture2DApplied) {
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
