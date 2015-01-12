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

		public IFileSystem fileSystem;

		[SetUp]
		public void Init() {
			fileSystem = FileSystemService.GetInstance();
		}

		[Test()]
		public void FileSystemTest() {
			Assert.AreEqual(null, fileSystem);
		}

		[Test()]
		public void GetHomeDirectoryTest() {
			String homePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			IFolder homeFolder = fileSystem.GetHomeDirectory();
			fileSystem.ChangeDirectory(homeFolder);
			Assert.AreEqual(homeFolder.GetName(), fileSystem.CurrentFolder.GetName());
			Assert.AreEqual(homeFolder.GetPath(), fileSystem.CurrentFolder.GetPath());
			Assert.AreEqual(homePath, fileSystem.CurrentFolder.GetPath());
			Assert.AreEqual(homePath, homeFolder.GetPath());
		}

		[Test()]
		public void ChangeDirectoryTest() {

			// Testing valid values
			IFolder newDirectory = null;
			IFolder initialDirectory = fileSystem.GetHomeDirectory();
			fileSystem.ChangeDirectory(fileSystem.GetHomeDirectory());
			Assert.AreEqual(ElementType.Folder, initialDirectory.GetElementType());
			List<IElement> directoryContent = fileSystem.GetDirectoryContent(fileSystem.CurrentFolder);
			foreach (IElement element in directoryContent) {
				if (element.GetElementType().Equals(ElementType.Folder)) {
					newDirectory = element as IFolder;
					fileSystem.ChangeDirectory(element as IFolder);
					break;
				}
			}
			Assert.AreNotEqual(null, newDirectory);
			Assert.AreNotEqual(newDirectory.GetPath(), initialDirectory.GetPath());
			Assert.AreEqual(newDirectory.GetPath(), fileSystem.CurrentFolder.GetPath());

			// Testing invalid value
			IFolder invalidFolder = new Folder("/invalid/path", "toto");
			fileSystem.ChangeDirectory(invalidFolder); // Should return true/false ?
			Assert.AreNotEqual(invalidFolder.GetPath(), fileSystem.CurrentFolder.GetPath());
			Assert.AreEqual(newDirectory.GetPath(), fileSystem.CurrentFolder.GetPath());


			// Add test with invalid folder like a IFile.
		}

		[Test()]
		public void GetDirectoryContentTest() {
			Assert.Fail();
		}

		[Test()]
		public void CopyTest() {
			Assert.Fail();
		}

		[Test()]
		public void MoveTest() {
			Assert.Fail();
		}

		[Test()]
		public void RemoveTest() {
			Assert.Fail();
		}
	}
}
