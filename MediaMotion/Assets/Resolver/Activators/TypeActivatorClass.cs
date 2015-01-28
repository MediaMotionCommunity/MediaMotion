using System;
using System.Linq;
using System.Reflection;

using MediaMotion.Resolver.Exceptions;

namespace MediaMotion.Resolver.Activators {
	/// <summary>
	/// The type activator class.
	/// </summary>
	/// <typeparam name="T">
	/// </typeparam>
	internal class TypeActivatorClass<T> : IActivatorClass<T>
		where T : class {
		/// <summary>
		/// The resolver.
		/// </summary>
		private readonly Resolver resolver;

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
		private T instance;

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeActivatorClass{T}"/> class.
		/// </summary>
		/// <param name="resolver">
		/// The resolver.
		/// </param>
		public TypeActivatorClass(Resolver resolver) {
			this.resolver = resolver;
			this.globalType = typeof(T);
			this.SingleInstance = false;
		}

		/// <summary>
		/// Gets or sets a value indicating whether single instance.
		/// </summary>
		public bool SingleInstance { get; set; }

		/// <summary>
		/// The build.
		/// </summary>
		public void Build() {
			var constructorInfos = this.globalType.GetConstructors();
			foreach (var info in constructorInfos) {
				var parameters = info.GetParameters();
				var isValidConstructor = parameters.All(parameter => this.resolver.IsRegisterType(parameter.ParameterType));
				if (!isValidConstructor) {
					continue;
				}
				this.constructorInfo = info;
				this.parametersTypes = parameters;
				return;
			}
			throw new InjectableConstructorNotFoundException("The class don't have any constructor injectable");
		}

		/// <summary>
		/// The resolve the dependencies
		/// </summary>
		/// <returns>
		/// The <see cref="T"/> class generate
		/// </returns>
		public T Resolve() {
			T result;
			if (this.SingleInstance && this.instance != null) {
				return this.instance;
			}
			if (!this.parametersTypes.Any()) {
				result = (T)Activator.CreateInstance(this.globalType);
			}
			else {
				result = (T)this.constructorInfo.Invoke(this.ResolveParameters());
			}
			if (this.SingleInstance) {
				this.instance = result;
			}
			return result;
		}

		/// <summary>
		/// The resolve parameters.
		/// </summary>
		/// <returns>
		/// The array of parameters
		/// </returns>
		private object[] ResolveParameters() {
			var parameters = new object[this.parametersTypes.Count()];
			for (var i = 0; i < this.parametersTypes.Count(); ++i) {
				parameters[i] = this.resolver.Resolve(this.parametersTypes[i].ParameterType);
			}
			return parameters;
		}
	}
}