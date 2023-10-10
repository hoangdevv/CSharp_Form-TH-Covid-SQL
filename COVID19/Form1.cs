using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COVID19.Model;
namespace COVID19
{
    public partial class Form1 : Form
    {
        ModelBenhNhan modelBenhNhan = new ModelBenhNhan();
        int index = -1;
        public Form1()
        {
            InitializeComponent();
        }
        void loadDGV()
        {
            List<BenhNhan> listBenhNhan = modelBenhNhan.BenhNhans.ToList();
            dataGridView1.Rows.Clear();
            foreach(var item in listBenhNhan)
            {

                dataGridView1.Rows.Add(item.MaBN, item.TenBN, item.TinhTrang.TenTT,item.GhiChu,item.BNTXG);
            }
        }

        bool kiemtra()
        {
            if(txtID.Text == "" || txtName.Text == "")
            {
                MessageBox.Show("“Vui lòng nhập đầy đủ thông tin bệnh nhân!");
                return false;
            }
            if(txtID.Text.Length != 6)
            {
                MessageBox.Show("Mã bệnh nhân phải có 6 kí tự!");
                return false;
            }
            return true;
        }
        void clear()
        {
            txtID.Clear();
            txtName.Clear();
            txtNote.Clear();
            cbbLayNhiemTu.SelectedIndex = 0;
            cbbTinhTrang.SelectedIndex = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            List<BenhNhan> listBenhNhan = modelBenhNhan.BenhNhans.ToList();
            List<TinhTrang> listTinhTrang = modelBenhNhan.TinhTrangs.ToList();
            cbbTinhTrang.DataSource = listTinhTrang;
            cbbTinhTrang.DisplayMember = "TenTT";
            cbbTinhTrang.ValueMember = "MaTT";
            List<BenhNhan> danhSachCombo = new List<BenhNhan>();
            danhSachCombo.Add(new BenhNhan { MaBN = "", BNTXG = "" }); 
            danhSachCombo.AddRange(listBenhNhan);
            cbbLayNhiemTu.DataSource = danhSachCombo;
            cbbLayNhiemTu.DisplayMember = "MaBN";
            cbbLayNhiemTu.SelectedIndex = 0;
            loadDGV();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<BenhNhan> listBenhNhan = modelBenhNhan.BenhNhans.ToList();
            index = e.RowIndex;
            if(index < 0)
            {
                return;
            }
            txtID.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            cbbTinhTrang.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            txtNote.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();

            if (dataGridView1.Rows[index].Cells[5].Value == null)
            {
                cbbLayNhiemTu.Text = "";
            }
            else
            {
                cbbLayNhiemTu.Text = dataGridView1.Rows[index].Cells[5].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var context = new ModelBenhNhan();
            using(var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (!kiemtra())
                    {
                        return;
                    }
                    string id = txtID.Text;
                    string name = txtName.Text;
                    int tinhTrang = Convert.ToInt32(cbbTinhTrang.SelectedValue);
                    string note = txtNote.Text;
                    string layNhiem = cbbLayNhiemTu.Text;
                    var masoBenhNhan = modelBenhNhan.BenhNhans.FirstOrDefault(p => p.MaBN == txtID.Text);
                    if(masoBenhNhan != null) // trùng id
                    {
                        masoBenhNhan.MaBN = id;
                        masoBenhNhan.TenBN = name;
                        masoBenhNhan.MaTT = tinhTrang;
                        masoBenhNhan.GhiChu = note;
                        masoBenhNhan.BNTXG = layNhiem;
                        modelBenhNhan.SaveChanges();
                        loadDGV();
                        transaction.Commit();
                        MessageBox.Show("Cập nhật dữ liêu thành công");
                        clear();
                    }
                    else
                    {
                        BenhNhan benhNhan = new BenhNhan()
                        {
                            MaBN = id ,
                            TenBN = name,
                            MaTT = tinhTrang,
                            GhiChu = note,
                            BNTXG = layNhiem
                        };
                        modelBenhNhan.BenhNhans.Add(benhNhan);
                        modelBenhNhan.SaveChanges();
                        loadDGV();
                        transaction.Commit();
                        MessageBox.Show("Thêm thành công");
                        clear();

                    }
                }
                catch (Exception)
                {

                    transaction.Rollback();
                    MessageBox.Show("Lỗi");
                }
            }
        }

        private void truyViếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbbLayNhiemTu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
