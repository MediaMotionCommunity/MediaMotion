using System;
using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using Leap;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	abstract class ASecureLeapDetection : ALeapDetection {
		#region Constant
		private readonly TimeSpan TIME_INTERVAL = new TimeSpan(0, 0, 0, 0, 250);
		#endregion

		#region Fields
		DateTime lastDetection;
		#endregion

		#region Constructor
		public ASecureLeapDetection() {
			this.lastDetection = DateTime.Now;
		}
		#endregion

		#region Methods
		public override IEnumerable<IAction> Detection(Gesture gesture) {
			IEnumerable<IAction> list = new List<IAction>();

			if (DateTime.Now - this.lastDetection > TIME_INTERVAL) {
				list = this.SecureDetection(gesture);
			}
			this.lastDetection = DateTime.Now;
			return list;
		}

		protected abstract IEnumerable<IAction> SecureDetection(Gesture gesture);
		#endregion
	}
}
