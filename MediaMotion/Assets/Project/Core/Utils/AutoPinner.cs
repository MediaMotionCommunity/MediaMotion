using System;
using System.Runtime.InteropServices;

namespace MediaMotion.Core.Utils {
	/// <summary>
	/// Wrap a C# object to be accessible as C pointer
	/// </summary>
	public class AutoPinner : IDisposable {
		/// <summary>
		/// The object
		/// </summary>
		public object Obj { get; private set; }

		/// <summary>
		/// Gets the pinned object pointer.
		/// </summary>
		/// <value>
		/// The PTR.
		/// </value>
		public IntPtr Ptr {
			get {
				return (this.objHandle.AddrOfPinnedObject());
			}
		}

		/// <summary>
		/// The object handle
		/// </summary>
		protected GCHandle objHandle;

		/// <summary>
		/// Initializes a new instance of the <see cref="AutoPinner"/> class.
		/// </summary>
		/// <param name="obj">The object.</param>
		public AutoPinner(object obj) {
			this.Obj = obj;
			this.objHandle = GCHandle.Alloc(this.Obj, GCHandleType.Pinned);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			this.objHandle.Free();
		}
	}
}
