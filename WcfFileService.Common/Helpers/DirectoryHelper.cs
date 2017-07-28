using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WcfFileService.BL.Helpers
{
    public class DirectoryHelper : IDirectoryHelper
    {
        public IEnumerable<string> GetAllExtensions(string dirPath)
        {
            var dir = new System.IO.DirectoryInfo(dirPath);
            return dir.EnumerateFiles("*.*").Select(f => f.Extension);
        }

        public IEnumerable<string> GetAllSubDirectories(string dirPath)
        {            
            return Directory.GetDirectories(dirPath);
        }

        public IEnumerable<string> GetAllFiles(string dirPath)
        {
            var dir = new System.IO.DirectoryInfo(dirPath);
            return dir.EnumerateFiles("*.*").Select(f => f.FullName);
        }
        public bool Exists(string dirPath)
        {
            var dir = new System.IO.DirectoryInfo(dirPath);
            return dir.Exists;

        }
    }
}
