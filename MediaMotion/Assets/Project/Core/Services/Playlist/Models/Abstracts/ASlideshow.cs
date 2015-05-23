using System;
using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Abstracts {
	/// <summary>
	/// SlideshowController
	/// </summary>
	/// <typeparam name="Module">The type of the module.</typeparam>
	/// <typeparam name="TileScript">The type of the tile script.</typeparam>
	/// <typeparam name="ElementScript">The type of the animation script.</typeparam>
	public class ASlideshow<Module, TileScript, ElementScript> : AScript<Module, ASlideshow<Module, TileScript, ElementScript>>
		where Module : class, IModule
		where TileScript : MonoBehaviour, ISlideshowTile
		where ElementScript : MonoBehaviour, ISlideshowElement {
		/// <summary>
		/// The number of elements
		/// </summary>
		[Range(0, 7)]
		public int NumberOfSideElements;

		/// <summary>
		/// The element
		/// </summary>
		public GameObject BaseElement;

		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The playlist service
		/// </summary>
		private IPlaylistService playlistService;

		/// <summary>
		/// The elements
		/// </summary>
		private GameObject[] elements;

		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="playlist">The playlist.</param>
		public void Init(IInputService input, IPlaylistService playlist) {
			this.inputService = input;
			this.playlistService = playlist;
			this.elements = new GameObject[this.NumberOfSideElements * 2 + 1];

			this.playlistService.Configure(this.module.Parameters.FirstOrDefault(), this.module.SupportedExtensions);
			for (int offset = -this.NumberOfSideElements; offset <= this.NumberOfSideElements; ++offset) {
				this.elements[offset + this.NumberOfSideElements] = this.CreateSlideshowElement(offset);
			}
		}

		/// <summary>
		/// Update the instance
		/// </summary>
		public void Update() {
			foreach (IAction action in this.inputService.GetMovements()) {
				switch (action.Type) {
					case ActionType.Right:
						this.Next();
						break;
					case ActionType.Left:
						this.Previous();
						break;
				}
			}
		}

		/// <summary>
		/// Next element in the playlist
		/// </summary>
		protected virtual void Next() {
			if (this.playlistService.Next() != null) {
				for (int i = 0; i < this.elements.Length; ++i) {
					this.AnimateElement(i, -1, i == 0);
					if (i > 0) {
						this.elements[i - 1] = this.elements[i];
					}
				}
				this.elements[this.elements.Length - 1] = this.CreateSlideshowElement((this.elements.Length - 1) / 2);
			}
		}

		/// <summary>
		/// Previous element in the playlist
		/// </summary>
		protected virtual void Previous() {
			if (this.playlistService.Previous() != null) {
				for (int i = this.elements.Length - 1; i >= 0; --i) {
					this.AnimateElement(i, 1, i == this.elements.Length - 1);
					if (i < this.elements.Length - 1) {
						this.elements[i + 1] = this.elements[i];
					}
				}
				this.elements[0] = this.CreateSlideshowElement(-((this.elements.Length - 1) / 2));
			}
		}

		/// <summary>
		/// Animates the element.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="destroy">if set to <c>true</c> [destroy].</param>
		protected virtual void AnimateElement(int index, int offset, bool destroy) {
			if (this.elements[index] != null) {
				ElementScript elementScript = this.elements[index].GetComponent<ElementScript>();

				elementScript.Offset += offset;
				elementScript.TransformTo(this.ComputeLocalScale(elementScript.Offset), this.ComputeLocalPosition(elementScript.Offset), this.ComputeLocalRotation(elementScript.Offset), destroy);
			}
		}

		/// <summary>
		/// Draws the slideshow element.
		/// </summary>
		/// <param name="offset">The offset.</param>
		protected virtual GameObject CreateSlideshowElement(int offset) {
			IFile file = this.playlistService.Peek(offset);

			if (file != null) {
				GameObject element = GameObject.Instantiate(this.BaseElement);
				GameObject tile = element.transform.FindChild("Tile").gameObject;

				element.AddComponent<ElementScript>().Offset = offset;
				tile.AddComponent<TileScript>().File = file;

				element.transform.parent = this.gameObject.transform;
				element.transform.localScale = this.ComputeLocalScale(offset);
				element.transform.localPosition = this.ComputeLocalPosition(offset);
				element.transform.localRotation = this.ComputeLocalRotation(offset);
				return (element);
			}
			return (null);
		}

		/// <summary>
		/// Computes the local scale using the <see cref="offset"/>.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		///   The local scale
		/// </returns>
		protected virtual Vector3 ComputeLocalScale(int offset) {
			return (new Vector3(Math.Max(0.38f - (Math.Abs(offset) * 0.05f), 0.0f), Math.Max(0.38f - (Math.Abs(offset) * 0.05f), 0.0f), Math.Max(0.38f - (Math.Abs(offset) * 0.05f), 0.0f)));
		}

		/// <summary>
		/// Computes the local position using the <see cref="offset"/>.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		///   The local position
		/// </returns>
		protected virtual Vector3 ComputeLocalPosition(int offset) {
			return (new Vector3(Math.Sign(offset) * (5 + Math.Abs(offset) - 1), 0.0f, Math.Abs(Math.Sign(offset)) * (3.0f - Math.Abs(offset) * 0.5f)));
		}

		/// <summary>
		/// Computes the local rotation using the <see cref="offset"/>.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		///   The local rotation
		/// </returns>
		protected virtual Quaternion ComputeLocalRotation(int offset) {
			return (Quaternion.Euler(0.0f, Math.Sign(offset) * (50f + Math.Abs(offset) * 10), 0.0f));
		}
	}

	/// <summary>
	/// Slideshow Abstract
	/// </summary>
	/// <typeparam name="Module">The type of the module.</typeparam>
	/// <typeparam name="TileScript">The type of the tile script.</typeparam>
	public class ASlideshow<Module, TileScript> : ASlideshow<Module, TileScript, SlideshowElement>
		where Module : class, IModule
		where TileScript : MonoBehaviour, ISlideshowTile {
	}
}
