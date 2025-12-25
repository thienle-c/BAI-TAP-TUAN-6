using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmfaculty : Form
    {
        // Use FacultyService to access data and avoid long-lived DbContext
            private readonly Lab05.BUS.services.FacultyService facultyService = new Lab05.BUS.services.FacultyService();

        public frmfaculty()
        {
            InitializeComponent();
            SetupGridView();
        }

        private async void frmfaculty_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Cấu hình bảng hiển thị
        private void SetupGridView()
        {
            dgvFaculty.AutoGenerateColumns = false;
            dgvFaculty.Columns.Clear();
            dgvFaculty.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FacultyID", HeaderText = "Mã Khoa", Name = "colID" });
            dgvFaculty.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FacultyName", HeaderText = "Tên Khoa", Name = "colName" });
            dgvFaculty.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalProfessor", HeaderText = "Tổng số GS", Name = "colProf" });
            dgvFaculty.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Tải dữ liệu từ DB lên GridView
        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            Logger.Log("frmfaculty.LoadDataAsync start");
            var listFaculties = await System.Threading.Tasks.Task.Run(() => facultyService.GetAll());
            dgvFaculty.DataSource = listFaculties;
            Logger.Log($"frmfaculty.LoadDataAsync end loaded={listFaculties?.Count}");
        }

        // Chức năng Thêm hoặc Cập nhật
        private async void btnAddUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Log($"frmfaculty.btnAddUpdate_Click start id={txtFacultyID.Text}");
                if (string.IsNullOrWhiteSpace(txtFacultyID.Text) || string.IsNullOrWhiteSpace(txtFacultyName.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Mã Khoa và Tên Khoa!");
                    return;
                }

                int facultyID = int.Parse(txtFacultyID.Text);

                await System.Threading.Tasks.Task.Run(() =>
                {
                    using (var ctx = new StudentModel())
                    {
                        var f = ctx.Faculties.FirstOrDefault(x => x.FacultyID == facultyID);
                        if (f == null)
                        {
                            f = new Faculty { FacultyID = facultyID };
                            ctx.Faculties.Add(f);
                        }
                        f.FacultyName = txtFacultyName.Text;
                        ctx.SaveChanges();
                    }
                });

                await LoadDataAsync();
                ClearInput();
                MessageBox.Show("Cập nhật dữ liệu thành công!");
                Logger.Log($"frmfaculty.btnAddUpdate_Click end id={txtFacultyID.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm/sửa: " + ex.Message);
            }
        }

        // Chức năng Xóa
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFacultyID.Text)) return;

                int facultyID = int.Parse(txtFacultyID.Text);

                var dr = MessageBox.Show("Bạn có chắc muốn xóa khoa này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes) return;
                Logger.Log($"frmfaculty.btnDelete_Click start id={facultyID}");
                await System.Threading.Tasks.Task.Run(() =>
                {
                    using (var ctx = new StudentModel())
                    {
                        var f = ctx.Faculties.Find(facultyID);
                        if (f != null)
                        {
                            if (f.Students != null && f.Students.Any())
                                throw new Exception("Không thể xóa khoa này vì vẫn còn sinh viên đang theo học!");

                            ctx.Faculties.Remove(f);
                            ctx.SaveChanges();
                        }
                    }
                });

                await LoadDataAsync();
                ClearInput();
                MessageBox.Show("Xóa thành công!");
                Logger.Log($"frmfaculty.btnDelete_Click end id={facultyID}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        // Sự kiện khi click vào một dòng trên bảng
        private void dgvFaculty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvFaculty.Rows[e.RowIndex];
                txtFacultyID.Text = row.Cells["colID"].Value.ToString();
                txtFacultyName.Text = row.Cells["colName"].Value.ToString();
                txtTotalProf.Text = row.Cells["colProf"].Value?.ToString();

                // Khóa ô Mã Khoa khi sửa để tránh sửa khóa chính
                txtFacultyID.ReadOnly = true;
            }
        }

        private void ClearInput()
        {
            txtFacultyID.Clear();
            txtFacultyName.Clear();
            txtTotalProf.Clear();
            txtFacultyID.ReadOnly = false;
        }
    }
}