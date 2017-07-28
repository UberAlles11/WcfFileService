using System;
using System.Collections.Generic;
using System.IO;
using WcfFileService.BL.Common;
using WcfFileService.BL.Helpers;
using WcfFileService.Common;

namespace WcfFileService.BL.Operations
{
    public class DirectoryCopyOperation : IDirectoryCopyOperation
    {
        IFileCopyOperation fileOperation;
        IDirectoryCreateOperation directoryOperation;
        IDirectoryHelper directoryHelper;

        public DirectoryCopyOperation(IFileCopyOperation fileop, IDirectoryCreateOperation dirop, IDirectoryHelper helper)
        {
            Guard.Against<ArgumentNullException>(fileop == null, "DirectoryOperation: fileop is null");
            Guard.Against<ArgumentNullException>(dirop == null, "DirectoryOperation: dirop is null");
            Guard.Against<ArgumentNullException>(helper == null, "DirectoryOperation: helper is null");

            fileOperation = fileop;
            directoryOperation = dirop;
            directoryHelper = helper;
        }

        public IEnumerable<string> CopyEntireDirectory(string sourceDirectory, string destinationDirectory)
        {
            var files = new List<string>();

            Guard.Against<ArgumentNullException>(sourceDirectory.IsNullOrEmpty(),
                "DirectoryCopyOperation.CopyEntireDirectory: sourceDirectory is null");

            Guard.Against<ArgumentNullException>(destinationDirectory.IsNullOrEmpty(),
                "DirectoryCopyOperation.CopyEntireDirectory: destinationDirectory is null");

            if (!directoryHelper.Exists(sourceDirectory) || sourceDirectory.ToLower() == destinationDirectory.ToLower())
                return files;

            directoryOperation.CreateIfNotExists(destinationDirectory);

            directoryHelper.GetAllFiles(sourceDirectory).ForEach(file =>
            {
                var target = Path.Combine(destinationDirectory, Path.GetFileName(file));
                if (fileOperation.CopyIfNotExists(file, target))
                {
                    files.Add(target);
                }
            });

            directoryHelper.GetAllSubDirectories(sourceDirectory).ForEach(directory =>
            {                
                destinationDirectory = Path.Combine(destinationDirectory, Path.GetFileName(directory));
                var subfiles = CopyEntireDirectory(directory, destinationDirectory);
                files.AddRange(subfiles);
            });

            return files;
        }
    }
}
