using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WcfFileService.BL.Helpers;
using WcfFileService.BL.Operations;

namespace WcfFileService.Tests
{
    [TestClass]
    public class DirectoryInfoOperationTest
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void GetExtensionCountPairListOrderedByCountDescending321()
        {
            var helper = new Mock<IDirectoryHelper>();
            var path = "/testpath";
            helper.Setup(d => d.GetAllExtensions(path))
                .Returns(new List<string>() { ".exe", ".exe", ".exe", ".txt", ".txt", ".doc"});
            helper.Setup(d => d.Exists(path)).Returns(true);

            IDirectoryInfoOperation info = new DirectoryInfoOperation(helper.Object);

            var extensions = info.GetExtensionsCount(path).ToList();

            CollectionAssert.AreEqual(extensions, 
                new List<KeyValuePair<string,int>>()
                {
                    new KeyValuePair<string, int>(".exe", 3),
                    new KeyValuePair<string, int>(".txt", 2),
                    new KeyValuePair<string, int>(".doc", 1)
                });
        }

        [TestCleanup]
        public void Cleanup()
        {
        }
    }
}
