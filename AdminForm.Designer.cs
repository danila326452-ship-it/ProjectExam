namespace ProjectExam
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView(); this.tbLogin = new System.Windows.Forms.TextBox(); this.tbPass = new System.Windows.Forms.TextBox();
            this.cbRole = new System.Windows.Forms.ComboBox(); this.btnAdd = new System.Windows.Forms.Button(); this.btnUnblock = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit(); this.SuspendLayout();
            this.dataGridView1.AllowUserToAddRows = false; this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 10); this.dataGridView1.Name = "dataGridView1"; this.dataGridView1.ReadOnly = true; this.dataGridView1.Size = new System.Drawing.Size(460, 200);
            this.tbLogin.Location = new System.Drawing.Point(10, 230); this.tbLogin.Name = "tbLogin"; this.tbLogin.Size = new System.Drawing.Size(100, 20);
            this.tbPass.Location = new System.Drawing.Point(120, 230); this.tbPass.Name = "tbPass"; this.tbPass.Size = new System.Drawing.Size(100, 20);
            this.cbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cbRole.Location = new System.Drawing.Point(230, 230); this.cbRole.Name = "cbRole"; this.cbRole.Size = new System.Drawing.Size(100, 21);
            this.cbRole.Items.AddRange(new object[] { "Администратор", "Пользователь" }); this.cbRole.SelectedIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(340, 230); this.btnAdd.Name = "btnAdd"; this.btnAdd.Size = new System.Drawing.Size(100, 30); this.btnAdd.Text = "Добавить";
            this.btnUnblock.Location = new System.Drawing.Point(10, 270); this.btnUnblock.Name = "btnUnblock"; this.btnUnblock.Size = new System.Drawing.Size(130, 30); this.btnUnblock.Text = "Разблокировать";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.ClientSize = new System.Drawing.Size(480, 320);
            this.Controls.Add(this.dataGridView1); this.Controls.Add(this.tbLogin); this.Controls.Add(this.tbPass); this.Controls.Add(this.cbRole); this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnUnblock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox = false; this.Name = "AdminForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; this.Text = "Панель администратора";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit(); this.ResumeLayout(false); this.PerformLayout();
        }
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox tbLogin, tbPass;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Button btnAdd, btnUnblock;
    }
}