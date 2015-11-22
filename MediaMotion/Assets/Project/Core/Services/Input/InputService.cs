using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MediaMotion.Core.Exceptions;
using MediaMotion.Core.Models.Interfaces;
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

			this.MandatoryActions = new ActionType[] {
				ActionType.Leave
			};

			this.movements = new List<IAction>();
			this.defaultInput = new Dictionary<KeyCode, IAction>();

			this.AddDefaultInput(KeyCode.LeftArrow, new MediaMotion.Motion.Actions.Action(ActionType.Left, null));
			this.AddDefaultInput(KeyCode.RightArrow, new MediaMotion.Motion.Actions.Action(ActionType.Right, null));
			this.AddDefaultInput(KeyCode.UpArrow, new MediaMotion.Motion.Actions.Action(ActionType.ScrollIn, null));
			this.AddDefaultInput(KeyCode.DownArrow, new MediaMotion.Motion.Actions.Action(ActionType.ScrollOut, null));
			this.AddDefaultInput(KeyCode.Space, new MediaMotion.Motion.Actions.Action(ActionType.Select, null));
			this.AddDefaultInput(KeyCode.Backspace, new MediaMotion.Motion.Actions.Action(ActionType.Back, null));
			this.AddDefaultInput(KeyCode.Escape, new MediaMotion.Motion.Actions.Action(ActionType.Leave, null));
			this.AddDefaultInput(KeyCode.KeypadPlus, new MediaMotion.Motion.Actions.Action(ActionType.ZoomIn, 1.0f));
			this.AddDefaultInput(KeyCode.KeypadMinus, new MediaMotion.Motion.Actions.Action(ActionType.ZoomOut, 1.0f));
			this.AddDefaultInput(KeyCode.L, new MediaMotion.Motion.Actions.Action(ActionType.RotateLeft, null));
			this.AddDefaultInput(KeyCode.R, new MediaMotion.Motion.Actions.Action(ActionType.RotateRight, null));

			this.AddDefaultInput(KeyCode.B, new MediaMotion.Motion.Actions.Action(ActionType.StartBack, new TimeSpan(0, 0, 3)));
			this.AddDefaultInput(KeyCode.C, new MediaMotion.Motion.Actions.Action(ActionType.CancelBack, null));

			this.AddDefaultInput(KeyCode.D, new MediaMotion.Motion.Actions.Action(ActionType.StartLeave, new TimeSpan(0, 0, 3)));
			this.AddDefaultInput(KeyCode.V, new MediaMotion.Motion.Actions.Action(ActionType.CancelLeave, null));

			this.LoadWrapper();
		}

		/// <summary>
		/// Gets the mandatory actions.
		/// </summary>
		/// <value>
		/// The mandatory actions.
		/// </value>
		public ActionType[] MandatoryActions { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this instance is loaded.
		/// </summary>
		/// <value>
		/// <c>true</c> if this input device is loaded; otherwise, <c>false</c>.
		/// </value>
		public bool IsLoaded { get; private set; }

		/// <summary>
		/// Loads the wrapper.
		/// </summary>
		/// <param name="Name">Name of the wrapper device.</param>
		/// <param name="Path">The wrapper device path.</param>
		/// <exception cref="WrapperNotFoundException">WrapperDevice library not found or Bad Wrapper Library</exception>
		public void LoadWrapper(string Name = "MediaMotion.*.dll", string Path = null) {
			try {
				Type type = null;
				string[] files = null;
				IEnumerable<Type> types = null;

				type = typeof(IWrapperDevice);
				files = Directory.GetFiles(Path ?? System.IO.Path.Combine(this.fileSystemService.InitialFolder.GetPath(), "WrapperDevicesLibraries"), Name);
				if (files.Length < 1) {
					throw new WrapperNotFoundException("WrapperDevice library not found");
				}
				types = Assembly.LoadFrom(files[0]).GetTypes().Where(type.IsAssignableFrom);
				if (!types.Any()) {
					throw new WrapperNotFoundException("Bad Wrapper Library");
				}
				this.wrapper = Activator.CreateInstance(types.FirstOrDefault()) as IWrapperDevice;
			} catch (WrapperNotFoundException exception) {
				Debug.LogError("Wrapper not found: " + exception.Message);
			} catch (DllNotFoundException exception) {
				Debug.LogError("Dll not found: " + exception.Message);
			} catch (Exception exception) {
				Debug.LogError(exception);
			}
			this.IsLoaded = this.wrapper != null;
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

				if (this.IsLoaded) {
					foreach (IAction action in this.wrapper.GetActions()) {
						this.AddMovement(action);
					}
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
		/// Gets the specific movements.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The movements</returns>
		public List<IAction> GetMovements(ActionType type) {
			return (new List<IAction>(this.GetMovements().Where(action => action.Type == type)));
		}

		/// <summary>
		/// Gets the cursor.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The action</returns>
		public IAction GetCursorMovement(int id) {
			return (this.GetMovements(ActionType.BrowsingCursor).Find(action => (action.Parameter as MediaMotion.Motion.Actions.Parameters.Object3).Id == id));
		}

		/// <summary>
		/// Enable actions
		/// </summary>
		/// <param name="actions">action's list</param>
		public void EnableActions(IEnumerable<ActionType> actions) {
			if (this.MandatoryActions != null) {
				if (actions != null) {
					actions = actions.Concat(this.MandatoryActions);
				} else {
					actions = this.MandatoryActions;
				}
			}
			this.wrapper.EnableActions(actions);
		}

		/// <summary>
		/// Adds the movement.
		/// </summary>
		/// <param name="action">The action.</param>
		private void AddMovement(IAction action) {
			if (action.Type == ActionType.Leave) {
				this.moduleManagerService.Unload();
			} else {
				this.movements.Add(action);
			}
		}
	}
}
