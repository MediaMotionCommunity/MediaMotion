using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.XInput.Controller;

namespace MediaMotion.Motion.XInput.Detections {
	/// <summary>
	/// The a detection.
	/// </summary>
	internal abstract class ADetection {
		/// <summary>
		/// The controller.
		/// </summary>
		protected readonly XboxController Controller;

		/// <summary>
		/// Initializes a new instance of the <see cref="ADetection"/> class.
		/// </summary>
		/// <param name="controller">
		/// The controller.
		/// </param>
		protected ADetection(XboxController controller) {
			this.Controller = controller;
		}

		/// <summary>
		/// The get actions.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public abstract IEnumerable<IAction> GetActions();
	}
}