using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Copy Buffer Controller
	/// </summary>
	public class CopyBufferController : MonoBehaviour {
		/// <summary>
		/// Texture for file icon.
		/// </summary>
		private Texture2D fileTexture;

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start() {
			this.fileTexture = Resources.Load<Texture2D>("Icons/File-icon");
		}

		/// <summary>
		/// Prints the copy-paste buffer.
		/// </summary>
		/// <param name="WindowID">The window identifier.</param>
		public void PrintBuffer(int WindowID) {
			GUI.color = new Color(1, 1, 1, 0.65f);
			GUI.DrawTexture(new Rect(1010, 10, 125, 125), this.fileTexture, ScaleMode.StretchToFill, true);
			GUI.Label(new Rect(1025, 140, 125, 30), "File.jpg");
		}
	}
}
