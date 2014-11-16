using System;
using System.Threading;

namespace MediaMotion.Motion.LeapMotion.Testing {
	/// <summary>
	/// The program.
	/// </summary>
	public static class Program {
		/// <summary>
		/// The main.
		/// </summary>
		/// <param name="args">
		/// The args.
		/// </param>
		public static void Main(string[] args) {
			Console.WriteLine("Testing wrapper device");
			var wrapper = new LeapMotion();
			wrapper.Load();
			var timer = new Timer(Display, wrapper, 0, 1000 / 30);
			Console.ReadLine();
			wrapper.Unload();
		}

		/// <summary>
		/// The display.
		/// </summary>
		/// <param name="state">
		/// The state.
		/// </param>
		private static void Display(object state) {
			var wrapper = state as IWrapperDevice;
			if (wrapper != null) {
				wrapper.GetActions();
			}
		}
	}
}
