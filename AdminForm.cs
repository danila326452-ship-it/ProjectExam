using MySql.Data.MySqlClient;
using ProjectExam;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectExam
{
    public class AdminForm : Form
    {
        private DataGridView dataGridView1;
        private TextBox tbLogin, tbPass;
        private ComboBox cbRole;
        private Button btnAdd, btnUnblock;
        
        public AdminForm() 
        { 
            InitializeComponent(); 
            LoadUsers(); 
            btnAdd.Click += AddUser; 
            btnUnblock.Click += Unblock; 
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new DataGridView();
            this.tbLogin = new TextBox();
            this.tbPass = new TextBox();
            this.cbRole = new ComboBox();
            this.btnAdd = new Button();
            this.btnUnblock = new Button();
            
            this.SuspendLayout();

            // dataGridView1
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(560, 200);
            this.dataGridView1.TabIndex = 0;

            // tbLogin
            this.tbLogin.Location = new System.Drawing.Point(12, 230);
            this.tbLogin.PlaceholderText = "Логин";
            this.tbLogin.Size = new System.Drawing.Size(180, 20);
            this.tbLogin.TabIndex = 1;

            // tbPass
            this.tbPass.Location = new System.Drawing.Point(200, 230);
            this.tbPass.PasswordChar = '*';
            this.tbPass.PlaceholderText = "Пароль";
            this.tbPass.Size = new System.Drawing.Size(180, 20);
            this.tbPass.TabIndex = 2;

            // cbRole
            this.cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbRole.Items.AddRange(new object[] { "Администратор", "Аналитик" });
            this.cbRole.Location = new System.Drawing.Point(390, 230);
            this.cbRole.Size = new System.Drawing.Size(182, 21);
            this.cbRole.TabIndex = 3;

            // btnAdd
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(12, 260);
            this.btnAdd.Size = new System.Drawing.Size(270, 35);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Добавить пользователя";
            this.btnAdd.UseVisualStyleBackColor = false;

            // btnUnblock
            this.btnUnblock.BackColor = System.Drawing.Color.Green;
            this.btnUnblock.ForeColor = System.Drawing.Color.White;
            this.btnUnblock.Location = new System.Drawing.Point(300, 260);
            this.btnUnblock.Size = new System.Drawing.Size(272, 35);
            this.btnUnblock.TabIndex = 5;
            this.btnUnblock.Text = "Разблокировать выбранного";
            this.btnUnblock.UseVisualStyleBackColor = false;

            // AdminForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 310);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.cbRole);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUnblock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Администрирование пользователей";
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadUsers() => dataGridView1.DataSource = DbHelper.Query("SELECT id, логин, роль, заблокирован FROM Пользователи");
        
        private void AddUser(object sender, EventArgs e)
        {
            try
            {
                var check = DbHelper.Query("SELECT 1 FROM Пользователи WHERE логин=@l", new MySqlParameter("@l", tbLogin.Text));
                if (check.Rows.Count > 0) { MessageBox.Show("Пользователь с таким логином уже существует.", "Дубликат", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DbHelper.Execute("INSERT INTO Пользователи (логин, пароль, роль) VALUES (@l,@p,@r)", new MySqlParameter("@l", tbLogin.Text), new MySqlParameter("@p", tbPass.Text), new MySqlParameter("@r", cbRole.Text));
                LoadUsers(); MessageBox.Show("Пользователь создан.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        
        private void Unblock(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            try { DbHelper.Execute("UPDATE Пользователи SET заблокирован=FALSE, попытки_входа=0 WHERE id=@id", new MySqlParameter("@id", dataGridView1.CurrentRow.Cells["id"].Value)); LoadUsers(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}