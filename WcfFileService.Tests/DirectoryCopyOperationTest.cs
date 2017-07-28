using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WcfFileService.BL.Helpers;
using WcfFileService.BL.Operations;

namespace WcfFileService.Tests
{
    [TestClass]
    public class DirectoryCopyOperationTest
    {
        string sourcePath = string.Empty;
        string destPath = string.Empty;

        public TestContext TestContext { get; set; }

        private static TestContext _testContext;

        [ClassInitialize]
        public static void SetupTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [TestInitialize]
        public void Initialize()
        {
            sourcePath = Environment.CurrentDirectory;
            var directory = Directory.GetParent(TestContext.TestRunDirectory);
            destPath = Path.Combine(directory.FullName, Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void CopyTest()
        {            
            var helper = new Mock<IDirectoryHelper>();
            helper.Setup(d => d.Exists(sourcePath)).Returns(true);

            var source_files = new List<string>() {
                sourcePath + "\\testfile1.txt",
                sourcePath + "\\testfile2.txt",
                sourcePath + "\\testfile3.txt"
            };
            var dest_files = new List<string>() {
                destPath + "\\testfile1.txt",
                destPath + "\\testfile2.txt",
                destPath + "\\testfile3.txt"
            };

            var source_file = sourcePath + "\\testfile1.txt";
            var dest_file = destPath + "\\testfile1.txt";

            helper.Setup(d => d.GetAllFiles(sourcePath)).Returns(source_files);
            helper.Setup(d => d.GetAllSubDirectories(sourcePath)).Returns(new List<string>());

            var dirOp = new Mock<IDirectoryCreateOperation>();
            var fileOp = new Mock<IFileCopyOperation>();
            fileOp.Setup(d => d.CopyIfNotExists(source_files[0], dest_files[0])).Returns(true);
            fileOp.Setup(d => d.CopyIfNotExists(source_files[1], dest_files[1])).Returns(true);
            fileOp.Setup(d => d.CopyIfNotExists(source_files[2], dest_files[2])).Returns(true);

            IDirectoryCopyOperation op = new DirectoryCopyOperation(fileOp.Object, dirOp.Object, helper.Object);

            var files = op.CopyEntireDirectory(sourcePath, destPath).ToList();

            CollectionAssert.AreEqual(dest_files, files);
        }

        [TestCleanup]
        public void Cleanup()
        {            
            var directory = new DirectoryInfo(destPath);

            if (directory.Exists)
            {
                directory.EnumerateFiles()
                    .ToList().ForEach(f => f.Delete());

                directory.EnumerateDirectories()
                    .ToList().ForEach(d => d.Delete(true));
            }
        }
    }
}
