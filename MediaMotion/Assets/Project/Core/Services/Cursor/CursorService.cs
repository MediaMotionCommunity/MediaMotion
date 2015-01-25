using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaMotion.Core.Models.Wrapper.Events;
using UnityEngine;

namespace MediaMotion.Core.Services.Cursor {
	/// <summary>
	/// Cursor service
	/// </summary>
	class CursorService {
		/// <summary>
		/// The instance
		/// </summary>
		private static readonly CursorService Instance = new CursorService();

		/// <summary>
		/// Prevents a default instance of the <see cref="CursorService"/> class from being created.
		/// </summary>
		private CursorService() {
			this.Position = new Vector3();
		}
		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>
		/// The position.
		/// </value>
		public Vector3 Position { get; set; }

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <returns></returns>
		public static CursorService GetInstance() {
			return (CursorService.Instance);
		}
	}
}
