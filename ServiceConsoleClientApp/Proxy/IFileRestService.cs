using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfFileServiceProxy
{
    [ServiceContract]
    public interface IFileRestService
    {
        /// <summary>  
        ///  Return JSON data list with extension counters (GET)
        /// </summary> 
        [OperationContract, WebGet(UriTemplate = "/extensions/{*uriEncodedLocalPath}", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<ExtCount> GetExtensionsCount(string uriEncodedLocalPath);

        /// <summary>  
        ///  Copy source directory entire content to new destination (POST)
        /// </summary> 
        [OperationContract, WebInvoke(Method = "POST", UriTemplate = "/directory/", BodyStyle = WebMessageBodyStyle.Wrapped)]
        void CopyEntireDirectory(string sourcePath, string destinationPath);
    }
}
