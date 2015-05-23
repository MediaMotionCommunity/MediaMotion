using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Abstracts {
	public abstract class ASlideshowTile<Module, Child> : AScript<Module, Child>, ISlideshowTile
		where Module : class, IModule
		where Child : ASlideshowTile<Module, Child> {
		/// <summary>
		/// The texture
		/// </summary>
		protected Texture2D texture2D;

		/// <summary>
		/// Gets or sets the file.
		/// </summary>
		/// <value>
		/// The file.
		/// </value>
		public IFile File { get; set; }

		/// <summary>
		/// Gets a value indicating whether [texture2 d applied].
		/// </summary>
		/// <value>
		///   <c>true</c> if [texture2 d applied]; otherwise, <c>false</c>.
		/// </value>
		public bool Texture2DApplied { get; protected set; }

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public virtual void Update() {
			this.Texture2DLoadingProcess();
		}

		/// <summary>
		/// Texture2D loading process.
		/// </summary>
		protected virtual void Texture2DLoadingProcess() {
			if (!this.Texture2DApplied) {
				if (this.IsTexture2DReady()) {
					this.ApplyTexture2D();
					this.CleanTexture2D();
				} else {
					this.LoadTexture2D();
				}
			}
		}

		/// <summary>
		/// Is texture 2D ready.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the texture 2D is ready, <c>false</c> otherwise
		/// </returns>
		protected virtual bool IsTexture2DReady() {
			return (this.texture2D != null);
		}

		/// <summary>
		/// Apply the texture 2D.
		/// </summary>
		/// <param name="maxWidth">The maximum width.</param>
		/// <param name="maxHeight">The maximum height.</param>
		protected virtual void ApplyTexture2D(float maxWidth = 1.3f, float maxHeight = 1.0f) {
			if (this.texture2D != null && this.gameObject.GetComponent<Renderer>() != null) {
				float coeff = (float)this.texture2D.height / (float)this.texture2D.width;

				if (maxWidth * coeff <= maxHeight) {
					this.gameObject.transform.localScale = new Vector3(maxWidth, 1.0f, maxWidth * coeff);
				} else {
					coeff = (float)this.texture2D.width / (float)this.texture2D.height;
					this.gameObject.transform.localScale = new Vector3(maxHeight * coeff, 1.0f, maxHeight);
				}
				this.texture2D.wrapMode = TextureWrapMode.Clamp;
				this.gameObject.GetComponent<Renderer>().material.mainTexture = this.texture2D;
				this.Texture2DApplied = true;
			}
		}

		/// <summary>
		/// Clean the texture 2D.
		/// </summary>
		protected virtual void CleanTexture2D() {
			this.texture2D = null;
		}

		/// <summary>
		/// Loads the texture2 d.
		/// </summary>
		protected abstract void LoadTexture2D();
	}
}
