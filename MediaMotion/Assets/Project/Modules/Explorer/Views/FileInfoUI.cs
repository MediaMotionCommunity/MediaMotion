using System.Collections;
using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.View {
	/// <summary>
	/// File Info UI
	/// </summary>
	public class FileInfoUI : MonoBehaviour {
		/// <summary>
		/// Popup container
		/// </summary>
		public Rect WindowRect = new Rect(20, 20, 300, 65);

		/// <summary>
		/// Filename
		/// </summary>
		public string Filename = "File";

		/// <summary>
		/// File type
		/// </summary>
		public string Filetype = "File";

		/// <summary>
		/// File/Folder Size
		/// </summary>
		public string Size = "0 MB";

		/// <summary>
		/// Visibility of the popup
		/// </summary>
		private bool Visibility = false;

		/// <summary>
		/// Map of additional properties & values to display.
		/// </summary>
		private Dictionary<string, string> InfoMap = null;

		#region Styles

		/// <summary>
		/// Style for the name of the property.
		/// </summary>
		public GUIStyle PropertyLabelStyle;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="FileInfoUI"/> class.
		/// </summary>
		public FileInfoUI() {
			this.PropertyLabelStyle = new GUIStyle();
			this.PropertyLabelStyle.normal.textColor = Color.white;
		}

		/// <summary>
		/// Called when [GUI].
		/// </summary>
		public void OnGUI() {
			if (this.Visibility == true) {
				WindowRect = GUI.Window(0, WindowRect, DoMyWindow, "Information");
			}
		}

		/// <summary>
		/// Does my window.
		/// </summary>
		/// <param name="WindowID">The window identifier.</param>
		public void DoMyWindow(int WindowID) {
			GUI.Label(new Rect(10, 20, 100, 20), "Name", this.PropertyLabelStyle);
			GUI.Label(new Rect(10, 40, 100, 20), "Type", this.PropertyLabelStyle);
			GUI.Label(new Rect(110, 20, 200, 20), this.Filename);
			GUI.Label(new Rect(110, 40, 200, 20), this.Filetype);

			if (this.InfoMap != null) {
				int Offset = 60;
				foreach (KeyValuePair<string, string> Entry in this.InfoMap) {
					GUI.Label(new Rect(10, Offset, 100, 20), Entry.Key + " - " + Entry.Value);
					Offset += 20;
				}
			}
		}

		/// <summary>
		/// Generates the base information.
		/// </summary>
		/// <param name="Filename">The filename.</param>
		/// <param name="Type">The type.</param>
		public void GenerateBaseInfo(string Filename, string Type) {
			this.Filename = Filename;
			string[] strs = Type.Split('.');
			if (strs != null && strs.Length > 0) {
				this.Filetype = strs[strs.Length - 1];
			} else {
				this.Filetype = "Undefined";
			}
		}

		/// <summary>
		/// Records the additional information.
		/// </summary>
		/// <param name="InfoMap">The information map.</param>
		public void RecordAdditionalInfo(Dictionary<string, string> InfoMap) {
			this.InfoMap = InfoMap;
		}

		/// <summary>
		/// Shows this instance.
		/// </summary>
		public void Show() {
			this.Visibility = true;
		}

		/// <summary>
		/// Hides this instance.
		/// </summary>
		public void Hide() {
			this.Visibility = false;
		}
	}
}