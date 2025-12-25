using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    partial class frmsearch
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox grpSearch;
        private Label lblMaSV;
        private Label lblTenSV;
        private Label lblKhoa;
        private Label lblChuyenNganh;
        private Label lblDiem;

        private TextBox txtMaSV;
        private TextBox txtTenSV;
        private ComboBox cboKhoa;
        private ComboBox cboChuyenNganh;
        private NumericUpDown numDiem;

        private Button btnSearch;
        private Button btnReset;

        private DataGridView dgvStudent;
        private PictureBox picAvatar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.lblMaSV = new System.Windows.Forms.Label();
            this.txtMaSV = new System.Windows.Forms.TextBox();
            this.lblTenSV = new System.Windows.Forms.Label();
            this.txtTenSV = new System.Windows.Forms.TextBox();
            this.lblKhoa = new System.Windows.Forms.Label();
            this.cboKhoa = new System.Windows.Forms.ComboBox();
            this.lblChuyenNganh = new System.Windows.Forms.Label();
            this.cboChuyenNganh = new System.Windows.Forms.ComboBox();
            this.lblDiem = new System.Windows.Forms.Label();
            this.numDiem = new System.Windows.Forms.NumericUpDown();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.dgvStudent = new System.Windows.Forms.DataGridView();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.lblMaSV);
            this.grpSearch.Controls.Add(this.txtMaSV);
            this.grpSearch.Controls.Add(this.lblTenSV);
            this.grpSearch.Controls.Add(this.txtTenSV);
            this.grpSearch.Controls.Add(this.lblKhoa);
            this.grpSearch.Controls.Add(this.cboKhoa);
            this.grpSearch.Controls.Add(this.lblChuyenNganh);
            this.grpSearch.Controls.Add(this.cboChuyenNganh);
            this.grpSearch.Controls.Add(this.lblDiem);
            this.grpSearch.Controls.Add(this.numDiem);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.btnReset);
            this.grpSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSearch.Location = new System.Drawing.Point(0, 0);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(741, 160);
            this.grpSearch.TabIndex = 2;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Tìm kiếm sinh viên";
            // 
            // lblMaSV
            // 
            this.lblMaSV.Location = new System.Drawing.Point(20, 30);
            this.lblMaSV.Name = "lblMaSV";
            this.lblMaSV.Size = new System.Drawing.Size(120, 25);
            this.lblMaSV.TabIndex = 0;
            this.lblMaSV.Text = "MASV";
            // 
            // txtMaSV
            // 
            this.txtMaSV.Location = new System.Drawing.Point(150, 30);
            this.txtMaSV.Name = "txtMaSV";
            this.txtMaSV.Size = new System.Drawing.Size(200, 20);
            this.txtMaSV.TabIndex = 1;
            // 
            // lblTenSV
            // 
            this.lblTenSV.Location = new System.Drawing.Point(20, 60);
            this.lblTenSV.Name = "lblTenSV";
            this.lblTenSV.Size = new System.Drawing.Size(120, 25);
            this.lblTenSV.TabIndex = 2;
            this.lblTenSV.Text = "TENSV";
            // 
            // txtTenSV
            // 
            this.txtTenSV.Location = new System.Drawing.Point(150, 66);
            this.txtTenSV.Name = "txtTenSV";
            this.txtTenSV.Size = new System.Drawing.Size(200, 20);
            this.txtTenSV.TabIndex = 3;
            // 
            // lblKhoa
            // 
            this.lblKhoa.Location = new System.Drawing.Point(446, 63);
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.Size = new System.Drawing.Size(100, 23);
            this.lblKhoa.TabIndex = 4;
            this.lblKhoa.Text = "khoa";
            // 
            // cboKhoa
            // 
            this.cboKhoa.Location = new System.Drawing.Point(551, 57);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Size = new System.Drawing.Size(121, 21);
            this.cboKhoa.TabIndex = 5;
            // 
            // lblChuyenNganh
            // 
            this.lblChuyenNganh.Location = new System.Drawing.Point(445, 30);
            this.lblChuyenNganh.Name = "lblChuyenNganh";
            this.lblChuyenNganh.Size = new System.Drawing.Size(100, 23);
            this.lblChuyenNganh.TabIndex = 6;
            this.lblChuyenNganh.Text = "chuyên ngành";
            this.lblChuyenNganh.Click += new System.EventHandler(this.lblChuyenNganh_Click);
            // 
            // cboChuyenNganh
            // 
            this.cboChuyenNganh.Location = new System.Drawing.Point(551, 30);
            this.cboChuyenNganh.Name = "cboChuyenNganh";
            this.cboChuyenNganh.Size = new System.Drawing.Size(121, 21);
            this.cboChuyenNganh.TabIndex = 7;
            // 
            // lblDiem
            // 
            this.lblDiem.Location = new System.Drawing.Point(397, 63);
            this.lblDiem.Name = "lblDiem";
            this.lblDiem.Size = new System.Drawing.Size(100, 23);
            this.lblDiem.TabIndex = 8;
            // 
            // numDiem
            // 
            this.numDiem.Location = new System.Drawing.Point(552, 84);
            this.numDiem.Name = "numDiem";
            this.numDiem.Size = new System.Drawing.Size(120, 20);
            this.numDiem.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(374, 126);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "tìm";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(493, 126);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(52, 28);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "reset";
            // 
            // dgvStudent
            // 
            this.dgvStudent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStudent.Location = new System.Drawing.Point(0, 160);
            this.dgvStudent.Name = "dgvStudent";
            this.dgvStudent.Size = new System.Drawing.Size(561, 251);
            this.dgvStudent.TabIndex = 0;
            // 
            // picAvatar
            // 
            this.picAvatar.Dock = System.Windows.Forms.DockStyle.Right;
            this.picAvatar.Location = new System.Drawing.Point(561, 160);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(180, 251);
            this.picAvatar.TabIndex = 1;
            this.picAvatar.TabStop = false;
            // 
            // frmsearch
            // 
            this.ClientSize = new System.Drawing.Size(741, 411);
            this.Controls.Add(this.dgvStudent);
            this.Controls.Add(this.picAvatar);
            this.Controls.Add(this.grpSearch);
            this.Name = "frmsearch";
            this.Text = "Tìm kiếm sinh viên";
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
