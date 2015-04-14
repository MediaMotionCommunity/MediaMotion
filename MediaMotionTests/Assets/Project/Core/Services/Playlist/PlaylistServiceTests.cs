using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Playlist;
using NUnit.Framework;
namespace MediaMotion.Core.Services.Playlist.Tests {
	[TestFixture()]
	public class PlaylistServiceTests {
		private PlaylistService PlaylistService;
		private ElementFactory ElementFactory;
		private FileSystemService FileSystemService;
		private String PathToTmp;
		private int FilesCreated;

		[SetUp]
		public void Init() {
			this.ElementFactory = new ElementFactory();
			this.FileSystemService = new FileSystemService(this.ElementFactory);
			this.PlaylistService = new PlaylistService(this.FileSystemService);
			this.PathToTmp = Environment.GetFolderPath(Environment.SpecialFolder.Personal) != string.Empty ? Environment.GetFolderPath(Environment.SpecialFolder.Personal) : Environment.GetFolderPath(Environment.SpecialFolder.System);
			this.PathToTmp = this.PathToTmp + "/UnitTestMediaMotionTMP";
			System.IO.Directory.CreateDirectory(this.PathToTmp);
			Random rnd = new Random();
			this.FilesCreated = rnd.Next(2, 20);
		}
		[Test()]
		public void PlaylistServiceTest() {
			Assert.AreNotEqual(null, this.PlaylistService);
		}

		[Test()]
		public void ConfigureTest() {
			Assert.IsFalse(this.PlaylistService.Configure(null, null));

			IElement folder = this.ElementFactory.CreateFolder("/DOES/NOT/EXIST");
			Assert.IsFalse(this.PlaylistService.Configure(folder, null));

			System.IO.File.Create(this.PathToTmp + "/filePlaylist.test");
			IElement file = this.ElementFactory.CreateFile(this.PathToTmp + "/filePlaylist.test");
			Assert.IsFalse(this.PlaylistService.Configure(file, null));

			folder = this.ElementFactory.CreateFolder(this.PathToTmp);
			Assert.IsFalse(this.PlaylistService.Configure(folder, null));
		}

		[Test()]
		public void CurrentTest() {
			for (int i = 0; i < this.FilesCreated; i++) {
				System.IO.File.Create(this.PathToTmp + "/filePlaylist" + i + ".test");
			}
			IElement file = this.ElementFactory.CreateFile(this.PathToTmp + "/filePlaylist0.test");
			string[] filters = new string[1];
			filters[0] = "test";

			// should handle invalid filter
			Assert.IsFalse(this.PlaylistService.Configure(file, filters));

			// Should handle Configure not called
			Assert.Throws<InvalidOperationException>(delegate { this.PlaylistService.Current(); });

			filters[0] = ".test";
			this.PlaylistService.Configure(file, filters);
			Assert.AreEqual(file.GetPath(), this.PlaylistService.Current().GetPath());

			this.PlaylistService.Next();
			Assert.AreNotEqual(file.GetPath(), this.PlaylistService.Current().GetPath());
		}

		[Test()]
		public void PreviousTest() {

		}

		[Test()]
		public void NextTest() {

		}

		[TearDown]
		public void OnEnd() {
			System.GC.Collect();                                // Needed to delete directory with success.
			System.GC.WaitForPendingFinalizers();               // If not, direcory is still in used by the process and delete fails.
			System.IO.Directory.Delete(this.PathToTmp, true);
		}
	}
}
