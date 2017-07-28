using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using WcfFileService;


namespace ServiceConsoleHostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileRestServiceBaseAddress = "http://localhost:8000/WcfService/FileRestService/";
            ServiceHost restHost = new ServiceHost(typeof(FileRestService), new Uri(FileRestServiceBaseAddress));

            var endpoint = restHost.AddServiceEndpoint(typeof(IFileRestService), new WebHttpBinding(), "");
            endpoint.Behaviors.Add(new WebHttpBehavior() { AutomaticFormatSelectionEnabled = false, DefaultOutgoingResponseFormat = WebMessageFormat.Json });

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior()
            {
                HttpGetEnabled = true,
                HttpGetUrl = new Uri(FileRestServiceBaseAddress)
            };            
            restHost.Description.Behaviors.Add(smb);

            restHost.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));            
            restHost.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });

            restHost.Open();            
            
            Console.WriteLine("Сервер запущен");
            Console.ReadLine();

            // Закрываем службу
            restHost.Close();
        }
    }
}
