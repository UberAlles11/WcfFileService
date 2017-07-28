using Newtonsoft.Json;
using System;
using System.ServiceModel.Web;
using WcfFileServiceProxy;

namespace ServiceConsoleClientApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            var baseAddress = new Uri("http://localhost:8000/WcfService/FileRestService/");            

            using (var factory = new WebChannelFactory<IFileRestService>(baseAddress))
            {
                var service = factory.CreateChannel();

                var dir = new Uri(Environment.CurrentDirectory);

                Console.WriteLine("Get JSON extensions counters:");
                var response = service.GetExtensionsCount(dir.ToString());
                Console.WriteLine(JsonConvert.SerializeObject(response));
                Console.WriteLine();

                var sourcePath = Environment.CurrentDirectory;
                
                var destinationPath = sourcePath.Substring(0,sourcePath.LastIndexOf('\\')+1) + Guid.NewGuid();
                Console.WriteLine("Copy:{0}{1}{2}to{3}{4}", Environment.NewLine, sourcePath, Environment.NewLine,
                    Environment.NewLine, destinationPath);

                service.CopyEntireDirectory(sourcePath, destinationPath);
                Console.WriteLine();
            }

            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
