using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using MediaMotion.Core;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Modules.DefaultViewer;
using MediaMotion.Modules.Explorer.View;
using MediaMotion.Modules.ImageViewer;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Explorer Controller
	/// </summary>
	public class FolderContentController : BaseUnityScript<FolderContentController> {
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
		/// The input
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The file service
		/// </summary>
		private IFileSystemService fileSystemService;

		/// <summary>
		/// The module manager service
		/// </summary>
		private IModuleManagerService moduleManagerService;

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
		/// The camera position
		/// </summary>
		private Vector3 CamPos;

		/// <summary>
		/// The content
		/// </summary>
		private List<IElement> Content;

		/// <summary>
		/// The arial font
		/// </summary>
		private Font ArialFont;

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
		/// The lights
		/// </summary>
		private List<GameObject> Lights;

		/// <summary>
		/// Initializes the specified input service.
		/// </summary>
		/// <param name="input">The input service.</param>
		/// <param name="fileSystem">The file system service.</param>
		/// <param name="moduleManager">The module manager service.</param>
		public void Init(IInputService input, IFileSystemService fileSystem, IModuleManagerService moduleManager) {
			this.inputService = input;
			this.fileSystemService = fileSystem;
			this.moduleManagerService = moduleManager;

			this.CamPos = new Vector3(0, 0, 0);
			this.Tiles = new List<GameObject>();
			this.Filenames = new List<GameObject>();
			this.Lights = new List<GameObject>();

			this.TextureMap = new Dictionary<ElementType, string>();
			this.TextureMap.Add(ElementType.File, "File-icon");
			this.TextureMap.Add(ElementType.Folder, "Folder-icon");

			this.Line = 0;
			this.CurrentIndex = 0;
			this.ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
			this.Camera = GameObject.Find("ReferenceFrame/Camera/Main");
			this.Camera.AddComponent("FileInfoUI");

			this.Popup = this.Camera.GetComponent<FileInfoUI>();
			this.PopupTime = 2000;
			this.PopupTimer = new Timer(this.PopupTime);
			this.PopupTimer.Elapsed += this.DisplayFilePopup;

			iTween.Init(this.Camera);

			this.EnterDirectory();
		}

		/// <summary>
		/// Actions the handle.
		/// </summary>
		public void Update() {
			foreach (IAction Action in this.inputService.GetMovements()) {
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

		/// <summary>
		/// Highlights the current.
		/// </summary>
		/// <param name="Current">
		/// The object.
		/// </param>
		private void HighlightCurrent(GameObject Current) {
			//// Current.transform.position = new Vector3(Current.transform.position.x, 2, Current.transform.position.z);
			iTween.MoveTo(Current, new Vector3(Current.transform.position.x, 2, Current.transform.position.z), 0.5f);
			this.PopupTimer.Interval = this.PopupTime;
			this.PopupTimer.Start();
		}

		/// <summary>
		/// Cancels the highlight.
		/// </summary>
		/// <param name="Current">
		/// The object.
		/// </param>
		private void CancelHighlight(GameObject Current) {
			//// Current.transform.position = new Vector3(Current.transform.position.x, 1, Current.transform.position.z);
			iTween.MoveTo(Current, new Vector3(Current.transform.position.x, 1, Current.transform.position.z), 0.5f);
		}

		/// <summary>
		/// Changes the selection.
		/// </summary>
		/// <param name="Offset">
		/// The offset.
		/// </param>
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
					Hashtable camHash = new Hashtable();

					if (this.CamPos == Vector3.zero) {
						this.CamPos = this.Camera.transform.position + vect;
					}
					else {
						this.CamPos += vect;
					}
					camHash.Add("z", this.CamPos.z);
					camHash.Add("space", Space.World);
					camHash.Add("islocal", true);
					camHash.Add("easetype", iTween.EaseType.easeInOutSine);
					camHash.Add("time", time);
					iTween.MoveTo(this.Camera, camHash);

					//// this.Camera.transform.Translate(0, 0, -3, Space.World);
				}
				this.ChangeSelection(-this.RowSize);
				this.Camera.GetComponent<FileInfoUI>().Hide();
				Debug.Log(this.CamPos);
			}
		}

		/// <summary>
		/// Moves down.
		/// </summary>
		private void MoveDown() {
			if (this.CurrentIndex + this.RowSize < this.Tiles.Count) {
				if (--this.Line % 2 == 0) {
					//// this.Camera.transform.Translate(0, 0, 3, Space.World);
					float time = 0.5f;
					Vector3 vect = new Vector3(0, 0, 3);
					Hashtable camHash = new Hashtable();

					if (this.CamPos == Vector3.zero) {
						this.CamPos = this.Camera.transform.position + vect;
					} else {
						this.CamPos += vect;
					}
					camHash.Add("z", this.CamPos.z);
					camHash.Add("space", Space.World);
					camHash.Add("islocal", true);
					camHash.Add("easetype", iTween.EaseType.easeInOutSine);
					camHash.Add("time", time);
					iTween.MoveTo(this.Camera, camHash);
				}
				this.ChangeSelection(this.RowSize);
				this.Camera.GetComponent<FileInfoUI>().Hide();
				Debug.Log(this.CamPos);
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
		/// Adds the light.
		/// </summary>
		/// <param name="position">
		/// The position.
		/// </param>
		/// <param name="range">
		/// The range.
		/// </param>
		/// <param name="intensity">
		/// The intensity.
		/// </param>
		private void AddLight(Vector3 position, float range = 9f, float intensity = 1) {
			GameObject lightC = new GameObject();

			lightC.AddComponent<Light>();
			lightC.light.name = "light_" + position.x + "_" + position.y + "_" + position.z;
			lightC.light.transform.position = position;
			lightC.light.color = new Color(0.9f, 1, 1);
			lightC.light.range = range;
			lightC.light.intensity = intensity;
			lightC.light.renderMode = LightRenderMode.ForceVertex;
			this.Lights.Add(lightC);
		}

		/// <summary>
		/// Displays the content.
		/// </summary>
		private void DisplayContent() {
			int i = 0;
			float x = this.OriginX;
			float z = this.OriginZ;

			this.Clear();

			if (this.Content.Count == 0) {
				this.AddLight(new Vector3(-1, 5, z), 14f, 1.2f);
				this.AddLight(new Vector3(1, 5, z), 14f, 1.2f);
			}

			foreach (IElement file in this.Content) {
				GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
				GameObject tileText = new GameObject();

				tileText.transform.parent = tile.transform;
				tile.transform.position = new Vector3(x, 1, z);
				tile.transform.eulerAngles = new Vector3(60, 180, 0);
				tile.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
				tile.renderer.material.mainTexture = Resources.Load<Texture2D>(this.TextureMap[file.GetElementType()]);

				//// tile.renderer.material.mainTextureOffset = new Vector2(0, -0.001f);
				tile.renderer.material.shader = Shader.Find("Transparent/Diffuse");
				tile.renderer.material.color = new Color(0.3f, 0.6f, 0.9f, 1);
				tile.name = "tile_" + file.GetName();

				//// tile.AddComponent("FolderHover");
				//// tile.AddComponent(COMPONENT_POUR_INFOS_FICHIER);
				tileText.transform.position = new Vector3(x - 0.4f, 0.45f, z - 0.2f);
				TextMesh tileTextMesh = tileText.AddComponent(typeof(TextMesh)) as TextMesh;
				tileTextMesh.transform.parent = tileText.transform;
				tileTextMesh.font = this.ArialFont;
				tileTextMesh.fontSize = 16;
				tileTextMesh.renderer.material = tileTextMesh.font.material;
				tileTextMesh.text = file.GetName();
				tileText.transform.eulerAngles = new Vector3(30, 0, 0);
				tileText.transform.localScale = new Vector3(0.9F, 0.9F, 0.9F);

				//// tileText.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
				this.Filenames.Add(tileText);

				Hashtable color = new Hashtable();
				color.Add("r", 0.8);
				color.Add("g", 0.9);
				color.Add("b", 1);
				color.Add("time", 0.1);
				iTween.ColorTo(tileText, color);

				color = new Hashtable();
				if (tile.renderer.bounds.size.x < tileText.renderer.bounds.size.x) {
					//// color.Add("r", 1);
					//// color.Add("g", 0.5);
					//// color.Add("b", 0.5);
					//// color.Add("time", 0.5);
					//// iTween.ColorTo(tileText, color);
					tileTextMesh.text = file.GetName().Substring(0, 10) + "...";
				}

				this.Tiles.Add(tile);
				++i;
				if (i % this.RowSize == 0) {
					x = this.OriginX;
				}
				else {
					x += this.IncrementX;
				}
				if (i % this.RowSize == 0) {
					z += this.IncrementZ;
				}
				if (i == 1) {
					for (int idx = 0; idx < 5; idx++) {
						this.AddLight(new Vector3(-3, 5, z - (idx * 1.5f)));
						this.AddLight(new Vector3(3, 5, z - (idx * 1.5f)));
					}
				}
				if (i % 5 == 0) {
					this.AddLight(new Vector3(-3, 5, z));
					this.AddLight(new Vector3(3, 5, z));
				}

				this.HighlightCurrent(this.Tiles[this.CurrentIndex]);
			}
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		private void Clear() {
			GameObject.Find("ReferenceFrame").GetComponent<ReferenceFrameController>().Reset = true;
			foreach (GameObject light in this.Lights) {
				UnityEngine.Object.Destroy(light);
			}
			this.Camera.transform.localPosition = new Vector3(0, 5, -15);
			this.CamPos = this.Camera.transform.localPosition;
			foreach (GameObject tile in this.Tiles) {
				UnityEngine.Object.Destroy(tile);
			}
			this.Tiles.Clear();
			this.CurrentIndex = 0;
			this.Line = 0;
		}

		/// <summary>
		/// Enters the directory.
		/// </summary>
		/// <param name="destination">The destination.</param>
		private void EnterDirectory(IFolder destination = null) {
			this.fileSystemService.ChangeDirectory((destination == null) ? (null) : (destination.GetPath()));
			this.Content = this.fileSystemService.GetContent(null);
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
					case ElementType.File:
						IFile file = this.Content[this.CurrentIndex] as IFile;

						switch (file.GetFileType()) {
							case FileType.Image:
								this.moduleManagerService.LoadModule<ImageViewerModule>(new IElement[] { file });
								break;
							default:
								this.moduleManagerService.LoadModule<DefaultViewerModule>(new IElement[] { this.Content[this.CurrentIndex] });
								break;
						}
						break;
				}
			}
		}

		/// <summary>
		/// Displays the file popup.
		/// </summary>
		/// <param name="Source">
		/// The source.
		/// </param>
		/// <param name="E">
		/// The <see cref="ElapsedEventArgs"/> instance containing the event data.
		/// </param>
		private void DisplayFilePopup(object Source, ElapsedEventArgs E) {
			this.Popup.Show();
			this.Popup.GenerateBaseInfo(this.Content[this.CurrentIndex].GetName(), this.Content[this.CurrentIndex].GetType().ToString());
			this.PopupTimer.Enabled = false;
		}

		/// <summary>
		/// Backs this instance.
		/// </summary>
		private void Back() {
			this.fileSystemService.ChangeDirectory(this.fileSystemService.CurrentFolder.GetParent() ?? this.fileSystemService.CurrentFolder.GetPath());
			this.Content = this.fileSystemService.GetContent(null);
			this.DisplayContent();
		}
	}
}