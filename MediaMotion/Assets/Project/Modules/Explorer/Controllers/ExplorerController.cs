﻿using System.Linq;
using MediaMotion.Core;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Explorer controller
	/// </summary>
	public class ExplorerController : AScript<ExplorerModule, ExplorerController> {
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
		public const float LineSpacing = 2.3f;

		/// <summary>
		/// The base element
		/// </summary>
		public GameObject BaseElement;

		/// <summary>
		/// Visibility of popup
		/// </summary>
		public bool PopupVisibility;

		/// <summary>
		/// The element factory
		/// </summary>
		private IElementFactory elementFactory;

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
		/// The popup controller
		/// </summary>
		private PopupController popupController;

		/// <summary>
		/// The selected element
		/// </summary>
		private GameObject selectedElement;

		/// <summary>
		/// The wheel tool
		/// </summary>
		private MenuBehavior wheelTool;

		/// <summary>
		/// The wheel tool
		/// </summary>
		private MenuBehavior wheelLaunch;

		/// <summary>
		/// Popup container
		/// </summary>
		private Rect popupRect;

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public int Count { get; private set; }

		/// <summary>
		/// Initializes the specified explorer module.
		/// </summary>
		/// <param name="elementFactory">The element factory.</param>
		/// <param name="fileSystemService">The file system service.</param>
		/// <param name="inputService">The input service.</param>
		/// <param name="moduleManagerService">The module manager service.</param>
		public void Init(IElementFactory elementFactory, IFileSystemService fileSystemService, IInputService inputService, IModuleManagerService moduleManagerService) {
			// services
			this.elementFactory = elementFactory;
			this.fileSystemService = fileSystemService;
			this.inputService = inputService;
			this.moduleManagerService = moduleManagerService;

			// popup
			this.PopupVisibility = false;
			this.popupRect = new Rect(20, 20, 320, 65);

			// game object
			this.referenceFrameController = GameObject.Find("ReferenceFrame").GetComponent<ReferenceFrameController>();
			this.popupController = GameObject.Find("ReferenceFrame/Cameras/Main").GetComponent<PopupController>();
			this.wheelTool = GameObject.Find("ReferenceFrame/Cameras/Main/Menu").GetComponent<MenuBehavior>();
			this.wheelLaunch = GameObject.Find("ReferenceFrame/Cameras/Main/MenuLaunch").GetComponent<MenuBehavior>();

			// Open directory
			if (this.module.Parameters == null || this.module.Parameters.Count(parameter => parameter is IFolder) == 0) {
				this.OpenDirectory(this.fileSystemService.GetHomeFolder());
			} else {
				this.OpenDirectory(this.module.Parameters.FirstOrDefault(parameter => parameter is IFolder) as IFolder);
			}
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
					case ActionType.GrabStart:
						if (this.selectedElement != null) {
							this.wheelTool.ActiveWheelTool(this.selectedElement.gameObject.GetComponent<ElementController>().Element);
						}
						break;
				case ActionType.GrabStop:
					this.wheelTool.DeactiveWheelTool();
					break;
				}
			}
		}

		/// <summary>
		/// Called when [GUI].
		/// </summary>
		public void OnGUI() {
			if (this.PopupVisibility == true) {
				this.popupRect = GUI.Window(0, this.popupRect, this.HydratePopupContent, "Warning");
			}
		}

		/// <summary>
		/// Selects the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		public void Select(GameObject element) {
			ElementController elementController;

			this.Deselect(this.selectedElement);
			this.selectedElement = element;
			elementController = this.selectedElement.GetComponent<ElementController>();
			elementController.Select();
			this.popupController.SetFile(elementController.Element);
		}

		/// <summary>
		/// Deselects the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		public void Deselect(GameObject element) {
			if (this.selectedElement != null && this.selectedElement.Equals(element)) {
				this.selectedElement.GetComponent<ElementController>().Deselect();
				this.popupController.UnsetFile();
				this.selectedElement = null;
			}
		}

		/// <summary>
		/// Hydrates the content of the popup.
		/// </summary>
		/// <param name="WindowID">The window identifier.</param>
		private void HydratePopupContent(int WindowID) {
			GUI.Label(new Rect(10, 20, 320, 20), "Do you want to open the file outside MediaMotion ?");
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		private void Clear() {
			this.selectedElement = null;
			this.popupController.UnsetFile();
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

			if (this.fileSystemService.IsAccessible(parentPath)) {
				this.Open(this.elementFactory.CreateFolder(parentPath));
			}
		}

		/// <summary>
		/// Opens the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		private void Open(IElement element) {
			if (!this.moduleManagerService.Load(new IElement[] { element })) {
				this.wheelLaunch.ActiveWheelTool(element);
				this.PopupVisibility = true;
			}
		}

		/// <summary>
		/// Opens the directory.
		/// </summary>
		/// <param name="folder">The folder.</param>
		private void OpenDirectory(IFolder folder) {
			int count = 0;

			this.Clear();
			this.fileSystemService.ChangeDirectory(folder.GetPath());
			foreach (IElement element in this.fileSystemService.GetFolderElements()) {
				GameObject uiElement;

				uiElement = Instantiate(this.BaseElement) as GameObject;
				uiElement.name = "Element_" + element.GetName();
				uiElement.transform.parent = this.transform;

				uiElement.transform.localPosition = new Vector3(((count % FilePerLine) - (FilePerLine / 2)) * FileSpacing, 0.0f, (count / FilePerLine) * LineSpacing);
				uiElement.AddComponent<ElementController>().Element = element;
				++count;
			}
			this.Count = count;
		}
	}
}
