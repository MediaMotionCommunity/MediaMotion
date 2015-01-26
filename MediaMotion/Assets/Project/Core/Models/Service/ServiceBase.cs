using MediaMotion.Core.Models.Core;

namespace MediaMotion.Core.Models.Service {
	/// <summary>
	/// Abstract Service
	/// </summary>
	public abstract class ServiceBase {
		/// <summary>
		/// The core
		/// </summary>
		protected ICore Core;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceBase"/> class.
		/// </summary>
		/// <param name="Core">The core.</param>
		public ServiceBase(ICore Core) {
			this.Core = Core;
		}
	}
}
