using System;

namespace MediaMotion.Motion.LeapMotion.Core.Exceptions {
	public class DetectionResolveException : Exception {
		public DetectionResolveException(string message)
			: base(message) {	
		}
	}
}