using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;

namespace WCF3_5
{
    [ServiceContract]
    public interface IZadanie3
    {
        [OperationContract, WebGet(UriTemplate = "/index"), XmlSerializerFormat]
        XmlDocument Index();

        [OperationContract, WebGet(UriTemplate = "scripts.js")]
        Stream Script();

        [OperationContract]
        [WebInvoke(UriTemplate = "Dodaj/{a}/{b}")]
        int Dodaj(string a, string b);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ChannelFactory<IZadanie3>(
                new WebHttpBinding(),
                new EndpointAddress("http://localhost:30704/Zadanie3.svc"));
            factory.Endpoint.Behaviors.Add(new WebHttpBehavior());
            var client = factory.CreateChannel();

            Console.WriteLine(client.Dodaj("123", "12"));
            Console.ReadKey();

            ((IDisposable)client).Dispose();
            factory.Close();
           
        }
    }
}
