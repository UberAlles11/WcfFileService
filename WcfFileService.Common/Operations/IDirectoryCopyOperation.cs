using System.Collections.Generic;

namespace WcfFileService.BL.Operations
{
    public interface IDirectoryCopyOperation
    {
        IEnumerable<string> CopyEntireDirectory(string sourceDirectory, string destinationDirectory);
    }
}