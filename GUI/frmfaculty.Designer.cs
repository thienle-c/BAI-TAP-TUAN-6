namespace GUI
{
    partial class frmfaculty
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblMaKhoa = new System.Windows.Forms.Label();
            this.txtFacultyID = new System.Windows.Forms.TextBox();
            this.lblTenKhoa = new System.Windows.Forms.Label();
            this.txtFacultyName = new System.Windows.Forms.TextBox();
            this.lblTongGS = new System.Windows.Forms.Label();
            this.txtTotalProf = new System.Windows.Forms.TextBox();
            this.btnAddUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvFaculty = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaculty)).BeginInit();
            this.grpThongTin.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.lblMaKhoa);
            this.grpThongTin.Controls.Add(this.txtFacultyID);
            this.grpThongTin.Controls.Add(this.lblTenKhoa);
            this.grpThongTin.Controls.Add(this.txtFacultyName);
            this.grpThongTin.Controls.Add(this.lblTongGS);
            this.grpThongTin.Controls.Add(this.txtTotalProf);
            this.grpThongTin.Controls.Add(this.btnAddUpdate);
            this.grpThongTin.Controls.Add(this.btnDelete);
            this.grpThongTin.Location = new System.Drawing.Point(12, 12);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(320, 426);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin khoa";
            // 
            // lblMaKhoa
            // 
            this.lblMaKhoa.Location = new System.Drawing.Point(20, 40);
            this.lblMaKhoa.Name = "lblMaKhoa";
            this.lblMaKhoa.Size = new System.Drawing.Size(100, 23);
            this.lblMaKhoa.TabIndex = 0;
            this.lblMaKhoa.Text = "Mã Khoa:";
            // 
            // txtFacultyID
            // 
            this.txtFacultyID.Location = new System.Drawing.Point(120, 37);
            this.txtFacultyID.Name = "txtFacultyID";
            this.txtFacultyID.Size = new System.Drawing.Size(180, 22);
            this.txtFacultyID.TabIndex = 1;
            // 
            // lblTenKhoa
            // 
            this.lblTenKhoa.Location = new System.Drawing.Point(20, 80);
            this.lblTenKhoa.Name = "lblTenKhoa";
            this.lblTenKhoa.Size = new System.Drawing.Size(100, 23);
            this.lblTenKhoa.TabIndex = 2;
            this.lblTenKhoa.Text = "Tên Khoa:";
            // 
            // txtFacultyName
            // 
            this.txtFacultyName.Location = new System.Drawing.Point(120, 77);
            this.txtFacultyName.Name = "txtFacultyName";
            this.txtFacultyName.Size = new System.Drawing.Size(180, 22);
            this.txtFacultyName.TabIndex = 3;
            // 
            // lblTongGS
            // 
            this.lblTongGS.Location = new System.Drawing.Point(20, 120);
            this.lblTongGS.Name = "lblTongGS";
            this.lblTongGS.Size = new System.Drawing.Size(100, 23);
            this.lblTongGS.TabIndex = 4;
            this.lblTongGS.Text = "Tổng số GS:";
            // 
            // txtTotalProf
            // 
            this.txtTotalProf.Location = new System.Drawing.Point(120, 117);
            this.txtTotalProf.Name = "txtTotalProf";
            this.txtTotalProf.Size = new System.Drawing.Size(180, 22);
            this.txtTotalProf.TabIndex = 5;
            // 
            // btnAddUpdate
            // 
            this.btnAddUpdate.Location = new System.Drawing.Point(120, 160);
            this.btnAddUpdate.Name = "btnAddUpdate";
            this.btnAddUpdate.Size = new System.Drawing.Size(85, 30);
            this.btnAddUpdate.TabIndex = 6;
            this.btnAddUpdate.Text = "Thêm / Sửa";
            this.btnAddUpdate.UseVisualStyleBackColor = true;
            this.btnAddUpdate.Click += new System.EventHandler(this.btnAddUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(215, 160);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 30);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvFaculty
            // 
            this.dgvFaculty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFaculty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFaculty.Location = new System.Drawing.Point(350, 20);
            this.dgvFaculty.Name = "dgvFaculty";
            this.dgvFaculty.RowHeadersWidth = 51;
            this.dgvFaculty.RowTemplate.Height = 24;
            this.dgvFaculty.Size = new System.Drawing.Size(430, 418);
            this.dgvFaculty.TabIndex = 1;
            this.dgvFaculty.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFaculty_CellClick);
            // 
            // frmfaculty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvFaculty);
            this.Controls.Add(this.grpThongTin);
            this.Name = "frmfaculty";
            this.Text = "Quản lý khoa";
            this.Load += new System.EventHandler(this.frmfaculty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaculty)).EndInit();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaKhoa;
        private System.Windows.Forms.Label lblTenKhoa;
        private System.Windows.Forms.Label lblTongGS;
        private System.Windows.Forms.TextBox txtFacultyID;
        private System.Windows.Forms.TextBox txtFacultyName;
        private System.Windows.Forms.TextBox txtTotalProf;
        private System.Windows.Forms.Button btnAddUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvFaculty;
    }
}