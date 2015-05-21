using System.Linq;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace Assets.Project.Core.Services.Playlist.Models.Abstracts {
	/// <summary>
	/// SlideshowController
	/// </summary>
	public class ASlideshow<Module, Child> : AScript<Module, Child>
		where Module : class, IModule
		where Child : ASlideshow<Module, Child> {
		/// <summary>
		/// The number of side elements
		/// </summary>
		public int NumberOfSideElements;

		/// <summary>
		/// The element
		/// </summary>
		public GameObject BaseElement;

		/// <summary>
		/// The last left element
		/// </summary>
		private GameObject lastLeftElement;

		/// <summary>
		/// The last right element
		/// </summary>
		private GameObject lastRightElement;

		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The playlist service
		/// </summary>
		private IPlaylistService playlistService;

		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="playlist">The playlist.</param>
		public void Init(IInputService input, IPlaylistService playlist) {
			this.inputService = input;
			this.playlistService = playlist;
			this.playlistService.Configure(this.module.Parameters.FirstOrDefault(), new string[] { ".png", ".jpg", ".jpeg" });
		}

		/// <summary>
		/// Update the instance
		/// </summary>
		public void Update() {
			foreach (IAction action in this.inputService.GetMovements()) {
				switch (action.Type) {
					case ActionType.Right:
						this.playlistService.Next();
						break;
					case ActionType.Left:
						this.playlistService.Previous();
						break;
					case ActionType.Rotate:
						this.transform.Rotate(new Vector3(0, 1, 0), 90);
						break;
				}
			}
		}

		/// <summary>
		/// Draws the slideshow.
		/// </summary>
		private void DrawSlideshow() {
			for (int i = 0; i < (this.NumberOfSideElements * 2) + 1; ++i) {
				this.DrawSlideshowElement((i / 2) * (((i % 2) * 2) - 1));
			}
		}

		/// <summary>
		/// Draws the slideshow element.
		/// </summary>
		/// <param name="offset">The offset.</param>
		private void DrawSlideshowElement(int offset) {
			GameObject Element = GameObject.Instantiate(this.BaseElement);
		}
	}
}
