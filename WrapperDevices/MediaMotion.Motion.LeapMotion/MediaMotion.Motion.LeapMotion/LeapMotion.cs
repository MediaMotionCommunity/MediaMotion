using System;
using System.Collections.Generic;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion
{
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
			this.Type = "HandMotion";
			this.Link = "https://www.leapmotion.com/";
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
		public IEnumerable<Action> GetActions() {
			return this.core.Frame();
		}

		/// <summary>
		/// The get cursor position.
		/// </summary>
		/// <returns>
		/// The <see cref="CursorPosition"/>.
		/// </returns>
		/// <exception cref="NotImplementedException">
		/// Not Implemented
		/// </exception>
		public CursorPosition GetCursorPosition() {
			return this.core.GetCursorPosition();
		}
		#endregion
	}
}

