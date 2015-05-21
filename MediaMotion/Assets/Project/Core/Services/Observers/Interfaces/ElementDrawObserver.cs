using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Observers.Interfaces {
	/// <summary>
	/// Element Draw Observer
	/// </summary>
	public interface IElementDrawObserver {
		/// <summary>
		/// Draws this instance.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		/// The gameObject drew
		/// </returns>
		GameObject Draw(IElement element);
	}
}
