using System.Collections.Generic;

namespace WcfFileService.BL.Operations
{
    public interface IDirectoryInfoOperation
    {
        IEnumerable<KeyValuePair<string, int>> GetExtensionsCount(string path);
    }
}
