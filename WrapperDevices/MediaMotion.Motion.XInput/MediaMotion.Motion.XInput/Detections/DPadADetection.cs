using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.XInput.Controller;

namespace MediaMotion.Motion.XInput.Detections {
	/// <summary>
	/// The d pad a detection.
	/// </summary>
	internal class DPadADetection : ADetection {
		/// <summary>
		/// The map.
		/// </summary>
		private readonly IDictionary<string, bool> map = new Dictionary<string, bool> {
			{ "Down", true },
			{ "Up", true },
			{ "Right", true },
			{ "Left", true },
		};

		/// <summary>
		/// Initializes a new instance of the <see cref="DPadADetection"/> class.
		/// </summary>
		/// <param name="controller">
		/// The controller.
		/// </param>
		public DPadADetection(XboxController controller)
			: base(controller) {
		}

		/// <summary>
		/// The get actions.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public override IEnumerable<IAction> GetActions() {
			var list = new List<IAction>();
			if (this.Controller.IsDPadRightPressed) {
				if (this.map["Right"]) {
					list.Add(new Action(ActionType.Right, null));
					this.map["Right"] = false;
				}
			}
			else {
				this.map["Right"] = true;
			}

			if (this.Controller.IsDPadLeftPressed) {
				if (this.map["Left"]) {
					list.Add(new Action(ActionType.Left, null));
					this.map["Left"] = false;
				}
			}
			else {
				this.map["Left"] = true;
			}

			if (this.Controller.IsDPadDownPressed) {
				if (this.map["Down"]) {
					list.Add(new Action(ActionType.ScrollOut, null));
					this.map["Down"] = false;
				}
			}
			else {
				this.map["Down"] = true;
			}

			if (this.Controller.IsDPadUpPressed) {
				if (this.map["Up"]) {
					list.Add(new Action(ActionType.ScrollIn, null));
					this.map["Up"] = false;
				}
			}
			else {
				this.map["Up"] = true;
			}
			return list;
		}
	}
}