﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
    public abstract class ALeapDetection {
		#region Fields
		protected Gesture.GestureType type;
		#endregion
		#region Methods
		public abstract IEnumerable<IAction> Detection(Gesture gesture);

		public Gesture.GestureType GetGestureType() {
			return this.type;
		}
		#endregion
	}
}
