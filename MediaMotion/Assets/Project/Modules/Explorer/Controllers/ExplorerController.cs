using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Modules.DefaultViewer;
using MediaMotion.Modules.ImageViewer;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Explorer controller
	/// </summary>
	public class ExplorerController : BaseUnityScript<ExplorerController> {
		/// <summary>
		/// The line length
		/// </summary>
		public const int FilePerLine = 5;

		/// <summary>
		/// The file spacing
		/// </summary>
		public const float FileSpacing = 1.5f;

		/// <summary>
		/// The line spacing
		/// </summary>
		public const float LineSpacing = 1.5f;

		/// <summary>
		/// The folder factory
		/// </summary>
		private FolderFactory folderFactory;

		/// <summary>
		/// The file system service
		/// </summary>
		private IFileSystemService fileSystemService;

		/// <summary>
		/// The module manager service
		/// </summary>
		private IModuleManagerService moduleManagerService;

		/// <summary>
		/// The reference frame controller
		/// </summary>
		private ReferenceFrameController referenceFrameController;

		/// <summary>
		/// The texture name
		/// </summary>
		private Dictionary<KeyValuePair<ElementType, FileType?>, string> textureName;

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Init(FolderFactory folderFactory, IFileSystemService fileSystemService, IModuleManagerService moduleManagerService) {
			// services
			this.folderFactory = folderFactory;
			this.fileSystemService = fileSystemService;
			this.moduleManagerService = moduleManagerService;

			// game object
			this.referenceFrameController = GameObject.Find("ReferenceFrame").GetComponent<ReferenceFrameController>();

			// texture
			this.textureName = new Dictionary<KeyValuePair<ElementType, FileType?>, string>();
			this.textureName.Add(new KeyValuePair<ElementType, FileType?>(ElementType.Folder, null), "Folder-icon");
			this.textureName.Add(new KeyValuePair<ElementType, FileType?>(ElementType.File, FileType.Regular), "File-icon");
			this.textureName.Add(new KeyValuePair<ElementType, FileType?>(ElementType.File, FileType.Image), "Image-icon");
			this.textureName.Add(new KeyValuePair<ElementType, FileType?>(ElementType.File, FileType.Video), "Video-icon");
			this.textureName.Add(new KeyValuePair<ElementType, FileType?>(ElementType.File, FileType.Sound), "Music-icon");
			this.textureName.Add(new KeyValuePair<ElementType, FileType?>(ElementType.File, FileType.Text), "File-icon");
			this.textureName.Add(new KeyValuePair<ElementType, FileType?>(ElementType.File, FileType.PDF), "PDF-icon");

			// Open directory
			this.Open(this.folderFactory.Create(this.fileSystemService.GetHome()));
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public void Clear() {
			this.referenceFrameController.Reset = true;
			for (int current = this.transform.childCount - 1; current >= 0; --current) {
				GameObject.Destroy(transform.GetChild(current).gameObject);
			}
		}

		/// <summary>
		/// Opens the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		public void Open(IElement element) {
			this.Clear();
			switch (element.GetElementType()) {
				case ElementType.Folder:
					this.OpenDirectory(element as IFolder);
					break;
				case ElementType.File:
					this.OpenFile(element as IFile);
					break;
			}
		}

		/// <summary>
		/// Backs this instance.
		/// </summary>
		public void Back() {
			string parentPath = this.fileSystemService.CurrentFolder.GetParent();

			if (parentPath != null) {
				this.Open(this.folderFactory.Create(parentPath));
			}
		}

		/// <summary>
		/// Opens the directory.
		/// </summary>
		/// <param name="folder">The folder.</param>
		private void OpenDirectory(IFolder folder) {
			int count = 0;

			this.fileSystemService.ChangeDirectory(folder.GetPath());
			foreach (IElement element in this.fileSystemService.GetContent(null)) {
				ElementController uiElementScript;
				GameObject uiElement;

				// creation
				uiElement = GameObject.CreatePrimitive(PrimitiveType.Plane);
				uiElement.name = "element_" + element.GetName();
				uiElement.transform.parent = this.transform;

				// position
				uiElement.transform.position = new Vector3(((count % FilePerLine) - (FilePerLine / 2)) * FileSpacing, 1.0f, (count / FilePerLine) * LineSpacing);
				uiElement.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				uiElement.transform.eulerAngles = new Vector3(60, 180, 0);

				// renderer
				uiElement.renderer.material.mainTexture = this.FindTexture(element);
				uiElement.renderer.material.shader = Shader.Find("Transparent/Diffuse");
				uiElement.renderer.material.color = new Color(0.3f, 0.6f, 0.9f, 1);

				// script
				uiElementScript = uiElement.AddComponent<ElementController>();
				uiElementScript.SetElement(element);

				// counter
				++count;
			}
		}

		/// <summary>
		/// Opens the file.
		/// </summary>
		/// <param name="file">The file.</param>
		private void OpenFile(IFile file) {
			switch (file.GetFileType()) {
				case FileType.Image:
					this.moduleManagerService.LoadModule<ImageViewerModule>(new IElement[] { file });
					break;
				default:
					this.moduleManagerService.LoadModule<DefaultViewerModule>(new IElement[] { file });
					break;
			}
		}

		/// <summary>
		/// Finds the texture.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>The texture</returns>
		private Texture2D FindTexture(IElement element) {
			string textureName = null;

			switch (element.GetElementType()) {
				case ElementType.Folder:
					textureName = this.textureName[new KeyValuePair<ElementType, FileType?>(ElementType.Folder, null)];
					break;
				case ElementType.File:
					textureName = this.textureName[new KeyValuePair<ElementType, FileType?>(ElementType.File, (element as IFile).GetFileType())];
					break;
			}
			return (Resources.Load<Texture2D>(textureName));
		}
	}
}
