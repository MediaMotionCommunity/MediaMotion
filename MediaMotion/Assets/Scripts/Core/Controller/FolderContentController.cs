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

		private int rowSize = 5;

		/* Coordinates of the first placed tile */
		private float originX = -3f;
		private float originZ = -12.5f;

		/* Value to increment to get the next IElement or next row */
		private float incrementX = 1.5f;
		private float incrementZ = 1.5f;

		private List<GameObject> tiles;
		private List<GameObject> filenames;
		private int CurrentIndex;

		private FileSystem FileService;
		Dictionary<ElementType, string> TextureMap;

		private int Line;
		private GameObject Camera;
		private List<IElement> Content;
        Font ArialFont;

		public FolderContentController() {
			this.FileService = new FileSystem();
			this.TextureMap = new Dictionary<ElementType, string>();
			this.TextureMap.Add(ElementType.File, "File-icon");
			this.TextureMap.Add(ElementType.Folder, "Folder-icon");
			this.tiles = new List<GameObject>();
			this.filenames = new List<GameObject>();
			this.CurrentIndex = 0;
			this.Line = 0;
            this.ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
			this.Camera = GameObject.Find("Main Camera");
		}

		public void Load() {
			this.EnterDirectory();
		}

		public void CancelHighlight() {
			this.tiles[CurrentIndex].transform.position = new Vector3(this.tiles[CurrentIndex].transform.position.x, 1, this.tiles[CurrentIndex].transform.position.z);
		}

		public void HighlightCurrent() {
			this.tiles[CurrentIndex].transform.position = new Vector3(this.tiles[CurrentIndex].transform.position.x, 2, this.tiles[CurrentIndex].transform.position.z);
		}

		private void moveUp() {
			if (this.CurrentIndex - this.rowSize >= 0) {
				this.CancelHighlight();
				if (this.Line++ % 2 == 0)
					this.Camera.transform.Translate(0, 0, -3, Space.World);
				this.CurrentIndex -= this.rowSize;
				this.HighlightCurrent();
			}
		}

		private void moveDown() {
			if (this.CurrentIndex + this.rowSize < this.tiles.Count) {
				this.CancelHighlight();
				if (--this.Line % 2 == 0)
					this.Camera.transform.Translate(0, 0, 3, Space.World);
				this.CurrentIndex += this.rowSize;
				this.HighlightCurrent();
			}
		}

		private void moveLeft() {
			if (this.CurrentIndex - 1 >= 0) {
				this.CancelHighlight();
				--this.CurrentIndex;
				this.HighlightCurrent();
			}
		}

		private void moveRight() {
			if (this.CurrentIndex + 1 < this.tiles.Count) {
				this.CancelHighlight();
				++this.CurrentIndex;
				this.HighlightCurrent();
			}
		}

		private void DisplayContent() {
			int i = 0;
			float x = originX;
			float z = originZ;

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

                GameObject tileText = new GameObject();
                tileText.transform.position = new Vector3(x - 1, 0.6f, z - 0.5f);
                TextMesh tileTextMesh = tileText.AddComponent(typeof(TextMesh)) as TextMesh;
                tileTextMesh.transform.parent = tileText.transform;
                tileTextMesh.font = this.ArialFont;
                tileTextMesh.fontSize = 16;
                tileTextMesh.renderer.material = tileTextMesh.font.material;
                tileTextMesh.text = file.GetName();
                tileText.transform.eulerAngles = new Vector3(60, 0, 0);
                tileText.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
                filenames.Add(tileText);

				this.tiles.Add(tile);
				++i;
				if (i % this.rowSize == 0) {
					x = this.originX;
				} else {
					x += this.incrementX;
				}
				if (i % this.rowSize == 0) {
					z += this.incrementZ;
				}
				this.HighlightCurrent();
			};
		}

		private void Clear() {
			this.Camera.transform.position = new Vector3(0, 5, -15);
			foreach (GameObject tile in tiles) {
				GameObject.Destroy(tile);
			}
			this.tiles.Clear();
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