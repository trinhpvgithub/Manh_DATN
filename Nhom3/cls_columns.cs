using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace Nhom3
{
    internal class cls_columns
    {

        XYZ location = new XYZ();
        string stt = "";
        string name = "";
        string id = "";
        string b = "";
        string h = "";
        string a = "";

        string l = "";
        string mchancot = "";
        string nchancot = "";
        string mdinhcot = "";
        string ndinhcot = "";
        string asyc = "";
        string phi = "";
        string sophi = "";
        string astt = "";
        string thepdai = "";
        string kcdai = "";

        public string Stt { get => stt; set => stt = value; }
        public string Name { get => name; set => name = value; }
        public string Id { get => id; set => id = value; }
        public string B { get => b; set => b = value; }
        public string H { get => h; set => h = value; }
        public string L { get => l; set => l = value; }
        public string Mchancot { get => mchancot; set => mchancot = value; }
        public string Nchancot { get => nchancot; set => nchancot = value; }
        public string Mdinhcot { get => mdinhcot; set => mdinhcot = value; }
        public string Ndinhcot { get => ndinhcot; set => ndinhcot = value; }
        public string Asyc { get => asyc; set => asyc = value; }
        public string Phi { get => phi; set => phi = value; }
        public string Sophi { get => sophi; set => sophi = value; }
        public string Astt { get => astt; set => astt = value; }
        public string Thepdai { get => thepdai; set => thepdai = value; }
        public string Kcdai { get => kcdai; set => kcdai = value; }
        public XYZ Location { get => location; set => location = value; }
        public string A { get => a; set => a = value; }
    }
}
