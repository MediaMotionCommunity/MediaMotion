using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.XInput.Controller;
using MediaMotion.Motion.XInput.Detections;

namespace MediaMotion.Motion.XInput
{
	/// <summary>
	/// The x input.
	/// </summary>
	public class XInput : IWrapperDevice {
		/// <summary>
		/// The controller.
		/// </summary>
		private XboxController controller;

		/// <summary>
		/// The detection controller.
		/// </summary>
		private DetectionController detectionController;

		/// <summary>
		/// Initializes a new instance of the <see cref="XInput"/> class.
		/// </summary>
		public XInput() {
		    this.Name = "XInput";
		    this.Type = "XInput";
		    this.Link = string.Empty;
		    this.Author = "MediaMotion";
	    }

		/// <summary>
		/// Gets the name.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the type.
		/// </summary>
		public string Type { get; private set; }

		/// <summary>
		/// Gets the link.
		/// </summary>
		public string Link { get; private set; }

		/// <summary>
		/// Gets the author.
		/// </summary>
		public string Author { get; private set; }

		/// <summary>
		/// The dispose.
		/// </summary>
		public void Dispose() {
		}

		/// <summary>
		/// The load.
		/// </summary>
		public void Load() {
			this.controller = XboxController.RetrieveController(0);
		    this.detectionController = new DetectionController(this.controller);
	    }

		/// <summary>
		/// The unload.
		/// </summary>
		public void Unload() {
		    this.controller = null;
	    }

		/// <summary>
		/// The get actions.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public IEnumerable<IAction> GetActions() {
			this.controller.UpdateState();
		    var actions = this.detectionController.GetActions();
			return actions;
	    }
    }
}
