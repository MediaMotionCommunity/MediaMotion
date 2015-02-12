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
		/// The base element
		/// </summary>
		public GameObject BaseElement;

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
		/// Initializes this instance.
		/// </summary>
		public void Init(FolderFactory folderFactory, IFileSystemService fileSystemService, IModuleManagerService moduleManagerService) {
			// services
			this.folderFactory = folderFactory;
			this.fileSystemService = fileSystemService;
			this.moduleManagerService = moduleManagerService;

			// game object
			this.referenceFrameController = GameObject.Find("ReferenceFrame").GetComponent<ReferenceFrameController>();

			// Open directory
			this.Open(this.folderFactory.Create(this.fileSystemService.GetHome()));
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public void Clear() {
			this.referenceFrameController.ResetPosition();
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
				GameObject uiElement;

				uiElement = Instantiate(this.BaseElement) as GameObject;
				uiElement.name = "Element_" + element.GetName();
				uiElement.transform.parent = this.transform;

				uiElement.transform.position = new Vector3(((count % FilePerLine) - (FilePerLine / 2)) * FileSpacing, 2.0f, (count / FilePerLine) * LineSpacing);
				uiElement.AddComponent<ElementController>().SetElement(element);
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
	}
}
