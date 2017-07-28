using System;
using System.IO;

namespace WcfFileService.BL.Operations
{
    public class DirectoryCreateOperation : IDirectoryCreateOperation
    {
        public bool CreateIfNotExists(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                return true;
            }
            return false;
        }
    }
}
