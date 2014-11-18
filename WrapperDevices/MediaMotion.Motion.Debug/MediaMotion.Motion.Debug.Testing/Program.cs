using System;
using System.Threading;

namespace MediaMotion.Motion.Debug.Testing {
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
			var wrapper = new Debug();
			wrapper.Load();
			var timer = new Timer(Display, wrapper, 0, 1 / 30);
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
