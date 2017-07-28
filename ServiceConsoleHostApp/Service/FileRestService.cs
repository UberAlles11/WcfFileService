using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using WcfFileService.BL.Common;
using WcfFileService.BL.Operations;

namespace WcfFileService
{
    public class FileRestService : IFileRestService
    {
        /// <summary>  
        ///  Return JSON data list with extension counters (GET)
        /// </summary> 
        public IEnumerable<ExtCount> GetExtensionsCount(string uriEncodedLocalPath)
        {
            var exts = new List<ExtCount>();

            if (uriEncodedLocalPath.IsNullOrEmpty())
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return exts;
            }            

            var builder = new ContainerBuilder();
            builder.RegisterModule(new ServiceModule());            

            using (var container = builder.Build())
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var operation = scope.Resolve<IDirectoryInfoOperation>();
                    Uri dir = new Uri(uriEncodedLocalPath);
                    exts = operation.GetExtensionsCount(dir.LocalPath).Select(e => new ExtCount() { Ext = e.Key, Count = e.Value }).ToList();
                }
            }

            if (exts.IsNullOrEmpty())
            {
                WebOperationContext.Current.OutgoingResponse.SetStatusAsNotFound();
            }

            return exts;
        }

        /// <summary>  
        ///  Copy source directory entire content to new destination (POST)
        /// </summary> 
        public void CopyEntireDirectory(string sourcePath, string destinationPath)
        {
            WebOperationContext context = WebOperationContext.Current;

            if (sourcePath.IsNullOrEmpty() || destinationPath.IsNullOrEmpty())
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return;
            }

            var builder = new ContainerBuilder();
            builder.RegisterModule(new ServiceModule());

            using (var container = builder.Build())
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var operation = scope.Resolve<IDirectoryCopyOperation>();
                    var files = operation.CopyEntireDirectory(sourcePath, destinationPath);

                    if (!files.IsNullOrEmpty())
                    {
                        context.OutgoingResponse.SetStatusAsCreated(new Uri(destinationPath));
                    }
                }
            }          
        }
    }
}
