using UnityEngine;
using System.Collections;
using MediaMotion.Core.Models.FileManager.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Core.View {
    public class FileInfoUI : MonoBehaviour {
        
        /// <summary>
        /// Popup container
        /// </summary>
        public Rect WindowRect = new Rect(20, 20, 200, 65);

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

        public FileInfoUI() {
            this.PropertyLabelStyle = new GUIStyle();
            this.PropertyLabelStyle.normal.textColor = Color.white;
        }

        public void OnGUI() {
            if (this.Visibility == true) {
                WindowRect = GUI.Window(0, WindowRect, DoMyWindow, "Information");
            }
        }

        public void DoMyWindow(int WindowID) {
            GUI.Label(new Rect(10, 20, 100, 20), "Name", this.PropertyLabelStyle);
            GUI.Label(new Rect(10, 40, 100, 20), "Type", this.PropertyLabelStyle);
            GUI.Label(new Rect(110, 20, 100, 20), this.Filename);
            GUI.Label(new Rect(110, 40, 100, 20), this.Filetype);
            
            if (this.InfoMap != null) {
                int Offset = 60;
                foreach (KeyValuePair<string, string> Entry in this.InfoMap) {
                    GUI.Label(new Rect(10, Offset, 100, 20), Entry.Key + " - " + Entry.Value);
                    Offset += 20;
                }
            }
        }

        public void GenerateBaseInfo(string Filename, string Type) {
            this.Filename = Filename;
            this.Filetype = Type;
        }

        public void RecordAdditionalInfo(Dictionary<string, string> InfoMap) {
            this.InfoMap = InfoMap;
        }

        public void Show() {
            this.Visibility = true;
        }

        public void Hide() {
            this.Visibility = false;
        }
    }
}