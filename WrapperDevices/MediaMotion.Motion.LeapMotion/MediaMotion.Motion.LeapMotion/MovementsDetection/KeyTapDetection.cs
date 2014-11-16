using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaMotion.Motion.Actions;
using Leap;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	class KeyTapDetection : ALeapDetection {
		#region Constructor
		public KeyTapDetection() {
			this.type = Gesture.GestureType.TYPE_KEY_TAP;
		}
		#endregion

		#region Methods
		public override IEnumerable<IAction> Detection(Gesture gesture) {
			IEnumerable<IAction> list = new List<IAction>();
			Console.WriteLine("# In KeyTape");
			return list;
		}
		#endregion
	}
}
