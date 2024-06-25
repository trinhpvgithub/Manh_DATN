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
                foreach (cls_columns clm in cls_modul.cotvethep)
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
                    Level level = doc.GetElement(column.LevelId) as Level;
                    //lấy vextor thép
                    List<List<Curve>> curves = Getlstcurve(clm, clmid);
                    LocationPoint locp = column.Location as LocationPoint;
                    XYZ location = locp.Point;
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
                                                                 RebarHookOrientation.Left, RebarHookOrientation.Left, true, true);
                            if (rebar != null)
                            {
                                rebar.GetShapeDrivenAccessor().SetLayoutAsFixedNumber(2, (b - 2 * a), true, true, true);
                            }
                            transaction.Commit();
                        }

                    }

                }
                dlg = frm.ShowDialog();
            }//vẽ thép
            return Result.Succeeded;        
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

    }

}
