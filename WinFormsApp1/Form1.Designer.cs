namespace WinFormsApp1
{
    partial class Form1
    {
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageOrder;
        private System.Windows.Forms.TabPage tabPageReport;


        private System.Windows.Forms.ComboBox comboBoxCustomers;
        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.Button btnSubmitOrder;

        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.DataGridView dataGridViewReport;

        private TabPage tabPageDelivery;
        private ComboBox comboBoxOrders;
        private DataGridView dataGridViewDelivery;
        private TextBox txtInvoiceNumber;
        private Button btnCreateDelivery;
        private System.Windows.Forms.ComboBox comboBoxReportType;

        private TabPage tabPageManagement;
        private ComboBox comboBoxTableSelect;
        private DataGridView dataGridViewEdit;
        private Button btnAdd;
        private Button btnSave;
        private Button btnDelete;
        private Button btnEdit;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartReport;




        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            tabControlMain = new TabControl();
            tabPageOrder = new TabPage();
            comboBoxCustomers = new ComboBox();

            dataGridViewProducts = new DataGridView();
            btnSubmitOrder = new Button();
            tabPageReport = new TabPage();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            btnGenerateReport = new Button();
            btnExportExcel = new Button();
            dataGridViewReport = new DataGridView();
            tabControlMain.SuspendLayout();
            tabPageOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).BeginInit();
            tabPageReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).BeginInit();
            SuspendLayout();
            // 
            // tabControlMain


            // 
            tabControlMain.Controls.Add(tabPageOrder);
            tabControlMain.Controls.Add(tabPageReport);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(0, 0);
            tabControlMain.Margin = new Padding(3, 4, 3, 4);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1029, 800);
            tabControlMain.TabIndex = 0;
            // 
            // tabPageOrder
            // 
            tabPageOrder.Controls.Add(comboBoxCustomers);
            tabPageOrder.Controls.Add(dataGridViewProducts);
            tabPageOrder.Controls.Add(btnSubmitOrder);
            tabPageOrder.Location = new Point(4, 29);
            tabPageOrder.Margin = new Padding(3, 4, 3, 4);
            tabPageOrder.Name = "tabPageOrder";
            tabPageOrder.Padding = new Padding(3, 4, 3, 4);
            tabPageOrder.Size = new Size(1021, 767);
            tabPageOrder.TabIndex = 0;
            tabPageOrder.Text = "Создать заказ";
            tabPageOrder.UseVisualStyleBackColor = true;
            // 
            // comboBoxCustomers
            // 
            // === tabPageManagement ===
            tabPageManagement = new TabPage();
            tabPageManagement.Text = "Управление";
            tabPageManagement.UseVisualStyleBackColor = true;
            tabPageManagement.Padding = new Padding(3);
            tabPageManagement.Size = new Size(1021, 767);

            // comboBoxTableSelect
            comboBoxTableSelect = new ComboBox();
            comboBoxTableSelect.Location = new Point(17, 20);
            comboBoxTableSelect.Size = new Size(200, 28);
            comboBoxTableSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTableSelect.Items.AddRange(new[] { "Клиенты", "Товары" });
            comboBoxTableSelect.SelectedIndexChanged += comboBoxTableSelect_SelectedIndexChanged;
            tabPageManagement.Controls.Add(comboBoxTableSelect);

            // dataGridViewEdit
            dataGridViewEdit = new DataGridView();
            dataGridViewEdit.Location = new Point(17, 60);
            dataGridViewEdit.Size = new Size(983, 600);
            dataGridViewEdit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabPageManagement.Controls.Add(dataGridViewEdit);

            // btnAdd
            btnAdd = new Button();
            btnAdd.Text = "Добавить";
            btnAdd.Location = new Point(17, 680);
            btnAdd.Size = new Size(100, 40);
            btnAdd.Click += btnAdd_Click;
            tabPageManagement.Controls.Add(btnAdd);

            // btnSave
            btnSave = new Button();
            btnSave.Text = "Сохранить";
            btnSave.Location = new Point(130, 680);
            btnSave.Size = new Size(100, 40);
            btnSave.Click += btnSave_Click;
            tabPageManagement.Controls.Add(btnSave);

            // btnDelete
            btnDelete = new Button();
            btnDelete.Text = "Удалить";
            btnDelete.Location = new Point(243, 680);
            btnDelete.Size = new Size(100, 40);
            btnDelete.Click += btnDelete_Click;
            tabPageManagement.Controls.Add(btnDelete);

            // добавить вкладку в tabControl
            tabControlMain.Controls.Add(tabPageManagement);


            comboBoxCustomers.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCustomers.Location = new Point(17, 20);
            comboBoxCustomers.Margin = new Padding(3, 4, 3, 4);
            comboBoxCustomers.Name = "comboBoxCustomers";
            comboBoxCustomers.Size = new Size(285, 28);
            comboBoxCustomers.TabIndex = 0;
            // 
            // dataGridViewProducts
            // 
            dataGridViewProducts.AllowUserToAddRows = false;
            dataGridViewProducts.AllowUserToDeleteRows = false;
            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewProducts.Location = new Point(17, 67);
            dataGridViewProducts.Margin = new Padding(3, 4, 3, 4);
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.RowHeadersVisible = false;
            dataGridViewProducts.RowHeadersWidth = 51;
            dataGridViewProducts.RowTemplate.Height = 25;
            dataGridViewProducts.Size = new Size(983, 600);
            dataGridViewProducts.TabIndex = 1;
            // 
            // btnSubmitOrder
            // 
            btnSubmitOrder.Location = new Point(17, 687);
            btnSubmitOrder.Margin = new Padding(3, 4, 3, 4);
            btnSubmitOrder.Name = "btnSubmitOrder";
            btnSubmitOrder.Size = new Size(171, 40);
            btnSubmitOrder.TabIndex = 2;
            btnSubmitOrder.Text = "Создать заказ";
            btnSubmitOrder.UseVisualStyleBackColor = true;
            btnSubmitOrder.Click += btnSubmitOrder_Click;
            // 
            // tabPageReport
            // 
            tabPageReport.Controls.Add(dtpStart);
            tabPageReport.Controls.Add(dtpEnd);
            tabPageReport.Controls.Add(btnGenerateReport);
            tabPageReport.Controls.Add(btnExportExcel);
            tabPageReport.Controls.Add(dataGridViewReport);
            tabPageReport.Location = new Point(4, 29);
            tabPageReport.Margin = new Padding(3, 4, 3, 4);
            tabPageReport.Name = "tabPageReport";
            tabPageReport.Padding = new Padding(3, 4, 3, 4);
            tabPageReport.Size = new Size(1021, 767);
            tabPageReport.TabIndex = 1;
            tabPageReport.Text = "Отчёты";
            tabPageReport.UseVisualStyleBackColor = true;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(17, 20);
            dtpStart.Margin = new Padding(3, 4, 3, 4);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(171, 27);
            dtpStart.TabIndex = 0;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(206, 20);
            dtpEnd.Margin = new Padding(3, 4, 3, 4);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(171, 27);
            dtpEnd.TabIndex = 1;
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.Location = new Point(400, 13);
            btnGenerateReport.Margin = new Padding(3, 4, 3, 4);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Size = new Size(137, 40);
            btnGenerateReport.TabIndex = 2;
            btnGenerateReport.Text = "Сформировать";
            btnGenerateReport.UseVisualStyleBackColor = true;
            btnGenerateReport.Click += btnGenerateReport_Click;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(549, 13);
            btnExportExcel.Margin = new Padding(3, 4, 3, 4);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(137, 40);
            btnExportExcel.TabIndex = 3;
            btnExportExcel.Text = "Экспорт в Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // dataGridViewReport
            // 
            dataGridViewReport.AllowUserToAddRows = false;
            dataGridViewReport.AllowUserToDeleteRows = false;
            dataGridViewReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReport.Location = new Point(17, 67);
            dataGridViewReport.Margin = new Padding(3, 4, 3, 4);
            dataGridViewReport.Name = "dataGridViewReport";
            dataGridViewReport.RowHeadersVisible = false;
            dataGridViewReport.RowHeadersWidth = 51;
            dataGridViewReport.RowTemplate.Height = 25;
            dataGridViewReport.Size = new Size(983, 320);
            dataGridViewReport.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 800);
            Controls.Add(tabControlMain);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Интернет-магазин";
            Load += Form1_Load;
            tabControlMain.ResumeLayout(false);
            tabPageOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).EndInit();
            tabPageReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewReport).EndInit();
            ResumeLayout(false);

            // === tabPageDelivery ===
            tabPageDelivery = new TabPage();
            comboBoxOrders = new ComboBox();
            dataGridViewDelivery = new DataGridView();
            txtInvoiceNumber = new TextBox();
            btnCreateDelivery = new Button();

            // tabPageDelivery
            tabPageDelivery.Text = "Оформить доставку";
            tabPageDelivery.UseVisualStyleBackColor = true;
            tabPageDelivery.Padding = new Padding(3, 4, 3, 4);
            tabPageDelivery.Size = new Size(1021, 767);

            // comboBoxOrders
            comboBoxOrders.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOrders.Location = new Point(17, 20);
            comboBoxOrders.Size = new Size(300, 28);
            comboBoxOrders.SelectedIndexChanged += comboBoxOrders_SelectedIndexChanged;
            tabPageDelivery.Controls.Add(comboBoxOrders);

            // txtInvoiceNumber
            txtInvoiceNumber.Location = new Point(340, 20);
            txtInvoiceNumber.Size = new Size(200, 27);
            txtInvoiceNumber.PlaceholderText = "Номер накладной";
            tabPageDelivery.Controls.Add(txtInvoiceNumber);

            // dataGridViewDelivery
            dataGridViewDelivery.Location = new Point(17, 60);
            dataGridViewDelivery.Size = new Size(983, 600);
            dataGridViewDelivery.AllowUserToAddRows = false;
            dataGridViewDelivery.AllowUserToDeleteRows = false;
            dataGridViewDelivery.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabPageDelivery.Controls.Add(dataGridViewDelivery);

            // btnCreateDelivery
            btnCreateDelivery.Text = "Создать накладную";
            btnCreateDelivery.Location = new Point(17, 680);
            btnCreateDelivery.Size = new Size(200, 40);
            btnCreateDelivery.Click += btnCreateDelivery_Click;
            tabPageDelivery.Controls.Add(btnCreateDelivery);

            // Добавить вкладку
            tabControlMain.Controls.Add(tabPageDelivery);

            comboBoxReportType = new ComboBox();
            comboBoxReportType.Items.Add("По товарам");
            comboBoxReportType.Items.Add("По клиентам");
            comboBoxReportType.SelectedIndex = 0;
            comboBoxReportType.Location = new Point(720, 20);
            comboBoxReportType.Size = new Size(180, 28);
            tabPageReport.Controls.Add(comboBoxReportType);

            dtpOrderDate = new DateTimePicker();
            dtpOrderDate.Location = new Point(320, 20);
            dtpOrderDate.Size = new Size(200, 27);
            dtpOrderDate.Name = "dtpOrderDate";
            tabPageOrder.Controls.Add(dtpOrderDate);

            btnEdit = new Button();
            btnEdit.Text = "Изменить";
            btnEdit.Location = new Point(356, 680);
            btnEdit.Size = new Size(100, 40);
            btnEdit.Click += btnEdit_Click;
            tabPageManagement.Controls.Add(btnEdit);

            chartReport = new System.Windows.Forms.DataVisualization.Charting.Chart();
            // chartReport
            chartReport.Location = new Point(17, 400);
            chartReport.Size = new Size(983, 320);
            chartReport.Visible = false;
            tabPageReport.Controls.Add(chartReport);



        }

        #endregion
    }
}
