using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Sidebar controller
	/// </summary>
	public class SidebarController : MonoBehaviour {
		/// <summary>
		/// Sidebar container
		/// </summary>
		private Rect sidebarRect;

		/// <summary>
		/// Style for the sidebar container.
		/// </summary>
		private GUIStyle sidebarWindowStyle;

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
		/// Indicates whether the sidebar is moving or not.
		/// </summary>
		private bool sidebarStatus;

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start() {
			this.sidebarVisibility = false;
			this.sidebarVisibleX = 1000;
			this.sidebarHiddenX = 1125;
			this.sidebarX = this.sidebarHiddenX;
			this.sidebarRect = new Rect(this.sidebarHiddenX, 200, 160, 300);
			this.sidebarStatus = false;
			this.sidebarWindowStyle = Resources.Load<GUISkin>("SidebarStyle").GetStyle("Box");
		}

		/// <summary>
		/// Prints the sidebar
		/// </summary>
		/// <param name="WindowID">The window identifier.</param>
		public void PrintSidebar(int WindowID) {
			// GUI.Label(new Rect(10, 20, 150, 20), "Prout");
		}

		/// <summary>
		/// Handles sidebar movement.
		/// </summary>
		public void MoveSidebar() {
			if (this.sidebarStatus == true) {
				if (this.sidebarVisibility == true) {
					this.sidebarX -= 2;
					if (this.sidebarX <= this.sidebarVisibleX) {
						this.sidebarStatus = false;
					}
				} else {
					this.sidebarX += 2;
					if (this.sidebarX >= this.sidebarHiddenX) {
						this.sidebarStatus = false;
					}
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
	}
}
