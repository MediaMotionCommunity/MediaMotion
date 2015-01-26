using System.Collections;
using System.Collections.Generic;
using System.IO;
using MediaMotion.Core;
using MediaMotion.Core.Models;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

using System.Timers;
using MediaMotion.Core.View;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Explorer Controller
	/// </summary>
	public class FolderContentController : MonoBehaviour {
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
		private IFileSystem FileService;

		/// <summary>
		/// The input
		/// </summary>
		private IInput Input;

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
		private GameObject Cursor;

		/// <summary>
		/// The camera position
		/// </summary>
		private Vector3 camPos = new Vector3(0, 0, 0);

		/// <summary>
		/// The content
		/// </summary>
		private List<IElement> Content;
		Font ArialFont;

        /// <summary>
        /// Timer to display the file info popup.
        /// </summary>
        private Timer PopupTimer;

        /// <summary>
        /// Popup time value.
        /// </summary>
        private int PopupTime;

        /// <summary>
        /// Popup reference.
        /// </summary>
        private FileInfoUI Popup;
        
        /// <summary>
		/// Initializes a new instance of the <see cref="FolderContentController"/> class.
		/// </summary>
		public FolderContentController() {
			this.Input = MediaMotionCore.Core.GetService("Input") as IInput;
			this.FileService = MediaMotionCore.Core.GetService("FileSystem") as IFileSystem;
			
            this.Tiles = new List<GameObject>();
			this.Filenames = new List<GameObject>();

			this.TextureMap = new Dictionary<ElementType, string>();
			this.TextureMap.Add(ElementType.File, "File-icon");
			this.TextureMap.Add(ElementType.Folder, "Folder-icon");
//			iTween.Init(this.light);
		}
			

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start() {
			this.Line = 0;
			this.CurrentIndex = 0;
			this.ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
			this.Camera = GameObject.Find("Camera/Main");
            this.Camera.AddComponent("FileInfoUI");

            this.Popup = this.Camera.GetComponent<FileInfoUI>();
            this.PopupTime = 2000;
            this.PopupTimer = new System.Timers.Timer(this.PopupTime);
            this.PopupTimer.Elapsed += DisplayFilePopup;
            
            iTween.Init(this.Camera);

			this.EnterDirectory();
			//			iTween.Init(this.light);
		}

		/// <summary>
		/// Highlights the current.
		/// </summary>
		/// <param name="Current">The object.</param>
		private void HighlightCurrent(GameObject Current) {
//			Current.transform.position = new Vector3(Current.transform.position.x, 2, Current.transform.position.z);
			iTween.MoveTo(Current, new Vector3(Current.transform.position.x, 2, Current.transform.position.z), 0.5f);
            this.PopupTimer.Interval = this.PopupTime;
            this.PopupTimer.Start();
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
                this.Camera.GetComponent<FileInfoUI>().Hide();
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
                this.Camera.GetComponent<FileInfoUI>().Hide();
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
            this.Camera.GetComponent<FileInfoUI>().Hide();
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
            this.Camera.GetComponent<FileInfoUI>().Hide();
		}

		/// <summary>
		/// Add a light
		/// </summary>
		private void AddLight(Vector3 position, Color color, float range) {
			GameObject lightC = new GameObject();
			lightC.AddComponent<Light>();
			lightC.light.name = "light_" + position.x + "_" + position.y + "_" + position.z;
			lightC.light.transform.position = position;
			lightC.light.color = color;
			lightC.light.range = range;
			lightC.light.intensity = 1.4f;
			lightC.light.renderMode = LightRenderMode.ForcePixel;
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
//				tile.renderer.material.mainTextureOffset = new Vector2(0, -0.001f);
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
				tileText.transform.eulerAngles = new Vector3(30, 0, 0);
				tileText.transform.localScale = new Vector3(0.9F, 0.9F, 0.9F);
//				tileText.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
                Filenames.Add(tileText);

				Hashtable color = new Hashtable();
				color.Add("r", 0.8);
				color.Add("g", 0.9);
				color.Add("b", 1);
				color.Add("time", 0.1);
				iTween.ColorTo(tileText, color);

				color = new Hashtable();
				if (tile.renderer.bounds.size.x < tileText.renderer.bounds.size.x) {
//					color.Add("r", 1);
//					color.Add("g", 0.5);
//					color.Add("b", 0.5);
//					color.Add("time", 0.5);
//					iTween.ColorTo(tileText, color);
					tileTextMesh.text = file.GetName().Substring(0, 10) + "...";
				}

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

				if (i == 1) {
					AddLight(new Vector3 (0, 5, z), new Color(0.9f, 1, 1), 9f);
					AddLight(new Vector3 (0, 5, z - 1.5f), new Color(0.9f, 1, 1), 9f);
					AddLight(new Vector3 (0, 5, z - 3f), new Color(0.9f, 1, 1), 14f);
				}
				if (i % 5 == 0) {
					AddLight(new Vector3 (0, 5, z), new Color(0.9f, 1, 1), 9f);
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
			this.Content = this.FileService.GetDirectoryContent(null);
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

        /// <summary>
        /// Displays the popup.
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="E"></param>
        private void DisplayFilePopup(System.Object Source, ElapsedEventArgs E) {
            this.Popup.Show();
            this.Popup.GenerateBaseInfo(this.Content[this.CurrentIndex].GetName(), this.Content[this.CurrentIndex].GetType().ToString());
            this.PopupTimer.Enabled = false;
        }

		/// <summary>
		/// Enters the directory.
		/// </summary>
		/// <param name="Destination">The destination.</param>
		private void Back() {
			this.FileService.ChangeDirectory(Path.Combine(this.FileService.CurrentFolder.GetPath(), ".."));
			this.Content = this.FileService.GetDirectoryContent(null);
			this.DisplayContent();
		}

		/// <summary>
		/// Actions the handle.
		/// </summary>
		public void Update() {
			foreach (IAction Action in this.Input.GetMovements()) {
				switch (Action.Type) {
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
					case ActionType.Back:
						this.Back();
						break;
					default:
						break;
				}
			}
		}
	}
}