namespace MediaMotion.Modules.MediaPlayer.MusicPlayer {
	public class MusicPlayer : AMediaPlayer, IMusicPlayer {
		//
		// Init
		//
		void init() {

		}

		//
		// Lecture
		//
		override public void Play() {
			base.Play();
		}

		override public void Pause() {
			base.Pause();
		}

		override public void Stop() {
			base.Stop();
		}
	}
}