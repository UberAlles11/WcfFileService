using System.Collections.Generic;
using System.Linq;
using WcfFileService.BL.Helpers;

namespace WcfFileService.BL.Operations
{
    public class DirectoryInfoOperation : IDirectoryInfoOperation
    {
        IDirectoryHelper helper;

        public DirectoryInfoOperation(IDirectoryHelper _helper)
        {
            helper = _helper;
        }

        public IEnumerable<KeyValuePair<string, int>> GetExtensionsCount(string path)
        {
            if (!helper.Exists(path))
                return new List<KeyValuePair<string, int>>();

            var dict = helper.GetAllExtensions(path).GroupBy(e => e)
                .Select(e => new
                {
                    Ext = e.Key,
                    Cnt = e.Sum(w => 1)
                })
                .OrderByDescending(e => e.Cnt)
                .ToDictionary(e => e.Ext, e => e.Cnt);

            return dict.ToList();
        }
    }
}
