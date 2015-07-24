using System;
using System.Threading;
using MediaMotion.Core.Utils;

namespace MediaMotion.Modules.VideoViewer.Services.VLC.Models.Interfaces {
	/// <summary>
	/// VLC player interface
	/// </summary>
	public interface IPlayer : IDisposable {
		/// <summary>
		/// Gets the media.
		/// </summary>
		/// <value>
		/// The media.
		/// </value>
		IMedia Media { get; }

		/// <summary>
		/// Gets or sets the pitches.
		/// </summary>
		/// <value>
		/// The pitches.
		/// </value>
		int Pitches { get; set; }

		/// <summary>
		/// Gets or sets the lines.
		/// </summary>
		/// <value>
		/// The lines.
		/// </value>
		int Lines { get; set; }

		/// <summary>
		/// Gets the lock.
		/// </summary>
		/// <value>
		/// The lock.
		/// </value>
		Mutex Lock { get; }

		/// <summary>
		/// Gets the buffer.
		/// </summary>
		/// <value>
		/// The buffer.
		/// </value>
		IntPtr Buffer { get; }

		/// <summary>
		/// Gets the texture.
		/// </summary>
		/// <value>
		/// The texture.
		/// </value>
		AutoPinner Texture { get; }

		/// <summary>
		/// Gets the player.
		/// </summary>
		/// <value>
		/// The player.
		/// </value>
		IntPtr Resource { get; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is playing.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is playing; otherwise, <c>false</c>.
		/// </value>
		bool IsPlaying { get; set; }

		/// <summary>
		/// Gets or sets the time.
		/// </summary>
		/// <value>
		/// The time.
		/// </value>
		int Time { get; set; }

		/// <summary>
		/// Sets the texture.
		/// </summary>
		/// <param name="texture">The texture.</param>
		void SetTexture(object texture);

		/// <summary>
		/// Plays this instance.
		/// </summary>
		void Play();

		/// <summary>
		/// Pauses this instance.
		/// </summary>
		void Pause();

		/// <summary>
		/// Stops this instance.
		/// </summary>
		void Stop();
	}
}
