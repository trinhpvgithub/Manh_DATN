using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace Nhom3
{
    internal class cls_modul
    {
        public static Betong Chonbetong = new Betong();
        public static Thep Thepdoc = new Thep();
        public static Thep Thepdai = new Thep();
        public static List<cls_columns> danhsachcot = new List<cls_columns>();
        public static List<cls_columns> cotvethep = new List<cls_columns>();
        public static List<ElementId> Idcolumns = new List<ElementId>();

    }
}
