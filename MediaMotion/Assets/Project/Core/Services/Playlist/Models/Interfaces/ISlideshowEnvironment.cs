namespace MediaMotion.Core.Services.Playlist.Models.Interfaces {
	/// <summary>
	/// Slideshow environment interface
	/// </summary>
	public interface ISlideshowEnvironment {
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IFloor"/> is fullscreen.
		/// </summary>
		/// <value>
		///   <c>true</c> if fullscreen; otherwise, <c>false</c>.
		/// </value>
		bool Fullscreen { get; set; }
	}
}
