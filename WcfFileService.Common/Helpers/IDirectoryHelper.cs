using System.Collections.Generic;

namespace WcfFileService.BL.Helpers
{
    public interface IDirectoryHelper
    {
        IEnumerable<string> GetAllExtensions(string path);
        IEnumerable<string> GetAllSubDirectories(string dirPath);
        IEnumerable<string> GetAllFiles(string dirPath);
        bool Exists(string dirPath);
    }
}
