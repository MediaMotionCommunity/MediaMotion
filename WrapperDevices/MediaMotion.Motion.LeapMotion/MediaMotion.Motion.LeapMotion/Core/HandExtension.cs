using Leap;

namespace MediaMotion.Motion.LeapMotion.Core {
	public static class HandExtension {
		public enum HandDirection {
			Right = 0,
			Left = 1
		}

		public static HandDirection GetHand(this Hand hand) {
			return hand.IsRight ? HandDirection.Right : HandDirection.Left;
		}
	}
}