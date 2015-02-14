using System;
using Leap;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Abstract for default LeapMotion gesture with time limitor.
	/// Implement protected IEnumerable SecureDetection(Gesture gesture)
	/// Use public IEnumerable Detection(Gesture gesture)
	/// </summary>
	public abstract class ASecureLeapDetection : ALeapDetection {
		#region Constant
		/// <summary>
		/// Minimal time between two detection
		/// </summary>
		private readonly TimeSpan timeInterval = new TimeSpan(0, 0, 0, 0, 250);
		#endregion

		#region Fields
		/// <summary>
		/// Date of the last detection
		/// </summary>
		private DateTime lastDetection;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="ASecureLeapDetection" /> class.
		/// </summary>
		public ASecureLeapDetection() {
			this.lastDetection = DateTime.Now;
		}
		#endregion

		#region Methods

		/// <summary>
		/// Return the list of IAction detected
		/// </summary>
		/// <param name="gesture">Leap Gesture</param>
		/// <param name="actionCollection"></param>
		/// <returns>list of IAction</returns>
		public override void Detection(Gesture gesture, IActionCollection actionCollection) {
			if (DateTime.Now - this.lastDetection > this.timeInterval) {
				this.SecureDetection(gesture, actionCollection);
			}
			this.lastDetection = DateTime.Now;
		}

		/// <summary>
		/// Call by Detection, return list of IAction.
		/// Need to be implemented.
		/// </summary>
		/// <param name="gesture">Leap Gesture</param>
		/// <param name="actionCollection"></param>
		/// <returns>list of IAction</returns>
		protected abstract void SecureDetection(Gesture gesture, IActionCollection actionCollection);
		#endregion
	}
}
