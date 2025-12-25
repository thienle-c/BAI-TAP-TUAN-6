using Lab05.BUS.services;
using Lab05.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmregistermajor : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private readonly MajorService majorService = new MajorService();
        public frmregistermajor()
        {
            InitializeComponent();
            SetupGridView();

            // Đảm bảo các dòng này tồn tại để kết nối code với giao diện
            this.Load += frmRegister_Load;
            this.cmbFaculty.SelectedIndexChanged += cmbFaculty_SelectedIndexChanged;
            this.btnRegister.Click += btnRegister_Click;
            this.chkUnregisterMajor.CheckedChanged += chkUnregisterMajor_CheckedChanged;
        }

        private void SetupGridView()
        {
            dgvStudent.Columns.Clear();

            // 1. Thêm cột Checkbox trước
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "Chọn";
            checkBoxColumn.Name = "colSelect";
            dgvStudent.Columns.Add(checkBoxColumn);

            // 2. Thêm các cột dữ liệu khác
            dgvStudent.Columns.Add("colMSSV", "MSSV");
            dgvStudent.Columns.Add("colHoTen", "Họ Tên");
            dgvStudent.Columns.Add("colKhoa", "Khoa");
            dgvStudent.Columns.Add("colDTB", "ĐTB");

            dgvStudent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void frmRegister_Load(object sender, EventArgs e)
        {
            try

            {
                var listFacultys = facultyService.GetAll();
                FillFalcultyCombobox(listFacultys);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
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
                dgvStudent.Rows[index].Cells["colMSSV"].Value = item.StudentID;
                dgvStudent.Rows[index].Cells["colHoTen"].Value = item.FullName;
                dgvStudent.Rows[index].Cells["colKhoa"].Value = item.Faculty?.FacultyName;
                dgvStudent.Rows[index].Cells["colDTB"].Value = item.AverageScore;

                // Thêm cột Checkbox hoặc trạng thái nếu cần
            }
        }
        private void FillMajorCombobox(List<Major> listMajors)
        {
            // Xóa dữ liệu cũ nếu có
            this.cmbMajor.DataSource = null;

            // Thiết lập dữ liệu nguồn
            this.cmbMajor.DataSource = listMajors;

            // Hiển thị tên chuyên ngành cho người dùng thấy
            this.cmbMajor.DisplayMember = "Name"; // Lưu ý: "Name" phải khớp với tên thuộc tính trong class Major của bạn

            // Giá trị thực sự được chọn sẽ là ID
            this.cmbMajor.ValueMember = "MajorID";
        }
        // Gọi hàm này khi thay đổi ComboBox Khoa
        private async void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Log($"frmregistermajor.cmbFaculty_SelectedIndexChanged start selected={(cmbFaculty.SelectedItem as Faculty)?.FacultyID}");
            Faculty selectedFaculty = cmbFaculty.SelectedItem as Faculty;
            if (selectedFaculty != null)
            {
                var listMajors = await Task.Run(() => majorService.GetAllByFaculty(selectedFaculty.FacultyID));
                FillMajorCombobox(listMajors);

                var listStudents = await Task.Run(() => studentService.GetAllHasNoMajor().Where(s => s.FacultyID == selectedFaculty.FacultyID).ToList());
                BindGrid(listStudents);
                Logger.Log($"frmregistermajor.cmbFaculty_SelectedIndexChanged loaded students={listStudents?.Count}");
            }
            Logger.Log("frmregistermajor.cmbFaculty_SelectedIndexChanged end");
        }
        private async void btnRegister_Click(object sender, EventArgs e)
        {
            Logger.Log("frmregistermajor.btnRegister_Click start");
            
            try
            {
                if (cmbMajor.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn chuyên ngành!");
                    return;
                }

                int selectedMajorID = (int)cmbMajor.SelectedValue;
                var toUpdate = new List<Student>();

                foreach (DataGridViewRow row in dgvStudent.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["colSelect"].Value);
                    if (isSelected)
                    {
                        string studentID = row.Cells["colMSSV"].Value.ToString();
                        var student = await Task.Run(() => studentService.FindStudentById(studentID));
                        if (student != null)
                        {
                            student.MajorID = selectedMajorID;
                            toUpdate.Add(student);
                        }
                    }
                }

                if (toUpdate.Count > 0)
                {
                    await Task.Run(() =>
                    {
                        foreach (var s in toUpdate)
                            studentService.InsertOrUpdateStudent(s);
                    });

                    MessageBox.Show($"Đã đăng ký thành công cho {toUpdate.Count} sinh viên!");
                    Logger.Log($"frmregistermajor.btnRegister_Click updated={toUpdate.Count}");
                    cmbFaculty_SelectedIndexChanged(null, null);
                }
                else
                {
                    MessageBox.Show("Vui lòng tích chọn sinh viên cần đăng ký!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private async void chkUnregisterMajor_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Log($"frmregistermajor.chkUnregisterMajor_CheckedChanged start checked={this.chkUnregisterMajor.Checked}");
            var listStudents = new List<Student>();
            if (this.chkUnregisterMajor.Checked)
                listStudents = await Task.Run(() => studentService.GetAllHasNoMajor());
            else
                listStudents = await Task.Run(() => studentService.GetAll());

            BindGrid(listStudents);
            Logger.Log($"frmregistermajor.chkUnregisterMajor_CheckedChanged end loaded={listStudents?.Count}");
        }
    }
    }

