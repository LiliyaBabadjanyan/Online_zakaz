namespace WinFormsApp1
{
    partial class AddClientOrProductForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblName;
        private Label lblEmail;
        private Label lblPhone;
        private TextBox txtName;
        private TextBox txtEmail;
        private TextBox txtPhone;

        private Label lblProductName;
        private Label lblPrice;
        private Label lblQty;
        private TextBox txtProductName;
        private NumericUpDown nudPrice;
        private NumericUpDown nudQty;

        private Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblName = new Label();
            this.lblEmail = new Label();
            this.lblPhone = new Label();
            this.txtName = new TextBox();
            this.txtEmail = new TextBox();
            this.txtPhone = new TextBox();

            this.lblProductName = new Label();
            this.lblPrice = new Label();
            this.lblQty = new Label();
            this.txtProductName = new TextBox();
            this.nudPrice = new NumericUpDown();
            this.nudQty = new NumericUpDown();

            this.btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).BeginInit();
            this.SuspendLayout();

            // lblName
            lblName.Text = "Имя:";
            lblName.Location = new System.Drawing.Point(20, 20);
            lblName.Size = new System.Drawing.Size(100, 25);

            // txtName
            txtName.Location = new System.Drawing.Point(130, 20);
            txtName.Size = new System.Drawing.Size(250, 27);

            // lblEmail
            lblEmail.Text = "Email:";
            lblEmail.Location = new System.Drawing.Point(20, 60);
            lblEmail.Size = new System.Drawing.Size(100, 25);

            // txtEmail
            txtEmail.Location = new System.Drawing.Point(130, 60);
            txtEmail.Size = new System.Drawing.Size(250, 27);

            // lblPhone
            lblPhone.Text = "Телефон:";
            lblPhone.Location = new System.Drawing.Point(20, 100);
            lblPhone.Size = new System.Drawing.Size(100, 25);

            // txtPhone
            txtPhone.Location = new System.Drawing.Point(130, 100);
            txtPhone.Size = new System.Drawing.Size(250, 27);

            // lblProductName
            lblProductName.Text = "Товар:";
            lblProductName.Location = new System.Drawing.Point(20, 20);
            lblProductName.Size = new System.Drawing.Size(100, 25);

            // txtProductName
            txtProductName.Location = new System.Drawing.Point(130, 20);
            txtProductName.Size = new System.Drawing.Size(250, 27);

            // lblPrice
            lblPrice.Text = "Цена:";
            lblPrice.Location = new System.Drawing.Point(20, 60);
            lblPrice.Size = new System.Drawing.Size(100, 25);

            // nudPrice
            nudPrice.Location = new System.Drawing.Point(130, 60);
            nudPrice.DecimalPlaces = 2;
            nudPrice.Maximum = 1000000;
            nudPrice.Minimum = 0;
            nudPrice.Size = new System.Drawing.Size(250, 27);

            // lblQty
            lblQty.Text = "Количество:";
            lblQty.Location = new System.Drawing.Point(20, 100);
            lblQty.Size = new System.Drawing.Size(100, 25);

            // nudQty
            nudQty.Location = new System.Drawing.Point(130, 100);
            nudQty.Maximum = 1000000;
            nudQty.Minimum = 0;
            nudQty.Size = new System.Drawing.Size(250, 27);

            // btnSave
            btnSave.Text = "Сохранить";
            btnSave.Location = new System.Drawing.Point(130, 150);
            btnSave.Size = new System.Drawing.Size(120, 35);
            btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // Form settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 210);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblPhone);
            this.Controls.Add(txtPhone);

            this.Controls.Add(lblProductName);
            this.Controls.Add(txtProductName);
            this.Controls.Add(lblPrice);
            this.Controls.Add(nudPrice);
            this.Controls.Add(lblQty);
            this.Controls.Add(nudQty);

            this.Controls.Add(btnSave);
            this.Name = "AddClientOrProductForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Добавление записи";
            this.Load += new System.EventHandler(this.AddClientOrProductForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
