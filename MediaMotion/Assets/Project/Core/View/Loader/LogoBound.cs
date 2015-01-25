﻿using UnityEngine;

namespace MediaMotion.Core.View.Loader {
	/// <summary>
	/// Logo bound
	/// </summary>
	class LogoBound : MonoBehaviour {
		/// <summary>
		/// The direction
		/// </summary>
		private Vector3 Direction;

		/// <summary>
		/// The factor
		/// </summary>
		private float Factor;

		/// <summary>
		/// Initializes a new instance of the <see cref="LogoBound"/> class.
		/// </summary>
		public void Start() {
			this.Factor = 0.1f;
			float InitialAngle = Random.Range(0, 359) * Mathf.Deg2Rad;

			this.Direction = new Vector3(Mathf.Cos(InitialAngle) * this.Factor, Mathf.Sin(InitialAngle) * this.Factor, 0.0f);
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.transform.Translate(this.Direction, Space.World);
		}
	}
}