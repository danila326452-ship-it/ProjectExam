using MySql.Data.MySqlClient;
using ProjectExam;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjectExam
{
    public class AuthForm : Form
    {
        private int failCount = 0, puzzleFails = 0;
        private Button[] captchaBtns;
        private Button firstSelected = null;
        
        private Panel mainPanel, sidePanel;
        private Label lblTitle, lblLogin, lblPass, lblCaptcha;
        private TextBox tbLogin, tbPass;
        private Button btnC1, btnC2, btnC3, btnC4, btnEnter, btnExit;

        public AuthForm()
        {
            InitializeComponent();
            captchaBtns = new Button[] { btnC1, btnC2, btnC3, btnC4 };
            foreach (var btn in captchaBtns) btn.Click += CaptchaBtn_Click;
            btnEnter.Click += BtnEnter_Click;
            btnExit.Click += (_, __) => Application.Exit();
            ShuffleCaptcha();
        }

        private void InitializeComponent()
        {
            this.mainPanel = new Panel();
            this.sidePanel = new Panel();
            this.lblTitle = new Label();
            this.lblLogin = new Label();
            this.lblPass = new Label();
            this.lblCaptcha = new Label();
            this.tbLogin = new TextBox();
            this.tbPass = new TextBox();
            this.btnC1 = new Button();
            this.btnC2 = new Button();
            this.btnC3 = new Button();
            this.btnC4 = new Button();
            this.btnEnter = new Button();
            this.btnExit = new Button();

            this.SuspendLayout();

            // sidePanel
            this.sidePanel.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Size = new System.Drawing.Size(250, 450);
            this.sidePanel.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 50);
            this.lblTitle.Text = "Вход в систему";

            // mainPanel
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.lblTitle);
            this.mainPanel.Controls.Add(this.lblLogin);
            this.mainPanel.Controls.Add(this.lblPass);
            this.mainPanel.Controls.Add(this.lblCaptcha);
            this.mainPanel.Controls.Add(this.tbLogin);
            this.mainPanel.Controls.Add(this.tbPass);
            this.mainPanel.Controls.Add(this.btnC1);
            this.mainPanel.Controls.Add(this.btnC2);
            this.mainPanel.Controls.Add(this.btnC3);
            this.mainPanel.Controls.Add(this.btnC4);
            this.mainPanel.Controls.Add(this.btnEnter);
            this.mainPanel.Controls.Add(this.btnExit);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(250, 0);
            this.mainPanel.Size = new System.Drawing.Size(550, 450);
            this.mainPanel.TabIndex = 1;

            // lblLogin
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(50, 80);
            this.lblLogin.Text = "Логин:";

            // lblPass
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(50, 140);
            this.lblPass.Text = "Пароль:";

            // lblCaptcha
            this.lblCaptcha.AutoSize = true;
            this.lblCaptcha.Location = new System.Drawing.Point(50, 200);
            this.lblCaptcha.Text = "Капча (расставьте 1-2-3-4):";

            // tbLogin
            this.tbLogin.Location = new System.Drawing.Point(50, 105);
            this.tbLogin.Size = new System.Drawing.Size(200, 20);
            this.tbLogin.TabIndex = 1;

            // tbPass
            this.tbPass.Location = new System.Drawing.Point(50, 165);
            this.tbPass.PasswordChar = '*';
            this.tbPass.Size = new System.Drawing.Size(200, 20);
            this.tbPass.TabIndex = 2;

            // btnC1, btnC2, btnC3, btnC4
            this.btnC1.Location = new System.Drawing.Point(50, 230);
            this.btnC1.Size = new System.Drawing.Size(40, 40);
            this.btnC1.TabIndex = 3;
            this.btnC1.Text = "1";
            this.btnC1.UseVisualStyleBackColor = true;

            this.btnC2.Location = new System.Drawing.Point(100, 230);
            this.btnC2.Size = new System.Drawing.Size(40, 40);
            this.btnC2.TabIndex = 4;
            this.btnC2.Text = "2";
            this.btnC2.UseVisualStyleBackColor = true;

            this.btnC3.Location = new System.Drawing.Point(150, 230);
            this.btnC3.Size = new System.Drawing.Size(40, 40);
            this.btnC3.TabIndex = 5;
            this.btnC3.Text = "3";
            this.btnC3.UseVisualStyleBackColor = true;

            this.btnC4.Location = new System.Drawing.Point(200, 230);
            this.btnC4.Size = new System.Drawing.Size(40, 40);
            this.btnC4.TabIndex = 6;
            this.btnC4.Text = "4";
            this.btnC4.UseVisualStyleBackColor = true;

            // btnEnter
            this.btnEnter.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnEnter.Enabled = false;
            this.btnEnter.ForeColor = System.Drawing.Color.White;
            this.btnEnter.Location = new System.Drawing.Point(50, 300);
            this.btnEnter.Size = new System.Drawing.Size(200, 40);
            this.btnEnter.TabIndex = 7;
            this.btnEnter.Text = "Войти";
            this.btnEnter.UseVisualStyleBackColor = false;

            // btnExit
            this.btnExit.BackColor = System.Drawing.Color.Gray;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(50, 350);
            this.btnExit.Size = new System.Drawing.Size(200, 40);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = false;

            // AuthForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.sidePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация - Аналитика холдинга";
            
            this.ResumeLayout(false);
        }

        private void ShuffleCaptcha()
        {
            var rnd = new Random();
            var values = Enumerable.Range(1, 4).OrderBy(_ => rnd.Next()).ToArray();
            for (int i = 0; i < 4; i++)
            {
                captchaBtns[i].Text = values[i].ToString();
                captchaBtns[i].Tag = values[i];
                captchaBtns[i].BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
                captchaBtns[i].ForeColor = System.Drawing.Color.White;
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
                btn.BackColor = System.Drawing.Color.Gold;
            }
            else if (firstSelected != btn)
            {
                // Меняем местами текст и теги
                var tmpText = firstSelected.Text;
                var tmpTag = firstSelected.Tag;

                firstSelected.Text = btn.Text;
                firstSelected.Tag = btn.Tag;
                firstSelected.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
                firstSelected.ForeColor = System.Drawing.Color.White;

                btn.Text = tmpText;
                btn.Tag = tmpTag;
                btn.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
                btn.ForeColor = System.Drawing.Color.White;

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
                foreach (var b in captchaBtns)
                {
                    b.BackColor = System.Drawing.Color.LightGreen;
                    b.ForeColor = System.Drawing.Color.White;
                }
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
                // Используем таблицу 'users' с английскими полями, как в скрипте init_database.sql
                var dt = DbHelper.Query("SELECT role, username FROM users WHERE username=@u AND password_hash=@p",
                    new MySqlParameter("@u", tbLogin.Text), new MySqlParameter("@p", tbPass.Text));

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShuffleCaptcha();
                }
                else
                {
                    MessageBox.Show("Вы успешно авторизовались", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new MainForm(dt.Rows[0]["role"].ToString()).Show(); 
                    this.Hide();
                }
            }
            catch (MySqlException ex) when (ex.Number == 1049)
            {
                MessageBox.Show("База данных 'holding_db' не найдена.\n1. Откройте MySQL Workbench.\n2. Выполните скрипт init_database.sql.\n3. Перезапустите программу.", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (MySqlException ex)
            {
                string msg = "Ошибка подключения к БД:\n" + ex.Message;
                if (ex.InnerException != null) msg += "\n\nПодробности: " + ex.InnerException.Message;
                MessageBox.Show(msg, "Ошибка MySQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) 
            { 
                string msg = "Произошла непредвиденная ошибка:\n" + ex.Message;
                if (ex.InnerException != null) msg += "\n\nПодробности: " + ex.InnerException.Message;
                MessageBox.Show(msg, "Системная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
    }
}