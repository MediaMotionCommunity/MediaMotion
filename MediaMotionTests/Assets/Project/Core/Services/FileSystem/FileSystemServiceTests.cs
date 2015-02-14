using System;
using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using NUnit.Framework;
namespace MediaMotion.Core.Services.FileSystem.Tests
{
    [TestFixture()]
    public class FileSystemServiceTests
    {
        public IFileSystemService FileSystemService;

        [SetUp]
        public void Init()
        {
            this.FileSystemService = new FileSystemService(new FolderFactory(), new FileFactory());
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

            String InvalidPath = "/invalid/path";
            bool result3 = this.FileSystemService.ChangeDirectory(InvalidPath);
            Assert.AreEqual(false, result3);
            Assert.AreNotEqual(InvalidPath, this.FileSystemService.CurrentFolder.GetPath());
            Assert.AreEqual(newDirectory.GetPath(), this.FileSystemService.CurrentFolder.GetPath());

            bool result4 = this.FileSystemService.ChangeDirectory();
            Assert.AreEqual(true, result4);
            Assert.AreEqual(this.FileSystemService.CurrentFolder.GetPath(), this.FileSystemService.GetHome());

        }

        [Test()]
        public void GetContentTest()
        {
            List<IElement> results = this.FileSystemService.GetContent();
            Assert.AreNotEqual(null, results);

            results = this.FileSystemService.GetContent("/invalid/Path"); // FAIL should return null?
            Assert.AreEqual(null, results);
        }
    }
}