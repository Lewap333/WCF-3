using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF3_6_Klient
{
    [ServiceContract]
    public interface IZadanie6
    {
        [OperationContract]
        int Dodaj(int a, int b);
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string routerAddr = "net.pipe://localhost/router";

            var factory = new ChannelFactory<IZadanie6>(
                new NetNamedPipeBinding(),
                new EndpointAddress(routerAddr)
            );

            IZadanie6 proxy = factory.CreateChannel();


            int a = 10;
            int b = 20;

           Console.WriteLine($"Wynik dodawania {a} + {b} = {proxy.Dodaj(a, b)}");

           ((IClientChannel)proxy).Close();
           factory.Close();
           Console.ReadLine();
        }
    }
}
