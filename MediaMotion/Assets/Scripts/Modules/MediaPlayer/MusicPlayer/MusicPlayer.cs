using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;

namespace MediaMotion.Modules.MediaPlayer.MusicPlayer {
	/// <summary>
	/// Music Player Model
	/// </summary>
	public class MusicPlayer : AMediaPlayer, IMusicPlayer {
		/// <summary>
		/// Plays this instance.
		/// </summary>
		public override void Play() {
			base.Play();
		}

		/// <summary>
		/// Pauses this instance.
		/// </summary>
		public override void Pause() {
			base.Pause();
		}

		/// <summary>
		/// Stops this instance.
		/// </summary>
		public override void Stop() {
			base.Stop();
		}

		/// <summary>
		/// Registers this instance.
		/// </summary>
		/// <exception cref="System.NotImplementedException">Not implemented</exception>
		public void Register() {
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Unregisters this instance.
		/// </summary>
		/// <exception cref="System.NotImplementedException">Not implemented</exception>
		public void Unregister() {
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Loads this instance.
		/// </summary>
		/// <exception cref="System.NotImplementedException">Not implemented</exception>
		public void Load() {
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Unloads this instance.
		/// </summary>
		/// <exception cref="System.NotImplementedException">Not implemented</exception>
		public void Unload() {
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Actions the handle.
		/// </summary>
		/// <param name="Sender">The sender.</param>
		/// <param name="Action">The <see cref="ActionDetectedEventArgs" /> instance containing the event data.</param>
		public void ActionHandle(object Sender, ActionDetectedEventArgs Action) {
		}
	}
}