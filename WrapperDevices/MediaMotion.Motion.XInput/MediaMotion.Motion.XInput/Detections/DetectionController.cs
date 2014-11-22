using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.XInput.Controller;

namespace MediaMotion.Motion.XInput.Detections {
	/// <summary>
	/// The detection controller.
	/// </summary>
	internal class DetectionController {
		/// <summary>
		/// The controller.
		/// </summary>
		private readonly XboxController controller;

		/// <summary>
		/// The detections.
		/// </summary>
		private readonly IList<ADetection> detections = new List<ADetection>();

		/// <summary>
		/// Initializes a new instance of the <see cref="DetectionController"/> class.
		/// </summary>
		/// <param name="controller">
		/// The controller.
		/// </param>
		public DetectionController(XboxController controller) {
			this.controller = controller;
			this.detections.Add(new DPadADetection(controller));
			this.detections.Add(new ThumbStickDetection(controller));
			this.detections.Add(new KeyDetection(controller));
		}

		/// <summary>
		/// The get actions.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public IEnumerable<IAction> GetActions() {
			var actions = new List<IAction>();
			foreach (var detection in this.detections) {
				actions.AddRange(detection.GetActions());
			}
			return actions;
		}
	}
}
