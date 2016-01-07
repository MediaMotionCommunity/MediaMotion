using System;
using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion {
	/// <summary>
	/// The leap motion.
	/// </summary>
	public class LeapMotion : IWrapperDevice {
		#region Fields

		/// <summary>
		/// The core.
		/// </summary>
		private LeapMotionCore core;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="LeapMotion"/> class.
		/// </summary>
		public LeapMotion() {
			this.Name = "LeapMotion";
			this.Type = "Hand Motion";
			this.Link = "https://www.leapmotion.com/";
			this.Author = "MediaMotion";
			this.core = new LeapMotionCore();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the name of device.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the type of device.
		/// </summary>
		public string Type { get; private set; }

		/// <summary>
		/// Gets the link of device.
		/// </summary>
		public string Link { get; private set; }

		/// <summary>
		/// Gets the author.
		/// </summary>
		public string Author { get; private set; }

		/// <summary>
		/// The load.
		/// </summary>
		public void Load() {
			Console.WriteLine("Load");
		}

		/// <summary>
		/// The unload.
		/// </summary>
		public void Unload() {
			Console.WriteLine("UnLoad");
		}

		#endregion

		#region Methods
		/// <summary>
		/// The get actions.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		/// <exception cref="NotImplementedException">
		/// Not Implemented
		/// </exception>
		public IEnumerable<IAction> GetActions() {
			return this.core.Frame();
		}

		public void EnableAction(ActionType action) {
			this.core.EnableAction(action);
		}

		public void EnableActions(IEnumerable<ActionType> actions) {
			this.core.EnableAction(actions);
		}

		#endregion

		#region IDisposable
		/// <summary>
		/// The dispose.
		/// </summary>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public void Dispose() {
			Dispose(true);
		}

		public void Dispose(bool dispose) {
			if (dispose) {
				this.core.Dispose();
				GC.SuppressFinalize(this);
			}
		}
		#endregion
	}
}
