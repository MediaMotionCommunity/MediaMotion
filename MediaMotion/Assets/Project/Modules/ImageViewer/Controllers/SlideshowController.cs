﻿using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer.Controllers {
	/// <summary>
	/// Slideshow Controller
	/// </summary>
	public class SlideshowController : BaseUnityScript<SlideshowController> {
		/// <summary>
		/// The module instance
		/// </summary>
		private ImageViewerModule moduleInstance;

		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The playlist service
		/// </summary>
		private IPlaylistService playlistService;

		/// <summary>
		/// The image URL
		/// </summary>
		private WWW imageUrl = null;

		/// <summary>
		/// The image texture
		/// </summary>
		private Texture2D imageTexture = null;

		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="module">The module.</param>
		/// <param name="input">The input.</param>
		/// <param name="playlist">The playlist.</param>
		public void Init(ImageViewerModule module, IInputService input, IPlaylistService playlist) {
			this.moduleInstance = module;
			this.inputService = input;
			this.playlistService = playlist;

			this.playlistService.Configure(((this.moduleInstance.Parameters.Length > 0) ? (this.moduleInstance.Parameters[0]) : (null)), new string[] { ".png", ".jpg", ".jpeg" });
			this.LoadImage();
		}

		/// <summary>
		/// Update the instance
		/// </summary>
		public void Update() {
			this.CheckImageDownload();
			foreach (IAction action in this.inputService.GetMovements()) {
				if (action.Type == ActionType.Right) {
					this.playlistService.Next();
					this.gameObject.transform.Rotate(new Vector3(0, 90, 0));
					this.LoadImage();
				} else if (action.Type == ActionType.Left) {
					this.playlistService.Previous();
					this.gameObject.transform.Rotate(new Vector3(0, -90, 0));
					this.LoadImage();
				}
			}
		}

		/// <summary>
		/// Loads the image.
		/// </summary>
		private void LoadImage() {
			Debug.Log(this.playlistService.Current().GetPath());
			this.imageUrl = new WWW("file:///" + this.playlistService.Current().GetPath());
		}

		/// <summary>
		/// Checks the image download.
		/// </summary>
		private void CheckImageDownload() {
			if (this.imageUrl != null && this.imageUrl.isDone) {
				this.imageTexture = this.imageUrl.texture;

				this.imageTexture.wrapMode = TextureWrapMode.Clamp;
				this.gameObject.renderer.material.mainTexture = this.imageTexture;
				this.imageUrl = null;
			}
		}
	}
}