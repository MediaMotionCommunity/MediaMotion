using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers {
	/// <summary>
	/// VideoPlayer Controller
	/// </summary>
	public class VideoPlayerController : AScript<VideoViewerModule, VideoPlayerController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The playlist service
		/// </summary>
		private IPlaylistService playlistService;

		/// <summary>
		/// The video URL
		/// </summary>
		private WWW videoUrl;

		/// <summary>
		/// The video texture
		/// </summary>
		private Texture2D videoTexture;

		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="playlist">The playlist.</param>
		public void Init(IInputService input, IPlaylistService playlist) {
			this.inputService = input;
			this.playlistService = playlist;

			this.playlistService.Configure(((this.module.Parameters.Length > 0) ? (this.module.Parameters[0]) : (null)), new string[] { ".mkv", ".avi", ".wav" });
			this.LoadVideo();
		}

		/// <summary>
		/// Update the instance
		/// </summary>
		public void Update() {
			this.CheckVideoDownload();
			foreach (IAction action in this.inputService.GetMovements()) {
				if (action.Type == ActionType.Right) {
					this.playlistService.Next();
					this.gameObject.transform.Rotate(new Vector3(0, 90, 0));
					this.LoadVideo();
				} else if (action.Type == ActionType.Left) {
					this.playlistService.Previous();
					this.gameObject.transform.Rotate(new Vector3(0, -90, 0));
					this.LoadVideo();
				}
			}
		}

		/// <summary>
		/// Loads the video.
		/// </summary>
		private void LoadVideo() {
			Debug.Log(this.playlistService.Current().GetPath());
			this.videoUrl = new WWW("file:///" + this.playlistService.Current().GetPath());
		}

		/// <summary>
		/// Checks the image download.
		/// </summary>
		private void CheckVideoDownload() {
			if (this.videoUrl != null && this.videoUrl.isDone) {
				this.videoTexture = this.videoUrl.texture;
				this.videoTexture.wrapMode = TextureWrapMode.Clamp;
				this.gameObject.GetComponent<Renderer>().material.mainTexture = this.videoTexture;
				this.videoUrl = null;
			}
		}
	}
}
