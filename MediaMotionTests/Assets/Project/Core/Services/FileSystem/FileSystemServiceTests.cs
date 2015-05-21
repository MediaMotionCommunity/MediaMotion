using System;
using System.Collections.Generic;
using System.IO;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using NUnit.Framework;
namespace MediaMotion.Core.Services.FileSystem.Tests {
	[TestFixture]
	public class FileSystemServiceTests {
		public IFileSystemService FileSystemService;
		public IElementFactory ElementFactory;
		public int FilesCreated;
		public string PathToTmp;

		[SetUp]
		public void Init() {
			this.FileSystemService = new FileSystemService(new ElementFactory(null));
			this.PathToTmp = Environment.GetFolderPath(Environment.SpecialFolder.Personal) != string.Empty ? Environment.GetFolderPath(Environment.SpecialFolder.Personal) : Environment.GetFolderPath(Environment.SpecialFolder.System);
			this.PathToTmp = this.PathToTmp + "/UnitTestMediaMotionTMP";
			Random rnd = new Random();
			this.FilesCreated = rnd.Next(1, 20);
			System.IO.Directory.CreateDirectory(this.PathToTmp);
			this.ElementFactory = new ElementFactory(null);
		}

		[Test]
		public void FileSystemTest() {
			Assert.AreNotEqual(null, this.FileSystemService);
		}

		[Test]
		public void GetHomeDirectoryTest() {
			string HomePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) != string.Empty ? Environment.GetFolderPath(Environment.SpecialFolder.Personal) : Environment.GetFolderPath(Environment.SpecialFolder.System);
			string HomeFolder = this.FileSystemService.GetHome();

			this.FileSystemService.ChangeDirectory(HomeFolder);
			Assert.AreEqual(HomeFolder, this.FileSystemService.CurrentFolder.GetPath());
			Assert.AreEqual(HomePath, this.FileSystemService.CurrentFolder.GetPath());
			Assert.AreEqual(HomePath, HomeFolder);
		}

		[Test]
		public void ChangeDirectoryTest() {
			IFolder newDirectory = null;
			IFolder initialDirectory = null;

			bool result1 = this.FileSystemService.ChangeDirectory(this.FileSystemService.GetHome());
			Assert.AreEqual(true, result1);
			initialDirectory = this.FileSystemService.CurrentFolder;
			Assert.AreEqual(ElementType.Folder, initialDirectory.GetElementType());

			IElement[] directoryContent = this.FileSystemService.GetFolderElements(this.FileSystemService.CurrentFolder.GetPath());
			foreach (IElement element in directoryContent) {
				if (element.GetElementType().Equals(ElementType.Folder)) {
					newDirectory = element as IFolder;
					bool result2 = this.FileSystemService.ChangeDirectory(element.GetPath());
					Assert.AreEqual(true, result2);
					break;
				}
			}
			Assert.AreNotEqual(null, newDirectory);
			Assert.AreNotEqual(newDirectory.GetPath(), initialDirectory.GetPath());
			Assert.AreEqual(newDirectory.GetPath(), this.FileSystemService.CurrentFolder.GetPath());

			string InvalidPath = "/invalid/path";
			bool result3 = this.FileSystemService.ChangeDirectory(InvalidPath);
			Assert.AreEqual(false, result3);
			Assert.AreNotEqual(InvalidPath, this.FileSystemService.CurrentFolder.GetPath());
			Assert.AreEqual(newDirectory.GetPath(), this.FileSystemService.CurrentFolder.GetPath());

			bool result4 = this.FileSystemService.ChangeDirectory();
			Assert.AreEqual(true, result4);
			Assert.AreEqual(this.FileSystemService.CurrentFolder.GetPath(), this.FileSystemService.GetHome());
		}

