namespace ProjectExam
{
    partial class AuthForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.lblLogin = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.lblCaptcha = new System.Windows.Forms.Label();
            this.btnC1 = new System.Windows.Forms.Button();
            this.btnC2 = new System.Windows.Forms.Button();
            this.btnC3 = new System.Windows.Forms.Button();
            this.btnC4 = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Логин
            this.lblLogin.AutoSize = true; this.lblLogin.Location = new System.Drawing.Point(30, 30); this.lblLogin.Text = "Логин:";
            this.tbLogin.Location = new System.Drawing.Point(90, 27); this.tbLogin.Name = "tbLogin"; this.tbLogin.Size = new System.Drawing.Size(180, 20);

            // Пароль
            this.lblPass.AutoSize = true; this.lblPass.Location = new System.Drawing.Point(30, 65); this.lblPass.Text = "Пароль:";
            this.tbPass.Location = new System.Drawing.Point(90, 62); this.tbPass.Name = "tbPass"; this.tbPass.Size = new System.Drawing.Size(180, 20); this.tbPass.PasswordChar = '*';

            // Капча
            this.lblCaptcha.AutoSize = true; this.lblCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCaptcha.Location = new System.Drawing.Point(30, 110); this.lblCaptcha.Text = "Соберите порядок 1-2-3-4 (клик по двум кнопкам меняет их местами):";

            this.btnC1.Location = new System.Drawing.Point(30, 135); this.btnC1.Size = new System.Drawing.Size(55, 35); this.btnC1.Text = "1"; this.btnC1.UseVisualStyleBackColor = true;
            this.btnC2.Location = new System.Drawing.Point(90, 135); this.btnC2.Size = new System.Drawing.Size(55, 35); this.btnC2.Text = "2"; this.btnC2.UseVisualStyleBackColor = true;
            this.btnC3.Location = new System.Drawing.Point(150, 135); this.btnC3.Size = new System.Drawing.Size(55, 35); this.btnC3.Text = "3"; this.btnC3.UseVisualStyleBackColor = true;
            this.btnC4.Location = new System.Drawing.Point(210, 135); this.btnC4.Size = new System.Drawing.Size(55, 35); this.btnC4.Text = "4"; this.btnC4.UseVisualStyleBackColor = true;

            // Кнопки
            this.btnEnter.Enabled = false; this.btnEnter.Location = new System.Drawing.Point(30, 200); this.btnEnter.Name = "btnEnter"; this.btnEnter.Size = new System.Drawing.Size(240, 35); this.btnEnter.Text = "Войти";
            this.btnExit.Location = new System.Drawing.Point(30, 245); this.btnExit.Name = "btnExit"; this.btnExit.Size = new System.Drawing.Size(240, 35); this.btnExit.Text = "Выход";

            // Форма
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 310);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnC4);
            this.Controls.Add(this.btnC3);
            this.Controls.Add(this.btnC2);
            this.Controls.Add(this.btnC1);
            this.Controls.Add(this.lblCaptcha);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.lblLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход в систему";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblLogin, lblPass, lblCaptcha;
        private System.Windows.Forms.TextBox tbLogin, tbPass;
        private System.Windows.Forms.Button btnEnter, btnExit, btnC1, btnC2, btnC3, btnC4;
    }
}