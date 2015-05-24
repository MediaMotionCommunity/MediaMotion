using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist.Models.Abstracts {
	/// <summary>
	/// Slideshow tile abstract
	/// </summary>
	/// <typeparam name="Module">The type of the module.</typeparam>
	/// <typeparam name="Child">The type of the child.</typeparam>
	public abstract class ASlideshowTile<Module, Child> : AScript<Module, Child>, ISlideshowTile
		where Module : class, IModule
		where Child : ASlideshowTile<Module, Child> {
		/// <summary>
		/// The texture
		/// </summary>
		protected Texture2D texture2D;

		/// <summary>
		/// The file
		/// </summary>
		protected IFile file;

		/// <summary>
		/// The cumulative rotation
		/// </summary>
		protected float cumulativeRotation = 0.0f;

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
		/// Rotate the tile
		/// </summary>
		/// <param name="angle">The angle.</param>
		public virtual void Rotate(float angle) {
			this.cumulativeRotation += angle;
			this.gameObject.transform.Rotate(new Vector3(0.0f, angle, 0.0f), Space.Self);
		}

		/// <summary>
		/// Loads the file.
		/// </summary>
		/// <param name="file">The file.</param>
		public void LoadFile(IFile file) {
			if (file != null) {
				if (this.file != null && this.file.GetPath().CompareTo(file.GetPath()) == 0) {
					return;
				}
				this.file = file;
				this.ClearAll();
			}
		}

		/// <summary>
		/// Clears all.
		/// </summary>
		protected virtual void ClearAll() {
			this.Texture2DApplied = false;
			this.Rotate(-this.cumulativeRotation);
			this.CleanTexture2D();
		}

		/// <summary>
		/// Texture2D loading process.
		/// </summary>
		protected virtual void Texture2DLoadingProcess() {
			if (!this.Texture2DApplied) {
				if (this.IsTexture2DReady()) {
					this.ScaleTexture2D(this.texture2D);
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
		/// Scales the texture2 d.
		/// </summary>
		/// <param name="texture2D">The texture2 d.</param>
		/// <param name="maxWidth">The maximum width.</param>
		/// <param name="maxHeight">The maximum height.</param>
		protected virtual void ScaleTexture2D(Texture2D texture2D, float maxWidth = 1.3f, float maxHeight = 1.0f) {
			if (texture2D != null && this.gameObject.GetComponent<Renderer>() != null) {
				float coeff = (float)texture2D.height / (float)texture2D.width;

				if (maxWidth * coeff <= maxHeight) {
					this.gameObject.transform.localScale = new Vector3(maxWidth, 1.0f, maxWidth * coeff);
				} else {
					coeff = (float)texture2D.width / (float)texture2D.height;
					this.gameObject.transform.localScale = new Vector3(maxHeight * coeff, 1.0f, maxHeight);
				}
			}
		}

		/// <summary>
		/// Apply the texture 2D.
		/// </summary>
		/// <param name="maxWidth">The maximum width.</param>
		/// <param name="maxHeight">The maximum height.</param>
		protected virtual void ApplyTexture2D() {
			if (this.texture2D != null && this.gameObject.GetComponent<Renderer>() != null) {
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
