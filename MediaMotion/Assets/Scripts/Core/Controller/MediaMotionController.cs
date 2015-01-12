using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MediaMotion.Core.Exceptions;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Motion;
using UnityEngine;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Core.Controllers {
	/// <summary>
	/// The media motion controller.
	/// </summary>
	public class MediaMotionController : MonoBehaviour {
		private IFileSystem FileSystem;
		private IWrapperDevice Wrapper;
		private IModule Module;
		private string WrapperDevicePath;

		private delegate void ActionsHandler(object Sender, ActionDetectedEventArgs Args);
		private event ActionsHandler ActionsHandlers;

		/// <summary>
		/// Initializes a new instance of the <see cref="MediaMotionController"/> class.
		/// </summary>
		public MediaMotionController() {
			// this.Logger = LogManager.GetLogger("Core");
			this.FileSystem = FileSystemService.GetInstance();
			this.Module = null;
			this.WrapperDevicePath = Path.Combine(this.FileSystem.InitialFolder.GetPath(), "WrapperDevicesLibraries");
		}

		/// <summary>
		/// The start.
		/// </summary>
		public void Start() {
			this.LoadModule("MediaMotion.Core.Controllers.FolderContentController");
			this.LoadWrapper();
		}

		public void Update() {
			foreach (IAction Action in this.Wrapper.GetActions()) {
				this.ActionsHandlers(this, new ActionDetectedEventArgs(Action));
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				this.ActionsHandlers(this, new ActionDetectedEventArgs(new MediaMotion.Motion.Actions.Action(ActionType.Left, null)));
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				this.ActionsHandlers(this, new ActionDetectedEventArgs(new MediaMotion.Motion.Actions.Action(ActionType.Right, null)));
			}
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				this.ActionsHandlers(this, new ActionDetectedEventArgs(new MediaMotion.Motion.Actions.Action(ActionType.ScrollIn, null)));
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				this.ActionsHandlers(this, new ActionDetectedEventArgs(new MediaMotion.Motion.Actions.Action(ActionType.ScrollOut, null)));
			}
			if (Input.GetKeyDown(KeyCode.Space)) {
				this.ActionsHandlers(this, new ActionDetectedEventArgs(new MediaMotion.Motion.Actions.Action(ActionType.Select, null)));
			}
		}

		/// <summary>
		/// The load module.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		private void LoadModule(string name) {
			this.Module = new FolderContentController();
			this.Module.Load();
			this.ActionsHandlers += this.Module.ActionHandle;
		}

		/// <summary>
		/// The load wrapper.
		/// </summary>
		private void LoadWrapper() {
			Debug.Log("Load Wrapper");
			this.Wrapper = this.LoadWrapperDevice();
			this.Wrapper.Load();
		}

		/// <summary>
		/// The load wrapper device.
		/// </summary>
		/// <returns>
		/// The <see cref="IWrapperDevice"/>.
		/// </returns>
		private IWrapperDevice LoadWrapperDevice() {
			Debug.Log(this.WrapperDevicePath);
			var files = Directory.GetFiles(this.WrapperDevicePath, "MediaMotion.*.dll");
			if (files.Length < 1) {
				throw new WrapperNotFoundException("WrapperDevice library not found");
			}
			var wdt = typeof(IWrapperDevice);
			var assembly = Assembly.LoadFrom(files[0]);
			var types = assembly.GetTypes().Where(wdt.IsAssignableFrom);
			if (!types.Any()) {
				throw new WrapperNotFoundException("Bad Wrapper Library");
			}
			var type = types.FirstOrDefault();
			var wd = Activator.CreateInstance(type) as IWrapperDevice;
			Debug.Log("Wd : " + wd.Name + ", " + wd.Link);
			return wd;
		}
	}
}
