using System;

using MediaMotion.Resolver.Activators;

namespace MediaMotion.Resolver {
	/// <summary>
	/// The registration.
	/// </summary>
	/// <typeparam name="T">
	/// </typeparam>
	internal class Registration<T> : IRegistration where T : class {
		/// <summary>
		/// The activator class.
		/// </summary>
		private readonly IActivatorClass<T> activatorClass;

		/// <summary>
		/// The other type.
		/// </summary>
		private Type otherType;

		/// <summary>
		/// The global type.
		/// </summary>
		private Type globalType;

		/// <summary>
		/// Initializes a new instance of the <see cref="Registration{T}"/> class.
		/// </summary>
		/// <param name="activatorClass">
		/// The activator class.
		/// </param>
		public Registration(IActivatorClass<T> activatorClass) {
			this.activatorClass = activatorClass;
			this.otherType = null;
			this.globalType = typeof(T);
		}

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		public object Resolve() {
			return this.activatorClass.Resolve();
		}

		/// <summary>
		/// The as.
		/// </summary>
		/// <typeparam name="TOtherType">
		/// </typeparam>
		/// <returns>
		/// The <see cref="IRegistration"/>.
		/// </returns>
		public IRegistration As<TOtherType>() {
			this.otherType = typeof(TOtherType);
			return this;
		}

		/// <summary>
		/// The is type.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool IsType(Type type) {
			if (this.otherType != null && this.otherType == type) {
				return true;
			}
			return this.globalType == type;
		}

		/// <summary>
		/// The build.
		/// </summary>
		public void Build() {
			this.activatorClass.Build();
		}

		/// <summary>
		/// The single instance.
		/// </summary>
		/// <returns>
		/// The <see cref="IRegistration"/>.
		/// </returns>
		public IRegistration SingleInstance() {
			this.activatorClass.SingleInstance = true;
			return this;
		}
	}
}