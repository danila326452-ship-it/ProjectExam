using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectExam
{
    public class EditDynamicsForm : Form
    {
        private int id;
        public EditDynamicsForm(int id = 0)
        {
            this.id = id;
            InitializeComponent();
            this.Text = id == 0 ? "Добавление записи динамики" : "Редактирование записи динамики";
            if (id != 0) LoadData();
            else LoadComboData();
            btnSave.Click += Save;
        }

        private void LoadComboData()
        {
            try
            {
                var entDt = DbHelper.Query("SELECT enterprise_id, name FROM enterprises ORDER BY name");
                cbEnterprise.DataSource = entDt;
                cbEnterprise.DisplayMember = "name";
                cbEnterprise.ValueMember = "enterprise_id";

                var indDt = DbHelper.Query("SELECT indicator_id, name FROM indicators ORDER BY name");
                cbIndicator.DataSource = indDt;
                cbIndicator.DisplayMember = "name";
                cbIndicator.ValueMember = "indicator_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                var dt = DbHelper.Query("SELECT * FROM dynamics WHERE record_id=@id", new MySqlParameter("@id", id));
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    
                    // Загружаем списки
                    var entDt = DbHelper.Query("SELECT enterprise_id, name FROM enterprises ORDER BY name");
                    cbEnterprise.DataSource = entDt;
                    cbEnterprise.DisplayMember = "name";
                    cbEnterprise.ValueMember = "enterprise_id";
                    cbEnterprise.SelectedValue = r["enterprise_id"];

                    var indDt = DbHelper.Query("SELECT indicator_id, name FROM indicators ORDER BY name");
                    cbIndicator.DataSource = indDt;
                    cbIndicator.DisplayMember = "name";
                    cbIndicator.ValueMember = "indicator_id";
                    cbIndicator.SelectedValue = r["indicator_id"];

                    dtpReportDate.Value = Convert.ToDateTime(r["report_date"]);
                    tbValue.Text = r["value"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save(object sender, EventArgs e)
        {
            try
            {
                if (cbEnterprise.SelectedValue == null || cbIndicator.SelectedValue == null)
                {
                    MessageBox.Show("Выберите предприятие и показатель.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal value;
                if (!decimal.TryParse(tbValue.Text, out value))
                {
                    MessageBox.Show("Значение должно быть числом.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MySqlParameter[] pars = new MySqlParameter[]
                {
                    new MySqlParameter("@e", cbEnterprise.SelectedValue),
                    new MySqlParameter("@i", cbIndicator.SelectedValue),
                    new MySqlParameter("@d", dtpReportDate.Value.Date),
                    new MySqlParameter("@v", value)
                };

                if (id == 0)
                    DbHelper.Execute("INSERT INTO dynamics (enterprise_id, indicator_id, report_date, value) VALUES (@e,@i,@d,@v)", pars);
                else
                {
                    MySqlParameter[] parsUpd = new MySqlParameter[pars.Length + 1];
                    pars.CopyTo(parsUpd, 0);
                    parsUpd[parsUpd.Length - 1] = new MySqlParameter("@id", id);
                    DbHelper.Execute("UPDATE dynamics SET enterprise_id=@e, indicator_id=@i, report_date=@d, value=@v WHERE record_id=@id", parsUpd);
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
            this.cbEnterprise = new ComboBox();
            this.cbIndicator = new ComboBox();
            this.dtpReportDate = new DateTimePicker();
            this.tbValue = new TextBox();
            this.btnSave = new Button();
            this.lblEnterprise = new Label();
            this.lblIndicator = new Label();
            this.lblReportDate = new Label();
            this.lblValue = new Label();
            this.SuspendLayout();

            int y = 20;
            
            cbEnterprise.Size = new System.Drawing.Size(250, 23);
            cbIndicator.Size = new System.Drawing.Size(250, 23);
            dtpReportDate.Size = new System.Drawing.Size(250, 23);
            dtpReportDate.Format = DateTimePickerFormat.Short;
            tbValue.Size = new System.Drawing.Size(250, 23);

            AddField(lblEnterprise, cbEnterprise, "Предприятие:", ref y);
            AddField(lblIndicator, cbIndicator, "Показатель:", ref y);
            AddField(lblReportDate, dtpReportDate, "Дата отчета:", ref y);
            AddField(lblValue, tbValue, "Значение:", ref y);

            this.btnSave.Location = new System.Drawing.Point(180, y + 10);
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.Controls.Add(this.btnSave);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "EditDynamicsForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Динамика показателей";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Вспомогательный метод вынесен за пределы InitializeComponent
        private void AddField(Label l, Control c, string txt, ref int yPos)
        {
            l.Location = new System.Drawing.Point(20, yPos);
            l.Text = txt;
            l.AutoSize = true;
            this.Controls.Add(l);
            c.Location = new System.Drawing.Point(180, yPos);
            this.Controls.Add(c);
            yPos += 40;
        }

        private ComboBox cbEnterprise, cbIndicator;
        private DateTimePicker dtpReportDate;
        private TextBox tbValue;
        private Button btnSave;
        private Label lblEnterprise, lblIndicator, lblReportDate, lblValue;
    }
}
