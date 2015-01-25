using System.Collections.Generic;
using UnityEngine;

namespace MediaMotion.Core.View.Loader {
	/// <summary>
	/// Loading text
	/// </summary>
	class LoadingText : MonoBehaviour {
		/// <summary>
		/// The cumulative delta time
		/// </summary>
		private float CumulativeDeltaTime;

		/// <summary>
		/// The messages
		/// </summary>
		private List<string> Messages;

		/// <summary>
		/// The current
		/// </summary>
		private List<string>.Enumerator CurrentMessage;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadingText"/> class.
		/// </summary>
		public LoadingText() {
			this.CumulativeDeltaTime = 0.0f;
			this.Messages = new List<string>();

			this.Messages.Add("Loading.");
			this.Messages.Add("Loading..");
			this.Messages.Add("Loading...");
			this.Messages.Add("Loading ...");
			this.Messages.Add("Loading  ...");
			this.Messages.Add("Loading   ...");
			this.Messages.Add("Loading    ..");
			this.Messages.Add("Loading     .");

			this.CurrentMessage = this.Messages.GetEnumerator();
			this.CurrentMessage.MoveNext();
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.CumulativeDeltaTime += Time.deltaTime;

			if (this.CumulativeDeltaTime > 0.2f) {
				this.CumulativeDeltaTime = 0;
				this.gameObject.guiText.text = this.CurrentMessage.Current;

				if (!this.CurrentMessage.MoveNext()) {
					this.CurrentMessage = this.Messages.GetEnumerator();
					this.CurrentMessage.MoveNext();
				}
			}
		}
	}
}
