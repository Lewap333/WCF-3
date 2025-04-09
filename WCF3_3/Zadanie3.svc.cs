using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace WCF3_3
{
    [EnableCorsBehavior]
    public class Zadanie3 : IZadanie3
    {
        public XmlDocument Index()
        {
            var d = new XmlDocument();
            d.XmlResolver = null;
            d.Load("C:\\Git\\WCF3\\WCF3_3\\index.xhtml");
            return d;
        }

        public Stream Script()
        {
            return new FileStream("C:\\Git\\WCF3\\WCF3_3\\scripts.js", FileMode.Open);
        }

        public int Dodaj(string a, string b)
        {
            return Int32.Parse(a) + Int32.Parse(b);
        }
    }
}
