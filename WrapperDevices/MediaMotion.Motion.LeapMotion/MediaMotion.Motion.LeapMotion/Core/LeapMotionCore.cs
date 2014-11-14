using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Leap;
using MediaMotion.Motion.Actions;

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
		[SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1121:UseBuiltInTypeAlias", Justification = "Reviewed. Suppression is OK here."),
		SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")] 
		private readonly Object thisLock = new Object();
#endif
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="LeapMotionCore"/> class.
		/// </summary>
		public LeapMotionCore() {
			this.controller = new Controller();
			this.Configuration();
		}
		#endregion

		#region Methods

		/// <summary>
		/// The frame.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public IEnumerable<IAction> Frame() {
            return null;
        }
		#region Private
		/// <summary>
		/// The configuration of controller.
		/// </summary>
		private void Configuration() {
			this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
			this.controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
			this.controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
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

		#region IDisposable
		public void Dispose() {
			this.controller.Dispose();
		}
		#endregion
	}
}
