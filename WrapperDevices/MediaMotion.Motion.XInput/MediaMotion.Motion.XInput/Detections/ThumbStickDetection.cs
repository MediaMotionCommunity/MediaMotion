using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.XInput.Controller;
using Action = MediaMotion.Motion.Actions.Action;

namespace MediaMotion.Motion.XInput.Detections {
	/// <summary>
	/// The thumb stick detection.
	/// </summary>
	internal class ThumbStickDetection : ASecureDetection {
		/// <summary>
		/// The min val.
		/// </summary>
		private const int MinVal = 24000;

		/// <summary>
		/// Initializes a new instance of the <see cref="ThumbStickDetection"/> class.
		/// </summary>
		/// <param name="xboxController">
		/// The xbox controller.
		/// </param>
		public ThumbStickDetection(XboxController xboxController) : base(xboxController) {
		}

		/// <summary>
		/// The get actions.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public override IEnumerable<IAction> GetActions() {
			var list = new List<IAction>();
			if (!this.IsReady) {
				return list;
			}
			var left = this.Controller.LeftThumbStick;
			var right = this.Controller.RightThumbStick;
			if (left.X > MinVal) {
				list.Add(new Action(ActionType.Right, null));
			}
			if (left.X < -MinVal) {
				list.Add(new Action(ActionType.Left, null));
			}
			if (left.Y > MinVal) {
				list.Add(new Action(ActionType.ScrollIn, null));
			}
			if (left.Y < -MinVal) {
				list.Add(new Action(ActionType.ScrollOut, null));
			}
			base.GetActions();
			return list;
		}
	}
}