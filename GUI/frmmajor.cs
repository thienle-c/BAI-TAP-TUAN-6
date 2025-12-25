using Lab05.BUS.services;
using Lab05.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmmajor : Form
    {
        private readonly MajorService majorService = new MajorService();
        private readonly FacultyService facultyService = new FacultyService();

        public frmmajor()
        {
            InitializeComponent();

            dgvMajors.AutoGenerateColumns = false;

            dgvMajors.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MajorID",
                HeaderText = "Mã CN"
            });

            dgvMajors.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Tên chuyên ngành"
            });

            Load += FrmMajor_Load;
            cmbFaculty.SelectedIndexChanged += CmbFaculty_SelectedIndexChanged;
            dgvMajors.CellClick += DgvMajors_CellClick;
            btnAddUpdate.Click += BtnAddUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private async void FrmMajor_Load(object sender, EventArgs e)
        {
            cmbFaculty.DataSource = await Task.Run(() => facultyService.GetAll());
            cmbFaculty.DisplayMember = "FacultyName";
            cmbFaculty.ValueMember = "FacultyID";
            cmbFaculty.SelectedIndex = -1;
        }

        private async void CmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Use pattern matching to avoid invalid cast when SelectedValue is not an int
            if (cmbFaculty.SelectedValue is int facultyId)
            {
                dgvMajors.DataSource = await Task.Run(() => majorService.GetAllByFaculty(facultyId));
                return;
            }

            // Fallback: try to read from SelectedItem if it's a Faculty
            if (cmbFaculty.SelectedItem is Faculty f)
            {
                dgvMajors.DataSource = await Task.Run(() => majorService.GetAllByFaculty(f.FacultyID));
                return;
            }

            // Nothing usable selected
            dgvMajors.DataSource = null;
        }

        private void DgvMajors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtMajorID.Text = dgvMajors.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMajorName.Text = dgvMajors.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private async void BtnAddUpdate_Click(object sender, EventArgs e)
        {
            if (!(cmbFaculty.SelectedValue is int facultyId))
            {
                MessageBox.Show("Vui lòng chọn khoa");
                return;
            }
            int.TryParse(txtMajorID.Text, out int majorId);
            string name = txtMajorName.Text.Trim();

            await Task.Run(() =>
            {
                using (var ctx = new StudentModel())
                {
                    var major = ctx.Majors
                        .FirstOrDefault(x => x.MajorID == majorId && x.FacultyID == facultyId);

                    if (major == null)
                        ctx.Majors.Add(new Major
                        {
                            MajorID = majorId,
                            FacultyID = facultyId,
                            Name = name
                        });
                    else
                        major.Name = name;

                    ctx.SaveChanges();
                }
            });

            CmbFaculty_SelectedIndexChanged(null, null);
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            int.TryParse(txtMajorID.Text, out int majorId);
            if (majorId == 0) return;

            await Task.Run(() =>
            {
                using (var ctx = new StudentModel())
                {
                    var m = ctx.Majors.Find(majorId);
                    if (m != null)
                    {
                        ctx.Majors.Remove(m);
                        ctx.SaveChanges();
                    }
                }
            });

            CmbFaculty_SelectedIndexChanged(null, null);
        }
    }
}
