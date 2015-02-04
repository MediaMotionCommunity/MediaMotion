using MediaMotion.Core.Resolver.Activators.Interfaces;
using MediaMotion.Core.Resolver.Exceptions;

namespace MediaMotion.Core.Resolver.Activators {
	/// <summary>
	/// The simple activator class.
	/// </summary>
	/// <typeparam name="T">
	/// </typeparam>
	internal class SimpleActivatorClass<T> : IActivatorClass<T>
		where T : class {
		/// <summary>
		/// The instance.
		/// </summary>
		private readonly T instance;

		/// <summary>
		/// The single instance.
		/// </summary>
		private bool singleInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleActivatorClass{T}"/> class.
		/// </summary>
		/// <param name="instance">
		/// The instance.
		/// </param>
		public SimpleActivatorClass(T instance) {
			this.instance = instance;
			this.singleInstance = true;
		}

		/// <summary>
		/// Gets or sets a value indicating whether single instance.
		/// </summary>
		/// <exception cref="LifeScopeException">
		/// Throw when the value is false. The registering by instance is only in single instance mode.
		/// </exception>
		public bool SingleInstance {
			get {
				return this.singleInstance;
			}
			set {
				if (value == false) {
					throw new LifeScopeException("Register by instance is only in single instance mode");
				}
				this.singleInstance = true;
			}
		}

		/// <summary>
		/// The build.
		/// </summary>
		/// <param name="resolver"></param>
		public void Build(Resolver resolver) {
		}

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <returns>
		/// The <see cref="T"/>.
		/// </returns>
		public T Get() {
			return this.instance;
		}
	}
}