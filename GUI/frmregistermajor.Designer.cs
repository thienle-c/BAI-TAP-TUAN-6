namespace GUI
{
    partial class frmregistermajor
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
            this.dgvStudent = new System.Windows.Forms.DataGridView();

            // Tạo cột Checkbox
            System.Windows.Forms.DataGridViewCheckBoxColumn colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            colCheck.HeaderText = "Chọn";
            colCheck.Name = "colCheck";
            colCheck.Width = 50;

            // Thêm cột vào dgvStudent
            this.dgvStudent.Columns.Add(colCheck);

            // Giữ các thuộc tính cũ của bạn
            this.dgvStudent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudent.Location = new System.Drawing.Point(87, 158);
            this.dgvStudent.Name = "dgvStudent";
            this.dgvStudent.Size = new System.Drawing.Size(517, 215);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbFaculty = new System.Windows.Forms.ComboBox();
            this.cmbMajor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            // NOTE: dgvStudent already initialized above; do not re-create to avoid duplicate declaration
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.chkUnregisterMajor = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudent)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbFaculty);
            this.groupBox1.Controls.Add(this.cmbMajor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(9, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Tin";
            // 
            // cmbFaculty
            // 
            this.cmbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFaculty.FormattingEnabled = true;
            this.cmbFaculty.Items.AddRange(new object[] {
            "Ngôn Ngữ Anh",
            "Quản Trị Kinh Doanh",
            "Công Nghệ Thông Tin"});
            this.cmbFaculty.Location = new System.Drawing.Point(109, 25);
            this.cmbFaculty.Name = "cmbFaculty";
            this.cmbFaculty.Size = new System.Drawing.Size(382, 21);
            this.cmbFaculty.TabIndex = 1;
            this.cmbFaculty.SelectedIndexChanged += new System.EventHandler(this.cmbFaculty_SelectedIndexChanged);
            // 
            // cmbMajor
            // 
            this.cmbMajor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMajor.FormattingEnabled = true;
            this.cmbMajor.Items.AddRange(new object[] {
            "Tiếng Anh Thương Mại",
            "Ngôn Ngữ Anh"});
            this.cmbMajor.Location = new System.Drawing.Point(109, 60);
            this.cmbMajor.Name = "cmbMajor";
            this.cmbMajor.Size = new System.Drawing.Size(382, 21);
            this.cmbMajor.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Khoa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Chuyên ngành";
            // 
            // dgvStudent
            // 
            this.dgvStudent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudent.Location = new System.Drawing.Point(87, 158);
            this.dgvStudent.Name = "dgvStudent";
            this.dgvStudent.RowHeadersWidth = 51;
            this.dgvStudent.Size = new System.Drawing.Size(517, 215);
            this.dgvStudent.TabIndex = 1;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(224, 390);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(100, 30);
            this.btnRegister.TabIndex = 2;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(248, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(267, 26);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Đăng Ký Chuyên Ngành";
            // 
            // chkUnregisterMajor
            // 
            this.chkUnregisterMajor.AutoSize = true;
            this.chkUnregisterMajor.Location = new System.Drawing.Point(533, 117);
            this.chkUnregisterMajor.Margin = new System.Windows.Forms.Padding(2);
            this.chkUnregisterMajor.Name = "chkUnregisterMajor";
            this.chkUnregisterMajor.Size = new System.Drawing.Size(140, 17);
            this.chkUnregisterMajor.TabIndex = 4;
            this.chkUnregisterMajor.Text = "Chưa ĐK chuyên ngành";
            this.chkUnregisterMajor.UseVisualStyleBackColor = true;
            this.chkUnregisterMajor.CheckedChanged += new System.EventHandler(this.chkUnregisterMajor_CheckedChanged);
            // 
            // frmregistermajor
            // 
            this.ClientSize = new System.Drawing.Size(684, 472);
            this.Controls.Add(this.chkUnregisterMajor);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.dgvStudent);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmregistermajor";
            this.Text = "Đăng Ký Chuyên Ngành";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbFaculty;
        private System.Windows.Forms.ComboBox cmbMajor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvStudent;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkUnregisterMajor;
    }
}
