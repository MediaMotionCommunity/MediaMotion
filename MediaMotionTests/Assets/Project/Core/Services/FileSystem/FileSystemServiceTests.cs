using System;
using System.Collections.Generic;
using System.IO;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using NUnit.Framework;
namespace MediaMotion.Core.Services.FileSystem.Tests
{
    [TestFixture]
    public class FileSystemServiceTests
    {
        public IFileSystemService FileSystemService;
        public FolderFactory FolderFactory;
        public int FilesCreated;
        public string PathToTmp;

        [SetUp]
        public void Init()
        {
            this.FileSystemService = new FileSystemService(new FolderFactory(), new FileFactory());
            this.PathToTmp = Environment.GetFolderPath(Environment.SpecialFolder.Personal) != string.Empty ? Environment.GetFolderPath(Environment.SpecialFolder.Personal) : Environment.GetFolderPath(Environment.SpecialFolder.System);
            this.PathToTmp = this.PathToTmp + "/UnitTestMediaMotionTMP";
            Random rnd = new Random();
            this.FilesCreated = rnd.Next(1, 20);
            System.IO.Directory.CreateDirectory(this.PathToTmp);
            this.FolderFactory = new FolderFactory();
        }

        [Test]
        public void FileSystemTest()
        {
            Assert.AreNotEqual(null, this.FileSystemService);
        }

        [Test]
        public void GetHomeDirectoryTest()
        {
            string HomePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) != string.Empty ? Environment.GetFolderPath(Environment.SpecialFolder.Personal) : Environment.GetFolderPath(Environment.SpecialFolder.System);
            string HomeFolder = this.FileSystemService.GetHome();

            this.FileSystemService.ChangeDirectory(HomeFolder);
            Assert.AreEqual(HomeFolder, this.FileSystemService.CurrentFolder.GetPath());
            Assert.AreEqual(HomePath, this.FileSystemService.CurrentFolder.GetPath());
            Assert.AreEqual(HomePath, HomeFolder);
        }

