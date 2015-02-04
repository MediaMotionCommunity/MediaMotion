using System;
using System.Linq;
using System.Reflection;
using MediaMotion.Core.Resolver.Activators.Interfaces;
using MediaMotion.Core.Resolver.Exceptions;
using UnityEngine;

namespace MediaMotion.Core.Resolver.Activators {
	/// <summary>
	/// The type activator class.
	/// </summary>
	/// <typeparam name="Service">
	/// </typeparam>
	internal class TypeActivatorClass<Service> : IActivatorClass<Service>
		where Service : class {
		/// <summary>
		/// The resolver.
		/// </summary>
		private Resolver resolver = null;

		/// <summary>
		/// The global type.
		/// </summary>
		private Type globalType;

		/// <summary>
		/// The constructor info.
		/// </summary>
		private ConstructorInfo constructorInfo;

		/// <summary>
		/// The parameters types.
		/// </summary>
		private ParameterInfo[] parametersTypes;

		/// <summary>
		/// The instance.
		/// </summary>
		private Service instance;

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeActivatorClass{Service}"/> class.
		/// </summary>
		public TypeActivatorClass() {
			this.globalType = typeof(Service);
			this.SingleInstance = false;
		}

		/// <summary>
		/// Gets or sets a value indicating whether single instance.
		/// </summary>
		public bool SingleInstance { get; set; }

		/// <summary>
		/// The build.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		/// <exception cref="MediaMotion.Core.Resolver.Exceptions.InjectableConstructorNotFoundException">The class ' + this.globalType.Name + ' doesn't have any constructor injectable</exception>
		public void Build(Resolver resolver) {
			this.resolver = resolver;

			foreach (ConstructorInfo info in this.globalType.GetConstructors()) {
				ParameterInfo[] parameters = info.GetParameters();

				if (!parameters.All(parameter => this.resolver.IsRegisterType(parameter.ParameterType))) {
					continue;
				}
				this.constructorInfo = info;
				this.parametersTypes = parameters;
				return;
			}
			throw new InjectableConstructorNotFoundException("The class '" + this.globalType.Name + "' doesn't have any constructor injectable");
		}

		/// <summary>
		/// The resolve the dependencies
		/// </summary>
		/// <returns>
		/// The <see cref="Service"/> class generate
		/// </returns>
		public Service Get() {
			Service result;

			if (this.SingleInstance && this.instance != null) {
				return this.instance;
			}
			if (!this.parametersTypes.Any()) {
				result = (Service)Activator.CreateInstance(this.globalType);
			} else {
				result = (Service)this.constructorInfo.Invoke(this.ResolveParameters());
			}
			if (this.SingleInstance) {
				this.instance = result;
			}
			return (result);
		}

		/// <summary>
		/// The resolve parameters.
		/// </summary>
		/// <returns>
		/// The array of parameters
		/// </returns>
		private object[] ResolveParameters() {
			object[] parameters = new object[this.parametersTypes.Count()];

			for (int i = 0; i < this.parametersTypes.Count(); ++i) {
				parameters[i] = this.resolver.Get(this.parametersTypes[i].ParameterType);
			}
			return (parameters);
		}
	}
}