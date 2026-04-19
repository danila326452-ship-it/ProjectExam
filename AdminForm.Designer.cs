namespace ProjectExam
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblLogin = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUnblock = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // Панель
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15);
            this.pnlMain.Size = new System.Drawing.Size(650, 450);

            // Заголовок
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Text = "Управление пользователями";

            // Таблица
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.Location = new System.Drawing.Point(20, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(610, 200);
            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);

            // Логин
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblLogin.Location = new System.Drawing.Point(20, 270);
            this.lblLogin.Text = "Логин:";
            this.tbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbLogin.Location = new System.Drawing.Point(20, 295);
            this.tbLogin.Size = new System.Drawing.Size(180, 21);
            this.tbLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Пароль
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPass.Location = new System.Drawing.Point(220, 270);
            this.lblPass.Text = "Пароль:";
            this.tbPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbPass.Location = new System.Drawing.Point(220, 295);
            this.tbPass.Size = new System.Drawing.Size(180, 21);
            this.tbPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Роль
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblRole.Location = new System.Drawing.Point(420, 270);
            this.lblRole.Text = "Роль:";
            this.cbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbRole.Location = new System.Drawing.Point(420, 295);
            this.cbRole.Size = new System.Drawing.Size(150, 23);
            this.cbRole.Items.AddRange(new object[] { "Администратор", "Пользователь" });
            this.cbRole.SelectedIndex = 0;
            this.cbRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Кнопка Добавить
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Location = new System.Drawing.Point(20, 340);
            this.btnAdd.Size = new System.Drawing.Size(150, 35);
            this.btnAdd.Text = "Добавить";
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 153, 76);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Кнопка Разблокировать
            this.btnUnblock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnUnblock.Location = new System.Drawing.Point(20, 390);
            this.btnUnblock.Size = new System.Drawing.Size(150, 35);
            this.btnUnblock.Text = "Разблокировать";
            this.btnUnblock.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnUnblock.ForeColor = System.Drawing.Color.White;
            this.btnUnblock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.dataGridView1);
            this.pnlMain.Controls.Add(this.lblLogin);
            this.pnlMain.Controls.Add(this.tbLogin);
            this.pnlMain.Controls.Add(this.lblPass);
            this.pnlMain.Controls.Add(this.tbPass);
            this.pnlMain.Controls.Add(this.lblRole);
            this.pnlMain.Controls.Add(this.cbRole);
            this.pnlMain.Controls.Add(this.btnAdd);
            this.pnlMain.Controls.Add(this.btnUnblock);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Панель администратора";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle, lblLogin, lblPass, lblRole;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox tbLogin, tbPass;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Button btnAdd, btnUnblock;
    }
}