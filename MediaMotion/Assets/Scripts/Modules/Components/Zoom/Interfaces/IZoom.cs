namespace MediaMotion.Modules.Components.Zoom {
	/// <summary>
	/// Zoom Interface
	/// </summary>
	public interface IZoom {
		/// <summary>
		/// Gets the coefficient.
		/// </summary>
		/// <value>
		/// The coefficient.
		/// </value>
		float Coeff { get; }

		/// <summary>
		/// Zoom in.
		/// </summary>
		void ZoomIn();

		/// <summary>
		/// Zoom out.
		/// </summary>
		void ZoomOut();
	}
}
