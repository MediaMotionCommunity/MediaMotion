using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.Observers.Interfaces;
using MediaMotion.Modules.PDFViewer.Observers;
using MediaMotion.Modules.PDFViewer.Services.MuPDF;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class PDFViewerModule : AModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="PDFViewerModule"/> class.
		/// </summary>
		public PDFViewerModule() {
			this.Name = "PDF Viewer";
			this.Scene = "PDFViewer";
			this.Description = "Display your PDF in a wonderfull slideshow";
			this.SupportedExtensions = new string[] { ".pdf", ".xps" };
			this.SupportedAction = new ActionType[] {
				ActionType.Rotate,
				ActionType.Right,
				ActionType.Left
			};
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		/// <param name="container">The container</param>
		public override void Configure(IContainer container) {
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

			containerBuilderService.Register<MuPDFService>().As<IMuPDFService>().SingleInstance = true;
			containerBuilderService.Register<ElementDrawObserver>().As<IElementDrawObserver>().SingleInstance = true;
			containerBuilderService.Register<ElementFactoryObserver>().As<IElementFactoryObserver>().SingleInstance = true;
			return (containerBuilderService.Build(container));
		}
	}
}
