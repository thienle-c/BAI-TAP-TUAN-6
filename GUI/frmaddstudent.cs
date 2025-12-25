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
using System.Windows.Forms;

namespace GUI
{

    public partial class frmaddstudent : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private readonly Button btnExportCsv;

        public frmaddstudent()
        {
            InitializeComponent();
            // add export button programmatically (dock bottom so it doesn't depend on designer)
            btnExportCsv = new Button { Text = "Export Students (Excel CSV)", Dock = DockStyle.Bottom, Height = 30 };
            btnExportCsv.Click += BtnExportCsv_Click;
            Controls.Add(btnExportCsv);
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            Logger.Log("frmaddstudent.Form1_Load start");
            try
            {
                SetGridViewStyle(dgvStudent);

                var listFacultys = await Task.Run(() => facultyService.GetAll());
                var listStudents = await Task.Run(() => studentService.GetAll());

                if (listStudents == null || listStudents.Count == 0)
                {
                    // don't block UI with message boxes at startup
                }

                FillFalcultyCombobox(listFacultys);
                BindGrid(listStudents);
                Logger.Log($"frmaddstudent.Form1_Load loaded students={listStudents?.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Logger.Log("frmaddstudent.Form1_Load end");
        }

        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            listFacultys.Insert(0, new Faculty());
            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }
        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                // Dùng tên cột đã đặt ở hàm setGridViewStyle
                dgvStudent.Rows[index].Cells["StudentID"].Value = item.StudentID;
                dgvStudent.Rows[index].Cells["FullName"].Value = item.FullName;

                if (item.Faculty != null)
                    dgvStudent.Rows[index].Cells["Faculty"].Value = item.Faculty.FacultyName;

                dgvStudent.Rows[index].Cells["AverageScore"].Value = item.AverageScore;

                // Hiển thị ảnh vào cột "Avatar" (deferred/placeholder)
                ShowAvatar(dgvStudent.Rows[index]);
            }
        }

        private void ShowAvatar(DataGridViewRow row)
        {
            // Avoid loading images into grid synchronously (expensive). Leave cell null.
            row.Cells["Avatar"].Value = null;
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

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog instance
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set the filter to allow only image files
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                // Show the dialog and check if the user selected a file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Store the selected file path
                    avatarFilePath = openFileDialog.FileName;

                    // Set the picture box image to a copy to avoid locking the file
                    try
                    {
                        var data = File.ReadAllBytes(avatarFilePath);
                        using (var ms = new MemoryStream(data))
                        {
                            var tmp = Image.FromStream(ms);
                            var bmp = new Bitmap(tmp);
                            if (picAvatar.Image != null) picAvatar.Image.Dispose();
                            picAvatar.Image = bmp;
                        }
                    }
                    catch { picAvatar.Image = null; }
                }
            }
        }


        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                MessageBox.Show("Please enter a Student ID.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtFullname.Text))
            {
                MessageBox.Show("Please enter the student's full name.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtGPA.Text) || !double.TryParse(txtGPA.Text, out _))
            {
                MessageBox.Show("Please enter a valid GPA.");
                return false;
            }
            return true;
        }


        private async Task LoadAvatarAsync(string studentID)
        {
            Logger.Log($"frmaddstudent.LoadAvatarAsync start id={studentID}");
            string folderPath = Path.Combine(Application.StartupPath, "Images");
            var student = await Task.Run(() => studentService.FindStudentById(studentID));

            if (student?.Avatar is string avatar && !string.IsNullOrEmpty(avatar))
            {
                string avatarFilePath = Path.Combine(folderPath, avatar);

                if (File.Exists(avatarFilePath))
                {
                    try
                    {
                        byte[] data = await Task.Run(() => File.ReadAllBytes(avatarFilePath));
                        using (var ms = new MemoryStream(data))
                        {
                            var tmp = Image.FromStream(ms);
                            var bmp = new Bitmap(tmp);
                            if (picAvatar.Image != null) picAvatar.Image.Dispose();
                            picAvatar.Image = bmp;
                            Logger.Log("frmaddstudent.LoadAvatarAsync image set");
                        }
                    }
                    catch { picAvatar.Image = null; }
                }
                else
                {
                    picAvatar.Image = null;
                }
            }
            else
            {
                picAvatar.Image = null;
            }
            Logger.Log($"frmaddstudent.LoadAvatarAsync end id={studentID}");
        }
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
        private void ClearData()
        {
            txtStudentID.Clear();
            txtFullname.Clear();
            txtGPA.Clear();
            cmbFaculty.SelectedIndex = 0; // Or reset to a default faculty if necessary
            picAvatar.Image = null; // Reset the avatar picture box
        }



        private async void chkChuaDK_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Log($"frmaddstudent.chkChuaDK_CheckedChanged start checked={this.chkUnregisterMajor.Checked}");
            List<Student> listStudents;
            if (this.chkUnregisterMajor.Checked)
                listStudents = await Task.Run(() => studentService.GetAllHasNoMajor());
            else
                listStudents = await Task.Run(() => studentService.GetAll());

            BindGrid(listStudents);
            Logger.Log($"frmaddstudent.chkChuaDK_CheckedChanged end loaded={listStudents?.Count}");
        }


        private async void btnAddUpdate_Click(object sender, EventArgs e)
        {
            Logger.Log($"frmaddstudent.btnAddUpdate_Click start id={txtStudentID.Text}");
            try
            {
                if (!ValidateInput()) return;

                var student = await Task.Run(() => studentService.FindStudentById(txtStudentID.Text)) ?? new Student();

                // Update student details
                student.StudentID = txtStudentID.Text;
                student.FullName = txtFullname.Text;
                student.AverageScore = double.Parse(txtGPA.Text);
                if (cmbFaculty.SelectedValue is int fid)
                    student.FacultyID = fid;
                else
                    student.FacultyID = int.Parse(cmbFaculty.SelectedValue?.ToString() ?? "0");

                // Handle avatar file upload
                if (!string.IsNullOrEmpty(avatarFilePath))
                {
                    string avatarFileName = SaveAvatar(avatarFilePath, txtStudentID.Text);
                    if (!string.IsNullOrEmpty(avatarFileName))
                    {
                        student.Avatar = avatarFileName;
                    }
                }

                // Save student (Insert or Update)
                await Task.Run(() => studentService.InsertOrUpdateStudent(student));

                // Refresh the DataGridView
                var all = await Task.Run(() => studentService.GetAll());
                BindGrid(all);

                // Clear input fields after adding/updating
                ClearData();
                Logger.Log($"frmaddstudent.btnAddUpdate_Click end id={student.StudentID}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding/updating student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            Logger.Log($"frmaddstudent.BtnDelete_Click start id={txtStudentID.Text}");
            try
            {
                var selectedStudentID = txtStudentID.Text; // Ensure the student ID is available
                var student = await Task.Run(() => studentService.FindStudentById(selectedStudentID));

                if (student != null)
                {
                    await Task.Run(() => studentService.DeleteStudent(student.StudentID));
                    MessageBox.Show("Student deleted successfully.");

                    var all = await Task.Run(() => studentService.GetAll());
                    BindGrid(all);
                    Logger.Log($"frmaddstudent.BtnDelete_Click end deleted={selectedStudentID}");
                }
                else
                {
                    MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnExportCsv_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = "students_export.csv";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                var list = await Task.Run(() => studentService.GetAll());

                var sb = new StringBuilder();
                sb.AppendLine("StudentID,FullName,AverageScore,Faculty,Major");
                foreach (var s in list)
                {
                    var full = s.FullName?.Replace("\"","\"\"") ?? "";
                    var faculty = s.Faculty?.FacultyName ?? "";
                    var major = s.Major?.Name ?? "";
                    sb.AppendLine($"\"{s.StudentID}\",\"{full}\",{s.AverageScore},\"{faculty}\",\"{major}\"");
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Export completed: " + sfd.FileName);
            }
        }

        private async void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có bấm vào dòng dữ liệu (không phải tiêu đề) không
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudent.Rows[e.RowIndex];

                // 1. Hiển thị thông tin văn bản
                // Lưu ý: Sử dụng tên cột chính xác bạn đã đặt trong hàm setGridViewStyle
                txtStudentID.Text = row.Cells["StudentID"].Value?.ToString();
                txtFullname.Text = row.Cells["FullName"].Value?.ToString();
                txtGPA.Text = row.Cells["AverageScore"].Value?.ToString();

                // 2. Hiển thị Khoa trên ComboBox
                string facultyName = row.Cells["Faculty"].Value?.ToString();
                if (!string.IsNullOrEmpty(facultyName))
                {
                    cmbFaculty.Text = facultyName;
                }
                else
                {
                    cmbFaculty.SelectedIndex = 0;
                }

                // 3. Hiển thị ảnh đại diện lên PictureBox
                // Lấy tên file ảnh từ database (thông qua service) dựa trên StudentID
                var student = await Task.Run(() => studentService.FindStudentById(txtStudentID.Text));
                if (student != null && !string.IsNullOrEmpty(student.Avatar))
                {
                    string folderPath = Path.Combine(Application.StartupPath, "Images");
                    string avatarPath = Path.Combine(folderPath, student.Avatar);

                    if (File.Exists(avatarPath))
                    {
                        // Giải phóng ảnh cũ trước khi nạp ảnh mới để tránh lỗi khóa file
                        if (picAvatar.Image != null) picAvatar.Image.Dispose();

                        try
                        {
                            byte[] data = await Task.Run(() => File.ReadAllBytes(avatarPath));
                            using (var ms = new MemoryStream(data))
                            {
                                var tmp = Image.FromStream(ms);
                                var bmp = new Bitmap(tmp);
                                picAvatar.Image = bmp;
                            }
                        }
                        catch { picAvatar.Image = null; }
                    }
                    else
                    {
                        picAvatar.Image = null;
                    }
                }
                else
                {
                    picAvatar.Image = null;
                }
            }
        }

        private void đăngKýChuyênNgànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmregistermajor frm = new frmregistermajor();
            frm.ShowDialog();
        }
    }
}




