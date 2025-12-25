using Lab05.BUS.services;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmreport : Form
    {
        private readonly StudentService studentService = new StudentService();

        public frmreport()
        {
            InitializeComponent();
            Load += Frmreport_Load;
            btnExport.Click += BtnExport_Click;
        }

        private async void Frmreport_Load(object sender, EventArgs e)
        {
            Logger.Log("frmreport.Frmreport_Load start");
            var list = await Task.Run(() => studentService.GetAll());

            dgv.AutoGenerateColumns = true;
            dgv.DataSource = list.Select(s => new
            {
                s.StudentID,
                s.FullName,
                s.AverageScore,
                Faculty = s.Faculty != null ? s.Faculty.FacultyName : "",
                Major = s.Major != null ? s.Major.Name : ""
            }).ToList();
            Logger.Log($"frmreport.Frmreport_Load end loaded={list?.Count}");
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            Logger.Log("frmreport.BtnExport_Click start");
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = "students_report.csv";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                var list = await Task.Run(() => studentService.GetAll());

                var sb = new StringBuilder();
                sb.AppendLine("StudentID,FullName,AverageScore,Faculty,Major");

                foreach (var s in list)
                {
                    sb.AppendLine(string.Format("\"{0}\",\"{1}\",{2},\"{3}\",\"{4}\"",
                        s.StudentID,
                        (s.FullName ?? "").Replace("\"", "\"\""),
                        s.AverageScore,
                        s.Faculty?.FacultyName ?? "",
                        s.Major?.Name ?? ""));
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất báo cáo thành công!");
                Logger.Log("frmreport.BtnExport_Click end exported=" + sfd.FileName);
            }
        }
    }
}
