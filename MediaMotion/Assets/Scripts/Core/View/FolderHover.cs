using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Folder Hover
/// </summary>
public class FolderHover : MonoBehaviour {
	/// <summary>
	/// The animation height
	/// </summary>
	public float AnimationHeight = 0.3f;

	/// <summary>
	/// The animation speed
	/// </summary>
	public float AnimationSpeed = 1.6f;

	/// <summary>
	/// The animation scale
	/// </summary>
	public float AnimationScale = 0.09f;

	/// <summary>
	/// The hovering
	/// </summary>
	private bool Hovering = false;

	/// <summary>
	/// The default position
	/// </summary>
	private float DefaultPosition;

	/// <summary>
	/// The maximum position
	/// </summary>
	private float MaxPosition;

	/// <summary>
	/// The animate
	/// </summary>
	private bool Animate;

	/// <summary>
	/// The default scale
	/// </summary>
	private Vector3 DefaultScale;

	/// <summary>
	/// The scale
	/// </summary>
	private Vector3 Scale;

	/// <summary>
	/// The position
	/// </summary>
	private Vector3 Position;

	/// <summary>
	/// The temporary
	/// </summary>
	private bool Tmp = true;

	/// <summary>
	/// Starts this instance.
	/// </summary>
	public void Start() {
		this.Position = this.transform.position;
		this.DefaultPosition = transform.position.z;
		this.MaxPosition = this.DefaultPosition + this.AnimationHeight;
		this.DefaultScale = transform.localScale;
		this.Scale = new Vector3(this.AnimationScale, this.AnimationScale, this.AnimationScale);
		Debug.Log(this.DefaultPosition);
		Debug.Log(this.DefaultScale);
	}

	/// <summary>
	/// Updates this instance.
	/// </summary>
	public void Update() {
		// to delete
		this.Hover(this.Tmp);
		this.Tmp = !this.Tmp;

		if (!this.Animate) {
			return;
		}
		if (this.Hovering) {
			this.HoverAnimation();
		} else if (!this.Hovering) {
			this.UnhoverAnimation();
		}
	}

	/// <summary>
	/// Hovers the specified state.
	/// </summary>
	/// <param name="State">if set to <c>true</c> [state].</param>
	public void Hover(bool State) {
		if (!this.Animate && this.Hovering != State) {
			this.Hovering = State;
			this.Animate = true;
		}
	}

	/// <summary>
	/// Hovers the animation.
	/// </summary>
	private void HoverAnimation() {
		if (this.transform.position.z <= this.MaxPosition) {
			this.transform.Translate(Vector3.back * Time.deltaTime * this.AnimationSpeed);
			this.transform.localScale += Time.deltaTime * this.Scale;
		} else {
			Vector3 NewPosition = this.transform.position;
			NewPosition.z = this.DefaultPosition + this.AnimationHeight;
			this.transform.position = NewPosition;
			this.Animate = false;
			Debug.Log(this.transform.position);
		}
	}

	/// <summary>
	/// Un hovers the animation.
	/// </summary>
	private void UnhoverAnimation() {
		if (this.transform.position.z > this.DefaultPosition) {
			this.transform.Translate(Vector3.forward * Time.deltaTime * this.AnimationSpeed);
			this.transform.localScale -= Time.deltaTime * this.Scale;
		} else {
			this.transform.position = this.Position;
			this.transform.localScale = this.DefaultScale;
			this.Animate = false;
			Debug.Log(transform.position);
		}
	}
}
