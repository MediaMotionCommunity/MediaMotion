using System.Collections;
using System.Collections.Generic;
using System.Timers;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Scripts;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Popup Controller
	/// </summary>
	public class PopupController : BaseUnityScript<PopupController> {
		/// <summary>
		/// The popup timer
		/// </summary>
		private Timer timer;

		/// <summary>
		/// Style for the name of the property.
		/// </summary>
		private GUIStyle propertyLabelStyle;

		/// <summary>
		/// Popup container
		/// </summary>
		private Rect popupRect;

		/// <summary>
		/// Sidebar container
		/// </summary>
		public Rect sidebarRect;

		/// <summary>
		/// The element
		/// </summary>
		private IElement element;

		/// <summary>
		/// Texture for file icon.
		/// </summary>
		private Texture2D fileTexture;

		/// <summary>
		/// Style for the sidebar container.
		/// </summary>
		public GUIStyle sidebarWindowStyle;

		/// <summary>
		/// Visibility of the popup.
		/// </summary>
		private bool sidebarVisibility;

		/// <summary>
		/// X coordinate of the sidebar when hidden.
		/// </summary>
		private int sidebarHiddenX;

		/// <summary>
		/// X coordinate of the sidebar when visible.
		/// </summary>
		private int sidebarVisibleX;

		/// <summary>
		/// Current X coordinate of the sidebar.
		/// </summary>
		private int sidebarX;

		/// <summary>
		/// Indicates wether the sidebar is moving or not.
		/// </summary>
		private bool sidebarStatus;

		/// <summary>
		/// Gets or sets the delay.
		/// </summary>
		/// <value>
		/// The delay.
		/// </value>
		public double Delay { get; set; }

		/// <summary>
		/// Visibility of the popup
		/// </summary>
		/// <value>
		///   <c>true</c> if visibility; otherwise, <c>false</c>.
		/// </value>
		public bool PopupVisibility { get; private set; }

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Init() {
			//sidebar
			this.sidebarVisibility = false;
			this.sidebarVisibleX = 1000;
			this.sidebarHiddenX = 1125;
			this.sidebarX = this.sidebarHiddenX;
			this.sidebarRect = new Rect(this.sidebarHiddenX, 200, 160, 300);
			this.sidebarStatus = false;

			// timer
			this.Delay = 500.0;
			this.timer = new Timer(this.Delay);
			this.timer.Elapsed += this.Show;

			// gui element
			this.propertyLabelStyle = new GUIStyle();
			this.propertyLabelStyle.normal.textColor = Color.white;
			this.popupRect = new Rect(20, 20, 300, 65);
			this.fileTexture = Resources.Load<Texture2D>("Icons/File-icon");
			this.sidebarWindowStyle = Resources.Load<GUISkin>("SidebarStyle").GetStyle("Box");

			// element
			this.element = null;
		}

		/// <summary>
		/// Called when [GUI].
		/// </summary>
		public void OnGUI() {
			if (this.PopupVisibility && this.element != null) {
				this.popupRect = GUI.Window(0, this.popupRect, this.HydratePopupContent, "Information");
			}
			this.MoveSidebar();
			GUI.Window(1, this.sidebarRect, this.PrintSidebar, "", this.sidebarWindowStyle);
			this.PrintBuffer(0);
		}

		/// <summary>
		/// Prints the copy-paste buffer.
		/// </summary>
		/// <param name="WindowID"></param>
		public void PrintBuffer(int WindowID) {
			GUI.color = new Color(1, 1, 1, 0.65f);
			GUI.DrawTexture(new Rect(1010, 10, 125, 125), this.fileTexture, ScaleMode.StretchToFill, true);
			GUI.Label(new Rect(1025, 140, 125, 30), "File.jpg");
		}

		/// <summary>
		/// Prints the sidebar
		/// </summary>
		/// <param name="WindowID"></param>
		public void PrintSidebar(int WindowID) {
			//GUI.Label(new Rect(10, 20, 150, 20), "Prout");
		}

		/// <summary>
		/// Sets the file.
		/// </summary>
		/// <param name="element">The element.</param>
		public void SetFile(IElement element) {
			this.element = element;
			this.timer.Interval = this.Delay;
			this.timer.Start();
		}

		/// <summary>
		/// Unsets the file.
		/// </summary>
		public void UnsetFile() {
			if (this.timer != null) {
				this.element = null;
				this.timer.Enabled = false;
				this.Hide();
			}
		}

		/// <summary>
		/// Shows this instance.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="eventParams">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
		public void Show(object source = null, ElapsedEventArgs eventParams = null) {
			this.PopupVisibility = true;
			this.timer.Enabled = false;
		}

		/// <summary>
		/// Hides this instance.
		/// </summary>
		public void Hide() {
			this.PopupVisibility = false;
		}

		/// <summary>
		/// Handles sidebar movement.
		/// </summary>
		public void MoveSidebar() {
			if (this.sidebarStatus == true) {
				if (this.sidebarVisibility == true) {
					this.sidebarX -= 2;
					if (this.sidebarX <= this.sidebarVisibleX)
						this.sidebarStatus = false;
				}
				else {
					this.sidebarX += 2;
					if (this.sidebarX >= this.sidebarHiddenX)
						this.sidebarStatus = false;
				}
				this.sidebarRect = new Rect(this.sidebarX, 200, 160, 300);
			}
		}

		/// <summary>
		/// Shows the sidebar.
		/// </summary>
		public void ShowSidebar() {
			if (this.sidebarVisibility == false) {
				this.sidebarStatus = true;
				this.sidebarVisibility = true;
			}
		}

		/// <summary>
		/// Hides the sidebar.
		/// </summary>
		public void HideSidebar() {
			if (this.sidebarVisibility == true) {
				this.sidebarStatus = true;
				this.sidebarVisibility = false;
			}
		}

		/// <summary>
		/// Hydrate popup content
		/// </summary>
		/// <param name="WindowID">The window identifier.</param>
		private void HydratePopupContent(int WindowID) {
			string elementType = "undefined";

			GUI.Label(new Rect(10, 20, 100, 20), "Name", this.propertyLabelStyle);
			GUI.Label(new Rect(10, 40, 100, 20), "Type", this.propertyLabelStyle);
			switch (this.element.GetElementType()) {
				case ElementType.Folder:
					elementType = "Folder";
					break;
				case ElementType.File: {
						IFile file = this.element as IFile;

						switch (file.GetFileType()) {
							case FileType.Image:
								elementType = "Image";
								break;
							case FileType.Sound:
								elementType = "Sound";
								break;
							case FileType.Video:
								elementType = "Video";
								break;
							case FileType.PDF:
								elementType = "PDF";
								break;
							case FileType.Text:
								elementType = "Texte";
								break;
							case FileType.Regular:
							default:
								elementType = "Regular";
								break;
						}
					}
					break;
			}
			GUI.Label(new Rect(110, 20, 200, 20), this.element.GetName());
			GUI.Label(new Rect(110, 40, 200, 20), elementType);

			// if (this.InfoMap != null) {
			// int Offset = 60;
			// foreach (KeyValuePair<string, string> Entry in this.InfoMap) {
			// GUI.Label(new Rect(10, Offset, 100, 20), Entry.Key + " - " + Entry.Value);
			// Offset += 20;
			// }
			// }
		}
	}
}