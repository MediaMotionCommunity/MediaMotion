﻿using System;
using System.Collections.Generic;
using System.Threading;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Motion.LeapMotion.Testing {
	/// <summary>
	/// The program.
	/// </summary>
	public static class MainProgram {
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
				DisplayAction(wrapper.GetActions());
			}
		}

		/// <summary>
		/// Display list of IAction
		/// </summary>
		/// <param name="list"></param>
		private static void DisplayAction(IEnumerable<IAction> list) {
			var i = 0;
			foreach (var ac in list) {
				Console.WriteLine(i + ": " + GetActionName(ac));
				i++;
			}
		}

		/// <summary>
		/// Return string version of IAction
		/// </summary>
		/// <param name="ac"></param>
		/// <returns></returns>
		private static string GetActionName(IAction ac) {
			var list = new Dictionary<ActionType, string> {
				{ActionType.Down, "Down"},
				{ActionType.Left, "Left"},
				{ActionType.Right, "Right"},
				{ActionType.Up, "Up"},
				{ActionType.ScrollIn, "ScrollIn"},
				{ActionType.ScrollOut, "ScrollOut"},
				{ActionType.Select, "Select"}
			};

			return list[ac.Type];
		}
	}
}