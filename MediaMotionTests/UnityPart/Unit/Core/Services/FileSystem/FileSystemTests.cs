using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Models.FileManager.Enums;
using NUnit.Framework;


namespace MediaMotion.Core.Services.FileSystem.Tests {
	[TestFixture()]
	public class FileSystemTests {

		public IFileSystem FileSystem;

		[SetUp]
		public void Init() {
			FileSystem = MediaMotionCore.Core.GetService("FileSystem") as IFileSystem;
		}

		[Test()]
		public void FileSystemTest() {
			Assert.AreNotEqual(null, FileSystem);
		}

		[Test()]
		public void GetHomeDirectoryTest() {
			String HomePath = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) != string.Empty) ? (Environment.GetFolderPath(Environment.SpecialFolder.Personal)) : (Environment.GetFolderPath(Environment.SpecialFolder.System));
			IFolder HomeFolder = FileSystem.GetHomeDirectory();

			FileSystem.ChangeDirectory(HomeFolder);
			Assert.AreEqual(HomeFolder.GetName(), FileSystem.CurrentFolder.GetName());
			Assert.AreEqual(HomeFolder.GetPath(), FileSystem.CurrentFolder.GetPath());
			Assert.AreEqual(HomePath, FileSystem.CurrentFolder.GetPath());
			Assert.AreEqual(HomePath, HomeFolder.GetPath());
		}

		[Test()]
		public void ChangeDirectoryTest() {

			// Testing valid values
			IFolder newDirectory = null;
			IFolder initialDirectory = FileSystem.GetHomeDirectory();
			FileSystem.ChangeDirectory(FileSystem.GetHomeDirectory());
			Assert.AreEqual(ElementType.Folder, initialDirectory.GetElementType());
			List<IElement> directoryContent = FileSystem.GetDirectoryContent(FileSystem.CurrentFolder);
			foreach (IElement element in directoryContent) {
				if (element.GetElementType().Equals(ElementType.Folder)) {
					newDirectory = element as IFolder;
					FileSystem.ChangeDirectory(element as IFolder);
					break;
				}
			}
			Assert.AreNotEqual(null, newDirectory);
			Assert.AreNotEqual(newDirectory.GetPath(), initialDirectory.GetPath());
			Assert.AreEqual(newDirectory.GetPath(), FileSystem.CurrentFolder.GetPath());

			// Testing invalid value
			//IFolder invalidFolder = new Folder("/invalid/path", "toto");
			//FileSystem.ChangeDirectory(invalidFolder); // Should return true/false ?
			//Assert.AreNotEqual(invalidFolder.GetPath(), FileSystem.CurrentFolder.GetPath());
			//Assert.AreEqual(newDirectory.GetPath(), FileSystem.CurrentFolder.GetPath());


			// Add test with invalid folder like a IFile.
		}

		[Test()]
		public void GetDirectoryContentTest() {
			//Assert.Fail();
		}

		[Test()]
		public void CopyTest() {
			//Assert.Fail();
		}

		[Test()]
		public void MoveTest() {
			//Assert.Fail();
		}

		[Test()]
		public void RemoveTest() {
			//Assert.Fail();
		}
	}
}
