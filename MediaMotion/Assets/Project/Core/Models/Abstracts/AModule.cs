﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Core.Models.Abstracts {
	/// <summary>
	/// Abstract Module
	/// </summary>
	public abstract class AModule : IDisposable, IModule {
		/// <summary>
		/// Gets or sets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public int Priority { get; protected set; }

		/// <summary>
		/// Gets the name of the module.
		/// </summary>
		/// <value>
		/// The name of the module.
		/// </value>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets the module scene.
		/// </summary>
		/// <value>
		/// The module scene.
		/// </value>
		public string Scene { get; protected set; }

		/// <summary>
		/// Gets the module description.
		/// </summary>
		/// <value>
		/// The module description.
		/// </value>
		public string Description { get; protected set; }

		/// <summary>
		/// Gets a value indicating whether [support reload].
		/// </summary>
		/// <value>
		///   <c>true</c> if [support reload]; otherwise, <c>false</c>.
		/// </value>
		public bool SupportReload { get; protected set; }

		/// <summary>
		/// Gets a value indicating whether [support background].
		/// </summary>
		/// <value>
		///   <c>true</c> if [support background]; otherwise, <c>false</c>.
		/// </value>
		public bool SupportBackground { get; protected set; }

		/// <summary>
		/// Gets the supported extensions.
		/// </summary>
		/// <value>
		/// The supported extensions.
		/// </value>
		public string[] SupportedExtensions { get; protected set; }

		/// <summary>
		/// Gets the supported action.
		/// </summary>
		/// <value>
		/// The supported action.
		/// </value>
		public ActionType[] SupportedAction { get; protected set; }

		/// <summary>
		/// Gets or sets the services.
		/// </summary>
		/// <value>
		/// The services.
		/// </value>
		public IContainer Container { get; protected set; }

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		public IElement[] Parameters { get; protected set; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public virtual void Dispose() {
			this.Container.Dispose();
		}

		/// <summary>
		/// Configures the module.
		/// </summary>
		/// <param name="container">The container</param>
		public virtual void Configure(IContainer container) {
			this.Container = container;
		}

		/// <summary>
		/// Loads the specified files.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public virtual void Load(IElement[] parameters) {
			this.Parameters = parameters;
			this.EnabledActions();
		}

		/// <summary>
		/// Reloads the specified parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <exception cref="System.NotSupportedException">This module does not support the reload method</exception>
		public virtual void Reload(IElement[] parameters) {
			throw new NotSupportedException("This module does not support the reload method");
		}

		/// <summary>
		/// Load another module.
		/// </summary>
		/// <returns>
		/// The parameters to restore
		/// </returns>
		public virtual IElement[] Sleep() {
			return (this.Parameters);
		}

		/// <summary>
		/// Back to the module.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public virtual void WakeUp(IElement[] parameters) {
			this.Parameters = parameters;
			this.EnabledActions();
		}

		/// <summary>
		/// Unloads the module.
		/// </summary>
		public virtual void Unload() {
		}

		/// <summary>
		/// Supports the specified element.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		///   <c>true</c> if the element is supported, <c>false</c> otherwise
		/// </returns>
		public virtual bool Supports(string path) {
			if (File.Exists(path) && this.SupportedExtensions != null) {
				FileInfo fileInfo = new FileInfo(path);

				return (this.SupportedExtensions.Contains(fileInfo.Extension));
			}
			return (false);
		}

		/// <summary>
		/// Enable list of action Only enabled actions will be return by inputService
		/// </summary>
		/// <exception cref="System.ArgumentNullException">inputService cannot be found</exception>
		protected void EnabledActions() {
			IInputService inputService = this.Container.Get<IInputService>();

			if (inputService == null) {
				throw new System.ArgumentNullException("inputService cannot be found");
			}
			inputService.EnableActions(this.SupportedAction);
		}
	}
}