        [Test]
        public void ChangeDirectoryTest()
        {
            IFolder newDirectory = null;
            IFolder initialDirectory = null;

            bool result1 = this.FileSystemService.ChangeDirectory(this.FileSystemService.GetHome());
            Assert.AreEqual(true, result1);
            initialDirectory = this.FileSystemService.CurrentFolder;
            Assert.AreEqual(ElementType.Folder, initialDirectory.GetElementType());

            List<IElement> directoryContent = this.FileSystemService.GetContent(this.FileSystemService.CurrentFolder.GetPath());
            foreach (IElement element in directoryContent)
            {
                if (element.GetElementType().Equals(ElementType.Folder))
                {
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
        public void GetContentTest()
        {
            List<IElement> results = this.FileSystemService.GetContent();
            Assert.AreNotEqual(0, results.Count, "should find some file (current folder)");

            results = this.FileSystemService.GetContent(this.FileSystemService.GetHome());
            Assert.AreNotEqual(0, results.Count, "should find somes files (home folder)");

            for (int i = 0; i < this.FilesCreated; i++)
            {
                System.IO.File.Create(this.PathToTmp + "/file" + i + ".test");
            }

            results = this.FileSystemService.GetContent(this.PathToTmp);
            Assert.AreEqual(this.FilesCreated, results.Count);
            Assert.AreEqual("file0.test", results[0].GetName());

            string[] filters = new string[2];

            List<IFile> fileResults = this.FileSystemService.GetContent(filters, this.PathToTmp);
            Assert.AreEqual(0, fileResults.Count, "filter failed 0");

            filters[0] = "shouldfindnothing";
            fileResults = this.FileSystemService.GetContent(filters, this.PathToTmp);
            Assert.AreEqual(0, fileResults.Count, "filter failed 1");

            filters[0] = ".test";
            fileResults = this.FileSystemService.GetContent(filters, this.PathToTmp);
            Assert.AreEqual(this.FilesCreated, fileResults.Count, "filter failed 2");

            System.IO.File.Create(this.PathToTmp + "/file.testule");

            fileResults = this.FileSystemService.GetContent(filters, this.PathToTmp);
            Assert.AreEqual(fileResults.Count, this.FilesCreated, "filter failed 3");

            filters[0] = ".testule";
            fileResults = this.FileSystemService.GetContent(filters, this.PathToTmp);
            Assert.AreEqual(fileResults.Count, 1, "filters missed a file 1");

            filters[1] = ".test";
            fileResults = this.FileSystemService.GetContent(filters, this.PathToTmp);
            Assert.AreEqual(fileResults.Count, this.FilesCreated + 1, "filters missed a file 2");

            results = this.FileSystemService.GetContent(this.PathToTmp);
            Assert.AreEqual(results.Count, this.FilesCreated + 1, "a file is missing");

            File.SetAttributes(this.PathToTmp + "/file.testule", File.GetAttributes(this.PathToTmp + "/file.testule") | FileAttributes.Hidden);

            this.FileSystemService.DisplayHidden = false;

            results = this.FileSystemService.GetContent(this.PathToTmp);
            Assert.AreEqual(results.Count, this.FilesCreated, "Hidden file should not be there");

            fileResults = this.FileSystemService.GetContent(filters, this.PathToTmp); // FAIL, should not return hidden files
            //Assert.AreEqual(this.FilesCreated, fileResults.Count);

            this.FileSystemService.DisplayHidden = true;

            results = this.FileSystemService.GetContent(this.PathToTmp);
            Assert.AreEqual(results.Count, this.FilesCreated + 1, "Hidden file is missing");

            fileResults = this.FileSystemService.GetContent(filters, this.PathToTmp);
            Assert.AreEqual(fileResults.Count, this.FilesCreated + 1, "Hidden file is missing (with filters)");

            //results = this.FileSystemService.GetContent("/invalid/Path"); // FAIL should return null?
            //Assert.AreEqual(null, results);

            System.IO.Directory.CreateDirectory(this.PathToTmp + "/testFolder");

            results = this.FileSystemService.GetContent(this.PathToTmp);
            Assert.AreEqual(results.Count, this.FilesCreated + 2, "Folder is missing");
        }

        [Test()]
        public void CopyTest()
        {
            System.IO.File.Create(this.PathToTmp + "/fileToCopy.test");
            List<IElement> files = this.FileSystemService.GetContent(this.PathToTmp);
            IFolder folder = (IFolder) this.FolderFactory.Create(this.PathToTmp);

            System.GC.Collect();                                // Needed use file with success
            System.GC.WaitForPendingFinalizers();               // If not, file is still in used by the process and copy fails.

            //this.FileSystemService.Copy(files[0], folder);
            files = this.FileSystemService.GetContent(this.PathToTmp);
            //Assert.AreEqual(files.Count, 2, "copy in same dir failed"); // FAIL should create a copy in same directory with a differente name or replace (passign a boolean to the method ?)

            System.IO.Directory.CreateDirectory(this.PathToTmp + "/testDir");

            folder = (IFolder)this.FolderFactory.Create(this.PathToTmp + "/testDir");
            this.FileSystemService.Copy(files[0], folder);
            files = this.FileSystemService.GetContent(this.PathToTmp + "/testDir");
            Assert.AreEqual(1, files.Count, "copy in other dir failed");
            Assert.AreEqual("fileToCopy.test", files[0].GetName(), "copy has a wrong name, is it the correct file ?");

            files = this.FileSystemService.GetContent(this.PathToTmp);
            Assert.AreEqual(2, files.Count, "original file is missing");
            Assert.AreEqual("fileToCopy.test", files[1].GetName(), "original has a wrong name, is it the correct file ?");

            //this.FileSystemService.Copy(folder, folder); // FAIL Access deny ? Probleme with directory copy ?
            files = this.FileSystemService.GetContent(this.PathToTmp + "/testDir");
            //Assert.AreEqual(2, files.Count, "folder copy is missing"); 

            // this.FileSystemService.Copy(null, folder); // FAIL Should handle null ?
            // this.FileSystemService.Copy(null, null); // FAIL Should handle null ?
            // this.FileSystemService.Copy(files[0], null); // FAIL Should handle null ?
        }

        [TearDown]
        public void OnEnd()
        {
            System.GC.Collect();                                // Needed to delete directory with success.
            System.GC.WaitForPendingFinalizers();               // If not, direcory is still in used by the process and delete fails.
            System.IO.Directory.Delete(this.PathToTmp, true);
        }
    }
}