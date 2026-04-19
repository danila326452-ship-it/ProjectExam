using MySql.Data.MySqlClient;
using ProjectExam;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectExam
{
    public partial class AdminForm : Form
    {
        public AdminForm() { InitializeComponent(); LoadUsers(); btnAdd.Click += AddUser; btnUnblock.Click += Unblock; }
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