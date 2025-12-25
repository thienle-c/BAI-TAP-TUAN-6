namespace GUI
{
    partial class frmmajor
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvMajors;
        private System.Windows.Forms.ComboBox cmbFaculty;
        private System.Windows.Forms.TextBox txtMajorID;
        private System.Windows.Forms.TextBox txtMajorName;
        private System.Windows.Forms.Button btnAddUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblFaculty;
        private System.Windows.Forms.Label lblMajorID;
        private System.Windows.Forms.Label lblMajorName;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvMajors = new System.Windows.Forms.DataGridView();
            this.cmbFaculty = new System.Windows.Forms.ComboBox();
            this.txtMajorID = new System.Windows.Forms.TextBox();
            this.txtMajorName = new System.Windows.Forms.TextBox();
            this.btnAddUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblFaculty = new System.Windows.Forms.Label();
            this.lblMajorID = new System.Windows.Forms.Label();
            this.lblMajorName = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.dgvMajors)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // 
            // dgvMajors
            // 
            this.dgvMajors.AllowUserToAddRows = false;
            this.dgvMajors.AllowUserToDeleteRows = false;
            this.dgvMajors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMajors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMajors.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvMajors.Height = 280;
            this.dgvMajors.Name = "dgvMajors";
            this.dgvMajors.ReadOnly = true;
            this.dgvMajors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // 
            // panelTop
            // 
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 280;
            this.panelTop.Controls.Add(this.dgvMajors);

            // 
            // panelBottom
            // 
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Padding = new System.Windows.Forms.Padding(15);
            this.panelBottom.Controls.Add(this.btnDelete);
            this.panelBottom.Controls.Add(this.btnAddUpdate);
            this.panelBottom.Controls.Add(this.txtMajorName);
            this.panelBottom.Controls.Add(this.lblMajorName);
            this.panelBottom.Controls.Add(this.txtMajorID);
            this.panelBottom.Controls.Add(this.lblMajorID);
            this.panelBottom.Controls.Add(this.cmbFaculty);
            this.panelBottom.Controls.Add(this.lblFaculty);

            // 
            // lblFaculty
            // 
            this.lblFaculty.AutoSize = true;
            this.lblFaculty.Location = new System.Drawing.Point(20, 20);
            this.lblFaculty.Text = "Khoa";

            // 
            // cmbFaculty
            // 
            this.cmbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFaculty.Location = new System.Drawing.Point(150, 16);
            this.cmbFaculty.Width = 250;

            // 
            // lblMajorID
            // 
            this.lblMajorID.AutoSize = true;
            this.lblMajorID.Location = new System.Drawing.Point(20, 60);
            this.lblMajorID.Text = "Mã chuyên ngành";

            // 
            // txtMajorID
            // 
            this.txtMajorID.Location = new System.Drawing.Point(150, 56);
            this.txtMajorID.Width = 250;

            // 
            // lblMajorName
            // 
            this.lblMajorName.AutoSize = true;
            this.lblMajorName.Location = new System.Drawing.Point(20, 100);
            this.lblMajorName.Text = "Tên chuyên ngành";

            // 
            // txtMajorName
            // 
            this.txtMajorName.Location = new System.Drawing.Point(150, 96);
            this.txtMajorName.Width = 400;

            // 
            // btnAddUpdate
            // 
            this.btnAddUpdate.Location = new System.Drawing.Point(150, 140);
            this.btnAddUpdate.Size = new System.Drawing.Size(120, 35);
            this.btnAddUpdate.Text = "Thêm / Sửa";

            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(290, 140);
            this.btnDelete.Size = new System.Drawing.Size(120, 35);
            this.btnDelete.Text = "Xóa";

            // 
            // frmmajor
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Text = "Quản lý chuyên ngành";

            ((System.ComponentModel.ISupportInitialize)(this.dgvMajors)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
