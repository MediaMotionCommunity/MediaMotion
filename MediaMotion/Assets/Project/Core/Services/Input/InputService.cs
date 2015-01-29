using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MediaMotion.Core.Exceptions;
using MediaMotion.Core.Models.Core;
using MediaMotion.Core.Models.Service;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Core.Services.Input {
	/// <summary>
	/// LeapMotion Service
	/// </summary>
	public class InputService : ServiceBase, IInputService {
		private readonly IFileSystemService fileSystemService;

		/// <summary>
		/// The wrapper
		/// </summary>
		private IWrapperDevice Wrapper;

		/// <summary>
		/// The last frame
		/// </summary>
		private int? LastFrame;

		/// <summary>
		/// The movements
		/// </summary>
		private List<IAction> Movements;

		/// <summary>
		/// The default input
		/// </summary>
		private Dictionary<KeyCode, IAction> DefaultInput;

		/// <summary>
		/// Initializes a new instance of the <see cref="InputService"/> class.
		/// </summary>
		/// <param name="Core">The core.</param>
		/// <param name="fileSystemService"></param>
		public InputService(IFileSystemService fileSystemService) {
			this.fileSystemService = fileSystemService;
			this.LastFrame = null;
			this.Movements = new List<IAction>();
			this.DefaultInput = new Dictionary<KeyCode, IAction>();

			this.AddDefaultInput(KeyCode.LeftArrow, new MediaMotion.Motion.Actions.Action(ActionType.Left, null));
			this.AddDefaultInput(KeyCode.RightArrow, new MediaMotion.Motion.Actions.Action(ActionType.Right, null));
			this.AddDefaultInput(KeyCode.UpArrow, new MediaMotion.Motion.Actions.Action(ActionType.ScrollIn, null));
			this.AddDefaultInput(KeyCode.DownArrow, new MediaMotion.Motion.Actions.Action(ActionType.ScrollOut, null));
			this.AddDefaultInput(KeyCode.Space, new MediaMotion.Motion.Actions.Action(ActionType.Select, null));
			this.AddDefaultInput(KeyCode.Backspace, new MediaMotion.Motion.Actions.Action(ActionType.Return, null));

			this.LoadWrapper();
		}

		/// <summary>
		/// Loads the wrapper.
		/// </summary>
		/// <param name="Name">Name of the wrapper device.</param>
		/// <param name="Path">The wrapper device path.</param>
		/// <exception cref="WrapperNotFoundException">WrapperDevice library not found or Bad Wrapper Library</exception>
		public void LoadWrapper(string Name = "MediaMotion.*.dll", string Path = null) {
			Type Type = null;
			string[] Files = null;
			IEnumerable<Type> Types = null;

			Type = typeof(IWrapperDevice);
			Files = Directory.GetFiles(Path ?? System.IO.Path.Combine(this.fileSystemService.InitialFolder.GetPath(), "WrapperDevicesLibraries"), Name);
			if (Files.Length < 1) {
				throw new WrapperNotFoundException("WrapperDevice library not found");
			}
			Types = Assembly.LoadFrom(Files[0]).GetTypes().Where(Type.IsAssignableFrom);
			if (!Types.Any()) {
				throw new WrapperNotFoundException("Bad Wrapper Library");
			}
			this.Wrapper = Activator.CreateInstance(Types.FirstOrDefault()) as IWrapperDevice;
		}

		/// <summary>
		/// Adds the default input.
		/// </summary>
		/// <param name="Key">The key.</param>
		/// <param name="Movement">The movement.</param>
		public void AddDefaultInput(KeyCode Key, IAction Movement) {
			if (this.DefaultInput.ContainsKey(Key)) {
				this.DefaultInput.Remove(Key);
			}
			this.DefaultInput.Add(Key, Movement);
		}

		/// <summary>
		/// Gets the movements.
		/// </summary>
		/// <returns>The movements</returns>
		public List<IAction> GetMovements() {
			if (this.LastFrame == null || this.LastFrame != Time.frameCount) {
				this.LastFrame = Time.frameCount;
				this.Movements.Clear();

				foreach (IAction Action in this.Wrapper.GetActions()) {
					this.Movements.Add(Action);
				}
				foreach (KeyValuePair<KeyCode, IAction> Input in this.DefaultInput) {
					if (UnityEngine.Input.GetKeyDown(Input.Key)) {
						this.Movements.Add(Input.Value);
					}
				}
			}
			return (this.Movements);
		}
	}
}
