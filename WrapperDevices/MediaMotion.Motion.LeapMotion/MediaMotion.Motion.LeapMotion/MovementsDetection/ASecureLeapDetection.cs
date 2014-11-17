using System;
using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using Leap;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Abstract for default LeapMotion gesture with time limitor.
	/// Implement protected IEnumerable<IAction> SecureDetection(Gesture gesture)
	/// Use public IEnumerable<IAction> Detection(Gesture gesture)
	/// </summary>
	public abstract class ASecureLeapDetection : ALeapDetection {
		#region Constant
		/// <summary>
		/// Minimal time between two detection
		/// </summary>
		private readonly TimeSpan TimeInterval = new TimeSpan(0, 0, 0, 0, 250);
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
		/// <returns>list of IAction</returns>
		public override IEnumerable<IAction> Detection(Gesture gesture) {
			IEnumerable<IAction> list = new List<IAction>();

			if (DateTime.Now - this.lastDetection > this.TimeInterval) {
				list = this.SecureDetection(gesture);
			}
			this.lastDetection = DateTime.Now;
			return list;
		}

		/// <summary>
		/// Call by Detection, return list of IAction.
		/// Need to be implemented.
		/// </summary>
		/// <param name="gesture">Leap Gesture</param>
		/// <returns>list of IAction</returns>
		protected abstract IEnumerable<IAction> SecureDetection(Gesture gesture);
		#endregion
	}
}
