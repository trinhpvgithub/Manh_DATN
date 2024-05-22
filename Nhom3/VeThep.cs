using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom3
{
	public class VeThep
	{
		public static int[] dkthep = { 6, 8, 10, 12, 14, 16, 18, 20, 22, 25, 28, 30, 32 };
		public static List<RebarBarType> m_rebarTypes = new List<RebarBarType>();
		public static List<RebarHookType> m_hookTypes = new List<RebarHookType>();
		public static void taodkthep(Autodesk.Revit.DB.Document doc)
		{

			RebarBarType rebarType = null;
			using (Transaction t = new Transaction(doc, "Tao thep"))
			{
				t.Start();
				for (int i = 0; i < 13; i++)
				{
					rebarType = RebarBarType.Create(doc);
					rebarType.Name = "Փ" + dkthep[i];
					rebarType.BarDiameter = Util.MmToFoot(dkthep[i]);
					rebarType.MaximumBendRadius = 7.5 * rebarType.BarDiameter;
					rebarType.StirrupTieBendDiameter = 7.5 * rebarType.BarDiameter;
					rebarType.StandardBendDiameter = 7.5 * rebarType.BarDiameter;
					rebarType.StandardHookBendDiameter = 7.5 * rebarType.BarDiameter;
				}
				t.Commit();

			}

		}
		public static void vethepdai(Autodesk.Revit.DB.Document doc, Element ptdam)
		{

			GetRebarTypes(doc);
			RebarBarType barTypeTD = null;
			foreach (RebarBarType rb in m_rebarTypes)
			{
				if (rb.Name == ("Փ" + dkthep[7])) //phi20
				{
					barTypeTD = rb;
					break;
				}

			}
			RebarBarType barTypeCDai = null;
			foreach (RebarBarType rb in m_rebarTypes)
			{
				if (rb.Name == ("Փ" + dkthep[1])) //phi8
				{
					barTypeCDai = rb;
					break;
				}

			}

			//==============kieu moc thep 2 dau
			GetHookTypes(doc);
			RebarHookType hookType = null;
			string kieunoi = "SL"; //SL = sole
			double cdainoi = 0; //chieu dai noi
			double lopbv = Util.MmToFoot(10);
			double kcdai_noithep = Util.MmToFoot(100); //khoang cách thép đai trong vùng nối thép
			double kcdai_giua = Util.MmToFoot(200); //khoang cách thép đai trong vùng nối thép     
			if (kieunoi == "SL")
			{
				cdainoi = Util.MmToFoot(40 * dkthep[7]);
			}
			else
			{
				cdainoi = Util.MmToFoot(30 * dkthep[7]);
			}
			//====kich thuoc dam
			double bdam = doc.GetElement(ptdam.GetTypeId()).GetParameters("b")[0].AsDouble();
			double hdam = doc.GetElement(ptdam.GetTypeId()).GetParameters("h")[0].AsDouble();

			LocationCurve location = ptdam.Location as LocationCurve;
			double cdthongthuy = ptdam.get_Parameter(BuiltInParameter.STRUCTURAL_FRAME_CUT_LENGTH).AsDouble();

			XYZ origin = location.Curve.GetEndPoint(1);

			// ===========ve thep========
			using (Transaction tr = new Transaction(doc))
			{
				tr.Start("ve thep dai dam");
				//=========================== lop bao ve
				RebarCoverType rct = RebarCoverType.Create(doc, "LopBVthepDam", lopbv);
				ptdam.LookupParameter("Rebar Cover - Top Face").Set(rct.Id);
				ptdam.LookupParameter("Rebar Cover - Bottom Face").Set(rct.Id);
				ptdam.LookupParameter("Rebar Cover - Other Faces").Set(rct.Id);
				//========================= vec to chi phuong duoi len tren, trai qua phai
				//XYZ normaltd = new XYZ(0, 0, 1); // phuong B
				//CreateRebar(doc, ptcot, barTypeTD, hookType);
				//===============thep doc========================
				//XYZ rebarLinestart = new XYZ(origin.X - 0.5 * bdam, origin.Y - lopbv, origin.Z - hdam + lopbv);
				//XYZ rebarLineEnd = new XYZ(origin.X - 0.5 * bdam + cdthongthuy + Util.MmToFoot(400), origin.Y - lopbv , origin.Z - hdam + lopbv );
				//Line rebarLine = Line.CreateBound(rebarLinestart, rebarLineEnd);

				// Create the line rebar
				//IList<Curve> curvess = new List<Curve>();
				//curvess.Add(rebarLine);

				//Rebar rebartd = Rebar.CreateFromCurves(doc, Autodesk.Revit.DB.Structure.RebarStyle.Standard, barTypeTD, hookType, hookType,
				//                ptdam, normaltd, curvess, RebarHookOrientation.Right, RebarHookOrientation.Left, true, true);
				//////so thanh + chieu dai noi
				//rebartd.GetShapeDrivenAccessor().SetLayoutAsFixedNumber(3, bdam, true, true, true);


				//XYZ normaltd1 = new XYZ(1, 0, 0); // phuong h
				////CreateRebar(doc, ptcot, barTypeTD, hookType);
				////===============thep doc========================
				//XYZ rebarLinestart1 = new XYZ(origin.X - 0.5 * bdam, origin.Y - lopbv, origin.Z -lopbv);
				//XYZ rebarLineEnd1 = new XYZ(origin.X - 0.5 * bdam, origin.Y - lopbv + cdthongthuy + Util.MmToFoot(400), origin.Z - lopbv);
				//Line rebarLine1 = Line.CreateBound(rebarLinestart1, rebarLineEnd1);

				//// Create the line rebar
				//IList<Curve> curvess1 = new List<Curve>();
				//curvess1.Add(rebarLine1);

				//Rebar rebartd1 = Rebar.CreateFromCurves(doc, Autodesk.Revit.DB.Structure.RebarStyle.Standard, barTypeTD, hookType, hookType,
				//                ptdam, normaltd1, curvess1, RebarHookOrientation.Right, RebarHookOrientation.Left, true, true);
				////////so thanh + chieu dai noi
				//rebartd1.GetShapeDrivenAccessor().SetLayoutAsFixedNumber(3, bdam, true, true, true);

				////==============chan cot - mong len=============

				////==============tang trung gian=================

				////==============dinh cot=======================

				////===============THEP DAI - truong họp noi thep chan cot =============================         
				//========================= vec to chi phuong
				XYZ normal = new XYZ(0, 1, 0);
				// dai vuong
				IList<Curve> curves = new List<Curve>();
				XYZ p0 = new XYZ(origin.X - 0.5 * bdam + lopbv, origin.Y + Util.MmToFoot(200), origin.Z - lopbv);
				XYZ p1 = new XYZ(origin.X + 0.5 * bdam - lopbv, origin.Y + Util.MmToFoot(200), origin.Z - lopbv);
				XYZ p2 = new XYZ(origin.X + 0.5 * bdam - lopbv, origin.Y + Util.MmToFoot(200), origin.Z - hdam + lopbv);
				XYZ p3 = new XYZ(origin.X - 0.5 * bdam + lopbv, origin.Y + Util.MmToFoot(200), origin.Z - hdam + lopbv);

				curves.Add(Line.CreateBound(p0, p1));
				curves.Add(Line.CreateBound(p1, p2));
				curves.Add(Line.CreateBound(p2, p3));
				curves.Add(Line.CreateBound(p3, p0));
				// trong doan noi thep 
				Rebar rebar = Rebar.CreateFromCurves(doc, RebarStyle.StirrupTie, barTypeCDai, hookType, hookType, ptdam, normal, curves,
						RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);
				RebarShape rbsh = getRebarShape(doc, "M_T1");
				rebar.LookupParameter("Shape").Set(rbsh.Id);
				//kc giua cac thanh thanh + chieu dai vung dai thep
				rebar.GetShapeDrivenAccessor().SetLayoutAsMaximumSpacing(kcdai_noithep, cdainoi, true, true, true);
				//// phan con lai
				IList<Curve> curves1 = new List<Curve>();
				//XYZ p00 = new XYZ(origin.X - 0.5 * bdam + lopbv + cdthongthuy, origin.Y  , origin.Z  - lopbv);
				//XYZ p10 = new XYZ(origin.X + 0.5 * bdam - lopbv + cdthongthuy, origin.Y , origin.Z- lopbv);
				//XYZ p20 = new XYZ(origin.X + 0.5 * bdam - lopbv + cdthongthuy, origin.Y  , origin.Z - hdam + lopbv);
				//XYZ p30 = new XYZ(origin.X - 0.5 * bdam + lopbv + cdthongthuy, origin.Y , origin.Z - hdam + lopbv);
				XYZ p00 = new XYZ(origin.X - 0.5 * bdam + lopbv, p0.Y + cdainoi, origin.Z - lopbv);
				XYZ p10 = new XYZ(origin.X + 0.5 * bdam - lopbv, p0.Y + cdainoi, origin.Z - lopbv);
				XYZ p20 = new XYZ(origin.X + 0.5 * bdam - lopbv, p0.Y + cdainoi, origin.Z - hdam + lopbv);
				XYZ p30 = new XYZ(origin.X - 0.5 * bdam + lopbv, p0.Y + cdainoi, origin.Z - hdam + lopbv);

				curves1.Add(Line.CreateBound(p00, p10));
				curves1.Add(Line.CreateBound(p10, p20));
				curves1.Add(Line.CreateBound(p20, p30));
				curves1.Add(Line.CreateBound(p30, p00));
				Rebar rebar1 = Rebar.CreateFromCurves(doc, RebarStyle.StirrupTie, barTypeCDai, hookType, hookType, ptdam, normal, curves1,
						 RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);
				RebarShape rbsh1 = getRebarShape(doc, "M_T1");
				rebar1.LookupParameter("Shape").Set(rbsh1.Id);
				//kc giua cac thanh thanh + chieu dai vung dai thep
				rebar1.GetShapeDrivenAccessor().SetLayoutAsMaximumSpacing(kcdai_giua, cdthongthuy - 2 * cdainoi, true, true, true);

				IList<Curve> curves2 = new List<Curve>();
				//XYZ p00 = new XYZ(origin.X - 0.5 * bdam + lopbv + cdthongthuy, origin.Y  , origin.Z  - lopbv);
				//XYZ p10 = new XYZ(origin.X + 0.5 * bdam - lopbv + cdthongthuy, origin.Y , origin.Z- lopbv);
				//XYZ p20 = new XYZ(origin.X + 0.5 * bdam - lopbv + cdthongthuy, origin.Y  , origin.Z - hdam + lopbv);
				//XYZ p30 = new XYZ(origin.X - 0.5 * bdam + lopbv + cdthongthuy, origin.Y , origin.Z - hdam + lopbv);
				XYZ p01 = new XYZ(origin.X - 0.5 * bdam + lopbv, p0.Y + cdthongthuy - cdainoi, origin.Z - lopbv);
				XYZ p11 = new XYZ(origin.X + 0.5 * bdam - lopbv, p0.Y + cdthongthuy - cdainoi, origin.Z - lopbv);
				XYZ p21 = new XYZ(origin.X + 0.5 * bdam - lopbv, p0.Y + cdthongthuy - cdainoi, origin.Z - hdam + lopbv);
				XYZ p31 = new XYZ(origin.X - 0.5 * bdam + lopbv, p0.Y + cdthongthuy - cdainoi, origin.Z - hdam + lopbv);

				curves2.Add(Line.CreateBound(p01, p11));
				curves2.Add(Line.CreateBound(p11, p21));
				curves2.Add(Line.CreateBound(p21, p31));
				curves2.Add(Line.CreateBound(p31, p01));
				Rebar rebar2 = Rebar.CreateFromCurves(doc, RebarStyle.StirrupTie, barTypeCDai, hookType, hookType, ptdam, normal, curves2,
						 RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);
				RebarShape rbsh2 = getRebarShape(doc, "M_T1");
				rebar2.LookupParameter("Shape").Set(rbsh2.Id);
				//kc giua cac thanh thanh + chieu dai vung dai thep
				rebar2.GetShapeDrivenAccessor().SetLayoutAsMaximumSpacing(kcdai_noithep, cdainoi, true, true, true);

				tr.Commit();
			}

		}
		private static RebarShape getRebarShape(Autodesk.Revit.DB.Document doc, string rebaname)
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
		private static bool GetHookTypes(Autodesk.Revit.DB.Document doc)
		{
			// Initialize the m_hookTypes which used to store all hook types.
			FilteredElementCollector filteredElementCollector = new FilteredElementCollector(doc);
			filteredElementCollector.OfClass(typeof(RebarHookType));
			m_hookTypes = filteredElementCollector.Cast<RebarHookType>().ToList<RebarHookType>();

			// If no hook types in revit return false, otherwise true
			return (0 == m_hookTypes.Count) ? false : true;
		}
		private static bool GetRebarTypes(Autodesk.Revit.DB.Document doc)
		{
			// Initialize the m_rebarTypes which used to store all rebar types.
			// Get all rebar types in revit and add them in m_rebarTypes
			FilteredElementCollector filteredElementCollector = new FilteredElementCollector(doc);
			filteredElementCollector.OfClass(typeof(RebarBarType));
			m_rebarTypes = filteredElementCollector.Cast<RebarBarType>().ToList<RebarBarType>();

			// If no rebar types in revit return false, otherwise true
			return (0 == m_rebarTypes.Count) ? false : true;
		}
	}
}
