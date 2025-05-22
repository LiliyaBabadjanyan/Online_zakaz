using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public enum EntryMode { Клиент, Товар }

    public partial class AddClientOrProductForm : Form
    {
        public EntryMode Mode { get; set; }

        public AddClientOrProductForm()
        {
            InitializeComponent();
        }

        // Свойства для получения введённых данных
        public string ClientName => txtName.Text.Trim();
        public string ClientEmail => txtEmail.Text.Trim();
        public string ClientPhone => txtPhone.Text.Trim();

        public string ProductName => txtProductName.Text.Trim();
        public decimal ProductPrice => nudPrice.Value;
        public int ProductQty => (int)nudQty.Value;

        private void AddClientOrProductForm_Load(object sender, EventArgs e)
        {
            if (Mode == EntryMode.Клиент)
            {
                txtName.Visible = lblName.Visible = true;
                txtEmail.Visible = lblEmail.Visible = true;
                txtPhone.Visible = lblPhone.Visible = true;

                txtProductName.Visible = lblProductName.Visible = false;
                nudPrice.Visible = lblPrice.Visible = false;
                nudQty.Visible = lblQty.Visible = false;
            }
            else
            {
                txtName.Visible = lblName.Visible = false;
                txtEmail.Visible = lblEmail.Visible = false;
                txtPhone.Visible = lblPhone.Visible = false;

                txtProductName.Visible = lblProductName.Visible = true;
                nudPrice.Visible = lblPrice.Visible = true;
                nudQty.Visible = lblQty.Visible = true;
            }
        }
        public void SetClientData(string name, string email, string phone)
        {
            txtName.Text = name;
            txtEmail.Text = email;
            txtPhone.Text = phone;
        }

        public void SetProductData(string name, decimal price, int qty)
        {
            txtProductName.Text = name;
            nudPrice.Value = price;
            nudQty.Value = qty;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Mode == EntryMode.Клиент)
            {
                if (string.IsNullOrWhiteSpace(ClientName))
                {
                    MessageBox.Show("Введите имя клиента.");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(ProductName))
                {
                    MessageBox.Show("Введите название товара.");
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
