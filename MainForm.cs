using MySql.Data.MySqlClient;
using ProjectExam;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjectExam
{
    public partial class MainForm : Form
    {
        private string role;
        private TabControl mainTabControl;
        private TabPage tabEnterprises, tabIndicators, tabDynamics, tabQueries, tabFunction, tabViews;
        
        public MainForm(string role)
        {
            this.role = role; 
            InitializeComponent(); 
            this.Text = "Главная - " + role;
            this.Size = new Size(1000, 700);
            
            SetupTabControl();
            
            btnBack.Click += (s, e) => { new AuthForm().Show(); this.Close(); };
            btnAdmin.Click += (s, e) => { if (role == "Администратор") new AdminForm().ShowDialog(); else MessageBox.Show("Доступ запрещен", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning); };
            
            LoadEnterprisesData();
        }

        private void SetupTabControl()
        {
            mainTabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Size = new Size(984, 623)
            };

            // Вкладка Предприятия
            tabEnterprises = new TabPage("Предприятия");
            SetupEnterprisesTab();
            
            // Вкладка Показатели
            tabIndicators = new TabPage("Показатели");
            SetupIndicatorsTab();
            
            // Вкладка Динамика
            tabDynamics = new TabPage("Динамика показателей");
            SetupDynamicsTab();
            
            // Вкладка Запросы
            tabQueries = new TabPage("SQL Запросы");
            SetupQueriesTab();
            
            // Вкладка Функция
            tabFunction = new TabPage("Пользовательская функция");
            SetupFunctionTab();
            
            // Вкладка Представления
            tabViews = new TabPage("Представления");
            SetupViewsTab();

            mainTabControl.TabPages.AddRange(new TabPage[] { tabEnterprises, tabIndicators, tabDynamics, tabQueries, tabFunction, tabViews });
            this.Controls.Clear();
            this.Controls.Add(mainTabControl);
            
            // Добавляем кнопку Назад поверх всех элементов
            btnBack.Parent = mainTabControl;
            btnBack.Location = new Point(880, 10);
            btnBack.Size = new Size(100, 30);
            btnBack.BringToFront();
        }

        private DataGridView dgvEnterprises;
        private Button btnAddEnt, btnEditEnt, btnDeleteEnt;
        
        private void SetupEnterprisesTab()
        {
            dgvEnterprises = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(750, 350),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };

            btnAddEnt = new Button { Text = "Добавить", Location = new Point(770, 10), Size = new Size(180, 35) };
            btnAddEnt.Click += (s, e) => { new EditEnterpriseForm().ShowDialog(); LoadEnterprisesData(); };

            btnEditEnt = new Button { Text = "Изменить", Location = new Point(770, 50), Size = new Size(180, 35) };
            btnEditEnt.Click += (s, e) => { if (dgvEnterprises.CurrentRow == null) return; new EditEnterpriseForm((int)dgvEnterprises.CurrentRow.Cells["enterprise_id"].Value).ShowDialog(); LoadEnterprisesData(); };

            btnDeleteEnt = new Button { Text = "Удалить", Location = new Point(770, 90), Size = new Size(180, 35) };
            btnDeleteEnt.Click += (s, e) => DeleteEnterprise();

            tabEnterprises.Controls.AddRange(new Control[] { dgvEnterprises, btnAddEnt, btnEditEnt, btnDeleteEnt });
        }

        private void LoadEnterprisesData()
        {
            try
            {
                var dt = DbHelper.Query(@"SELECT enterprise_id AS 'Код', name AS 'Название', 
                    bank_details AS 'Реквизиты', phone AS 'Телефон', contact_person AS 'Контактное лицо' 
                    FROM enterprises ORDER BY name");
                dgvEnterprises.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DeleteEnterprise()
        {
            if (dgvEnterprises.CurrentRow == null) return;
            var result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DbHelper.Execute("DELETE FROM enterprises WHERE enterprise_id = @id", 
                        new MySqlParameter("@id", dgvEnterprises.CurrentRow.Cells["enterprise_id"].Value));
                    LoadEnterprisesData();
                    MessageBox.Show("Запись удалена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private DataGridView dgvIndicators;
        private Button btnAddInd, btnEditInd, btnDeleteInd;

        private void SetupIndicatorsTab()
        {
            dgvIndicators = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(750, 350),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };

            btnAddInd = new Button { Text = "Добавить", Location = new Point(770, 10), Size = new Size(180, 35) };
            btnAddInd.Click += (s, e) => { new EditIndicatorForm().ShowDialog(); LoadIndicatorsData(); };

            btnEditInd = new Button { Text = "Изменить", Location = new Point(770, 50), Size = new Size(180, 35) };
            btnEditInd.Click += (s, e) => { if (dgvIndicators.CurrentRow == null) return; new EditIndicatorForm((int)dgvIndicators.CurrentRow.Cells["indicator_id"].Value).ShowDialog(); LoadIndicatorsData(); };

            btnDeleteInd = new Button { Text = "Удалить", Location = new Point(770, 90), Size = new Size(180, 35) };
            btnDeleteInd.Click += (s, e) => DeleteIndicator();

            tabIndicators.Controls.AddRange(new Control[] { dgvIndicators, btnAddInd, btnEditInd, btnDeleteInd });
        }

        private void LoadIndicatorsData()
        {
            try
            {
                var dt = DbHelper.Query(@"SELECT indicator_id AS 'Код', name AS 'Название', 
                    importance AS 'Важность', unit AS 'Ед.изм.' FROM indicators ORDER BY name");
                dgvIndicators.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DeleteIndicator()
        {
            if (dgvIndicators.CurrentRow == null) return;
            var result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DbHelper.Execute("DELETE FROM indicators WHERE indicator_id = @id", 
                        new MySqlParameter("@id", dgvIndicators.CurrentRow.Cells["indicator_id"].Value));
                    LoadIndicatorsData();
                    MessageBox.Show("Запись удалена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private DataGridView dgvDynamics;
        private Button btnAddDyn, btnEditDyn, btnDeleteDyn;

        private void SetupDynamicsTab()
        {
            dgvDynamics = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(750, 350),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };

            btnAddDyn = new Button { Text = "Добавить", Location = new Point(770, 10), Size = new Size(180, 35) };
            btnAddDyn.Click += (s, e) => { new EditDynamicsForm().ShowDialog(); LoadDynamicsData(); };

            btnEditDyn = new Button { Text = "Изменить", Location = new Point(770, 50), Size = new Size(180, 35) };
            btnEditDyn.Click += (s, e) => { if (dgvDynamics.CurrentRow == null) return; new EditDynamicsForm((int)dgvDynamics.CurrentRow.Cells["record_id"].Value).ShowDialog(); LoadDynamicsData(); };

            btnDeleteDyn = new Button { Text = "Удалить", Location = new Point(770, 90), Size = new Size(180, 35) };
            btnDeleteDyn.Click += (s, e) => DeleteDynamics();

            tabDynamics.Controls.AddRange(new Control[] { dgvDynamics, btnAddDyn, btnEditDyn, btnDeleteDyn });
        }

        private void LoadDynamicsData()
        {
            try
            {
                var dt = DbHelper.Query(@"SELECT d.record_id AS 'Код', e.name AS 'Предприятие', i.name AS 'Показатель', 
                    d.report_date AS 'Дата отчета', d.value AS 'Значение'
                    FROM dynamics d
                    JOIN enterprises e ON d.enterprise_id = e.enterprise_id
                    JOIN indicators i ON d.indicator_id = i.indicator_id
                    ORDER BY e.name, i.name, d.report_date");
                dgvDynamics.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DeleteDynamics()
        {
            if (dgvDynamics.CurrentRow == null) return;
            var result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DbHelper.Execute("DELETE FROM dynamics WHERE record_id = @id", 
                        new MySqlParameter("@id", dgvDynamics.CurrentRow.Cells["record_id"].Value));
                    LoadDynamicsData();
                    MessageBox.Show("Запись удалена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private TextBox txtQueryResult;
        private Button btnQueryA, btnQueryB, btnQueryC, btnQueryD, btnQueryE, btnQueryF, btnQueryG, btnQueryH;

        private void SetupQueriesTab()
        {
            Label lblTitle = new Label { Text = "Выберите запрос для выполнения:", Location = new Point(10, 10), AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold) };
            
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Location = new Point(10, 40),
                Size = new Size(940, 120),
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };

            btnQueryA = new Button { Text = "a. Исходные данные", Width = 220, Height = 35, Margin = new Padding(5) };
            btnQueryA.Click += (s, e) => RunQuery(QueryType.A);
            
            btnQueryB = new Button { Text = "b. Предприятия и показатели", Width = 220, Height = 35, Margin = new Padding(5) };
            btnQueryB.Click += (s, e) => RunQuery(QueryType.B);
            
            btnQueryC = new Button { Text = "c. Алфавитный список", Width = 220, Height = 35, Margin = new Padding(5) };
            btnQueryC.Click += (s, e) => RunQuery(QueryType.C);
            
            btnQueryD = new Button { Text = "d. Список с условием", Width = 220, Height = 35, Margin = new Padding(5) };
            btnQueryD.Click += (s, e) => RunQuery(QueryType.D);
            
            btnQueryE = new Button { Text = "e. Верхний регистр", Width = 220, Height = 35, Margin = new Padding(5) };
            btnQueryE.Click += (s, e) => RunQuery(QueryType.E);
            
            btnQueryF = new Button { Text = "f. Динамика по предприятию", Width = 220, Height = 35, Margin = new Padding(5) };
            btnQueryF.Click += (s, e) => RunQuery(QueryType.F);
            
            btnQueryG = new Button { Text = "g. Самый важный показатель", Width = 220, Height = 35, Margin = new Padding(5) };
            btnQueryG.Click += (s, e) => RunQuery(QueryType.G);
            
            btnQueryH = new Button { Text = "h. Среднее значение динамики", Width = 220, Height = 35, Margin = new Padding(5) };
            btnQueryH.Click += (s, e) => RunQuery(QueryType.H);

            pnlButtons.Controls.AddRange(new Control[] { btnQueryA, btnQueryB, btnQueryC, btnQueryD, btnQueryE, btnQueryF, btnQueryG, btnQueryH });

            txtQueryResult = new TextBox
            {
                Location = new Point(10, 170),
                Size = new Size(940, 350),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Font = new Font("Consolas", 9F)
            };

            tabQueries.Controls.AddRange(new Control[] { lblTitle, pnlButtons, txtQueryResult });
        }

        private enum QueryType { A, B, C, D, E, F, G, H }

        private void RunQuery(QueryType type)
        {
            try
            {
                string sql = "";
                switch (type)
                {
                    case QueryType.A:
                        sql = @"SELECT e.name AS Предприятие, e.contact_person AS Контактное_лицо, e.phone AS Телефон, 
                            i.name AS Показатель, i.importance AS Важность, i.unit AS Единица_измерения, 
                            d.report_date AS Дата_отчета, d.value AS Значение
                            FROM enterprises e JOIN dynamics d ON e.enterprise_id = d.enterprise_id
                            JOIN indicators i ON d.indicator_id = i.indicator_id ORDER BY e.name, i.name, d.report_date";
                        break;
                    case QueryType.B:
                        sql = @"SELECT e.name AS Предприятие, i.name AS Показатель FROM enterprises e CROSS JOIN indicators i ORDER BY e.name, i.name";
                        break;
                    case QueryType.C:
                        sql = @"SELECT name AS Название FROM enterprises WHERE LEFT(name, 1) BETWEEN 'А' AND 'Т' 
                            UNION ALL SELECT name AS Название FROM indicators WHERE LEFT(name, 1) BETWEEN 'А' AND 'Т' ORDER BY Название";
                        break;
                    case QueryType.D:
                        sql = @"SELECT * FROM indicators WHERE importance > 8.00 ORDER BY importance DESC";
                        break;
                    case QueryType.E:
                        sql = @"SELECT UPPER(name) AS Название_Верхний_Регистр FROM enterprises 
                            UNION ALL SELECT UPPER(name) AS Название_Верхний_Регистр FROM indicators";
                        break;
                    case QueryType.F:
                        sql = @"SELECT e.name AS Предприятие, i.name AS Показатель, d.report_date AS Дата, d.value AS Значение
                            FROM dynamics d JOIN enterprises e ON d.enterprise_id = e.enterprise_id
                            JOIN indicators i ON d.indicator_id = i.indicator_id WHERE e.enterprise_id = 1 AND d.report_date = '2023-04-01'";
                        break;
                    case QueryType.G:
                        sql = @"SELECT * FROM indicators WHERE importance = (SELECT MAX(importance) FROM indicators)";
                        break;
                    case QueryType.H:
                        sql = @"SELECT i.name AS Показатель, AVG(d.value) AS Среднее_значение FROM dynamics d 
                            JOIN indicators i ON d.indicator_id = i.indicator_id GROUP BY i.name";
                        break;
                }
                
                var dt = DbHelper.Query(sql);
                txtQueryResult.Text = DataTableToString(dt);
            }
            catch (Exception ex) { txtQueryResult.Text = "Ошибка: " + ex.Message; }
        }

        private string DataTableToString(DataTable dt)
        {
            if (dt.Rows.Count == 0) return "Нет данных";
            
            string result = "";
            foreach (DataColumn col in dt.Columns)
                result += col.ColumnName.PadRight(25);
            result += "\n" + new string('-', dt.Columns.Count * 25) + "\n";
            
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                    result += item.ToString().PadRight(25);
                result += "\n";
            }
            return result;
        }

        private TextBox txtFuncEnterprise, txtFuncIndicator, txtFuncDateStart, txtFuncDateEnd, txtFuncResult;
        private Button btnRunFunction;

        private void SetupFunctionTab()
        {
            GroupBox grpParams = new GroupBox
            {
                Text = "Параметры функции",
                Location = new Point(10, 10),
                Size = new Size(450, 200)
            };

            Label lbl1 = new Label { Text = "Код предприятия:", Location = new Point(20, 30), AutoSize = true };
            txtFuncEnterprise = new TextBox { Location = new Point(150, 27), Size = new Size(100, 23), Text = "1" };

            Label lbl2 = new Label { Text = "Код показателя:", Location = new Point(20, 60), AutoSize = true };
            txtFuncIndicator = new TextBox { Location = new Point(150, 57), Size = new Size(100, 23), Text = "1" };

            Label lbl3 = new Label { Text = "Дата начала:", Location = new Point(20, 90), AutoSize = true };
            txtFuncDateStart = new DateTimePicker { Location = new Point(150, 87), Size = new Size(150, 23), Value = new DateTime(2023, 1, 1), Format = DateTimePickerFormat.Short };

            Label lbl4 = new Label { Text = "Дата окончания:", Location = new Point(20, 120), AutoSize = true };
            txtFuncDateEnd = new DateTimePicker { Location = new Point(150, 117), Size = new Size(150, 23), Value = new DateTime(2023, 4, 1), Format = DateTimePickerFormat.Short };

            btnRunFunction = new Button { Text = "Вычислить динамику", Location = new Point(20, 155), Size = new Size(280, 35), Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold) };
            btnRunFunction.Click += (s, e) => CalculateDynamics();

            grpParams.Controls.AddRange(new Control[] { lbl1, txtFuncEnterprise, lbl2, txtFuncIndicator, lbl3, txtFuncDateStart, lbl4, txtFuncDateEnd, btnRunFunction });

            GroupBox grpResult = new GroupBox
            {
                Text = "Результат",
                Location = new Point(480, 10),
                Size = new Size(470, 200)
            };

            txtFuncResult = new TextBox
            {
                Location = new Point(20, 30),
                Size = new Size(430, 150),
                Multiline = true,
                ReadOnly = true,
                Font = new Font("Consolas", 12F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center
            };

            grpResult.Controls.Add(txtFuncResult);
            tabFunction.Controls.AddRange(new Control[] { grpParams, grpResult });
        }

        private void CalculateDynamics()
        {
            try
            {
                int entId = int.Parse(txtFuncEnterprise.Text);
                int indId = int.Parse(txtFuncIndicator.Text);
                DateTime dateStart = txtFuncDateStart.Value.Date;
                DateTime dateEnd = txtFuncDateEnd.Value.Date;

                var dt = DbHelper.Query("SELECT CalculateDynamicsValue(@e, @i, @ds, @de) as result",
                    new MySqlParameter("@e", entId),
                    new MySqlParameter("@i", indId),
                    new MySqlParameter("@ds", dateStart.ToString("yyyy-MM-dd")),
                    new MySqlParameter("@de", dateEnd.ToString("yyyy-MM-dd")));

                decimal result = Convert.ToDecimal(dt.Rows[0]["result"]);
                txtFuncResult.Text = $"{result:F2}%\n\nИзменение показателя за период:\nс {dateStart:dd.MM.yyyy} по {dateEnd:dd.MM.yyyy}";
                
                if (result > 0)
                    txtFuncResult.ForeColor = Color.Green;
                else if (result < 0)
                    txtFuncResult.ForeColor = Color.Red;
                else
                    txtFuncResult.ForeColor = Color.Black;
            }
            catch (Exception ex) { txtFuncResult.Text = "Ошибка: " + ex.Message; txtFuncResult.ForeColor = Color.Red; }
        }

        private DataGridView dgvViews;
        private Button btnView1, btnView2;

        private void SetupViewsTab()
        {
            Label lblTitle = new Label { Text = "Представления (Views):", Location = new Point(10, 10), AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold) };

            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Location = new Point(10, 40),
                Size = new Size(940, 60),
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            btnView1 = new Button { Text = "vw_InitialData (Исходные данные)", Width = 250, Height = 40, Margin = new Padding(5) };
            btnView1.Click += (s, e) => ShowView("vw_InitialData");

            btnView2 = new Button { Text = "vw_EnterpriseDynamics (Динамика)", Width = 250, Height = 40, Margin = new Padding(5) };
            btnView2.Click += (s, e) => ShowView("vw_EnterpriseDynamics");

            pnlButtons.Controls.AddRange(new Control[] { btnView1, btnView2 });

            dgvViews = new DataGridView
            {
                Location = new Point(10, 110),
                Size = new Size(940, 410),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };

            tabViews.Controls.AddRange(new Control[] { lblTitle, pnlButtons, dgvViews });
        }

        private void ShowView(string viewName)
        {
            try
            {
                var dt = DbHelper.Query($"SELECT * FROM {viewName}");
                dgvViews.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void InitializeComponent()
        {
            btnBack = new System.Windows.Forms.Button();
            btnAdmin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(0, 0);
            this.dataGridView1.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(880, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // btnAdmin
            // 
            this.btnAdmin.Location = new System.Drawing.Point(880, 50);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(100, 30);
            this.btnAdmin.TabIndex = 1;
            this.btnAdmin.Text = "Админка";
            this.btnAdmin.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Аналитика холдинга";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }
        
        // Оставляем старые кнопки для совместимости
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnFunc, btnHist;
    }
}