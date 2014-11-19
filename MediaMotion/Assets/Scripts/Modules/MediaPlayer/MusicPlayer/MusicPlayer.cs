using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;

namespace MediaMotion.Modules.MediaPlayer.MusicPlayer {
	public class MusicPlayer : AMediaPlayer, IMusicPlayer {
		override public void Play() {
			base.Play();
		}

		override public void Pause() {
			base.Pause();
		}

		override public void Stop() {
			base.Stop();
		}

		public void Register() {
			throw new System.NotImplementedException();
		}

		public void Unregister() {
			throw new System.NotImplementedException();
		}

		public void Load() {
			throw new System.NotImplementedException();
		}

		public void Unload() {
			throw new System.NotImplementedException();
		}

        public void ActionHandle(object Sender, ActionDetectedEventArgs Action) {
        }
	}
}