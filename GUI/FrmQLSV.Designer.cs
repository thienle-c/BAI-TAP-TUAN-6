using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    partial class FrmQLSV
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelSidebar;
        private Panel panelMain;
        private Panel panelLogo;
        private Label lblLogo;

        // ===== Buttons =====
        private Button btnSinhVien;
        private Button btnDangKy;
        private Button btnTimKiem;
        private Button btnKhoa;
        private Button btnChuyenNganh;
        private Button btnBaoThongKe;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new Panel();
            this.panelLogo = new Panel();
            this.lblLogo = new Label();
            this.panelMain = new Panel();

            // 1. Khởi tạo các Button (Thứ tự này sẽ hiển thị từ trên xuống dưới trên Menu)
            this.btnSinhVien = CreateButton("Quản lý sinh viên", BtnSinhVien_Click);
            this.btnDangKy = CreateButton("Đăng ký ngành", BtnDangKy_Click);
            this.btnTimKiem = CreateButton("Tìm kiếm", BtnTimKiem_Click);
            this.btnKhoa = CreateButton("Quản lý khoa", BtnKhoa_Click);
            this.btnChuyenNganh = CreateButton("Chuyên ngành", BtnChuyenNganh_Click);
            this.btnBaoThongKe = CreateButton("Báo cáo thống kê", BtnBaoThongKe_Click);

            this.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelLogo.SuspendLayout();

            // 2. Cấu hình panelSidebar
            this.panelSidebar.BackColor = Color.FromArgb(15, 23, 42);
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Width = 220;

            // 3. Cấu hình panelLogo
            this.panelLogo.BackColor = Color.FromArgb(2, 6, 23);
            this.panelLogo.Dock = DockStyle.Top;
            this.panelLogo.Height = 60;

            this.lblLogo.Dock = DockStyle.Fill;
            this.lblLogo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Text = "QLSV SYSTEM";
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            this.panelLogo.Controls.Add(this.lblLogo);

            // 4. Thêm các điều khiển vào Sidebar (Add theo thứ tự ngược để Dock.Top xếp đúng)
            // Những gì Add sau cùng sẽ nằm trên cùng (dưới Logo)
            this.panelSidebar.Controls.Add(this.btnBaoThongKe);
            this.panelSidebar.Controls.Add(this.btnChuyenNganh);
            this.panelSidebar.Controls.Add(this.btnKhoa);
            this.panelSidebar.Controls.Add(this.btnTimKiem);
            this.panelSidebar.Controls.Add(this.btnDangKy);
            this.panelSidebar.Controls.Add(this.btnSinhVien);
            this.panelSidebar.Controls.Add(this.panelLogo);

            // 5. Cấu hình panelMain
            this.panelMain.BackColor = Color.White;
            this.panelMain.Dock = DockStyle.Fill;

            // 6. Cấu hình Form chính
            this.ClientSize = new Size(1200, 750);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSidebar);
            this.Text = "Hệ thống Quản lý Sinh viên";
            this.WindowState = FormWindowState.Maximized;

            this.panelLogo.ResumeLayout(false);
            this.panelSidebar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ===== HÀM TẠO BUTTON – KHÔNG XOÁ =====
        private Button CreateButton(string text, EventHandler click)
        {
            Button btn = new Button();
            btn.Text = "   " + text;
            btn.Dock = DockStyle.Top;
            btn.Height = 45;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(15, 23, 42);
            btn.Font = new Font("Segoe UI", 10);
            btn.TextAlign = ContentAlignment.MiddleLeft;

            btn.MouseEnter += (s, e) =>
                btn.BackColor = Color.FromArgb(30, 41, 59);

            btn.MouseLeave += (s, e) =>
                btn.BackColor = Color.FromArgb(15, 23, 42);

            if (click != null)
                btn.Click += click;

            return btn;
        }

        // ===== LOAD FORM CON =====
        private void LoadForm(Form f)
        {
            panelMain.Controls.Clear();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            panelMain.Controls.Add(f);
            f.Show();
        }

        // ===== EVENTS =====
        private void BtnSinhVien_Click(object sender, EventArgs e)
        {
            LoadForm(new frmaddstudent());
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            LoadForm(new frmregistermajor());
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            LoadForm(new frmsearch());
        }

        private void BtnKhoa_Click(object sender, EventArgs e)
        {
            LoadForm(new frmfaculty());
        }
      
    }
}
