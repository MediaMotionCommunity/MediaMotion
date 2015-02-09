using System;
using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using NUnit.Framework;

namespace MediaMotion.Core.Services.FileSystem.Tests {
	[TestFixture()]
	public class FileSystemTests {

		public IFileSystemService FileSystemService;

		[SetUp]
		public void Init() {
			this.FileSystemService = new FileSystemService(new FolderFactory(), new FileFactory());
		}

		[Test()]
		public void FileSystemTest() {
			Assert.AreNotEqual(null, this.FileSystemService);
		}

		[Test()]
		public void GetHomeDirectoryTest() {
			string HomePath = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) != string.Empty) ? Environment.GetFolderPath(Environment.SpecialFolder.Personal) : (Environment.GetFolderPath(Environment.SpecialFolder.System));
			string HomeFolder = this.FileSystemService.GetHome();

			this.FileSystemService.ChangeDirectory(HomeFolder);
			Assert.AreEqual(HomeFolder, this.FileSystemService.CurrentFolder.GetPath());
			Assert.AreEqual(HomePath, this.FileSystemService.CurrentFolder.GetPath());
			Assert.AreEqual(HomePath, HomeFolder);
		}

		[Test()]
		public void ChangeDirectoryTest() {
			// Testing valid values
			IFolder newDirectory = null;
			IFolder initialDirectory = null;

			this.FileSystemService.ChangeDirectory(this.FileSystemService.GetHome());
			initialDirectory = this.FileSystemService.CurrentFolder;
			Assert.AreEqual(ElementType.Folder, initialDirectory.GetElementType());

			List<IElement> directoryContent = this.FileSystemService.GetContent(this.FileSystemService.CurrentFolder.GetPath());
			foreach (IElement element in directoryContent) {
				if (element.GetElementType().Equals(ElementType.Folder)) {
					newDirectory = element as IFolder;
					this.FileSystemService.ChangeDirectory(element.GetPath());
					break;
				}
			}
			Assert.AreNotEqual(null, newDirectory);
			Assert.AreNotEqual(newDirectory.GetPath(), initialDirectory.GetPath());
			Assert.AreEqual(newDirectory.GetPath(), this.FileSystemService.CurrentFolder.GetPath());

			// Testing invalid value
			// IFolder invalidFolder = new Folder("/invalid/path", "toto");
			// FileSystem.ChangeDirectory(invalidFolder); // Should return true/false ?
			// Assert.AreNotEqual(invalidFolder.GetPath(), FileSystem.CurrentFolder.GetPath());
			// Assert.AreEqual(newDirectory.GetPath(), FileSystem.CurrentFolder.GetPath());


			// Add test with invalid folder like a IFile.
		}

		[Test()]
		public void GetDirectoryContentTest() {
			// Assert.Fail();
		}

		[Test()]
		public void CopyTest() {
			// Assert.Fail();
		}

		[Test()]
		public void MoveTest() {
			// Assert.Fail();
		}

		[Test()]
		public void RemoveTest() {
			// Assert.Fail();
		}
	}
}
