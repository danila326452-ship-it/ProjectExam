using MySql.Data.MySqlClient;
using ProjectExam;
using System;
using System.Windows.Forms;

namespace ProjectExam
{
    public partial class EditEnterpriseForm : Form
    {
        private int id;
        public EditEnterpriseForm(int id = 0)
        {
            this.id = id;
            InitializeComponent();
            this.Text = id == 0 ? "Добавление предприятия" : "Редактирование предприятия";
            if (id != 0) LoadData();
            btnSave.Click += Save;
        }

        private void LoadData()
        {
            var dt = DbHelper.Query("SELECT * FROM enterprises WHERE enterprise_id=@id", new MySqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                tbName.Text = r["name"].ToString();
                tbBankDetails.Text = r["bank_details"].ToString();
                tbPhone.Text = r["phone"].ToString();
                tbContactPerson.Text = r["contact_person"].ToString();
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

                MySqlParameter[] pars = new MySqlParameter[]
                {
                    new MySqlParameter("@n", tbName.Text),
                    new MySqlParameter("@b", tbBankDetails.Text ?? ""),
                    new MySqlParameter("@p", tbPhone.Text ?? ""),
                    new MySqlParameter("@c", tbContactPerson.Text ?? "")
                };

                if (id == 0)
                    DbHelper.Execute("INSERT INTO enterprises (name, bank_details, phone, contact_person) VALUES (@n,@b,@p,@c)", pars);
                else
                {
                    MySqlParameter[] parsUpd = new MySqlParameter[pars.Length + 1];
                    pars.CopyTo(parsUpd, 0);
                    parsUpd[parsUpd.Length - 1] = new MySqlParameter("@id", id);
                    DbHelper.Execute("UPDATE enterprises SET name=@n, bank_details=@b, phone=@p, contact_person=@c WHERE enterprise_id=@id", parsUpd);
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
            this.tbBankDetails = new TextBox();
            this.tbPhone = new TextBox();
            this.tbContactPerson = new TextBox();
            this.btnSave = new Button();
            this.lblName = new Label();
            this.lblBankDetails = new Label();
            this.lblPhone = new Label();
            this.lblContactPerson = new Label();
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
            AddField(lblBankDetails, tbBankDetails, "Банковские реквизиты:", ref y);
            AddField(lblPhone, tbPhone, "Телефон:", ref y);
            AddField(lblContactPerson, tbContactPerson, "Контактное лицо:", ref y);

            this.btnSave.Location = new System.Drawing.Point(180, y + 10);
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.Controls.Add(this.btnSave);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditEnterpriseForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Предприятие";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private TextBox tbName, tbBankDetails, tbPhone, tbContactPerson;
        private Button btnSave;
        private Label lblName, lblBankDetails, lblPhone, lblContactPerson;
    }
}
