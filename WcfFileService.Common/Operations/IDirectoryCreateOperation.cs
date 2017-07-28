namespace WcfFileService.BL.Operations
{
    public interface IDirectoryCreateOperation
    {
        bool CreateIfNotExists(string dir);
    }
}