		[Test]
		public void GetContentTest() {
			IElement[] results = this.FileSystemService.GetFolderElements();
			Assert.AreNotEqual(0, results.Length, "should find some file (current folder)");

			results = this.FileSystemService.GetFolderElements(this.FileSystemService.GetHome());
			Assert.AreNotEqual(0, results.Length, "should find somes files (home folder)");

			for (int i = 0; i < this.FilesCreated; ++i) {
				System.IO.File.Create(this.PathToTmp + "/file" + i + ".test");
			}

			results = this.FileSystemService.GetFolderElements(this.PathToTmp);

			Assert.AreEqual(this.FilesCreated, results.Length);
			Assert.AreEqual("file0.test", results[0].GetName());

			string[] filters = new string[2] { null, ".extensionPlaceholder" };

			IElement[] fileResults = this.FileSystemService.GetFolderElements(this.PathToTmp, filters);
			Assert.IsNull(fileResults, "invalid filter 0");

			filters[0] = "shouldfindnothing";
			fileResults = this.FileSystemService.GetFolderElements(this.PathToTmp, filters);
			Assert.IsNull(fileResults, "invalid filter 1");

			filters[0] = ".test";
			fileResults = this.FileSystemService.GetFolderElements(this.PathToTmp, filters);
			Assert.AreEqual(this.FilesCreated, fileResults.Length, "filter failed 0");

			System.IO.File.Create(this.PathToTmp + "/file.testule");

			fileResults = this.FileSystemService.GetFolderElements(this.PathToTmp, filters);
			Assert.AreEqual(fileResults.Length, this.FilesCreated, "filter failed 1");

			filters[0] = ".testule";
			fileResults = this.FileSystemService.GetFolderElements(this.PathToTmp, filters);
			Assert.AreEqual(fileResults.Length, 1, "filters missed a file 1");

			filters[1] = ".test";
			fileResults = this.FileSystemService.GetFolderElements(this.PathToTmp, filters);
			Assert.AreEqual(fileResults.Length, this.FilesCreated + 1, "filters missed a file 2");

			results = this.FileSystemService.GetFolderElements(this.PathToTmp);
			Assert.AreEqual(results.Length, this.FilesCreated + 1, "a file is missing");

			File.SetAttributes(this.PathToTmp + "/file.testule", File.GetAttributes(this.PathToTmp + "/file.testule") | FileAttributes.Hidden);

			this.FileSystemService.DisplayHiddenElements = false;

			results = this.FileSystemService.GetFolderElements(this.PathToTmp);
			Assert.AreEqual(results.Length, this.FilesCreated, "Hidden file should not be there");

			fileResults = this.FileSystemService.GetFolderElements(this.PathToTmp, filters);
			Assert.AreEqual(this.FilesCreated, fileResults.Length);

			this.FileSystemService.DisplayHiddenElements = true;

			results = this.FileSystemService.GetFolderElements(this.PathToTmp);
			Assert.AreEqual(results.Length, this.FilesCreated + 1, "Hidden file is missing");

			fileResults = this.FileSystemService.GetFolderElements(this.PathToTmp, filters);
			Assert.AreEqual(fileResults.Length, this.FilesCreated + 1, "Hidden file is missing (with filters)");

			results = this.FileSystemService.GetFolderElements("/invalid/Path"); // FAIL should return null?
			Assert.AreEqual(null, results);

			System.IO.Directory.CreateDirectory(this.PathToTmp + "/testFolder");

			results = this.FileSystemService.GetFolderElements(this.PathToTmp);
			Assert.AreEqual(results.Length, this.FilesCreated + 2, "Folder is missing");
		}

		//[Test()]
		//public void CopyTest() {
		//	System.IO.File.Create(this.PathToTmp + "/fileToCopy.test");
		//	IElement[] files = this.FileSystemService.GetFolderElements(this.PathToTmp);
		//	IFolder folder = this.ElementFactory.CreateFolder(this.PathToTmp);

		//	System.GC.Collect();                                // Needed use file with success
		//	System.GC.WaitForPendingFinalizers();               // If not, file is still in used by the process and copy fails.

		//	this.FileSystemService.Copy(files[0]); // , folder
		//	this.FileSystemService.Paste(folder);
		//	files = this.FileSystemService.GetFolderElements(this.PathToTmp);
		//	Assert.AreEqual(2, files.Length, "copy in same dir failed"); // FAIL should create a copy in same directory with a different name or replace (passign a boolean to the method ?)

		//	System.IO.Directory.CreateDirectory(this.PathToTmp + "/testDir");

		//	folder = this.ElementFactory.CreateFolder(this.PathToTmp + "/testDir");
		//	this.FileSystemService.Copy(files[0]);
		//	bool res = this.FileSystemService.Paste(folder);
		//	files = this.FileSystemService.GetFolderElements(this.PathToTmp + "/testDir");
		//	Assert.AreEqual(true, res, "result should be true");
		//	Assert.AreEqual(1, files.Length, "copy in other dir failed");
		//	Assert.AreEqual("fileToCopy.test", files[0].GetName(), "copy has a wrong name, is it the correct file ?");
		//	Assert.AreEqual(true, files[0].GetPath().StartsWith(folder.GetPath()));

