using Lab05.BUS.services;
using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace GUI
{

    public partial class FrmQLSV : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();

        public FrmQLSV()
        {
            InitializeComponent();
            this.Load += FrmQLSV_Load;
        }
       



        // Add a PictureBox control to your form, if you haven't done so already
        // Example: private PictureBox picAvatar = new PictureBox();




        private void SetGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Add Avatar column as a DataGridViewImageColumn if not already added
            if (!dgview.Columns.Contains("Avatar"))
            {
                var avatarColumn = new DataGridViewImageColumn
                {
                    Name = "Avatar",
                    HeaderText = "Avatar",
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                dgview.Columns.Add(avatarColumn);
            }

            // Add other necessary columns (StudentID, FullName, etc.) if not already added
            if (!dgview.Columns.Contains("StudentID"))
                dgview.Columns.Add("StudentID", "MSSV");

            if (!dgview.Columns.Contains("FullName"))
                dgview.Columns.Add("FullName", "Họ Tên");

            if (!dgview.Columns.Contains("Faculty"))
                dgview.Columns.Add("Faculty", "Khoa");

            if (!dgview.Columns.Contains("AverageScore"))
                dgview.Columns.Add("AverageScore", "DTB");
        }


        private string avatarFilePath = string.Empty;

      


        


       
        private string SaveAvatar(string sourceFilePath, string studentID)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileExtension = Path.GetExtension(sourceFilePath);
                string targetFilePath = Path.Combine(folderPath, $"{studentID}{fileExtension}");

                // Only copy the file if it doesn't already exist
                if (!File.Exists(targetFilePath))
                {
                    File.Copy(sourceFilePath, targetFilePath);
                }

                return $"{studentID}{fileExtension}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving avatar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private async void FrmQLSV_Load(object sender, EventArgs e)
        {
            Logger.Log("FrmQLSV.FrmQLSV_Load start");

            // Quick DB connectivity check with short timeout to fail fast
            try
            {
                var cs = System.Configuration.ConfigurationManager.ConnectionStrings["StudentModel"]?.ConnectionString;
                if (string.IsNullOrEmpty(cs))
                {
                    Logger.Log("No connection string named 'StudentModel' found.");
                    MessageBox.Show("Không tìm thấy cấu hình kết nối DB (StudentModel). Kiểm tra App.config.", "DB Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var builder = new SqlConnectionStringBuilder(cs) { ConnectTimeout = 5 };
                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    var openTask = System.Threading.Tasks.Task.Run(() => conn.Open());
                    var finished = await System.Threading.Tasks.Task.WhenAny(openTask, System.Threading.Tasks.Task.Delay(5000));
                    if (finished != openTask || conn.State != System.Data.ConnectionState.Open)
                    {
                        Logger.Log("DB connection test failed or timed out.");
                        MessageBox.Show("Không thể kết nối tới cơ sở dữ liệu trong 5s. Kiểm tra SQL Server (SQLEXPRESS) đang chạy và chuỗi kết nối.", "DB Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("DB check exception: " + ex.ToString());
                MessageBox.Show("Lỗi khi kiểm tra kết nối DB: " + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await LoadStudentsAsync();
            Logger.Log("FrmQLSV.FrmQLSV_Load end");
        }

        private async System.Threading.Tasks.Task LoadStudentsAsync(int page = 1, int pageSize = 100)
        {
            Logger.Log($"FrmQLSV.LoadStudentsAsync start page={page} pageSize={pageSize}");
            try
            {
                // Run DB query on background thread and apply a timeout to avoid indefinite hang
                var queryTask = System.Threading.Tasks.Task.Run(() =>
                {
                    using (var ctx = new StudentModel())
                    {
                        var items = ctx.Students
                            .OrderBy(s => s.StudentID)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Select(s => new
                            {
                                s.StudentID,
                                s.FullName,
                                s.AverageScore,
                                Faculty = s.Faculty.FacultyName,
                                Major = s.Major != null ? s.Major.Name : ""
                            })
                            .ToList();

                        return items;
                    }
                });

                var finished = await System.Threading.Tasks.Task.WhenAny(queryTask, System.Threading.Tasks.Task.Delay(15000));
                if (finished != queryTask)
                {
                    Logger.Log("FrmQLSV.LoadStudentsAsync timed out after 15s");
                    MessageBox.Show("Kết nối cơ sở dữ liệu quá chậm hoặc không phản hồi (timeout). Kiểm tra kết nối DB.", "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var list = queryTask.Result;

                // If there's a grid on FrmQLSV named dgvStudent, set its data source
                var dgv = this.Controls.OfType<DataGridView>().FirstOrDefault();
                if (dgv != null)
                {
                    dgv.DataSource = list;
                }

                Logger.Log($"FrmQLSV.LoadStudentsAsync end loaded={list?.Count}");
            }
            catch (Exception ex)
            {
                Logger.Log("FrmQLSV.LoadStudentsAsync exception: " + ex.ToString());
                MessageBox.Show("Lỗi khi tải dữ liệu sinh viên: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void manageMajorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new frmmajor();
            f.ShowDialog();
        }

        private void exportReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new frmreport();
            f.ShowDialog();
        }

        private void BtnChuyenNganh_Click(object sender, EventArgs e)
        {
            // Gọi Form quản lý chuyên ngành bạn đã tạo
            LoadForm(new frmmajor());
        }

        private void BtnBaoThongKe_Click(object sender, EventArgs e)
        {
            // Gọi Form báo cáo (thường chứa ReportViewer)
            LoadForm(new frmreport());
        }





        private void đăngKýChuyênNgànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmregistermajor frm = new frmregistermajor();
            frm.ShowDialog();
        }
    }
}




