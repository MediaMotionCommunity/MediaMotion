using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MediaMotion.Core.Exceptions;
using MediaMotion.Core.Models.Core;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Motion;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Core.Services.Input {
	/// <summary>
	/// LeapMotion Service
	/// </summary>
	public class InputService : IInputService {
		/// <summary>
		/// The file system service
		/// </summary>
		private readonly IFileSystemService fileSystemService;

		/// <summary>
		/// The module manager service
		/// </summary>
		private readonly IModuleManagerService moduleManagerService;

		/// <summary>
		/// The wrapper
		/// </summary>
		private IWrapperDevice wrapper;

		/// <summary>
		/// The last frame
		/// </summary>
		private int? lastFrame;

		/// <summary>
		/// The movements
		/// </summary>
		private List<IAction> movements;

		/// <summary>
		/// The default input
		/// </summary>
		private Dictionary<KeyCode, IAction> defaultInput;

		/// <summary>
		/// Initializes a new instance of the <see cref="InputService" /> class.
		/// </summary>
		/// <param name="fileSystem">The file system service.</param>
		/// <param name="moduleManager">The module manager service.</param>
		public InputService(IFileSystemService fileSystem, IModuleManagerService moduleManager) {
			this.fileSystemService = fileSystem;
			this.moduleManagerService = moduleManager;

			this.lastFrame = null;
			this.movements = new List<IAction>();
			this.defaultInput = new Dictionary<KeyCode, IAction>();

			this.AddDefaultInput(KeyCode.LeftArrow, new MediaMotion.Motion.Actions.Action(ActionType.Left, null));
			this.AddDefaultInput(KeyCode.RightArrow, new MediaMotion.Motion.Actions.Action(ActionType.Right, null));
			this.AddDefaultInput(KeyCode.UpArrow, new MediaMotion.Motion.Actions.Action(ActionType.ScrollIn, null));
			this.AddDefaultInput(KeyCode.DownArrow, new MediaMotion.Motion.Actions.Action(ActionType.ScrollOut, null));
			this.AddDefaultInput(KeyCode.Space, new MediaMotion.Motion.Actions.Action(ActionType.Select, null));
			this.AddDefaultInput(KeyCode.Backspace, new MediaMotion.Motion.Actions.Action(ActionType.Back, null));
			this.AddDefaultInput(KeyCode.Escape, new MediaMotion.Motion.Actions.Action(ActionType.Leave, null));

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
			this.wrapper = Activator.CreateInstance(Types.FirstOrDefault()) as IWrapperDevice;
		}

		/// <summary>
		/// Adds the default input.
		/// </summary>
		/// <param name="Key">The key.</param>
		/// <param name="Movement">The movement.</param>
		public void AddDefaultInput(KeyCode Key, IAction Movement) {
			if (this.defaultInput.ContainsKey(Key)) {
				this.defaultInput.Remove(Key);
			}
			this.defaultInput.Add(Key, Movement);
		}

		/// <summary>
		/// Gets the movements.
		/// </summary>
		/// <returns>The movements</returns>
		public List<IAction> GetMovements() {
			if (this.lastFrame == null || this.lastFrame != Time.frameCount) {
				this.lastFrame = Time.frameCount;
				this.movements.Clear();

				foreach (IAction action in this.wrapper.GetActions()) {
					this.AddMovement(action);
				}
				foreach (KeyValuePair<KeyCode, IAction> input in this.defaultInput) {
					if (UnityEngine.Input.GetKeyDown(input.Key)) {
						this.AddMovement(input.Value);
					}
				}
			}
			return (this.movements);
		}

		/// <summary>
		/// Adds the movement.
		/// </summary>
		/// <param name="action">The action.</param>
		private void AddMovement(IAction action) {
			if (action.Type == ActionType.Leave) {
				this.moduleManagerService.UnloadModule();
			} else {
				this.movements.Add(action);
			}
		}
	}
}
