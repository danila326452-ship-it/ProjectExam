namespace ProjectExam
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button(); this.btnEdit = new System.Windows.Forms.Button(); this.btnBack = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button(); this.btnFunc = new System.Windows.Forms.Button(); this.btnHist = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit(); this.SuspendLayout();
            this.dataGridView1.AllowUserToAddRows = false; this.dataGridView1.AllowUserToDeleteRows = false; this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 10); this.dataGridView1.Name = "dataGridView1"; this.dataGridView1.ReadOnly = true; this.dataGridView1.Size = new System.Drawing.Size(600, 400);
            this.btnAdd.Location = new System.Drawing.Point(630, 10); this.btnAdd.Name = "btnAdd"; this.btnAdd.Size = new System.Drawing.Size(100, 30); this.btnAdd.Text = "Добавить";
            this.btnEdit.Location = new System.Drawing.Point(630, 40); this.btnEdit.Name = "btnEdit"; this.btnEdit.Size = new System.Drawing.Size(100, 30); this.btnEdit.Text = "Изменить";
            this.btnBack.Location = new System.Drawing.Point(630, 70); this.btnBack.Name = "btnBack"; this.btnBack.Size = new System.Drawing.Size(100, 30); this.btnBack.Text = "Назад";
            this.btnAdmin.Location = new System.Drawing.Point(630, 100); this.btnAdmin.Name = "btnAdmin"; this.btnAdmin.Size = new System.Drawing.Size(100, 30); this.btnAdmin.Text = "Админка";
            this.btnFunc.Location = new System.Drawing.Point(630, 130); this.btnFunc.Name = "btnFunc"; this.btnFunc.Size = new System.Drawing.Size(100, 30); this.btnFunc.Text = "Функция 19";
            this.btnHist.Location = new System.Drawing.Point(630, 160); this.btnHist.Name = "btnHist"; this.btnHist.Size = new System.Drawing.Size(100, 30); this.btnHist.Text = "История";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 450); this.Controls.Add(this.dataGridView1); this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnBack); this.Controls.Add(this.btnAdmin); this.Controls.Add(this.btnFunc); this.Controls.Add(this.btnHist);
            this.Name = "MainForm"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Главная";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit(); this.ResumeLayout(false);
        }
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnBack, btnAdmin, btnFunc, btnHist;

        #endregion
    }
}