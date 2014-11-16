using System;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		private static void DisplayAction(IEnumerable<IAction> list) {
			int i = 0;
			foreach(IAction ac in list) {
				Console.WriteLine(i + ": " + Program.GetActionName(ac));
				i++;
			}
		}

		private static string GetActionName(IAction ac) {
			Dictionary<ActionType, string> list = new Dictionary<ActionType,string>();
			list.Add(ActionType.Down, "Down");
			list.Add(ActionType.Left, "Left");
			list.Add(ActionType.Right, "Right");
			list.Add(ActionType.Up, "Up");
			list.Add(ActionType.ScrollIn, "ScrollIn");
			list.Add(ActionType.ScrollOut, "ScrollOut");

			return list[ac.Type];
		}
	}
}
