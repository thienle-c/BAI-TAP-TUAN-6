using Lab05.DAL.Entities;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;

// ... các using giữ nguyên ...

namespace GUI
{
    public partial class frmsearch : Form
    {

        public frmsearch()
        {
            InitializeComponent();
            SetupFullColumns();

            btnSearch.Click += btnSearch_Click;
            btnReset.Click += btnReset_Click;
            dgvStudent.SelectionChanged += dgvStudent_SelectionChanged;
            LoadFaculty();
            // LoadStudent moved to form load to allow async
        }
        private void SetupFullColumns()
        {
            dgvStudent.AutoGenerateColumns = false; // Tắt tự sinh cột để không bị thừa
            dgvStudent.Columns.Clear(); // Xóa hết cột cũ

            // 1. Cột Mã Số Sinh Viên
            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StudentID", // Phải khớp với tên biến trong câu lệnh LINQ
                HeaderText = "Mã Số SV",
                Name = "colMSSV"
            });

            // 2. Cột Họ Tên
            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Họ Tên",
                Name = "colFullName"
            });

            // 3. Cột Khoa
            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Faculty",
                HeaderText = "Khoa",
                Name = "colFaculty"
            });

            // 4. Cột Điểm Trung Bình
            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AverageScore",
                HeaderText = "Điểm TB",
                Name = "colGPA"
            });

            // 5. Cột Chuyên Ngành
            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Major",
                HeaderText = "Chuyên Ngành",
                Name = "colMajor"
            });
            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Avatar",
                Name = "Avatar",
                Visible = false // Ẩn đi để không làm xấu bảng
            });
        }
       


        // Đảm bảo tên phương thức này khớp chính xác với tên bạn gọi ở dòng 78
        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu chưa chọn gì thì thoát (pattern matching)
            if (!(cboKhoa.SelectedValue is int facultyId))
                return;

            // Load danh sách chuyên ngành tương ứng với khoa đã chọn
            using (var ctx = new StudentModel())
            {
                cboChuyenNganh.DataSource = ctx.Majors
                    .Where(m => m.FacultyID == facultyId)
                    .ToList();
            }

            cboChuyenNganh.DisplayMember = "Name";
            cboChuyenNganh.ValueMember = "MajorID";
            cboChuyenNganh.SelectedIndex = -1; // Để trống lúc mới chọn khoa
        }
        private async void Frmsearch_Load(object sender, EventArgs e)
        {
            LoadFaculty();
            await LoadStudentAsync();
        }

        private void LoadFaculty()
        {
            using (var ctx = new StudentModel())
            {
                var faculties = ctx.Faculties.ToList();
                cboKhoa.DataSource = faculties;
            }
            cboKhoa.DisplayMember = "FacultyName";
            cboKhoa.ValueMember = "FacultyID";
            cboKhoa.SelectedIndex = -1;

            // Đăng ký sự kiện ở đây để tránh bị gọi khi đang khởi tạo dữ liệu
            cboKhoa.SelectedIndexChanged += cboKhoa_SelectedIndexChanged;
        }

        private async Task LoadStudentAsync(int page = 1, int pageSize = 100)
        {
            Logger.Log($"frmsearch.LoadStudentAsync start page={page} pageSize={pageSize}");
            using (var ctx = new StudentModel())
            {
                var query = ctx.Students
                    .Include(s => s.Faculty)
                    .Include(s => s.Major)
                    .OrderBy(s => s.StudentID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(s => new
                    {
                        s.StudentID,
                        s.FullName,
                        s.AverageScore,
                        Faculty = s.Faculty.FacultyName,
                        Major = s.Major != null ? s.Major.Name : "Chưa có",
                        s.Avatar
                    });

                var list = await query.ToListAsync();
                Logger.Log($"frmsearch.LoadStudentAsync loaded {list.Count} items");

                dgvStudent.DataSource = null; // Reset lại nguồn dữ liệu
                dgvStudent.DataSource = list;
                Logger.Log("frmsearch.LoadStudentAsync end");
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            Logger.Log("frmsearch.btnSearch_Click start");
            using (var ctx = new StudentModel())
            {
                var query = ctx.Students.AsQueryable();

                if (!string.IsNullOrWhiteSpace(txtMaSV.Text))
                    query = query.Where(x => x.StudentID.Contains(txtMaSV.Text));

                if (!string.IsNullOrWhiteSpace(txtTenSV.Text))
                    query = query.Where(x => x.FullName.Contains(txtTenSV.Text));

                if (cboKhoa.SelectedValue != null)
                {
                    int facultyId = (int)cboKhoa.SelectedValue;
                    query = query.Where(x => x.FacultyID == facultyId);
                }

                if (cboChuyenNganh.SelectedValue != null)
                {
                    int majorId = (int)cboChuyenNganh.SelectedValue;
                    query = query.Where(x => x.MajorID == majorId);
                }

                if (numDiem.Value > 0)
                {
                    double score = (double)numDiem.Value;
                    query = query.Where(x => x.AverageScore >= score);
                }

                var list = await query
                    .OrderBy(s => s.StudentID)
                    .Select(s => new
                    {
                        s.StudentID,
                        s.FullName,
                        s.AverageScore,
                        Faculty = s.Faculty.FacultyName,
                        Major = s.Major != null ? s.Major.Name : "Chưa có",
                        s.Avatar
                    })
                    .Take(200) // giới hạn tối đa để tránh tải quá nặng
                    .ToListAsync();

                dgvStudent.DataSource = list;
                Logger.Log($"frmsearch.btnSearch_Click end results={list.Count}");
            }
        }

        private async void dgvStudent_SelectionChanged(object sender, EventArgs e)
        {
            Logger.Log("frmsearch.dgvStudent_SelectionChanged start");
            if (dgvStudent.CurrentRow == null) return;

            if (picAvatar.Image != null)
            {
                picAvatar.Image.Dispose();
                picAvatar.Image = null;
            }

            object avatarValue = dgvStudent.CurrentRow.Cells["Avatar"].Value;
            if (avatarValue == null || string.IsNullOrEmpty(avatarValue.ToString())) return;

            string path = Path.Combine(Application.StartupPath, "Images", avatarValue.ToString());

            if (!File.Exists(path)) return;

            try
            {

                byte[] data = await Task.Run(() => File.ReadAllBytes(path));

                // Tạo ảnh từ bytes trên UI thread (sao chép để tránh giữ stream)
                using (var ms = new MemoryStream(data))
                {
                    var tmp = Image.FromStream(ms);
                    var bmp = new Bitmap(tmp);
                    picAvatar.Image = bmp;
                    Logger.Log("frmsearch.dgvStudent_SelectionChanged image loaded");
                }
            }
            catch
            {
                // ignore load errors
            }
            Logger.Log("frmsearch.dgvStudent_SelectionChanged end");
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            Logger.Log("frmsearch.btnReset_Click start");
            txtMaSV.Clear();
            txtTenSV.Clear();
            cboKhoa.SelectedIndex = -1;
            cboChuyenNganh.DataSource = null;
            numDiem.Value = 0;
            if (picAvatar.Image != null) picAvatar.Image.Dispose();
            picAvatar.Image = null;
            await LoadStudentAsync();
            Logger.Log("frmsearch.btnReset_Click end");
        }

        private void lblChuyenNganh_Click(object sender, EventArgs e)
        {

        }
    }
}