		//	files = this.FileSystemService.GetFolderElements(this.PathToTmp);
		//	Assert.AreEqual(2, files.Length, "original file is missing");
		//	Assert.AreEqual("fileToCopy.test", files[1].GetName(), "original has a wrong name, is it the correct file ?");

		//	this.FileSystemService.Copy(folder); // FAIL Access deny ? Probleme with directory copy ?
		//	res = this.FileSystemService.Paste(folder);
		//	Assert.AreEqual(true, res);
		//	files = this.FileSystemService.GetFolderElements(this.PathToTmp + "/testDir");
		//	Assert.AreEqual(2, files.Length, "folder copy is missing");

		//	this.FileSystemService.Copy((IElement)null); // FAIL Should handle null and return false ?
		//	this.FileSystemService.Copy((IElement[])null);
		//	this.FileSystemService.Copy(files[0]);
		//	res = this.FileSystemService.Paste(null);

		//	folder = (IFolder)this.ElementFactory.Create("/Invalid/folder");
		//	this.FileSystemService.Copy(files[0]); // FAIL Should hande exception and return false
		//	res = res = this.FileSystemService.Paste(folder);
		//	Assert.AreEqual(false, res);
		//}

		//[Test()]
		//public void MoveTest() {
		//	System.IO.File.Create(this.PathToTmp + "/fileToMove.test");
		//	IElement[] files = this.FileSystemService.GetFolderElements(this.PathToTmp);
		//	Assert.AreEqual(1, files.Length, "should contain only one file");
		//	IFolder folder = this.ElementFactory.CreateFolder(this.PathToTmp);

		//	System.GC.Collect();                                // Needed use file with success
		//	System.GC.WaitForPendingFinalizers();               // If not, file is still in used by the process and copy fails.

		//	System.IO.Directory.CreateDirectory(this.PathToTmp + "/testDir");

		//	folder = (IFolder)this.ElementFactory.Create(this.PathToTmp + "/testDir");
		//	this.FileSystemService.Cut(files[0]);
		//	bool res = this.FileSystemService.Paste(folder); // , folder
		//	files = this.FileSystemService.GetFolderElements(this.PathToTmp + "/testDir");
		//	Assert.AreEqual(true, res, "result should be true");
		//	Assert.AreEqual(1, files.Length, "move in other dir failed");
		//	Assert.AreEqual("fileToMove.test", files[0].GetName(), "copy has a wrong name, is it the correct file ?");

		//	files = this.FileSystemService.GetFolderElements(this.PathToTmp);
		//	Assert.AreEqual(1, files.Length, "original file is still here");

		//	System.IO.Directory.CreateDirectory(this.PathToTmp + "/dirIsBack");

		//	files = this.FileSystemService.GetFolderElements(this.PathToTmp);
		//	Assert.AreEqual(2, files.Length, "move in other dir failed");

		//	this.FileSystemService.Cut(folder);
		//	res = this.FileSystemService.Paste(folder); // FAIL used by another process. Should catch the exception and return false move a folder inside himself should not be possible.
		//	Assert.AreEqual(false, res);

		//	IFolder folderToMove = this.ElementFactory.CreateFolder(this.PathToTmp + "/dirIsBack");
		//	this.FileSystemService.Cut(folderToMove);
		//	res = this.FileSystemService.Paste(folder);
		//	files = this.FileSystemService.GetFolderElements(this.PathToTmp);
		//	Assert.AreEqual(true, res);
		//	Assert.AreEqual(1, files.Length, "move in other dir failed");

		//	files = this.FileSystemService.GetFolderElements(folder.GetPath());
		//	Assert.AreEqual(2, files.Length);
		//	Assert.AreEqual("dirIsBack", files[0].GetName());
		//	Assert.AreEqual(true, files[0].GetPath().StartsWith(folder.GetPath()), "folder moved has a wrong path");

		//	//IFolder invalidFolder = (IFolder)this.FolderFactory.Create("/Invalid/folder"); // FAIL With used by another process, don't know why
		//	//res = this.FileSystemService.Move(files[0], folder); // FAIL Should handle exception and return false
		//	//Assert.AreEqual(false, res);
		//}

		[TearDown]
		public void OnEnd() {
			System.GC.Collect();                                // Needed to delete directory with success.
			System.GC.WaitForPendingFinalizers();               // If not, direcory is still in used by the process and delete fails.
			System.IO.Directory.Delete(this.PathToTmp, true);
		}
	}
}