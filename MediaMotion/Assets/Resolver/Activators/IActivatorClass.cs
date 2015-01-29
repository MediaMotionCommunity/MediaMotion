namespace MediaMotion.Resolver.Activators {
	/// <summary>
	/// The ActivatorClass interface.
	/// </summary>
	/// <typeparam name="T">
	/// </typeparam>
	internal interface IActivatorClass<T> where T : class {
		/// <summary>
		/// Gets or sets a value indicating whether single instance.
		/// </summary>
		bool SingleInstance { get; set; }

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <returns>
		/// The <see cref="T"/>.
		/// </returns>
		T Resolve();

		/// <summary>
		/// The build.
		/// </summary>
		void Build();
	}
}