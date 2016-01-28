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
	public class ASlideshow<Module, Child, TileScript, ElementScript, FloorScript, BackgroundScript> : AScript<Module, Child>
		where Module : class, IModule
		where Child : ASlideshow<Module, Child, TileScript, ElementScript, FloorScript, BackgroundScript>
		where TileScript : MonoBehaviour, ISlideshowTile
		where ElementScript : MonoBehaviour, ISlideshowElement
		where FloorScript : MonoBehaviour, ISlideshowEnvironment
		where BackgroundScript : MonoBehaviour, ISlideshowEnvironment {
		/// <summary>
		/// The editor number of side elements
		/// </summary>
		[Range(0, 10)]
		public int EditorSideElements = 3;

		/// <summary>
		/// The editor buffer size
		/// </summary>
		[Range(4, 20)]
		public int EditorBufferSize = 8;

		/// <summary>
		/// The editor center space
		/// </summary>
		public float EditorCenterSpace = 5.0f;

		/// <summary>
		/// The base element
		/// </summary>
		public GameObject BaseElement;

		/// <summary>
		/// The floor
		/// </summary>
		public GameObject Floor;

		/// <summary>
		/// The background
		/// </summary>
		public GameObject Background;

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
		/// The side elements
		/// </summary>
		protected int sideElements;

		/// <summary>
		/// The elements
		/// </summary>
		protected GameObject[] elements;

		/// <summary>
		/// The buffer size
		/// </summary>
		protected int bufferSize;

		/// <summary>
		/// The buffer
		/// </summary>
		protected Queue<GameObject> buffer;

		/// <summary>
		/// The center space
		/// </summary>
		protected float centerSpace;

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

			this.sideElements = this.EditorSideElements;
			this.bufferSize = this.EditorBufferSize;
			this.centerSpace = this.EditorCenterSpace;

			this.InitPlaylist();
			this.InitBuffers();
			this.InitScene();
			this.Select(this.elements[this.sideElements]);
		}

		/// <summary>
		/// Update the instance
		/// </summary>
		public virtual void Update() {
			foreach (IAction action in this.inputService.GetMovements()) {
				if (this.elements[this.sideElements] != null) {
					switch (action.Type) {
						case ActionType.Right:
							this.Unselect(this.elements[this.sideElements]);
							this.Next();
							break;
						case ActionType.Left:
							this.Unselect(this.elements[this.sideElements]);
							this.Previous();
							break;
						case ActionType.Select:
							this.Select(this.elements[this.sideElements]);
							break;
						case ActionType.Zoom:
							this.elements[this.sideElements].transform.Find("Tile").gameObject.GetComponent<TileScript>().Zoom((float)action.Parameter);
							break;
						case ActionType.Rotate:
							this.elements[this.sideElements].transform.Find("Tile").gameObject.GetComponent<TileScript>().Rotate((float)action.Parameter);
							break;
					}
				}
			}
		}

		/// <summary>
		/// Called when [destroy].
		/// </summary>
		public virtual void OnDestroy() {
			this.playlistService.Reset();
		}

		/// <summary>
		/// Selects the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		protected virtual void Select(GameObject element) {
			foreach (GameObject current in this.elements) {
				if (current != element) {
					this.Unselect(current);
				}
			}
			if (element != null) {
				element.transform.Find("Tile").gameObject.GetComponent<TileScript>().Fullscreen = true;
			}
			if (this.Floor != null) {
				this.Floor.GetComponent<FloorScript>().Fullscreen = true;
			}
			if (this.Background != null) {
				this.Background.GetComponent<BackgroundScript>().Fullscreen = true;
			}
		}

		/// <summary>
		/// Unselects all.
		/// </summary>
		protected virtual void UnselectAll() {
			foreach (GameObject element in this.elements) {
				this.Unselect(element);
			}
		}

		/// <summary>
		/// Unselects the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		protected virtual void Unselect(GameObject element) {
			if (element != null) {
				element.transform.Find("Tile").gameObject.GetComponent<TileScript>().Fullscreen = false;
			}
			if (this.Floor != null) {
				this.Floor.GetComponent<FloorScript>().Fullscreen = false;
			}
			if (this.Background != null) {
				this.Background.GetComponent<BackgroundScript>().Fullscreen = false;
			}
		}

		/// <summary>
		/// Initializes the playlist.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the playlist is correctly initialized, <c>false</c> otherwise
		/// </returns>
		protected virtual bool InitPlaylist() {
			return (this.module.Parameters != null && ((this.module.Parameters.Count() > 1) ? (this.playlistService.Configure(this.module.Parameters)) : (this.playlistService.Configure(this.module.Parameters.FirstOrDefault(), this.module.SupportedExtensions))));
		}

		/// <summary>
		/// Initializes the buffers.
		/// </summary>
		protected virtual void InitBuffers() {
			this.elements = new GameObject[(this.sideElements * 2) + 1];
			this.buffer = new Queue<GameObject>();

			for (int offset = -this.sideElements; offset <= this.sideElements; ++offset) {
				this.elements[offset + this.sideElements] = this.GetSlideshowElement(offset);
			}
			for (int i = 0; i < this.bufferSize; ++i) {
				this.buffer.Enqueue(this.CreateSlideshowElement());
			}
		}

		/// <summary>
		/// Initializes the scene.
		/// </summary>
		protected virtual void InitScene() {
			if (this.Floor != null) {
				this.Floor.AddComponent<FloorScript>();
			}
			if (this.Background != null) {
				this.Background.AddComponent<BackgroundScript>();
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
					int newPosition = index - this.sideElements + step;

					this.elements[index].GetComponent<ElementScript>().AnimateTo(this.ComputeLocalScale(this.elements[index], newPosition), this.ComputeLocalPosition(this.elements[index], newPosition), this.ComputeLocalRotation(this.elements[index], newPosition), !keepInSlideshow);
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
			this.elements[end] = this.GetSlideshowElement(-step * this.sideElements);
		}

		/// <summary>
		/// Gets the slideshow element.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		///   The game object initialized using the file <see cref="offset"/> or <c>null</c> if any object match
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
		/// <param name="gameObject">The element.</param>
		/// <param name="offset">The offset.</param>
		/// <returns>
		/// The game object or <c>null</c> if no file found
		/// </returns>
		protected virtual GameObject InitSlideshowElement(GameObject gameObject, int offset) {
			IComparable element = this.playlistService.Peek(offset);

			if (element != null) {
				if (gameObject == null) {
					gameObject = this.CreateSlideshowElement();
				}
				gameObject.GetComponent<ElementScript>().Reload();
				gameObject.transform.FindChild("Tile").gameObject.GetComponent<TileScript>().Load(element);

				gameObject.transform.localScale = this.ComputeLocalScale(gameObject, offset);
				gameObject.transform.localPosition = this.ComputeLocalPosition(gameObject, offset);
				gameObject.transform.localRotation = this.ComputeLocalRotation(gameObject, offset);
				gameObject.SetActive(true);
				return (gameObject);
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
		protected virtual Vector3 ComputeLocalScale(GameObject element, int offset) {
			return (new Vector3(Math.Max(0.38f - (Math.Abs(offset) * 0.05f), 0.0f), Math.Max(0.38f - (Math.Abs(offset) * 0.05f), 0.0f), Math.Max(0.38f - (Math.Abs(offset) * 0.05f), 0.0f)));
		}

		/// <summary>
		/// Computes the local position using the <see cref="offset"/>.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		///   The local position
		/// </returns>
		protected virtual Vector3 ComputeLocalPosition(GameObject element, int offset) {
			return (new Vector3(Math.Sign(offset) * ((this.centerSpace + Math.Abs(offset)) - 1), 2.0f, Math.Abs(Math.Sign(offset)) * (3.0f - (Math.Abs(offset) * 0.5f))));
		}

		/// <summary>
		/// Computes the local rotation using the <see cref="offset"/>.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		///   The local rotation
		/// </returns>
		protected virtual Quaternion ComputeLocalRotation(GameObject element, int offset) {
			return (Quaternion.Euler(0.0f, Math.Sign(offset) * (50f + (Math.Abs(offset) * 10)), 0.0f));
		}
	}

	/// <summary>
	/// Slideshow Abstract
	/// </summary>
	/// <typeparam name="Module">The type of the module.</typeparam>
	/// <typeparam name="Child">The type of the child.</typeparam>
	/// <typeparam name="TileScript">The type of the tile script.</typeparam>
	public class ASlideshow<Module, Child, TileScript, ElementScript> : ASlideshow<Module, Child, TileScript, ElementScript, SlideshowDefaultEnvironment, SlideshowDefaultEnvironment>
		where Module : class, IModule
		where Child : ASlideshow<Module, Child, TileScript, ElementScript>
		where TileScript : MonoBehaviour, ISlideshowTile
		where ElementScript : MonoBehaviour, ISlideshowElement {
	}

	/// <summary>
	/// Slideshow Abstract
	/// </summary>
	/// <typeparam name="Module">The type of the module.</typeparam>
	/// <typeparam name="TileScript">The type of the tile script.</typeparam>
	public class ASlideshow<Module, TileScript, ElementScript> : ASlideshow<Module, ASlideshow<Module, TileScript, ElementScript>, TileScript, ElementScript>
		where Module : class, IModule
		where TileScript : MonoBehaviour, ISlideshowTile
		where ElementScript : MonoBehaviour, ISlideshowElement {
	}
}
