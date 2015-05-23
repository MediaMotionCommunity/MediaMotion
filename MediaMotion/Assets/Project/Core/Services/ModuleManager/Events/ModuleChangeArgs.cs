using System;
using MediaMotion.Core.Models.Interfaces;

namespace MediaMotion.Core.Services.ModuleManager.Events {
	/// <summary>
	/// Module Change Args
	/// </summary>
	public class ModuleChangeArgs : EventArgs {
		/// <summary>
		/// Initializes a new instance of the <see cref="ModuleChangeArgs" /> class.
		/// </summary>
		/// <param name="module">The module.</param>
		public ModuleChangeArgs(IModule oldModule, IModule newModule) {
			this.OldModule = oldModule;
			this.NewModule = newModule;
		}

		/// <summary>
		/// Gets the old module.
		/// </summary>
		/// <value>
		/// The old module.
		/// </value>
		public IModule OldModule { get; private set; }

		/// <summary>
		/// Gets the new module.
		/// </summary>
		/// <value>
		/// The new module.
		/// </value>
		public IModule NewModule { get; private set; }
	}
}
