using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COVID19.Model;
namespace COVID19
{
    public partial class Form2 : Form
    {
        ModelBenhNhan modelBenhNhan = new ModelBenhNhan();
       // List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
        public Form2()
        {
            InitializeComponent();
        }
        void fillcmb()
        {
            List<BenhNhan> listBenhNhan = modelBenhNhan.BenhNhans.ToList();

            // Tạo một danh sách chuỗi kết hợp Mã BN và Tên BV
            List<string> combinedNames = listBenhNhan.Select(bn => $"{bn.MaBN} : {bn.TenBN}").ToList();

            // Gán danh sách chuỗi đã kết hợp vào ComboBox
            comboBox1.DataSource = combinedNames;

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            fillcmb();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPatientMaBN = comboBox1.SelectedValue.ToString(); 


            LoadInfectedAndRelatedPatients(selectedPatientMaBN);
        }
        private void LoadInfectedAndRelatedPatients(string maBN)
        {

            List<string> infectedAndRelatedPatients = GetInfectedPatients(maBN);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = infectedAndRelatedPatients;
        }
        public List<string> GetInfectedPatients(string maBN)
        {
            List<string> infectedPatients = new List<string>();

            string currentPatient = FindFirstPatient(maBN);

            while (!string.IsNullOrEmpty(currentPatient))
            {
                infectedPatients.Add(currentPatient);
                currentPatient = FindNextPatient(currentPatient);
            }

            return infectedPatients;
        }
        private string FindFirstPatient(string maBN)
        {
            using (var context = new ModelBenhNhan()) 
            {
                var F0Patient = context.BenhNhans.FirstOrDefault(bn => bn.BNTXG == null);

                if (F0Patient != null)
                {
                    return F0Patient.MaBN; 
                }
                else
                {
                    return null;
                }
            }
        }

        private string FindNextPatient(string previousPatient)
        {
            using (var context = new ModelBenhNhan())
            {

                var previousPatientInfo = context.BenhNhans.FirstOrDefault(bn => bn.MaBN == previousPatient);
                if (previousPatientInfo != null)
                {
                    var nextPatient = context.BenhNhans.FirstOrDefault(bn => bn.BNTXG == previousPatientInfo.MaBN);
                    if (nextPatient != null)
                    {
                        return nextPatient.MaBN; 
                    }
                }

                return null; 
            }
        }
    }
}
