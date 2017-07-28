namespace WcfFileService.BL.Operations
{
    public interface IFileCopyOperation
    {
        bool CopyIfNotExists(string sourceFile, string targetFile);
    }
}
