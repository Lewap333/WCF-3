using System;
using System.ServiceModel.Discovery;
using System.ServiceModel;

namespace WCF3_2
{
    [ServiceContract]
    public interface IZadanie1
    {
        [OperationContract]
        string ScalNapisy(string a, string b);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var discoveryClient = new DiscoveryClient(
                 new UdpDiscoveryEndpoint("soap.udp://localhost:30703"));
            var lst = discoveryClient.Find(new FindCriteria(typeof(IZadanie1))).Endpoints;
            discoveryClient.Close();

            discoveryClient.Close(); if (lst.Count > 0)
            {
                var addr = lst[0].Address;
                var client = ChannelFactory<IZadanie1>
                   .CreateChannel(new NetNamedPipeBinding(), addr);
                
                Console.WriteLine(client.ScalNapisy("Pierwszy", "Drugi"));

                Console.ReadKey();

                ((IDisposable)client).Dispose();
            }
        }
    }
}
