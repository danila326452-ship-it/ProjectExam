namespace ProjectExam
{
    partial class AuthForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.lblCaptcha = new System.Windows.Forms.Label();
            this.pnlCaptcha = new System.Windows.Forms.FlowLayoutPanel();
            this.btnC1 = new System.Windows.Forms.Button();
            this.btnC2 = new System.Windows.Forms.Button();
            this.btnC3 = new System.Windows.Forms.Button();
            this.btnC4 = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlCaptcha.SuspendLayout();
            this.SuspendLayout();

            // Панель
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(400, 450);

            // Заголовок
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTitle.Location = new System.Drawing.Point(80, 30);
            this.lblTitle.Text = "Авторизация";

            // Логин
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblLogin.Location = new System.Drawing.Point(40, 90);
            this.lblLogin.Text = "Логин:";
            this.tbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbLogin.Location = new System.Drawing.Point(40, 115);
            this.tbLogin.Size = new System.Drawing.Size(320, 23);
            this.tbLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Пароль
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPass.Location = new System.Drawing.Point(40, 150);
            this.lblPass.Text = "Пароль:";
            this.tbPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbPass.Location = new System.Drawing.Point(40, 175);
            this.tbPass.Size = new System.Drawing.Size(320, 23);
            this.tbPass.PasswordChar = '*';
            this.tbPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Капча
            this.lblCaptcha.AutoSize = true;
            this.lblCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblCaptcha.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblCaptcha.Location = new System.Drawing.Point(40, 215);
            this.lblCaptcha.Text = "Расставьте кнопки по порядку 1-2-3-4:";

            // Панель капчи
            this.pnlCaptcha.Location = new System.Drawing.Point(40, 245);
            this.pnlCaptcha.Size = new System.Drawing.Size(320, 50);
            this.pnlCaptcha.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.pnlCaptcha.WrapContents = true;
            this.pnlCaptcha.BackColor = System.Drawing.Color.White;
            this.pnlCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCaptcha.Padding = new System.Windows.Forms.Padding(5);

            this.btnC1.Size = new System.Drawing.Size(65, 40);
            this.btnC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnC1.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnC1.ForeColor = System.Drawing.Color.White;
            this.btnC1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnC1.Text = "1";

            this.btnC2.Size = new System.Drawing.Size(65, 40);
            this.btnC2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnC2.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnC2.ForeColor = System.Drawing.Color.White;
            this.btnC2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnC2.Text = "2";

            this.btnC3.Size = new System.Drawing.Size(65, 40);
            this.btnC3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnC3.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnC3.ForeColor = System.Drawing.Color.White;
            this.btnC3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnC3.Text = "3";

            this.btnC4.Size = new System.Drawing.Size(65, 40);
            this.btnC4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnC4.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnC4.ForeColor = System.Drawing.Color.White;
            this.btnC4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnC4.Text = "4";

            this.pnlCaptcha.Controls.Add(this.btnC1);
            this.pnlCaptcha.Controls.Add(this.btnC2);
            this.pnlCaptcha.Controls.Add(this.btnC3);
            this.pnlCaptcha.Controls.Add(this.btnC4);

            // Кнопки
            this.btnEnter.Enabled = false;
            this.btnEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnEnter.Location = new System.Drawing.Point(40, 315);
            this.btnEnter.Size = new System.Drawing.Size(320, 45);
            this.btnEnter.Text = "Войти";
            this.btnEnter.BackColor = System.Drawing.Color.FromArgb(0, 153, 76);
            this.btnEnter.ForeColor = System.Drawing.Color.White;
            this.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExit.Location = new System.Drawing.Point(40, 370);
            this.btnExit.Size = new System.Drawing.Size(320, 40);
            this.btnExit.Text = "Выход";
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(204, 0, 0);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Форма
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "AuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход в систему";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlCaptcha.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlMain, pnlCaptcha;
        private System.Windows.Forms.Label lblTitle, lblLogin, lblPass, lblCaptcha;
        private System.Windows.Forms.TextBox tbLogin, tbPass;
        private System.Windows.Forms.Button btnEnter, btnExit, btnC1, btnC2, btnC3, btnC4;
    }
}