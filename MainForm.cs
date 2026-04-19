using MySql.Data.MySqlClient;
using ProjectExam;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjectExam
{
    public partial class MainForm : Form
    {
        private string role;
        public MainForm(string role)
        {
            this.role = role; InitializeComponent(); this.Text = "Главная - " + role;
            btnAdd.Click += (s, e) => { new EditForm().ShowDialog(); LoadData(); };
            btnEdit.Click += (s, e) => { if (dataGridView1.CurrentRow == null) return; new EditForm((int)dataGridView1.CurrentRow.Cells["id"].Value).ShowDialog(); LoadData(); };
            btnBack.Click += (s, e) => { new AuthForm().Show(); this.Close(); };
            btnAdmin.Click += (s, e) => { if (role == "Администратор") new AdminForm().ShowDialog(); else MessageBox.Show("Доступ запрещен", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); };
            btnFunc.Click += (s, e) => RunFunction();
            btnHist.Click += (s, e) => ShowHistory();
            LoadData();
        }

        private void LoadData() => dataGridView1.DataSource = DbHelper.Query("SELECT id, название, тип_партнера, рейтинг, адрес, директор, телефон, email FROM Предприятия");

        private void ShowHistory()
        {
            try
            {
                var dt = DbHelper.Query("SELECT d.значение, d.дата_отчета, p.название FROM Динамика_показателей d JOIN Показатели p ON d.код_показателя=p.id ORDER BY d.дата_отчета DESC");
                string msg = "История:\n" + string.Join("\n", dt.AsEnumerable().Select(r => string.Format("{0} | {1} | {2}", r["дата_отчета"], r["название"], r["значение"])));
                MessageBox.Show(msg, "История продаж", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private string ShowInputDialog(string title, string prompt, string defaultText = "")
        {
            using (Form form = new Form())
            {
                form.Text = title; form.Size = new Size(280, 140); form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog; form.MaximizeBox = false; form.MinimizeBox = false;
                Label lbl = new Label() { Left = 20, Top = 20, Text = prompt, AutoSize = true };
                TextBox tb = new TextBox() { Left = 20, Top = 40, Width = 220, Text = defaultText };
                Button btnOk = new Button() { Text = "OK", Left = 100, Width = 70, Top = 75, DialogResult = DialogResult.OK };
                Button btnCancel = new Button() { Text = "Отмена", Left = 180, Width = 70, Top = 75, DialogResult = DialogResult.Cancel };
                form.Controls.AddRange(new Control[] { lbl, tb, btnOk, btnCancel });
                form.AcceptButton = btnOk; form.CancelButton = btnCancel;
                return form.ShowDialog() == DialogResult.OK ? tb.Text : null;
            }
        }

        private void RunFunction()
        {
            try
            {
                string s1 = ShowInputDialog("Функция (Вариант 19)", "Код предприятия:", "1");
                string s2 = ShowInputDialog("Функция (Вариант 19)", "Код показателя:", "1");
                if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2)) return;

                var dt = DbHelper.Query("SELECT `Вычисление значения Динамики показателя`(@e, @i) as res",
                    new MySqlParameter("@e", int.Parse(s1)), new MySqlParameter("@i", int.Parse(s2)));
                MessageBox.Show("Результат функции: " + dt.Rows[0][0].ToString(), "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { MessageBox.Show("Неверный формат ID или функция не создана в БД.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public static decimal GetDiscount(decimal totalSales)
        {
            if (totalSales < 10000) return 0m;
            if (totalSales < 50000) return 0.05m;
            if (totalSales < 300000) return 0.10m;
            return 0.15m;
        }
    }
}