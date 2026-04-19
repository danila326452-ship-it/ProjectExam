namespace ProjectExam
{
    partial class EditForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.tbName = new System.Windows.Forms.TextBox(); this.tbType = new System.Windows.Forms.TextBox(); this.tbRating = new System.Windows.Forms.TextBox();
            this.tbAddr = new System.Windows.Forms.TextBox(); this.tbDir = new System.Windows.Forms.TextBox(); this.tbPhone = new System.Windows.Forms.TextBox(); this.tbEmail = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button(); this.lblName = new System.Windows.Forms.Label(); this.lblType = new System.Windows.Forms.Label(); this.lblRating = new System.Windows.Forms.Label();
            this.lblAddr = new System.Windows.Forms.Label(); this.lblDir = new System.Windows.Forms.Label(); this.lblPhone = new System.Windows.Forms.Label(); this.lblEmail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            int y = 20;
            void AddField(System.Windows.Forms.Label l, System.Windows.Forms.TextBox t, string txt, ref int yPos) { l.Location = new System.Drawing.Point(20, yPos); l.Text = txt; l.AutoSize = true; this.Controls.Add(l); t.Location = new System.Drawing.Point(130, yPos); t.Size = new System.Drawing.Size(180, 20); this.Controls.Add(t); yPos += 30; }
            AddField(lblName, tbName, "Наименование", ref y); AddField(lblType, tbType, "Тип партнера", ref y); AddField(lblRating, tbRating, "Рейтинг", ref y);
            AddField(lblAddr, tbAddr, "Адрес", ref y); AddField(lblDir, tbDir, "Директор", ref y); AddField(lblPhone, tbPhone, "Телефон", ref y); AddField(lblEmail, tbEmail, "Email", ref y);
            this.btnSave.Location = new System.Drawing.Point(130, y + 10); this.btnSave.Name = "btnSave"; this.btnSave.Size = new System.Drawing.Size(100, 30); this.btnSave.Text = "Сохранить"; this.Controls.Add(this.btnSave);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.ClientSize = new System.Drawing.Size(350, 300);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox = false; this.Name = "EditForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; this.Text = "Партнер";
            this.ResumeLayout(false); this.PerformLayout();
        }
        private System.Windows.Forms.TextBox tbName, tbType, tbRating, tbAddr, tbDir, tbPhone, tbEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblName, lblType, lblRating, lblAddr, lblDir, lblPhone, lblEmail;
    }
}