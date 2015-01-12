using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Models;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Core.Controllers {
	public class FolderContentController : IModule {

		private int RowSize = 5;

		/* Coordinates of the first placed tile */
		private float OriginX = -3f;
		private float OriginZ = -12.5f;

		/* Value to increment to get the next IElement or next row */
		private float IncrementX = 1.5f;
		private float IncrementZ = 1.5f;

		private List<GameObject> Tiles;
		private List<GameObject> Filenames;
		private int CurrentIndex;

		private FileSystemService FileService;
		Dictionary<ElementType, string> TextureMap;

		private int Line;
		private GameObject Camera;
		private List<IElement> Content;

		public FolderContentController() {
			this.FileService = FileSystemService.GetInstance();
			this.TextureMap = new Dictionary<ElementType, string>();
			this.TextureMap.Add(ElementType.File, "File-icon");
			this.TextureMap.Add(ElementType.Folder, "Folder-icon");
			this.Tiles = new List<GameObject>();
			this.Filenames = new List<GameObject>();
			this.CurrentIndex = 0;
			this.Line = 0;
			this.Camera = GameObject.Find("Main Camera");
		}

		public void Load() {
			this.EnterDirectory();
		}

		private void HighlightCurrent(GameObject Object) {
			Object.transform.position = new Vector3(Object.transform.position.x, 2, Object.transform.position.z);
		}

		private void CancelHighlight(GameObject Object) {
			Object.transform.position = new Vector3(Object.transform.position.x, 1, Object.transform.position.z);
		}

		private void ChangeSelection(int offset) {
			this.CancelHighlight(this.Tiles[this.CurrentIndex]);
			this.HighlightCurrent(this.Tiles[this.CurrentIndex + offset]);
			this.CurrentIndex += offset;
		}

		private void moveUp() {
			if (this.CurrentIndex - this.RowSize >= 0) {
				if (this.Line++ % 2 == 0)
					this.Camera.transform.Translate(0, 0, -3, Space.World);
				this.ChangeSelection(-this.RowSize);
			}
		}

		private void moveDown() {
			if (this.CurrentIndex + this.RowSize < this.Tiles.Count) {
				if (--this.Line % 2 == 0)
					this.Camera.transform.Translate(0, 0, 3, Space.World);
				this.ChangeSelection(this.RowSize);
			}
		}

		private void moveLeft() {
			int LineOffset = 0;

			if ((this.CurrentIndex % this.RowSize) - 1 < 0) {
				if (this.CurrentIndex + this.RowSize - 1 >= this.Tiles.Count) {
					LineOffset += this.Tiles.Count % this.RowSize - 1;
				} else {
					LineOffset += this.RowSize - 1;
				}
			} else {
				LineOffset -= 1;
			}
			this.ChangeSelection(LineOffset);
		}

		private void moveRight() {
			int LineOffset = 0;

			if ((this.CurrentIndex + 1) % this.RowSize == 0 || this.CurrentIndex + 1 >= this.Tiles.Count) {
				LineOffset -= this.CurrentIndex % this.RowSize;
			} else {
				LineOffset += 1;
			}
			this.ChangeSelection(LineOffset);
		}

		private void DisplayContent() {
			int i = 0;
			float x = OriginX;
			float z = OriginZ;

			this.Clear();
			foreach (IElement file in this.Content) {
				GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Plane);

				tile.transform.position = new Vector3(x, 1, z);
				tile.transform.eulerAngles = new Vector3(60, 180, 0);
				tile.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
				tile.renderer.material.mainTexture = Resources.Load<Texture2D>(this.TextureMap[file.GetElementType()]);
				tile.renderer.material.shader = Shader.Find("Transparent/Diffuse");
				tile.renderer.material.color = new Color(0.3f, 0.6f, 0.9f, 1);
				tile.name = "tile_" + file.GetName();

				//				tile.AddComponent("FolderHover");
				//tile.AddComponent(COMPONENT_POUR_INFOS_FICHIER);

				//tile.AddComponent(typeof(TextMesh));
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
			};
		}

		private void Clear() {
			this.Camera.transform.position = new Vector3(0, 5, -15);
			foreach (GameObject tile in Tiles) {
				GameObject.Destroy(tile);
			}
			this.Tiles.Clear();
			this.CurrentIndex = 0;
		}

		private void EnterDirectory(IFolder Destination = null) {
			this.FileService.ChangeDirectory(Destination);
			this.Content = this.FileService.GetDirectoryContent();
			this.DisplayContent();
		}

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

		public void Register() {
		}

		public void Unregister() {
		}

		public void Unload() {
		}

		public void ActionHandle(object Sender, ActionDetectedEventArgs Action) {
			Debug.Log(Action.Action.Type);
			switch (Action.Action.Type) {
				case ActionType.Left:
					this.moveLeft();
					break;
				case ActionType.Right:
					this.moveRight();
					break;
				case ActionType.ScrollIn:
					this.moveDown();
					break;
				case ActionType.ScrollOut:
					this.moveUp();
					break;
				case ActionType.Select:
					this.Open();
					break;
				default:
					break;
			}
		}
	}
}