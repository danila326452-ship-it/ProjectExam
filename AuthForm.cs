using MySql.Data.MySqlClient;
using ProjectExam;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjectExam
{
    public partial class AuthForm : Form
    {
        private int failCount = 0, puzzleFails = 0;
        private Button[] captchaBtns;
        private Button firstSelected = null;

        public AuthForm()
        {
            InitializeComponent();
            captchaBtns = new Button[] { btnC1, btnC2, btnC3, btnC4 };
            foreach (var btn in captchaBtns) btn.Click += CaptchaBtn_Click;
            btnEnter.Click += BtnEnter_Click;
            btnExit.Click += (_, __) => Application.Exit();
            ShuffleCaptcha();
        }

        private void ShuffleCaptcha()
        {
            var rnd = new Random();
            var values = Enumerable.Range(1, 4).OrderBy(_ => rnd.Next()).ToArray();
            for (int i = 0; i < 4; i++)
            {
                captchaBtns[i].Text = values[i].ToString();
                captchaBtns[i].Tag = values[i];
                captchaBtns[i].BackColor = SystemColors.Control;
            }
            btnEnter.Enabled = false;
            firstSelected = null;
        }

        private void CaptchaBtn_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (firstSelected == null)
            {
                firstSelected = btn;
                btn.BackColor = Color.Gold;
            }
            else if (firstSelected != btn)
            {
                // Меняем местами текст и теги
                var tmpText = firstSelected.Text;
                var tmpTag = firstSelected.Tag;

                firstSelected.Text = btn.Text;
                firstSelected.Tag = btn.Tag;
                firstSelected.BackColor = SystemColors.Control;

                btn.Text = tmpText;
                btn.Tag = tmpTag;
                btn.BackColor = SystemColors.Control;

                firstSelected = null;
                CheckCaptcha();
            }
        }

        private void CheckCaptcha()
        {
            bool solved = captchaBtns[0].Tag.ToString() == "1" &&
                          captchaBtns[1].Tag.ToString() == "2" &&
                          captchaBtns[2].Tag.ToString() == "3" &&
                          captchaBtns[3].Tag.ToString() == "4";

            btnEnter.Enabled = solved;
            if (solved)
            {
                foreach (var b in captchaBtns) b.BackColor = Color.LightGreen;
            }
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLogin.Text) || string.IsNullOrWhiteSpace(tbPass.Text))
            {
                MessageBox.Show("Поля Логин и Пароль обязательны для заполнения.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dt = DbHelper.Query("SELECT роль, заблокирован, попытки_входа FROM Пользователи WHERE логин=@l AND пароль=@p",
                    new MySqlParameter("@l", tbLogin.Text), new MySqlParameter("@p", tbPass.Text));

                if (dt.Rows.Count == 0)
                {
                    failCount++; puzzleFails++;
                    DbHelper.Execute("UPDATE Пользователи SET попытки_входа=@c WHERE логин=@l", new MySqlParameter("@c", failCount), new MySqlParameter("@l", tbLogin.Text));

                    if (failCount >= 3 || puzzleFails >= 3)
                    {
                        DbHelper.Execute("UPDATE Пользователи SET заблокирован=TRUE WHERE логин=@l", new MySqlParameter("@l", tbLogin.Text));
                        MessageBox.Show("Вы заблокированы. Обратитесь к администратору", "Блокировка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ShuffleCaptcha();
                    }
                }
                else
                {
                    if (dt.Rows[0]["заблокирован"].ToString() == "1")
                    {
                        MessageBox.Show("Вы заблокированы. Обратитесь к администратору", "Блокировка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Application.Exit();
                        return;
                    }
                    MessageBox.Show("Вы успешно авторизовались", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new MainForm(dt.Rows[0]["роль"].ToString()).Show(); this.Hide();
                }
            }
            catch (MySqlException ex) when (ex.Number == 1049)
            {
                MessageBox.Show("База данных 'HoldingAnalytics' не найдена.\n1. Откройте MySQL Workbench.\n2. Выполните скрипт инициализации.\n3. Перезапустите программу.", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message, "Системная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}