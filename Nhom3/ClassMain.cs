using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using HcBimUtils.DocumentUtils;
using HcBimUtils;
using System.Security.Cryptography;

namespace Nhom3
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class ClassMain : IExternalCommand
    {
        UIApplication uiapp;
        UIDocument uidoc;
        Autodesk.Revit.ApplicationServices.Application app;
        Document doc;
        ExternalCommandData revit;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)//class chính chạy chương trình
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;//lây môi trường revit để thực thi
            revit = commandData;
            FormGiaoDien frm = new FormGiaoDien();
            var dlg = frm.ShowDialog();
            if(dlg == System.Windows.Forms.DialogResult.OK)
            {
                //chọn cột
                Reference references = uidoc.Selection.PickObject(ObjectType.Element,"Chọn Cột");
                List<Element> Colums = new List<Element>();

                // Phân loại cột
                //foreach (Reference r in references)
                //{
                    Element element = doc.GetElement(references);
                    if (element.Category.Name.Contains("Structural Columns"))
                    {
                        cls_modul.Idcolumns.Add(element.Id);
                        Colums.Add(element);
                    }
                //}
                //lấy cột đâu tiên trong danh sách
                BoundingBoxXYZ bbox = Colums[0].get_BoundingBox(null);
                double lengh = Math.Round((bbox.Max.Z - bbox.Min.Z) * 304.8)/1000;

                double width = Math.Round((bbox.Max.Y - bbox.Min.Y) * 304.8)/1000;

                double height = Math.Round((bbox.Max.X - bbox.Min.X) * 304.8)/1000;
              //lấy kích thước cột
                string w = width.ToString();
                string l = lengh.ToString();

                string h = height.ToString();
                frm.kichthuoc(w, h,l, Colums[0]);
                //Execute(commandData, ref message, elements);
                dlg = frm.ShowDialog();
            }//chọn và lấy thông tin cột
            if (dlg == System.Windows.Forms.DialogResult.Yes)
            {
                //Vẽ thép
                foreach (cls_columns clm in cls_modul.danhsachcot)
                {
                    ElementId clmid = null;
                    foreach (ElementId elementId in cls_modul.Idcolumns)
                    {
                        if (clm.Id == elementId.ToString())
                        {
                            clmid = elementId;
                            break;
                        }
                    }
                    if (clmid == null) break;
                    Element column = doc.GetElement(clmid);
                    //vẽ thép đai
                    var cur=GetPointCol(column);
					IList<Curve> curve1s = new List<Curve>();
                    curve1s.Add(Line.CreateBound(cur[0], cur[1]));
                    curve1s.Add(Line.CreateBound(cur[1], cur[2]));
                    curve1s.Add(Line.CreateBound(cur[2], cur[3]));
                    curve1s.Add(Line.CreateBound(cur[3], cur[0]));
					var vectorX = cur[1] - cur[0];
					var vectorY = cur[3] - cur[0];
                    var tran = new Transaction(doc, "dai");
                    tran.Start();
					RebarBarType barType1 = RebarBarType.Create(doc);
					barType1.BarDiameter = Convert.ToDouble(clm.Thepdai) / 304.8;
                    var kc=Convert.ToDouble(clm.Kcdai);
					Rebar rebar1 = Rebar.CreateFromCurves(doc, RebarStyle.Standard, barType1, null, null, column, XYZ.BasisZ, curve1s,
																 RebarHookOrientation.Left, RebarHookOrientation.Left, false, true);
					RebarShape rbsh = getRebarShape(doc, "M_T1");
					rebar1.LookupParameter("Shape").Set(rbsh.Id);
					rebar1.GetShapeDrivenAccessor().SetLayoutAsNumberWithSpacing(Convert.ToInt32(Convert.ToDouble(clm.L)*1000/kc),kc.MmToFoot(), true, true, true);
                    tran.Commit();
					Level level = doc.GetElement(column.LevelId) as Level;
                    //lấy vextor thép
                    List<List<Curve>> curves = Getlstcurve(clm, clmid);
                    LocationPoint locp = column.Location as LocationPoint;
                    XYZ location = (column as FamilyInstance).HandOrientation;
                    double a = Convert.ToDouble(clm.A) /304.8*1000;
                    double b = Convert.ToDouble(clm.H) /304.8 *1000;
                    foreach (List<Curve> curve in curves)
                    {
                        using (Transaction transaction = new Transaction(doc, "Add Rebar to Column"))
                        {
                            transaction.Start();
                            RebarBarType barType = RebarBarType.Create(doc);
                            barType.BarDiameter = Convert.ToDouble(clm.Phi) / 304.8 ;
                            // Tạo đối tượng Rebar
                            Rebar rebar = Rebar.CreateFromCurves(doc, RebarStyle.Standard, barType, null, null, column, location, curve,
                                                                 RebarHookOrientation.Left, RebarHookOrientation.Left, false, true);
                            if (rebar != null)
                            {
                                rebar.GetShapeDrivenAccessor().SetLayoutAsFixedNumber(2, (b-2*a), true, true, true);
                            }
                            transaction.Commit();
                        }

                    }

                }
                dlg = frm.ShowDialog();
            }//vẽ thép
            return Result.Succeeded;        
        }
		private RebarShape getRebarShape(Autodesk.Revit.DB.Document doc, string rebaname)
		{
			//hinh dang thep
			FilteredElementCollector fec = new FilteredElementCollector(doc).OfClass(typeof(RebarShape));
			IList<Element> shapeElems = fec.ToElements();
			foreach (var shapeElem in shapeElems)
			{
				RebarShape shape = shapeElem as RebarShape;
				if (shape.Name.Contains(rebaname))
					return shape;
			}
			return null;
		}
		private List<List<Curve>> Getlstcurve(cls_columns cls_Columns, ElementId id)
        {
            //method lấy vector thép
            double a = Convert.ToDouble( cls_Columns.A)/304.8*1000;
            List<List<Curve>> curves = new List<List<Curve>>();
            Element column = doc.GetElement(id);
            BoundingBoxXYZ bbox = column.get_BoundingBox(null);

            double b = Convert.ToDouble(cls_Columns.B)/304.8*1000;
            double sothanhthep = Convert.ToDouble(cls_Columns.Sophi);
            double sokc = Math.Round(sothanhthep / 2 -1);
            double kc =  (b - 2*a  )/ sokc;

            double bd = 0;
            for(int i=0; i<= sokc; i++)
            {
                List<Curve> c = new List<Curve>();
                XYZ startPoint = new XYZ(bbox.Min.X + a, bbox.Min.Y + a +bd, bbox.Min.Z); // Điểm bắt đầu
                XYZ endPoint = new XYZ(bbox.Min.X + a, bbox.Min.Y + a +bd, bbox.Max.Z);
                bd = bd + kc/**304.8*/;
                c.Add(Line.CreateBound(startPoint, endPoint));
                curves.Add(c);
            }

            return curves;
        }
		public List<XYZ> GetPointCol(Element coll)//lấy 4 điểmm của cột
		{
			var col = coll as FamilyInstance;
			var oriPoint = (col.Location as LocationPoint).Point.Add(XYZ.BasisZ*25.MmToFoot());
			var type = doc.GetElement(col.GetTypeId()) as ElementType;
			var w = type.LookupParameter("b").AsDouble()-50.MmToFoot();
			var l = type.LookupParameter("h").AsDouble()-50.MmToFoot();
			var facing = col.FacingOrientation.Normalize();
			var hand = col.HandOrientation.Normalize();
			var p1 = oriPoint.Add(-hand * w / 2 + -facing * l / 2);
			var p2 = oriPoint.Add(hand * w / 2 + -facing * l / 2);
			var p3 = p2.Add(facing * l);
			var p4 = p1.Add(facing * l);
			return new List<XYZ>() { p1, p2, p3, p4 };
		}
	}

}
