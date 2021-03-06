﻿using MediaMotion.Core.Models.Abstracts;
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
	public abstract class ASlideshowTile<Module, Child> : ASlideshowEnvironment<Module, Child>, ISlideshowTile
		where Module : class, IModule
		where Child : ASlideshowTile<Module, Child> {
		/// <summary>
		/// The origin angle
		/// </summary>
		protected Quaternion? originAngle;

		/// <summary>
		/// The origin scale
		/// </summary>
		protected Vector3? originScale;

		/// <summary>
		/// The ratio scale
		/// </summary>
		protected float ratioScale;

		/// <summary>
		/// The texture
		/// </summary>
		protected Texture2D texture2D;

		/// <summary>
		/// The element
		/// </summary>
		protected object element;

		/// <summary>
		/// The is texture applied
		/// </summary>
		protected bool isTextureApplied;

		/// <summary>
		/// The opposite x scale
		/// </summary>
		protected bool oppositeXScale = false;

		/// <summary>
		/// The opposite y scale
		/// </summary>
		protected bool oppositeYScale = false;

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
		/// Leaves the fullscreen.
		/// </summary>
		public override void LeaveFullscreen() {
			this.ClearZoom();
			this.ClearRotation();
		}

		/// <summary>
		/// Called when [destroy].
		/// </summary>
		public virtual void OnDestroy() {
			this.ClearAll();
		}

		/// <summary>
		/// Zooms the file
		/// </summary>
		/// <param name="multiplier">The multiplier.</param>
		public virtual void Zoom(float multiplier) {
			if (this.originScale.HasValue) {
				this.ratioScale = Mathf.Min(Mathf.Max(this.ratioScale * ((multiplier / 25.0f) + 1), 0.2f), 5.0f);
				this.gameObject.transform.localScale = this.originScale.Value * this.ratioScale;
			}
		}

		/// <summary>
		/// Clears the zoom.
		/// </summary>
		public void ClearZoom() {
			if (this.originScale.HasValue) {
				this.gameObject.transform.localScale = this.originScale.Value;
			}
		}

		/// <summary>
		/// Rotate the tile
		/// </summary>
		/// <param name="angle">The angle.</param>
		public virtual void Rotate(float angle) {
			this.gameObject.transform.Rotate(new Vector3(0.0f, angle * 3.0f, 0.0f), Space.Self);
		}

		/// <summary>
		/// Clears the rotation.
		/// </summary>
		public void ClearRotation() {
			if (this.originAngle.HasValue) {
				this.gameObject.transform.localRotation = this.originAngle.Value;
			}
		}

		/// <summary>
		/// Loads the element.
		/// </summary>
		/// <param name="element">The element.</param>
		public void Load(object element) {
			if (element != null) {
				this.element = element;
				this.ClearAll();
			}
		}

		/// <summary>
		/// Clears all.
		/// </summary>
		protected virtual void ClearAll() {
			this.Texture2DApplied = false;
			this.ClearRotation();
			this.ClearZoom();
			this.CleanTexture2D();
		}

		/// <summary>
		/// Texture2D loading process.
		/// </summary>
		protected virtual void Texture2DLoadingProcess() {
			if (!this.Texture2DApplied) {
				if (this.IsTexture2DReady()) {
					this.ScaleTexture2D();
					this.ApplyTexture2D();
					this.SaveElementValues();
					this.ResetElement();
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
		/// <param name="maxWidth">The maximum width.</param>
		/// <param name="maxHeight">The maximum height.</param>
		protected virtual void ScaleTexture2D(float maxWidth = 1.8f, float maxHeight = 1.2f) {
			if (this.texture2D != null && this.gameObject.GetComponent<Renderer>() != null) {
				float coeff = (float)this.texture2D.height / (float)this.texture2D.width;

				if (maxWidth * coeff <= maxHeight) {
					this.gameObject.transform.localScale = new Vector3(maxWidth * ((this.oppositeXScale) ? (-1) : (1)), 1.0f, maxWidth * coeff * ((this.oppositeYScale) ? (-1) : (1)));
				} else {
					coeff = (float)this.texture2D.width / (float)this.texture2D.height;
					this.gameObject.transform.localScale = new Vector3(maxHeight * coeff * ((this.oppositeXScale) ? (-1) : (1)), 1.0f, maxHeight * ((this.oppositeYScale) ? (-1) : (1)));
				}
			}
		}

		/// <summary>
		/// Apply the texture 2D.
		/// </summary>
		protected virtual void ApplyTexture2D() {
			if (this.texture2D != null && this.gameObject.GetComponent<Renderer>() != null) {
				this.texture2D.wrapMode = TextureWrapMode.Clamp;
				this.gameObject.GetComponent<Renderer>().material.mainTexture = this.texture2D;
				this.Texture2DApplied = true;
			}
		}

		/// <summary>
		/// Saves the element values.
		/// </summary>
		protected virtual void SaveElementValues() {
			this.originAngle = this.gameObject.transform.localRotation;
			this.originScale = this.gameObject.transform.localScale;
		}

		/// <summary>
		/// Clean the texture 2D.
		/// </summary>
		protected virtual void CleanTexture2D() {
			if (this.texture2D != null) {
				Texture2D.Destroy(this.texture2D);
			}
			this.texture2D = null;
		}

		/// <summary>
		/// Resets the element.
		/// </summary>
		protected virtual void ResetElement() {
			if (this.element is IResetable) {
				((IResetable)this.element).Reset();
			}
		}

		/// <summary>
		/// Loads the texture2 d.
		/// </summary>
		protected abstract void LoadTexture2D();
	}
}
