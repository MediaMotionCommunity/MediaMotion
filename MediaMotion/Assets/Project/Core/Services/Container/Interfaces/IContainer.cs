namespace MediaMotion.Core.Services.Container.Interfaces {
	/// <summary>
	/// Container Interface
	/// </summary>
	public interface IContainer {
		/// <summary>
		/// Gets the requested service.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		///   The service
		/// </returns>
		Service Get<Service>();

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		///   The parameter
		/// </returns>
		string GetParameter(string key);
	}
}
