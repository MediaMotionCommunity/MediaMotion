using System;
using System.Collections.Generic;
using System.Threading;
using MediaMotion.Motion.Actions;

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
				Program.DisplayAction(wrapper.GetActions());
			}
		}

		/// <summary>
		/// Display list of IAction
		/// </summary>
		/// <param name="list"></param>
		private static void DisplayAction(IEnumerable<IAction> list) {
			int i = 0;
			foreach (IAction ac in list) {
				Console.WriteLine(i + ": " + Program.GetActionName(ac));
				i++;
			}
		}

		/// <summary>
		/// Return string version of IAction
		/// </summary>
		/// <param name="ac"></param>
		/// <returns></returns>
		private static string GetActionName(IAction ac) {
			Dictionary<ActionType, string> list = new Dictionary<ActionType, string>();
			list.Add(ActionType.Down, "Down");
			list.Add(ActionType.Left, "Left");
			list.Add(ActionType.Right, "Right");
			list.Add(ActionType.Up, "Up");
			list.Add(ActionType.ScrollIn, "ScrollIn");
			list.Add(ActionType.ScrollOut, "ScrollOut");
			list.Add(ActionType.Select, "Select");

			return list[ac.Type];
		}
	}
}
