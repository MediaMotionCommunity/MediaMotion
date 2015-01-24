using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.MovementsDetection;

namespace MediaMotion.Motion.LeapMotion.Core {
	/// <summary>
	/// The leap motion core.
	/// </summary>
	public class LeapMotionCore : IDisposable {
		#region Fields

		/// <summary>
		/// The controller.
		/// </summary>
		private readonly Controller controller;

#if DEBUG
		[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1215:InstanceReadonlyElementsMustAppearBeforeInstanceNonReadonlyElements", Justification = "Reviewed. Suppression is OK here."),
		SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1121:UseBuiltInTypeAlias", Justification = "Reviewed. Suppression is OK here."),
		SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
		private readonly Object thisLock = new Object();
#endif
		/// <summary>
		/// The movement detection class
		/// </summary>
		private Detections movementsDetection;

		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="LeapMotionCore"/> class.
		/// </summary>
		public LeapMotionCore() {
			this.controller = new Controller();
			this.movementsDetection = new Detections();
			this.Configuration();
		}
		#endregion

		#region Methods
		#region IDisposable
		public void Dispose() {
			this.controller.Dispose();
		}
		#endregion

		/// <summary>
		/// The frame.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public IEnumerable<IAction> Frame() {
			return this.movementsDetection.MovementsDetection(this.controller.Frame());
        }
		#region Private
		/// <summary>
		/// The configuration of controller.
		/// </summary>
		private void Configuration() {
			this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
		}

		/// <summary>
		/// The safe write line.
		/// </summary>
		/// <param name="line">
		/// The line.
		/// </param>
		private void SafeWriteLine(string line) {
#if DEBUG
			lock (this.thisLock) {
				Console.WriteLine(line);
			}
#endif
		}
		#endregion
		#endregion
	}
}
