using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;

namespace Nhom3
{
	public partial class FormGiaoDien : System.Windows.Forms.Form
	{
		int index = 0;
		public FormGiaoDien()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;

		}
		public void kichthuoc(string b, string h, string l, Element elm)
		{
			textBox1.Text = b;
			textBox2.Text = h;
			textBox3.Text = l;
			textBox9.Text = elm.Name.ToString();
			textBox8.Text = elm.Id.ToString();


		}

		private void FormGiaoDien_Load(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox2.Text == "CB240-T")
			{
				textBox16.Text = "210";
				textBox17.Text = "170";
				textBox18.Text = "210";
				//textBox4.Text = "210";
				//textBox5.Text = "0.615";
				//textBox6.Text = "0.426";
			}
			if (comboBox2.Text == "CB300-T")
			{
				textBox16.Text = "260";
				textBox17.Text = "210";
				textBox18.Text = "260";
			}
			if (comboBox2.Text == "CB300-V")
			{
				textBox16.Text = "260";
				textBox17.Text = "210";
				textBox18.Text = "260";
			}
			if (comboBox2.Text == "CB400-V")
			{
				textBox16.Text = "350";
				textBox17.Text = "280";
				textBox18.Text = "350";
			}
			if (comboBox2.Text == "CB500-V")
			{
				textBox16.Text = "435";
				textBox17.Text = "300";
				textBox18.Text = "435";
			}

		}

		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox3.Text == "CB240-T")
			{
				textBox19.Text = "210";
				textBox20.Text = "170";
				textBox21.Text = "210";
			}
			if (comboBox3.Text == "CB300-T")
			{
				textBox19.Text = "260";
				textBox20.Text = "260";
				textBox21.Text = "260";
			}
			if (comboBox3.Text == "CB300-V")
			{
				textBox19.Text = "260";
				textBox20.Text = "210";
				textBox21.Text = "260";
			}
			if (comboBox3.Text == "CB400-V")
			{
				textBox19.Text = "350";
				textBox20.Text = "210";
				textBox21.Text = "350";
			}
			if (comboBox3.Text == "CB500-V")
			{
				textBox19.Text = "435";
				textBox20.Text = "300";
				textBox21.Text = "435";
			}
		}

		private void textBox19_TextChanged(object sender, EventArgs e)
		{

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.Text == "B20")
			{
				txt_rb.Text = "11.5";
				txt_rbt.Text = "0.9";
				txt_eb.Text = "22";
			}
			if (comboBox1.Text == "B25")
			{
				txt_rb.Text = "14.5";
				txt_rbt.Text = "1.050";
				txt_eb.Text = "24";
			}
			if (comboBox1.Text == "B30")
			{
				txt_rb.Text = "27.5";
				txt_rbt.Text = "1.15";
				txt_eb.Text = "26";
			}
			if (comboBox1.Text == "B35")
			{
				txt_rb.Text = "19.5";
				txt_rbt.Text = "1.3";
				txt_eb.Text = "27";
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//bê tông
			cls_modul.Chonbetong.Max = comboBox1.Text;
			cls_modul.Chonbetong.Rb = Convert.ToDouble(txt_rb.Text);
			cls_modul.Chonbetong.Rbt = Convert.ToDouble(txt_rbt.Text);
			//thép đai
			cls_modul.Thepdoc.Name = comboBox2.Text;
			cls_modul.Thepdoc.Rs = Convert.ToDouble(textBox16.Text);
			cls_modul.Thepdoc.Rsw = Convert.ToDouble(textBox17.Text);
			cls_modul.Thepdoc.Rsc = Convert.ToDouble(textBox18.Text);
			//thép đai
			cls_modul.Thepdai.Name = comboBox3.Text;
			cls_modul.Thepdai.Rs = Convert.ToDouble(textBox19.Text);
			cls_modul.Thepdai.Rsw = Convert.ToDouble(textBox20.Text);
			cls_modul.Thepdai.Rsc = Convert.ToDouble(textBox21.Text);
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;

		}

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void label7_Click(object sender, EventArgs e)
		{

		}

		private void button5_Click(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedRows.Count > 0)
			{
				foreach (DataGridViewRow row in dataGridView1.SelectedRows)
				{
					if (!row.IsNewRow) // Đảm bảo không xóa hàng mới được tạo trong DataGridView
					{
						dataGridView1.Rows.Remove(row);
						int rowIndex = dataGridView1.CurrentRow.Index;
					}
				}
			}
			else
			{
				MessageBox.Show("Chọn hàng muốn xóa trước khi nhấn nút Xóa.");
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{

			cls_columns cls_Columns = new cls_columns();
			cls_Columns.Stt = cls_modul.danhsachcot.Count.ToString();
			cls_Columns.Id = textBox8.Text;
			cls_Columns.Name = textBox9.Text;
			cls_Columns.B = textBox1.Text;
			cls_Columns.H = textBox2.Text;
			cls_Columns.L = textBox3.Text;
			cls_Columns.A = textBox11.Text;
			cls_Columns.Mchancot = textBox4.Text;
			cls_Columns.Nchancot = textBox5.Text;
			cls_Columns.Mdinhcot = textBox6.Text;
			cls_Columns.Ndinhcot = textBox7.Text;
			//tính As yêu càu
			List<double> doubles = TinhToan(Convert.ToDouble(cls_Columns.Mchancot), Convert.ToDouble(cls_Columns.Nchancot), Convert.ToDouble(cls_Columns.B), Convert.ToDouble(cls_Columns.H), Convert.ToDouble(cls_Columns.A), Convert.ToDouble(cls_Columns.L));
			cls_Columns.Asyc = Math.Round(doubles[0] / 1000, 2).ToString();
			//cls_modul.danhsachcot.Add(cls_Columns);
			cls_modul.danhsachcot.Add(cls_Columns);
			foreach (cls_columns cls in cls_modul.danhsachcot)
			{
				dataGridView1.Rows.Add(cls.Stt, cls.Id, cls.Name, cls.B, cls.H, cls.L, cls.A, cls.Mchancot
		, cls.Nchancot, cls.Mdinhcot, cls.Ndinhcot, cls_Columns.Asyc.ToString());
			}
			//        dataGridView1.Rows.Add(index, textBox8.Text, textBox9.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox11.Text, textBox4.Text
			//, textBox5.Text, textBox6.Text, textBox7.Text, cls_Columns.Asyc.ToString());

			index++;
		}
		public List<double> TinhToan(double M, double N, double b, double h, double a, double l0)
		{
			double Rs = cls_modul.Thepdoc.Rs;
			double Rb = cls_modul.Chonbetong.Rb;
			double Eb = cls_modul.Chonbetong.E;
			List<double> result = new List<double>();
			double h0 = h - a;
			double Za = h - 2 * a;
			double xi_R = 0.519;


			double e1 = DoLechTamTinhHoc(M, N);
			double ea = DoLechTamNgauNhien(l0, h);
			double e0 = DoLechTamBanDau(e1, ea);

			double Lambda = DoManh(l0, h);
			double Eta;
			if (Lambda < 8)
				Eta = 1;
			else
			{
				double I = MoMenQuanTinh(h, b);
				double Theta = HeSoDoLechTam(e0, h);
				Eta = 1 / (1 - N / (Ncr(Theta, Eb, I, l0)));
			}

			double e = Eta * e0 + 0.5 * h - a;

			double x1 = N / (Rb * b);

			//tinh xong x1 ... chia truong hop
			double Ast;
			double Muy;
			if (x1 < 2 * a)
			{
				// tính toán đúng tâm
				Ast = (N * (e - Za) / (Rs * Za)) * (100 * 100);
				Muy = ((Ast + Ast) / (b * 100 * h0 * 100)) * 100;
				result.Add(Ast);
				result.Add(Muy);

				//((Rb * b * h0) / Rsc) * ((alpha_m1 - x1 * (1 - 0.5 * x1)) / (1 - delta));
			}
			else
			{
				if (x1 > xi_R * h0)
				{
					// LT bé
					var n = N / (Rb * b * h0);
					var epxilon = e / h0;
					var gamma = Za / h0;

					double tu1 = ((1 - xi_R) * gamma * n + 2 * xi_R * (n * epxilon - 0.48)) * h0;
					double mau1 = ((1 - xi_R) * gamma + 2 * (n * epxilon - 0.48));
					double xmoi = tu1 / mau1;

					double tu = N * e - Rb * b * xmoi * (h0 - 0.5 * xmoi);
					double mau = Rs * Za;

					Ast = (tu / mau) * (100 * 100);

					Muy = ((Ast + Ast) / (b * 100 * h0 * 100)) * 100;

				}
				else
				{
					// LT lớn
					Ast = ((N * (e + 0.5 * x1 - h0)) / (Rs * Za)) * (100 * 100);
					// kiểm tra muy

					Muy = ((Ast + Ast) / (b * 100 * h0 * 100)) * 100;


				}
				result.Add(Math.Round(Ast, 3));
				result.Add(Math.Round(Muy, 3));

			}

			return result;
			//Ast = (N * e - Rb * b * x * (h0 - x / 2)) / (Rs * Za);
		}
		private double DoManh(double l0, double c)
		{
			return l0 / c;
		}
		private double DoLechTamNgauNhien(double L, double c)
		{
			return Math.Max(L / 600, c / 30);
		}
		private double DoLechTamTinhHoc(double M, double N)
		{
			return M / N;
		}
		private double DoLechTamBanDau(double e1, double ea)
		{
			return Math.Max(e1, ea);
		}
		private double Ncr(double Theta, double Eb, double I, double l0)
		{
			return 2.5 * Theta * Eb * I / (l0 * l0);
		}
		private double MoMenQuanTinh(double c1, double c2)
		{
			return c2 * c1 * c1 * c1 / 12;
		}
		private double HeSoDoLechTam(double e0, double c)
		{
			return (0.2 * e0 + 1.05 * c) / (1.5 * e0 + c);
		}
		private double MoMenQuyDoi(double N, double Eta, double e0)
		{
			return N * Eta * e0;
		}
		private double x_(double Er, double e0, double h0)
		{
			double esp = e0 / h0;
			return (Er + (1 - Er) / (1 + 50 * esp * esp)) * h0;
		}
		/// <summary>
		/// tính toán diện tích cốt thép
		/// </summary>
		/// <param name="phi">đường kính thép dọc</param>
		/// <param name="sothanh">số thanh thép dọc</param>
		/// <returns>As tính toán</returns>
		private double Astinhtoan(double phi, double sothanh)
		{
			double As = sothanh * Math.PI * Math.Pow(phi / 10, 2) / 4;
			return As;
		}


		private void button4_Click(object sender, EventArgs e)
		{
			cls_modul.danhsachcot.Clear();
			//lọc qua các hàng kiểm tra điều kiện As ->> đưa ra kết quả
			foreach (DataGridViewRow cell in dataGridView1.Rows)
			{
				try
				{
					cls_columns cls = new cls_columns();
					//string cot = cell.Cells[0].Value.ToString();
					//cls.Stt = cot;
					string vitri = cell.Cells[1].Value.ToString();
					cls.Id = vitri;
					string name = cell.Cells[2].Value.ToString();
					cls.Name = name;
					string width = cell.Cells[3].Value.ToString();
					cls.B = width;
					string heigh = cell.Cells[4].Value.ToString();
					cls.H = heigh;
					string lengh = cell.Cells[5].Value.ToString();
					cls.L = lengh;
					string a = cell.Cells[6].Value.ToString();
					cls.A = a;
					string mchancot = cell.Cells[7].Value.ToString();
					cls.Mchancot = mchancot;
					string nchancot = cell.Cells[8].Value.ToString();
					cls.Nchancot = nchancot;
					string mdinhcot = cell.Cells[9].Value.ToString();
					cls.Mdinhcot = mdinhcot;
					string ndinhcot = cell.Cells[10].Value.ToString();
					cls.Ndinhcot = ndinhcot;

					string Asyc = cell.Cells[11].Value.ToString();
					cls.Asyc = Asyc;
					string Phi = cell.Cells[12].Value.ToString();
					cls.Phi = Phi;
					string Thanh = cell.Cells[13].Value.ToString();
					cls.Sophi = Thanh;
					// diện tích cốt thép tính toán
					double Astt = Math.Round(Astinhtoan(Convert.ToDouble(Phi), Convert.ToDouble(Thanh)), 2);
					cell.Cells[14].Value = Astt.ToString();
					cls.Astt = Astt.ToString();
					if (Convert.ToDouble(Phi) < 24)
					{
						cls.Thepdai = 6.ToString();
						cell.Cells[15].Value = cls.Thepdai.ToString();

					}
					else
					{
						cls.Thepdai = 8.ToString();
						cell.Cells[15].Value = cls.Thepdai.ToString();

					}
					cls.Kcdai = (Convert.ToDouble(Phi) * 10).ToString();
					cell.Cells[16].Value = cls.Kcdai.ToString();

					cell.Cells[17].Value = "Thỏa Mãn";
					if (Astt < Convert.ToDouble(Asyc)) cell.Cells[17].Value = "Không Thỏa Mãn";

					cls_modul.danhsachcot.Add(cls);
				}
				catch (Exception ex) { }

			}
			//DialogResult = DialogResult.Yes;

		}

		private void button3_Click(object sender, EventArgs e)
		{
			dataGridView1.Rows.Clear();
			cls_modul.danhsachcot.Clear();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			//List<int> lstint = ExtractNumbersFromTextBox(textBox10);
			//try
			//{
			//    cls_modul.cotvethep.Clear();
			//    dataGridView2.Rows.Clear();
			//    foreach (int a in lstint)
			//    {
			//        foreach (cls_columns cls in cls_modul.danhsachcot)
			//        {
			//            if (a.ToString() == cls.Id)
			//            {
			//                cls_modul.cotvethep.Add(cls);
			//                dataGridView2.Rows.Add(cls.Stt, cls.Id, cls.Name, cls.B, cls.H, cls.L, cls.Asyc, cls.Phi, cls.Sophi, cls.Astt, cls.Thepdai, cls.Kcdai);
			//            }
			//        }

			//    }

			//}
			//catch
			//{
			//    MessageBox.Show("Chưa chọn đối tượng.");
			//}
		}
		private List<int> ExtractNumbersFromTextBox(TextBox textBox)
		{
			string text = textBox.Text; // Lấy văn bản từ TextBox
			List<int> numbers = new List<int>();

			// Tách các ký tự từ văn bản và chỉ giữ lại các số
			string[] parts = text.Split(new char[] { ' ', ',', '.', ':', '\t' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string part in parts)
			{
				int number;
				if (int.TryParse(part, out number))
				{
					numbers.Add(number);
				}
			}

			return numbers;
		}

		private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void groupBox6_Enter(object sender, EventArgs e)
		{

		}

		private void groupBox7_Enter(object sender, EventArgs e)
		{

		}

		private void button9_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void button13_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void button8_Click(object sender, EventArgs e)
		{
			cls_modul.danhsachcot.Clear();
			//lọc qua các hàng kiểm tra điều kiện As ->> đưa ra kết quả
			foreach (DataGridViewRow cell in dataGridView1.Rows)
			{
				try
				{
					cls_columns cls = new cls_columns();
					//string cot = cell.Cells[0].Value.ToString();
					//cls.Stt = cot;
					string vitri = cell.Cells[1].Value.ToString();
					cls.Id = vitri;
					string name = cell.Cells[2].Value.ToString();
					cls.Name = name;
					string width = cell.Cells[3].Value.ToString();
					cls.B = width;
					string heigh = cell.Cells[4].Value.ToString();
					cls.H = heigh;
					string lengh = cell.Cells[5].Value.ToString();
					cls.L = lengh;
					string a = cell.Cells[6].Value.ToString();
					cls.A = a;
					string mchancot = cell.Cells[7].Value.ToString();
					cls.Mchancot = mchancot;
					string nchancot = cell.Cells[8].Value.ToString();
					cls.Nchancot = nchancot;
					string mdinhcot = cell.Cells[9].Value.ToString();
					cls.Mdinhcot = mdinhcot;
					string ndinhcot = cell.Cells[10].Value.ToString();
					cls.Ndinhcot = ndinhcot;

					string Asyc = cell.Cells[11].Value.ToString();
					cls.Asyc = Asyc;
					string Phi = cell.Cells[12].Value.ToString();
					cls.Phi = Phi;
					string Thanh = cell.Cells[13].Value.ToString();
					cls.Sophi = Thanh;
					// diện tích cốt thép tính toán
					double Astt = Math.Round(Astinhtoan(Convert.ToDouble(Phi), Convert.ToDouble(Thanh)), 2);
					cell.Cells[14].Value = Astt.ToString();
					cls.Astt = Astt.ToString();
					//if (Convert.ToDouble(Phi) < 24)
					//{
					//	cls.Thepdai = 6.ToString();
					//	cell.Cells[15].Value = cls.Thepdai.ToString();

					//}
					//else
					//{
					//	cls.Thepdai = 8.ToString();
					//	cell.Cells[15].Value = cls.Thepdai.ToString();

					//}
					//cls.Kcdai = (Convert.ToDouble(Phi) * 10).ToString();
					//cell.Cells[16].Value = cls.Kcdai.ToString();

					cell.Cells[17].Value = "Thỏa Mãn";
					if (Astt < Convert.ToDouble(Asyc) && Convert.ToDouble(cell.Cells[16].Value) > 100 && Convert.ToDouble(cell.Cells[16].Value) < 300) cell.Cells[17].Value = "Không Thỏa Mãn";

					cls_modul.danhsachcot.Add(cls);
				}
				catch (Exception ex) { }

			}
		}

		private void button12_Click(object sender, EventArgs e)
		{
			dataGridView1.Rows.Clear();
			cls_modul.danhsachcot.Clear();
		}

		private void button11_Click(object sender, EventArgs e)
		{
			if (dataGridView1.SelectedRows.Count > 0)
			{
				foreach (DataGridViewRow row in dataGridView1.SelectedRows)
				{
					if (!row.IsNewRow) // Đảm bảo không xóa hàng mới được tạo trong DataGridView
					{
						dataGridView1.Rows.Remove(row);
						int rowIndex = dataGridView1.CurrentRow.Index;
					}
				}
			}
			else
			{
				MessageBox.Show("Chọn hàng muốn xóa trước khi nhấn nút Xóa.");
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{

			cls_columns cls_Columns = new cls_columns();
			cls_Columns.Stt = cls_modul.danhsachcot.Count.ToString();
			cls_Columns.Id = textBox8.Text;
			cls_Columns.Name = textBox9.Text;
			cls_Columns.B = textBox1.Text;
			cls_Columns.H = textBox2.Text;
			cls_Columns.L = textBox3.Text;
			cls_Columns.A = textBox11.Text;
			cls_Columns.Mchancot = textBox4.Text;
			cls_Columns.Nchancot = textBox5.Text;
			cls_Columns.Mdinhcot = textBox6.Text;
			cls_Columns.Ndinhcot = textBox7.Text;
			//tính As yêu càu
			List<double> doubles = TinhToan(Convert.ToDouble(cls_Columns.Mchancot), Convert.ToDouble(cls_Columns.Nchancot), Convert.ToDouble(cls_Columns.B), Convert.ToDouble(cls_Columns.H), Convert.ToDouble(cls_Columns.A), Convert.ToDouble(cls_Columns.L));
			cls_Columns.Asyc = Math.Round(doubles[0] / 1000, 2).ToString();
			//cls_modul.danhsachcot.Add(cls_Columns);
			cls_modul.danhsachcot.Add(cls_Columns);
			var cls = cls_modul.danhsachcot.LastOrDefault();
			dataGridView1.Rows.Add(cls.Stt, cls.Id, cls.Name, cls.B, cls.H, cls.L, cls.A, cls.Mchancot
	, cls.Nchancot, cls.Mdinhcot, cls.Ndinhcot, cls_Columns.Asyc.ToString());
			//        dataGridView1.Rows.Add(index, textBox8.Text, textBox9.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox11.Text, textBox4.Text
			//, textBox5.Text, textBox6.Text, textBox7.Text, cls_Columns.Asyc.ToString());

			index++;
		}

		private void thep_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}
	}
}
