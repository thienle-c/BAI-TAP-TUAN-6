namespace GUI
{
    partial class frmreport
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Text = "BÁO CÁO DANH SÁCH SINH VIÊN";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);

            // btnExport
            this.btnExport.Text = "Xuất báo cáo (CSV)";
            this.btnExport.Location = new System.Drawing.Point(620, 12);
            this.btnExport.Size = new System.Drawing.Size(160, 35);

            // dgv
            this.dgv.Location = new System.Drawing.Point(12, 60);
            this.dgv.Size = new System.Drawing.Size(768, 480);
            this.dgv.ReadOnly = true;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // frmreport
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgv);
            this.Text = "Xuất báo cáo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
