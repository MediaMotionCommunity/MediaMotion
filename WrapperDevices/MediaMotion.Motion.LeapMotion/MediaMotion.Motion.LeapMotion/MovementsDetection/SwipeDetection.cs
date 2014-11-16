using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaMotion.Motion.Actions;
using Leap;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
    class SwipeDetection : ALeapDetection {

		#region Constructor
		public SwipeDetection() {
			this.type = Gesture.GestureType.TYPE_SWIPE;
		}
		#endregion

		#region Methods
		public override IEnumerable<IAction> Detection(Gesture gesture) {
			IEnumerable<IAction> list = new List<IAction>();
			Console.WriteLine("## In Swipe");
            return list;
		}
		#endregion
	}
}
