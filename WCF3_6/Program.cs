using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Routing;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF3_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var routeTo1 = "net.pipe://localhost/serwis1";
            var routeTo2 = "net.pipe://localhost/serwis2";

            var routerAddr = "net.pipe://localhost/router";

            var h = new ServiceHost(typeof(RoutingService));
            h.AddServiceEndpoint(
                typeof(IRequestReplyRouter),
                 new NetNamedPipeBinding(),
                routerAddr
            );

            var rc = new RoutingConfiguration();
            var contract = ContractDescription.GetContract(typeof(IRequestReplyRouter));
            
            var client1 = new ServiceEndpoint(
                contract,
                 new NetNamedPipeBinding(),
                new EndpointAddress(routeTo1)
            );

            var client2 = new ServiceEndpoint(
                contract,
                new NetNamedPipeBinding(),
                new EndpointAddress(routeTo2)
            );

            var lst = new List<ServiceEndpoint>();
            lst.Add(client1);
            lst.Add(client2);


            rc.FilterTable.Add(new MatchAllMessageFilter(), lst);
            h.Description.Behaviors.Add(new RoutingBehavior(rc));

            h.Open();
            Console.WriteLine("Router started!");
            Console.ReadLine();
            h.Close();
        }
    }
}
