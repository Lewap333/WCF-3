using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF3_6_Service1
{
    [ServiceContract]
    public interface IZadanie6
    {
        [OperationContract]
        int Dodaj(int a, int b);
    }
    public class Zadanie6 : IZadanie6
    {
        public int Dodaj(int a, int b)
        {
            Console.WriteLine("Dowolna informacja w konsoli z serwisu1");
            return a + b;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie6));
            host.AddServiceEndpoint(
                typeof(IZadanie6),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/serwis1");

            host.Open();
            Console.WriteLine("Service1 started!");
            Console.ReadKey();
            host.Close();
        }
    }
}
