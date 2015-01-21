using System.Collections;
using System.Collections.Generic;
using MediaMotion.Core.Models;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Core.Controllers {
	/// <summary>
	/// Explorer Controller
	/// </summary>
	public class FolderContentController : IModule {
		/// <summary>
		/// The row size
		/// </summary>
		private int RowSize = 5;

		/// <summary>
		/// The origin x
		/// </summary>
		private float OriginX = -3f;

		/// <summary>
		/// The origin z
		/// </summary>
		private float OriginZ = -12.5f;

		/// <summary>
		/// The increment x
		/// </summary>
		private float IncrementX = 1.5f;

		/// <summary>
		/// The increment z
		/// </summary>
		private float IncrementZ = 1.5f;

		/// <summary>
		/// The tiles
		/// </summary>
		private List<GameObject> Tiles;

		/// <summary>
		/// The filenames
		/// </summary>
		private List<GameObject> Filenames;

		/// <summary>
		/// The current index
		/// </summary>
		private int CurrentIndex;

		/// <summary>
		/// The file service
		/// </summary>
		private FileSystemService FileService;

		/// <summary>
		/// The texture map
		/// </summary>
		private Dictionary<ElementType, string> TextureMap;

		/// <summary>
		/// The line
		/// </summary>
		private int Line;

		/// <summary>
		/// The camera
		/// </summary>
		private GameObject Camera;

		/// <summary>
		/// The content
		/// </summary>
		private List<IElement> Content;
        Font ArialFont;

		/// <summary>
		/// The light
		/// </summary>
		private GameObject light;

		/// <summary>
		/// Initializes a new instance of the <see cref="FolderContentController"/> class.
		/// </summary>
		public FolderContentController() {
			this.FileService = FileSystemService.GetInstance();
			this.TextureMap = new Dictionary<ElementType, string>();
			this.TextureMap.Add(ElementType.File, "File-icon");
			this.TextureMap.Add(ElementType.Folder, "Folder-icon");
			this.Tiles = new List<GameObject>();
			this.Filenames = new List<GameObject>();
			this.CurrentIndex = 0;
			this.Line = 0;
            this.ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
			this.Camera = GameObject.Find("Main Camera");
			this.light = GameObject.Find("Point light");
			this.light.transform.parent = this.Camera.transform;
			iTween.Init(this.Camera);
//			iTween.Init(this.light);

		}

		/// <summary>
		/// Loads this instance.
		/// </summary>
		public void Load() {
			this.EnterDirectory();
		}

		/// <summary>
		/// Registers this instance.
		/// </summary>
		public void Register() {
		}

		/// <summary>
		/// Unregisters this instance.
		/// </summary>
		public void Unregister() {
		}

		/// <summary>
		/// Unloads this instance.
		/// </summary>
		public void Unload() {
		}

		/// <summary>
		/// Actions the handle.
		/// </summary>
		/// <param name="Sender">The sender.</param>
		/// <param name="Action">The <see cref="ActionDetectedEventArgs"/> instance containing the event data.</param>
		public void ActionHandle(object Sender, ActionDetectedEventArgs Action) {
			Debug.Log(Action.Action.Type);
			switch (Action.Action.Type) {
				case ActionType.Left:
					this.MoveLeft();
					break;
				case ActionType.Right:
					this.MoveRight();
					break;
				case ActionType.ScrollIn:
					this.MoveDown();
					break;
				case ActionType.ScrollOut:
					this.MoveUp();
					break;
				case ActionType.Select:
					this.Open();
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Highlights the current.
		/// </summary>
		/// <param name="Current">The object.</param>
		private void HighlightCurrent(GameObject Current) {
//			Current.transform.position = new Vector3(Current.transform.position.x, 2, Current.transform.position.z);
			iTween.MoveTo(Current, new Vector3(Current.transform.position.x, 2, Current.transform.position.z), 0.5f);
		}

		/// <summary>
		/// Cancels the highlight.
		/// </summary>
		/// <param name="Current">The object.</param>
		private void CancelHighlight(GameObject Current) {
//			Current.transform.position = new Vector3(Current.transform.position.x, 1, Current.transform.position.z);
			iTween.MoveTo(Current, new Vector3(Current.transform.position.x, 1, Current.transform.position.z), 0.5f);
		}

		/// <summary>
		/// Changes the selection.
		/// </summary>
		/// <param name="Offset">The offset.</param>
		private void ChangeSelection(int Offset) {
			this.CancelHighlight(this.Tiles[this.CurrentIndex]);
			this.HighlightCurrent(this.Tiles[this.CurrentIndex + Offset]);
			this.CurrentIndex += Offset;
		}

		private Vector3 camPos = new Vector3(0, 0, 0);
		private Vector3 lightPos = new Vector3(0, 0, 0);

		/// <summary>
		/// Moves up.
		/// </summary>
		private void MoveUp() {
			if (this.CurrentIndex - this.RowSize >= 0) {
				if (this.Line++ % 2 == 0) {
					float time = 0.5f;
					Vector3 vect = new Vector3(0, 0, -3);
					if (camPos == Vector3.zero) {
						camPos = this.Camera.transform.position + vect;
					}
					else {
						camPos += vect;
					}
					Hashtable camHash = new Hashtable();
					camHash.Add("z", camPos.z);
					camHash.Add("space", Space.World);
					camHash.Add("islocal", true);
					camHash.Add("easetype", iTween.EaseType.easeInOutSine);
					camHash.Add("time", time);
					iTween.MoveTo(this.Camera, camHash);
//					this.Camera.transform.Translate(0, 0, -3, Space.World);
				}
				this.ChangeSelection(-this.RowSize);
			}
		}

		/// <summary>
		/// Moves down.
		/// </summary>
		private void MoveDown() {
			if (this.CurrentIndex + this.RowSize < this.Tiles.Count) {
				if (--this.Line % 2 == 0) {
//					this.Camera.transform.Translate(0, 0, 3, Space.World);
					float time = 0.5f;
					Vector3 vect = new Vector3(0, 0, 3);
					if (camPos == Vector3.zero) {
						camPos = this.Camera.transform.position + vect;
					}
					else {
						camPos += vect;
					}
					Hashtable camHash = new Hashtable();
					camHash.Add("z", camPos.z);
					camHash.Add("space", Space.World);
					camHash.Add("islocal", true);
					camHash.Add("easetype", iTween.EaseType.easeInOutSine);
					camHash.Add("time", time);
					iTween.MoveTo(this.Camera, camHash);
				}
				this.ChangeSelection(this.RowSize);
			}
		}

		/// <summary>
		/// Moves the left.
		/// </summary>
		private void MoveLeft() {
			int LineOffset = 0;

			if ((this.CurrentIndex % this.RowSize) - 1 < 0) {
				if (this.CurrentIndex + this.RowSize - 1 >= this.Tiles.Count) {
					LineOffset += (this.Tiles.Count % this.RowSize) - 1;
				} else {
					LineOffset += this.RowSize - 1;
				}
			} else {
				LineOffset -= 1;
			}
			this.ChangeSelection(LineOffset);
		}

		/// <summary>
		/// Moves the right.
		/// </summary>
		private void MoveRight() {
			int LineOffset = 0;

			if ((this.CurrentIndex + 1) % this.RowSize == 0 || this.CurrentIndex + 1 >= this.Tiles.Count) {
				LineOffset -= this.CurrentIndex % this.RowSize;
			} else {
				LineOffset += 1;
			}
			this.ChangeSelection(LineOffset);
		}

		/// <summary>
		/// Displays the content.
		/// </summary>
		private void DisplayContent() {
			int i = 0;
			float x = this.OriginX;
			float z = this.OriginZ;

			this.Clear();
			foreach (IElement file in this.Content) {
				GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
				GameObject tileText = new GameObject();
				tileText.transform.parent = tile.transform;

				tile.transform.position = new Vector3(x, 1, z);
				tile.transform.eulerAngles = new Vector3(60, 180, 0);
				tile.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
				tile.renderer.material.mainTexture = Resources.Load<Texture2D>(this.TextureMap[file.GetElementType()]);
				tile.renderer.material.shader = Shader.Find("Transparent/Diffuse");
				tile.renderer.material.color = new Color(0.3f, 0.6f, 0.9f, 1);
				tile.name = "tile_" + file.GetName();

				////				tile.AddComponent("FolderHover");
				////tile.AddComponent(COMPONENT_POUR_INFOS_FICHIER);

                tileText.transform.position = new Vector3(x - 0.4f, 0.45f, z - 0.2f);
                TextMesh tileTextMesh = tileText.AddComponent(typeof(TextMesh)) as TextMesh;
                tileTextMesh.transform.parent = tileText.transform;
                tileTextMesh.font = this.ArialFont;
                tileTextMesh.fontSize = 16;
                tileTextMesh.renderer.material = tileTextMesh.font.material;
                tileTextMesh.text = file.GetName();
//				tileText.transform.eulerAngles = new Vector3(30, 0, 0);
//				tileText.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
				tileText.transform.eulerAngles = new Vector3(30, 0, 0);
				tileText.transform.localScale = new Vector3(0.9F, 0.9F, 0.9F);
                Filenames.Add(tileText);

				this.Tiles.Add(tile);
				++i;
				if (i % this.RowSize == 0) {
					x = this.OriginX;
				} else {
					x += this.IncrementX;
				}
				if (i % this.RowSize == 0) {
					z += this.IncrementZ;
				}
				this.HighlightCurrent(this.Tiles[this.CurrentIndex]);
			}
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		private void Clear() {
			this.Camera.transform.position = new Vector3(0, 5, -15);
			foreach (GameObject tile in this.Tiles) {
				GameObject.Destroy(tile);
			}
			this.Tiles.Clear();
			this.CurrentIndex = 0;
		}

		/// <summary>
		/// Enters the directory.
		/// </summary>
		/// <param name="Destination">The destination.</param>
		private void EnterDirectory(IFolder Destination = null) {
			this.FileService.ChangeDirectory(Destination);
			this.Content = this.FileService.GetDirectoryContent();
			this.DisplayContent();
		}

		/// <summary>
		/// Opens this instance.
		/// </summary>
		private void Open() {
			if (this.Content != null && this.Content.Count > this.CurrentIndex) {
				switch (this.Content[this.CurrentIndex].GetElementType()) {
					case ElementType.Folder:
						this.EnterDirectory(this.Content[this.CurrentIndex] as IFolder);
						break;
					default:
						break;
				}
			}
		}
	}
}