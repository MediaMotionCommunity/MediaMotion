using System;
using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.XInput.Controller;

namespace MediaMotion.Motion.XInput.Detections {
	/// <summary>
	/// The a secure detection.
	/// </summary>
	internal abstract class ASecureDetection : ADetection {
		/// <summary>
		/// The time interval.
		/// </summary>
		private readonly TimeSpan timeInterval = new TimeSpan(0, 0, 0, 0, 250);

		/// <summary>
		/// The last detection.
		/// </summary>
		private DateTime lastDetection;

		/// <summary>
		/// Initializes a new instance of the <see cref="ASecureDetection"/> class.
		/// </summary>
		/// <param name="controller">
		/// The controller.
		/// </param>
		protected ASecureDetection(XboxController controller) : base(controller) {
			this.lastDetection = DateTime.Now;
		}

		/// <summary>
		/// Gets a value indicating whether is ready.
		/// </summary>
		protected bool IsReady {
			get { return DateTime.Now - this.lastDetection > this.timeInterval; }
		}

		/// <summary>
		/// The get actions.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public override IEnumerable<IAction> GetActions() {
			this.lastDetection = DateTime.Now;
			return null;
		}
	}
}