using System;
using MediaMotion.Core.Services.ModuleManager.Interfaces;

namespace MediaMotion.Core.Services.ModuleManager.Events.Handlers {
	/// <summary>
	/// Handler of module changement
	/// </summary>
	/// <param name="moduleManager">The module manager.</param>
	/// <param name="eventArgs">The event arguments.</param>
	public delegate void ModuleChangeHandler(IModuleManagerService moduleManager, ModuleChangeArgs eventArgs);
}
