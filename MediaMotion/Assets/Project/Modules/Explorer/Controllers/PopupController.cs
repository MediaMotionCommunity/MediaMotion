using System.Timers;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Popup Controller
	/// </summary>
	public class PopupController : AScript<ExplorerModule, PopupController> {
		/// <summary>
		/// The popup timer
		/// </summary>
		private Timer timer;

		/// <summary>
		/// Style for the name of the property.
		/// </summary>
		private GUIStyle titleLineStyle;

		/// <summary>
		/// Style for the name of the property.
		/// </summary>
		private GUIStyle valueLineStyle;

		/// <summary>
		/// Style for the popup.
		/// </summary>
		private GUIStyle windowStyle;

		/// <summary>
		/// Popup container
		/// </summary>
		private Rect popupRect;

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
		public bool PopupVisibility { get; private set; }

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Init() {
			// timer
			this.Delay = 500.0;
			this.timer = new Timer(this.Delay);
			this.timer.Elapsed += this.Show;

			// gui element
			this.popupRect = new Rect(20, 20, 300, 90);

			this.titleLineStyle = new GUIStyle();
			Color labelColor = new Color(0.84f, 0.87f, 0.91f, 1.0f);
			this.titleLineStyle.normal.textColor = labelColor;
			this.titleLineStyle.padding = new RectOffset(5, 5, 20, 0);

			this.valueLineStyle = new GUIStyle();
			this.valueLineStyle.normal.textColor = labelColor;
			this.valueLineStyle.padding = new RectOffset(5, 5, 20, 0);
			this.valueLineStyle.fontStyle = FontStyle.Bold;

			this.windowStyle = new GUIStyle();
			Color titleColor = new Color(0.53f, 0.72f, 0.91f, 1.0f);
			this.windowStyle.normal.textColor = titleColor;
			this.windowStyle.fontSize = 16;
			this.windowStyle.fontStyle = FontStyle.Bold;
			Texture2D windowBackground = Resources.Load("PopupPNG") as Texture2D;
			this.windowStyle.normal.background = windowBackground;
			this.windowStyle.padding = new RectOffset(15, 15, 12, 12);
			this.windowStyle.border = new RectOffset(10, 10, 10, 10);

			// element
			this.element = null;
		}

		/// <summary>
		/// Called when [GUI].
		/// </summary>
		public void OnGUI() {
			if (this.PopupVisibility && this.element != null) {
				this.popupRect = GUI.Window(0, this.popupRect, this.HydratePopupContent, "Information", this.windowStyle);
			}
		}

		/// <summary>
		/// Shows the information popup
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="eventParams">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
		public void Show(object source = null, ElapsedEventArgs eventParams = null) {
			this.PopupVisibility = true;
			this.timer.Enabled = false;
		}

		/// <summary>
		/// Hides the information popup
		/// </summary>
		public void Hide() {
			this.PopupVisibility = false;
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
		/// Hydrate popup content
		/// </summary>
		/// <param name="WindowID">The window identifier.</param>
		private void HydratePopupContent(int WindowID) {
			if (this.element != null) {
				GUI.Label(new Rect(10, 20, 100, 20), "Name:", this.titleLineStyle);
				GUI.Label(new Rect(10, 40, 100, 20), "Type:", this.titleLineStyle);
				GUI.Label(new Rect(70, 20, 200, 20), this.element.GetName(), this.valueLineStyle);
				GUI.Label(new Rect(70, 40, 200, 20), this.element.GetHumanTypeString(), this.valueLineStyle);
			}
		}
	}
}