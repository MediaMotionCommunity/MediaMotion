using Leap;

namespace MediaMotion.Motion.LeapMotion.Core {
	public static class HandExtension {
		public enum eHand {
			Right = 0,
			Left = 1
		}

		public static eHand GetHand(this Hand hand) {
			return hand.IsRight ? eHand.Right : eHand.Left;
		}
	}
}