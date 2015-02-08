using MediaMotion.Core.Models.Scripts;
using UnityEngine;

namespace MediaMotion.Core.Controllers.Loader {
	/// <summary>
	/// Logo bound
	/// </summary>
	public class LogoBound : BaseUnityScript<LogoBound> {
		/// <summary>
		/// The direction
		/// </summary>
		private Vector3 Direction;

		/// <summary>
		/// The factor
		/// </summary>
		private float Factor;

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Init() {
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
