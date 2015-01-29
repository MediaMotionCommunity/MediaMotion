using System;
using System.Collections.Generic;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Core.Models.Module {
	/// <summary>
	/// Module configuration
	/// </summary>
	public sealed class Configuration {
		/// <summary>
		/// Initializes a new instance of the <see cref="Configuration"/> class.
		/// </summary>
		public Configuration() {
			this.Movements = new Dictionary<ActionType, ActionHandler>();
			this.BackgroundMovements = new Dictionary<ActionType, ActionHandler>();
		}

		/// <summary>
		/// Event handler for event action
		/// </summary>
		/// <param name="Action">The <see cref="ActionDetectedEventArgs"/> instance containing the event data.</param>
		public delegate void ActionHandler(ActionDetectedEventArgs Action);

		/// <summary>
		/// Gets the name of the module.
		/// </summary>
		/// <value>
		/// The name of the module.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets the module scene.
		/// </summary>
		/// <value>
		/// The module scene.
		/// </value>
		public string Scene { get; set; }

		/// <summary>
		/// Gets the module description.
		/// </summary>
		/// <value>
		/// The module description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets the movements.
		/// </summary>
		/// <value>
		/// The movements.
		/// </value>
		public Dictionary<ActionType, ActionHandler> Movements { get; private set; }

		/// <summary>
		/// Gets the background movements.
		/// </summary>
		/// <value>
		/// The background movements.
		/// </value>
		public Dictionary<ActionType, ActionHandler> BackgroundMovements { get; private set; }
	}
}
