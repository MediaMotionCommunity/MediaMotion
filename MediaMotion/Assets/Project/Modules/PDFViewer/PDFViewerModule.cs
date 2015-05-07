using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Modules.PDFViewer.Observers;

namespace MediaMotion.Modules.PDFViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class PDFViewerModule : AModule {
		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure(IContainer container) {
			this.Name = "PDF Viewer";
			this.Scene = "PDFViewer";
			this.Description = "Read your PDFs documents";
			this.Container = this.BuildContainer(container);
		}

		/// <summary>
		/// Builds the container.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <returns>
		///   The container
		/// </returns>
		private IContainer BuildContainer(IContainer container) {
			IContainerBuilderService containerBuilderService = container.Get<IContainerBuilderService>();

			containerBuilderService.Register<ElementFactoryObserver>().As<IElementFactoryObserver>().SingleInstance = true;

			return (containerBuilderService.Build(container));
		}
	}
}
