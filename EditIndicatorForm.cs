using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectExam
{
    public partial class EditIndicatorForm : Form
    {
        private int id;
        public EditIndicatorForm(int id = 0)
        {
            this.id = id;
            InitializeComponent();
            this.Text = id == 0 ? "Добавление показателя" : "Редактирование показателя";
            if (id != 0) LoadData();
            btnSave.Click += Save;
        }

        private void LoadData()
        {
            var dt = DbHelper.Query("SELECT * FROM indicators WHERE indicator_id=@id", new MySqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                tbName.Text = r["name"].ToString();
                tbImportance.Text = r["importance"].ToString();
                tbUnit.Text = r["unit"].ToString();
            }
        }

        private void Save(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbName.Text))
                {
                    MessageBox.Show("Название обязательно для заполнения.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal importance;
                if (!decimal.TryParse(tbImportance.Text, out importance))
                {
                    MessageBox.Show("Важность должна быть числом.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MySqlParameter[] pars = new MySqlParameter[]
                {
                    new MySqlParameter("@n", tbName.Text),
                    new MySqlParameter("@i", importance),
                    new MySqlParameter("@u", tbUnit.Text ?? "")
                };

                if (id == 0)
                    DbHelper.Execute("INSERT INTO indicators (name, importance, unit) VALUES (@n,@i,@u)", pars);
                else
                {
                    MySqlParameter[] parsUpd = new MySqlParameter[pars.Length + 1];
                    pars.CopyTo(parsUpd, 0);
                    parsUpd[parsUpd.Length - 1] = new MySqlParameter("@id", id);
                    DbHelper.Execute("UPDATE indicators SET name=@n, importance=@i, unit=@u WHERE indicator_id=@id", parsUpd);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.tbName = new TextBox();
            this.tbImportance = new TextBox();
            this.tbUnit = new TextBox();
            this.btnSave = new Button();
            this.lblName = new Label();
            this.lblImportance = new Label();
            this.lblUnit = new Label();
            this.SuspendLayout();

            int y = 20;
            void AddField(Label l, TextBox t, string txt, ref int yPos)
            {
                l.Location = new System.Drawing.Point(20, yPos);
                l.Text = txt;
                l.AutoSize = true;
                this.Controls.Add(l);
                t.Location = new System.Drawing.Point(180, yPos);
                t.Size = new System.Drawing.Size(250, 23);
                this.Controls.Add(t);
                yPos += 40;
            }

            AddField(lblName, tbName, "Название:", ref y);
            AddField(lblImportance, tbImportance, "Важность:", ref y);
            AddField(lblUnit, tbUnit, "Ед. измерения:", ref y);

            this.btnSave.Location = new System.Drawing.Point(180, y + 10);
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.Controls.Add(this.btnSave);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditIndicatorForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Показатель";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private TextBox tbName, tbImportance, tbUnit;
        private Button btnSave;
        private Label lblName, lblImportance, lblUnit;
    }
}
