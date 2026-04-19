using MySql.Data.MySqlClient;
using ProjectExam;
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProjectExam
{
    public partial class EditForm : Form
    {
        private int id;
        public EditForm(int id = 0)
        {
            this.id = id; InitializeComponent();
            if (id == 0) this.Text = "Добавление партнера"; else { this.Text = "Редактирование партнера"; LoadData(); }
            btnSave.Click += Save;
        }

        private void LoadData()
        {
            var dt = DbHelper.Query("SELECT * FROM Предприятия WHERE id=@id", new MySqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0]; tbName.Text = r["название"].ToString(); tbType.Text = r["тип_партнера"].ToString();
                tbRating.Text = r["рейтинг"].ToString(); tbAddr.Text = r["адрес"].ToString(); tbDir.Text = r["директор"].ToString();
                tbPhone.Text = r["телефон"].ToString(); tbEmail.Text = r["email"].ToString();
            }
        }

        private void Save(object sender, EventArgs e)
        {
            try
            {
                int rating;
                if (!int.TryParse(tbRating.Text, out rating) || rating < 0) { MessageBox.Show("Рейтинг должен быть неотрицательным целым числом.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                MySqlParameter[] pars = new MySqlParameter[] { new MySqlParameter("@n", tbName.Text), new MySqlParameter("@t", tbType.Text), new MySqlParameter("@r", rating), new MySqlParameter("@a", tbAddr.Text), new MySqlParameter("@d", tbDir.Text), new MySqlParameter("@p", tbPhone.Text), new MySqlParameter("@e", tbEmail.Text) };
                if (id == 0) DbHelper.Execute("INSERT INTO Предприятия (название, тип_партнера, рейтинг, адрес, директор, телефон, email) VALUES (@n,@t,@r,@a,@d,@p,@e)", pars);
                else { MySqlParameter[] parsUpd = new MySqlParameter[pars.Length + 1]; pars.CopyTo(parsUpd, 0); parsUpd[parsUpd.Length - 1] = new MySqlParameter("@id", id); DbHelper.Execute("UPDATE Предприятия SET название=@n, тип_партнера=@t, рейтинг=@r, адрес=@a, директор=@d, телефон=@p, email=@e WHERE id=@id", parsUpd); }
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}