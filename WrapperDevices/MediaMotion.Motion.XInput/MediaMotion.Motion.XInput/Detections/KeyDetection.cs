using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.XInput.Controller;

namespace MediaMotion.Motion.XInput.Detections {
	/// <summary>
	/// The key detection.
	/// </summary>
	internal class KeyDetection : ADetection {
		/// <summary>
		/// The map.
		/// </summary>
		private readonly IDictionary<string, bool> map = new Dictionary<string, bool> {
			{ "A", true },
			{ "B", true },
			{ "Y", true },
			{ "X", true },
		};

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyDetection"/> class.
		/// </summary>
		/// <param name="xboxController">
		/// The xbox controller.
		/// </param>
		public KeyDetection(XboxController xboxController) : base(xboxController) {
		}

		/// <summary>
		/// The get actions.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public override IEnumerable<IAction> GetActions() {
			var list = new List<IAction>();
			if (this.Controller.IsAPressed) {
				if (this.map["A"]) {
					list.Add(new Action(ActionType.Select, null));
					this.map["A"] = false;
				}
			}
			else {
				this.map["A"] = true;
			}
			return list;
		}
	}
}