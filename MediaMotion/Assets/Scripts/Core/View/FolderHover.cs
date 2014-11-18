using UnityEngine;
using System.Collections;
using System;

public class FolderHover : MonoBehaviour {

	public float animationHeight = 0.3f;
	public float animationSpeed = 1.6f;
	public float animationScale = 0.09f;

	private bool hovering = false;
	private float defaultPosition;
	private float maxPosition;
	private bool animate;
	private Vector3 defaultScale;
	private Vector3 scale;
	private Vector3 position;
	
	// Use this for initialization
	void Start() {
		position = transform.position;
		defaultPosition = transform.position.z;
		maxPosition = defaultPosition + animationHeight;
		defaultScale = transform.localScale;
		scale = new Vector3(animationScale, animationScale, animationScale);
		Debug.Log(defaultPosition);
		Debug.Log(defaultScale);
	}

	private bool tmp = true;

	// Update is called once per frame
	void Update() {
		// to delete
		hover(tmp);
		tmp = !tmp;

		if (!animate) {
			return ;
		}
		if (hovering) {
			hoverAnimation();
		}
		else if (!hovering) {
			unhoverAnimation();
		}
	}

	private void hoverAnimation() {
		if (transform.position.z <= maxPosition) {
			transform.Translate(Vector3.back * Time.deltaTime * animationSpeed);
			transform.localScale += Time.deltaTime * scale;
		}
		else {
			Vector3 newPosition = transform.position;
			newPosition.z = defaultPosition + animationHeight;
			transform.position = newPosition;
			animate = false;
			Debug.Log(transform.position);
		}
	}
	private void unhoverAnimation() {
		if (transform.position.z > defaultPosition) {
			transform.Translate(Vector3.forward * Time.deltaTime * animationSpeed);
			transform.localScale -= Time.deltaTime * scale;
		}
		else {
			transform.position = position;
			transform.localScale = defaultScale;
			animate = false;
			Debug.Log(transform.position);
		}
	}

	public void hover(bool state) {
		if (!animate && hovering != state) {
			hovering = state;
			animate = true;
		}
	}
}
