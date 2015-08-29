using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaMotion.Core.Models.Interfaces {
	/// <summary>
	/// Defines a method to reset allocated resources.
	/// </summary>
	public interface IResetable {
		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. But leave the instance usable (contrary to Dispose)
		/// </summary>
		void Reset();
	}
}
