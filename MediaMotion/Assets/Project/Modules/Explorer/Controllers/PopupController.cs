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
		private Rect windowRect;

		/// <summary>
		/// The element
		/// </summary>
		private IElement element;

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
		public bool Visibility { get; private set; }

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Init() {
			// timer
			this.Delay = 500.0;
			this.timer = new Timer(this.Delay);
			this.timer.Elapsed += this.Show;

			// gui element
			this.propertyLabelStyle = new GUIStyle();
			this.propertyLabelStyle.normal.textColor = Color.white;
			this.windowRect = new Rect(20, 20, 300, 65);

			// element
			this.element = null;
		}

		/// <summary>
		/// Called when [GUI].
		/// </summary>
		public void OnGUI() {
			if (this.Visibility && this.element != null) {
				this.windowRect = GUI.Window(0, this.windowRect, this.HydratePopupContent, "Information");
			}
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
			this.element = null;
			this.timer.Enabled = false;
			this.Hide();
		}

		/// <summary>
		/// Shows this instance.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="eventParams">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
		public void Show(object source = null, ElapsedEventArgs eventParams = null) {
			this.Visibility = true;
			this.timer.Enabled = false;
		}

		/// <summary>
		/// Hides this instance.
		/// </summary>
		public void Hide() {
			this.Visibility = false;
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