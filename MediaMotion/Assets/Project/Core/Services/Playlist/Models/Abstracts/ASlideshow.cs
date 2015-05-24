using System;
using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Abstracts {
	/// <summary>
	/// Slideshow Abstract
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
		[Range(0, 10)]
		public int NumberOfSideElements = 3;

		/// <summary>
		/// The buffer size
		/// </summary>
		[Range(4, 20)]
		public int BufferSize = 8;

		/// <summary>
		/// The element
		/// </summary>
		public GameObject BaseElement;

		/// <summary>
		/// The buffer access
		/// </summary>
		protected readonly object BufferAccess = new object();

		/// <summary>
		/// The input service
		/// </summary>
		protected IInputService inputService;

		/// <summary>
		/// The element factory
		/// </summary>
		protected IElementFactory elementFactory;

		/// <summary>
		/// The playlist service
		/// </summary>
		protected IPlaylistService playlistService;

		/// <summary>
		/// The elements
		/// </summary>
		protected GameObject[] elements;

		/// <summary>
		/// The buffer
		/// </summary>
		protected Queue<GameObject> buffer;

		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="inputService">The input.</param>
		/// <param name="elementFactory">The element factory.</param>
		/// <param name="playlistService">The playlist.</param>
		public void Init(IInputService inputService, IElementFactory elementFactory, IPlaylistService playlistService) {
			this.inputService = inputService;
			this.elementFactory = elementFactory;
			this.playlistService = playlistService;

			this.InitPlaylist();
			this.InitBuffers();
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
		/// Initializes the playlist.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the playlist is correctly initialized, <c>false</c> otherwise
		/// </returns>
		protected virtual bool InitPlaylist() {
			return (this.module.Parameters != null && this.playlistService.Configure(this.module.Parameters.FirstOrDefault(), this.module.SupportedExtensions));
		}

		/// <summary>
		/// Initializes the buffers.
		/// </summary>
		protected virtual void InitBuffers() {
			this.elements = new GameObject[(this.NumberOfSideElements * 2) + 1];
			this.buffer = new Queue<GameObject>();

			for (int offset = -this.NumberOfSideElements; offset <= this.NumberOfSideElements; ++offset) {
				this.elements[offset + this.NumberOfSideElements] = this.GetSlideshowElement(offset);
			}
			for (int i = 0; i < this.BufferSize; ++i) {
				this.buffer.Enqueue(this.CreateSlideshowElement());
			}
		}

		/// <summary>
		/// Next element in the playlist
		/// </summary>
		protected virtual void Next() {
			if (this.playlistService.Next() != null) {
				this.AnimateElements(true);
			}
		}

		/// <summary>
		/// Previous element in the playlist
		/// </summary>
		protected virtual void Previous() {
			if (this.playlistService.Previous() != null) {
				this.AnimateElements(false);
			}
		}

		/// <summary>
		/// Animates the elements.
		/// </summary>
		/// <param name="rightToLeft">if set to <c>true</c> [right to left].</param>
		protected virtual void AnimateElements(bool rightToLeft) {
			int start = (rightToLeft) ? (0) : (this.elements.Length - 1);
			int end = (rightToLeft) ? (this.elements.Length - 1) : (0);
			int step = (rightToLeft) ? (-1) : (1);

			for (int index = start; (rightToLeft) ? (index <= end) : (index >= end); index -= step) {
				bool keepInSlideshow = (rightToLeft && index > start) || (!rightToLeft && index < start);

				if (this.elements[index] != null) {
					int newPosition = index - this.NumberOfSideElements + step;

					this.elements[index].GetComponent<ElementScript>().AnimateTo(this.ComputeLocalScale(newPosition), this.ComputeLocalPosition(newPosition), this.ComputeLocalRotation(newPosition), !keepInSlideshow);
					if (!keepInSlideshow) {
						lock (this.BufferAccess) {
							this.buffer.Enqueue(this.elements[index]);
						}
					}
				}
				if (keepInSlideshow) {
					this.elements[index + step] = this.elements[index];
				}
			}
			this.elements[end] = this.GetSlideshowElement(-step * this.NumberOfSideElements);
		}

		/// <summary>
		/// Gets the slideshow element.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		///   The game object initialized using the file <see cref="offset"/> or <c>null</c> if any file match
		/// </returns>
		protected virtual GameObject GetSlideshowElement(int offset) {
			lock (this.BufferAccess) {
				if (this.buffer.Count > 0) {
					GameObject element = this.InitSlideshowElement(this.buffer.Peek(), offset);

					if (element != null) {
						return (this.buffer.Dequeue());
					}
				}
			}
			return (this.InitSlideshowElement(null, offset));
		}

		/// <summary>
		/// Creates the slideshow element.
		/// </summary>
		/// <returns>
		///   The GameObject
		/// </returns>
		protected virtual GameObject CreateSlideshowElement() {
			GameObject element = GameObject.Instantiate(this.BaseElement);

			element.AddComponent<ElementScript>();
			element.transform.FindChild("Tile").gameObject.AddComponent<TileScript>();
			element.transform.parent = this.gameObject.transform;
			element.SetActive(false);
			return (element);
		}

		/// <summary>
		/// Initializes the slideshow element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="offset">The offset.</param>
		/// <returns>
		/// The game object or <c>null</c> if no file found
		/// </returns>
		protected virtual GameObject InitSlideshowElement(GameObject element, int offset) {
			IFile file = this.playlistService.Peek(offset);

			if (file != null) {
				if (element == null) {
					element = this.CreateSlideshowElement();
				}
				element.GetComponent<ElementScript>().Reload();
				element.transform.FindChild("Tile").gameObject.GetComponent<TileScript>().LoadFile(file);

				element.transform.localScale = this.ComputeLocalScale(offset);
				element.transform.localPosition = this.ComputeLocalPosition(offset);
				element.transform.localRotation = this.ComputeLocalRotation(offset);
				element.SetActive(true);
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
			return (new Vector3((Math.Sign(offset) * (5 + Math.Abs(offset)) - 1), 0.0f, (Math.Abs(Math.Sign(offset)) * (3.0f - Math.Abs(offset)) * 0.5f)));
		}

		/// <summary>
		/// Computes the local rotation using the <see cref="offset"/>.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		///   The local rotation
		/// </returns>
		protected virtual Quaternion ComputeLocalRotation(int offset) {
			return (Quaternion.Euler(0.0f, Math.Sign(offset) * (50f + (Math.Abs(offset) * 10)), 0.0f));
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
