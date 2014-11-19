using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Models;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Module.Interfaces;

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
		private int CurrentIndex;

		private FileSystem FileService;
		Dictionary<ElementType, string> TextureMap;

		public FolderContentController() {
			this.FileService = new FileSystem();
			this.FileService.ChangeDirectory();
			this.TextureMap = new Dictionary<ElementType, string>();
			this.TextureMap.Add(ElementType.Folder, "File-icon");
			this.TextureMap.Add(ElementType.File, "Folder-icon");
			this.tiles = new List<GameObject>();
			this.CurrentIndex = 0;
		}

        public void Load() {
            this.displayContent();
        }

        public void CancelHighlight() {
            
        }

        public void HighlightCurrent() {

        }

		private void moveUp() {
            if (this.CurrentIndex - this.rowSize >= 0) {
                this.CancelHighlight();
                this.CurrentIndex -= this.rowSize;
                this.enterDirectory();
                this.HighlightCurrent();
            }
		}

        private void moveDown() {
            if (this.CurrentIndex + this.rowSize < this.tiles.Count) {
                this.CancelHighlight();
                this.CurrentIndex += this.rowSize;
                this.enterDirectory();
                this.HighlightCurrent();
            }
        }

        private void moveLeft() {
            if (this.CurrentIndex - this.rowSize >= 0) {
                this.CancelHighlight();
                --this.CurrentIndex;
                this.enterDirectory();
                this.HighlightCurrent();
            }
        }

        private void moveRight() {
            if (this.CurrentIndex - this.rowSize < this.tiles.Count) {
                this.CancelHighlight();
                ++this.CurrentIndex;
                this.enterDirectory();
                this.HighlightCurrent();
            }
        }

		private void displayContent() {
			int i = 0;
			float x = originX;
			float z = originZ;
			List<IElement> content = this.FileService.GetDirectoryContent();
			foreach (IElement file in content) {
				GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
				tile.transform.position = new Vector3(x, 1, z);
                tile.transform.eulerAngles = new Vector3(60, 180, 0);
                tile.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
				//attribuer texture en fonction du fichier ?
                Debug.Log(this.TextureMap[file.GetElementType()]);
				tile.renderer.material.mainTexture = Resources.Load<Texture2D>(this.TextureMap[file.GetElementType()]);
				tile.renderer.material.shader = Shader.Find("Transparent/Diffuse");
				tile.renderer.material.color = new Color(0.3f, 0.6f, 0.9f, 1);
//				tile.AddComponent("FolderHover");
				//tile.AddComponent(COMPONENT_POUR_INFOS_FICHIER);
				tile.name = "Plane";
				this.tiles.Add(tile);
				++i;
                if (i % this.rowSize == 0)
                    x = this.originX;
                else
                    x += this.incrementX;
				if (i % this.rowSize == 0)
					z += this.incrementZ;
			};
		}


		private void enterDirectory() {
			this.FileService.ChangeDirectory(new Folder(this.tiles[this.CurrentIndex].name, ""));
			//VERIFIER que la mémoire soit bien free, je sais pas si Unity a encore accès aux tiles après ou pas.
			//Ajouter des animations quand on delete les folders.
			this.tiles.Clear();
			this.displayContent();
		}

        public void Register() {
        }

        public void Unregister() {
        }

        public void Unload() {
        }
    }
}