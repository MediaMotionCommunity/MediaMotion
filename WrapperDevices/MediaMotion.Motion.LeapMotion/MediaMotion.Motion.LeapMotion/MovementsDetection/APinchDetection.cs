using System;
using Leap;
using MediaMotion.Motion.LeapMotion.Core;
using MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	public abstract class APinchDetection : ICustomDetection {
		#region Constant
		/// <summary>
		/// Threshold for detection of pinch between 0-1
		/// </summary>
		protected readonly float ThresholdDetection = 0.9f;
		#endregion

		#region Fields
		/// <summary>
		/// Maximun time for do and release pinch for valid detection
		/// </summary>
		protected TimeSpan ReleaseTimeMax;

		/// <summary>
		/// State of detection global, use for avoid spamming select action
		/// </summary>
		protected bool detectionState;

		/// <summary>
		/// Time of the last action generate
		/// </summary>
		private DateTime lastAction;
		#endregion

		#region Consturtor
		/// <summary>
		/// Initializes a new instance of the <see cref="PinchSelectionDetection" /> class.
		/// </summary>
		public APinchDetection(TimeSpan releaseTimeMax) {
			this.ReleaseTimeMax = releaseTimeMax;
			this.lastAction = DateTime.Now;
			this.detectionState = false;		
		}
		#endregion

		#region Methods

		/// <summary>
		/// Detection of PinchSelection
		/// </summary>
		/// <param name="frame">Leap Frame</param>
		/// <param name="actionCollection"></param>
		/// <returns>List of IAction</returns>
		public void Detection(Frame frame, IActionCollection actionCollection) {
			if (this.detectionState) {
				if (!frame.Hands.IsEmpty) {
					foreach (var hand in frame.Hands) {
						if (hand.IsValid) {
							this.ValidDetection(hand.IsRight ? 0 : 1, hand.PinchStrength, actionCollection);
							if (!this.detectionState) {
								this.lastAction = DateTime.Now;
								continue;
							}
						}
					}
				}
			}
			else if (DateTime.Now - this.lastAction > this.ReleaseTimeMax) {
				this.detectionState = true;
			}
		}

		/// <summary>
		/// Validate detection of pinch movement. Set detectionState to flase for stop detection for ReleaseTimeMax time.
		/// </summary>
		/// <param name="hand">int represent the hand use</param>
		/// <param name="streng">float represent the streng of pinch</param>
		/// <returns>true if the movment is valid</returns>
		protected abstract void ValidDetection(int hand, float streng, IActionCollection actionCollection);
		#endregion
	}
}
