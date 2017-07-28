using System;
using System.IO;
using WcfFileService.BL.Common;
using WcfFileService.Common;

namespace WcfFileService.BL.Operations
{
    public class FileCopyOperation : IFileCopyOperation
    {
        public bool CopyIfNotExists(string sourceFile, string targetFile)
        {
            Guard.Against<ArgumentNullException>(sourceFile.IsNullOrEmpty(), "FileOperation.CopyIfNotExists: invalid sourceFile");
            Guard.Against<ArgumentNullException>(targetFile.IsNullOrEmpty(), "FileOperation.CopyIfNotExists: invalid targetFile");

            if (!File.Exists(targetFile))
            {
                File.Copy(sourceFile, targetFile);
                return true;
            }
            return false;
        }
    }
}
