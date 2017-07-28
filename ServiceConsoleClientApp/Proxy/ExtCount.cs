using System.Runtime.Serialization;

namespace WcfFileServiceProxy
{
    /// <summary>  
    ///  JSON data item for GetExtensionsCount response
    /// </summary> 
    [DataContract]
    public class ExtCount
    {
        int count = 0;
        string ext = "";

        /// <summary>  
        ///  Extension type string
        /// </summary> 
        [DataMember]
        public string Ext
        {
            get { return ext; }
            set { ext = value; }
        }

        /// <summary>  
        ///  Extension type counter
        /// </summary> 
        [DataMember]
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
}
