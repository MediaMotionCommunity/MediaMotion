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
		/// The editor next action
		/// </summary>
		public ActionType EditorNextAction = ActionType.Right;

		/// <summary>
		/// The editor next action
		/// </summary>
		public ActionType EditorPreviousAction = ActionType.Left;

		/// <summary>
		/// The base element
		/// </summary>
		public GameObject BaseElement;

		/// <summary>
		/// The base floor
		/// </summary>
		public GameObject BaseFloor;

		/// <summary>
		/// The base background
		/// </summary>
		public GameObject BaseBackground;

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
		/// The next action
		/// </summary>
		protected ActionType nextAction;

		/// <summary>
		/// The previous action
		/// </summary>
		protected ActionType previousAction;

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
		/// The background
		/// </summary>
		protected GameObject background;

		/// <summary>
		/// The floor
		/// </summary>
		protected GameObject floor;

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
			this.nextAction = this.EditorNextAction;
			this.previousAction = this.EditorPreviousAction;

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
				switch (action.Type) {
					case ActionType.Rotate:
						if (this.module.SupportedAction.Contains(ActionType.Rotate) && this.elements[this.sideElements] != null) {
							this.elements[this.sideElements].transform.Find("Tile").gameObject.GetComponent<TileScript>().Rotate(90.0f);
						}
						break;
					case ActionType.ZoomIn:
						if (this.module.SupportedAction.Contains(ActionType.ZoomIn)) {

						}
						break;
					case ActionType.Select:
						if (this.module.SupportedAction.Contains(ActionType.Select)) {
							this.Select(this.elements[this.sideElements]);
						}
						break;
					default:
						if (action.Type == this.nextAction) {
							this.Unselect(this.elements[this.sideElements]);
							this.Next();
						} else if (action.Type == this.previousAction) {
							this.Unselect(this.elements[this.sideElements]);
							this.Previous();
						}
						break;
				}
			}
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
				element.transform.Find("Tile").gameObject.GetComponent<TileScript>().Select();
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
				element.transform.Find("Tile").gameObject.GetComponent<TileScript>().Unselect();
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
			if (this.BaseFloor != null) {
				this.floor = GameObject.Instantiate(this.BaseFloor);
				this.floor.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
				this.floor.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
			}
			if (this.BaseBackground != null) {
				this.background = GameObject.Instantiate(this.BaseBackground);
				this.background.transform.localPosition = new Vector3(0.0f, 0.0f, 50.0f);
				this.background.transform.localRotation = Quaternion.Euler(new Vector3(270.0f, 0.0f, 0.0f));
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
			this.elements[end] = this.GetSlideshowElement(-step * this.sideElements);
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
			return (new Vector3(Math.Sign(offset) * ((5 + Math.Abs(offset)) - 1), 2.2f, Math.Abs(Math.Sign(offset)) * (3.0f - (Math.Abs(offset) * 0.5f))));
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
