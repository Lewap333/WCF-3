using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml;

namespace WCF3_3
{
    [ServiceContract]
    public interface IZadanie3
    {
        [OperationContract,WebGet(UriTemplate = "/index"), XmlSerializerFormat]
        XmlDocument Index();

        [OperationContract, WebGet(UriTemplate = "scripts.js")]
        Stream Script();

        [OperationContract]
        [WebInvoke(UriTemplate = "Dodaj/{a}/{b}")]
        int Dodaj(string a, string b);
    }
}
