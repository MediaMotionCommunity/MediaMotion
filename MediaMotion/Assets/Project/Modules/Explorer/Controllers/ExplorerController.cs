using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Modules.DefaultViewer;
using MediaMotion.Modules.ImageViewer;
using MediaMotion.Motion.Actions;
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
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The module manager service
		/// </summary>
		private IModuleManagerService moduleManagerService;

		/// <summary>
		/// The reference frame controller
		/// </summary>
		private ReferenceFrameController referenceFrameController;

		/// <summary>
		/// The selected element
		/// </summary>
		private GameObject selectedElement;

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="folderFactory">The folder factory.</param>
		/// <param name="fileSystemService">The file system service.</param>
		/// <param name="inputService">The input service.</param>
		/// <param name="moduleManagerService">The module manager service.</param>
		public void Init(FolderFactory folderFactory, IFileSystemService fileSystemService, IInputService inputService, IModuleManagerService moduleManagerService) {
			// services
			this.folderFactory = folderFactory;
			this.fileSystemService = fileSystemService;
			this.inputService = inputService;
			this.moduleManagerService = moduleManagerService;

			// game object
			this.referenceFrameController = GameObject.Find("ReferenceFrame").GetComponent<ReferenceFrameController>();

			// Open directory
			this.Open(this.folderFactory.Create(this.fileSystemService.GetHome()));
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			foreach (IAction action in this.inputService.GetMovements()) {
				switch (action.Type) {
					case ActionType.Select:
						if (this.selectedElement != null) {
							this.Open(this.selectedElement.gameObject.GetComponent<ElementController>().Element);
						}
						break;
					case ActionType.Back:
						this.Back();
						break;
				}
			}
		}

		/// <summary>
		/// Selects the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		public void Select(GameObject element) {
			this.Deselect(this.selectedElement);
			this.selectedElement = element;
			this.selectedElement.GetComponent<ElementController>().Select();
		}

		/// <summary>
		/// Deselects the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		public void Deselect(GameObject element) {
			if (this.selectedElement != null && this.selectedElement.Equals(element)) {
				this.selectedElement.GetComponent<ElementController>().Deselect();
				this.selectedElement = null;
			}
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		private void Clear() {
			this.selectedElement = null;
			this.referenceFrameController.ResetPosition();
			for (int current = this.transform.childCount - 1; current >= 0; --current) {
				GameObject.Destroy(transform.GetChild(current).gameObject);
			}
		}

		/// <summary>
		/// Backs this instance.
		/// </summary>
		private void Back() {
			string parentPath = this.fileSystemService.CurrentFolder.GetParent();

			if (parentPath != null) {
				this.Open(this.folderFactory.Create(parentPath));
			}
		}

		/// <summary>
		/// Opens the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		private void Open(IElement element) {
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

				uiElement.transform.localPosition = new Vector3(((count % FilePerLine) - (FilePerLine / 2)) * FileSpacing, 0.0f, (count / FilePerLine) * LineSpacing);
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
