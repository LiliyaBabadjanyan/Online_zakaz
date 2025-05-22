using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.ExtendedProperties;
using Npgsql;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private DbManager db;

        public Form1()
        {
            InitializeComponent();
            btnExportExcel.Click += btnExportExcel_Click;
            db = new DbManager("Host=127.0.0.1;Username=postgres;Password=Ilovechehov17105;Database=Dostavka");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCustomersToComboBox();
            LoadProductsToGrid();
            var orders = db.GetData("SELECT OrderID, 'Заказ #' || OrderID || ' (' || TotalAmount || ' руб)' as Label FROM Orders");
            comboBoxOrders.DataSource = orders;
            comboBoxOrders.ValueMember = "OrderID";
            comboBoxOrders.DisplayMember = "Label";
            chartReport.Visible = true;
        }

        private void comboBoxTableSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string table = comboBoxTableSelect.SelectedItem.ToString();
            string query = table == "Клиенты"
                ? "SELECT CustomerID, Name, Email, Phone FROM Customers ORDER BY CustomerID"
                : "SELECT ProductID, Name, Price, StockQty FROM Products ORDER BY ProductID";

            dataGridViewEdit.DataSource = db.ExecuteQuery(query);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string table = comboBoxTableSelect.SelectedItem?.ToString();
            if (table == null) return;

            var form = new AddClientOrProductForm();

            if (table == "Клиенты")
                form.Mode = EntryMode.Клиент;
            else
                form.Mode = EntryMode.Товар;

            if (form.ShowDialog() == DialogResult.OK)
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();

                    if (form.Mode == EntryMode.Клиент)
                    {
                        var cmd = new NpgsqlCommand("INSERT INTO Customers (Name, Email, Phone) VALUES (@n, @e, @p)", conn);
                        cmd.Parameters.AddWithValue("@n", form.ClientName);
                        cmd.Parameters.AddWithValue("@e", form.ClientEmail);
                        cmd.Parameters.AddWithValue("@p", form.ClientPhone);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        var cmd = new NpgsqlCommand("INSERT INTO Products (Name, Price, StockQty) VALUES (@n, @p, @q)", conn);
                        cmd.Parameters.AddWithValue("@n", form.ProductName);
                        cmd.Parameters.AddWithValue("@p", form.ProductPrice);
                        cmd.Parameters.AddWithValue("@q", form.ProductQty);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Обновить таблицу после добавления
                comboBoxTableSelect_SelectedIndexChanged(null, null);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewEdit.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для редактирования.");
                return;
            }

            string table = comboBoxTableSelect.SelectedItem?.ToString();
            if (table == null) return;

            var row = dataGridViewEdit.SelectedRows[0];
            var form = new AddClientOrProductForm();

            if (table == "Клиенты")
            {  form.Mode = EntryMode.Клиент;
                form.SetClientData(
                    row.Cells["Name"].Value?.ToString(),
                    row.Cells["Email"].Value?.ToString(),
                    row.Cells["Phone"].Value?.ToString()
                );
            }
            else
            {
                form.Mode = EntryMode.Товар;
                form.SetProductData(
                    row.Cells["Name"].Value?.ToString(),
                    Convert.ToDecimal(row.Cells["Price"].Value),
                    Convert.ToInt32(row.Cells["StockQty"].Value)
                );
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();

                    if (form.Mode == EntryMode.Клиент)
                    {
                        var cmd = new NpgsqlCommand("UPDATE Customers SET Name = @n, Email = @e, Phone = @p WHERE CustomerID = @id", conn);
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(row.Cells["CustomerID"].Value));
                        cmd.Parameters.AddWithValue("@n", form.ClientName);
                        cmd.Parameters.AddWithValue("@e", form.ClientEmail);
                        cmd.Parameters.AddWithValue("@p", form.ClientPhone);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        var cmd = new NpgsqlCommand("UPDATE Products SET Name = @n, Price = @p, StockQty = @q WHERE ProductID = @id", conn);
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(row.Cells["ProductID"].Value));
                        cmd.Parameters.AddWithValue("@n", form.ProductName);
                        cmd.Parameters.AddWithValue("@p", form.ProductPrice);
                        cmd.Parameters.AddWithValue("@q", form.ProductQty);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Обновляем таблицу
                comboBoxTableSelect_SelectedIndexChanged(null, null);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewEdit.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewEdit.SelectedRows)
                {
                    dataGridViewEdit.Rows.Remove(row);
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string table = comboBoxTableSelect.SelectedItem.ToString();
            string sqlSelect = table == "Клиенты"
                ? "SELECT CustomerID, Name, Email, Phone FROM Customers"
                : "SELECT ProductID, Name, Price, StockQty FROM Products";

            var adapter = db.GetAdapter(sqlSelect, table == "Клиенты" ? "Customers" : "Products");

            var dt = (DataTable)dataGridViewEdit.DataSource;
            try
            {
                adapter.Update(dt);
                MessageBox.Show("Изменения сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }


        private void RefreshOrderListForDelivery()
        {
            var orders = db.GetData("SELECT OrderID, 'Заказ #' || OrderID || ' (' || TotalAmount || ' руб)' as Label FROM Orders");
            comboBoxOrders.DataSource = orders;
            comboBoxOrders.ValueMember = "OrderID";
            comboBoxOrders.DisplayMember = "Label";
        }
        private bool isReportGenerated = false;


        private void comboBoxOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOrders.SelectedValue == null) return;
            int orderId = (int)comboBoxOrders.SelectedValue;

            string sql = @"
        SELECT 
            p.ProductID,
            p.Name,
            oi.Quantity AS OrderedQuantity,
            COALESCE(SUM(di.DeliveredQty), 0) AS AlreadyDelivered,
            (oi.Quantity - COALESCE(SUM(di.DeliveredQty), 0)) AS RemainingQty,
            0 AS DeliveredQty -- пользователь вводит
        FROM OrderItems oi
        JOIN Products p ON p.ProductID = oi.ProductID
        LEFT JOIN DeliveryItems di ON di.ProductID = oi.ProductID
        LEFT JOIN Deliveries d ON d.DeliveryID = di.DeliveryID AND d.OrderID = oi.OrderID
        WHERE oi.OrderID = @orderId
        GROUP BY p.ProductID, p.Name, oi.Quantity;
    ";

            var dt = db.ExecuteQuery(sql, new[] {
        new NpgsqlParameter("orderId", orderId)
    });

            dataGridViewDelivery.DataSource = dt;

            // Можно отключить редактирование всех колонок, кроме DeliveredQty
            foreach (DataGridViewColumn col in dataGridViewDelivery.Columns)
            {
                col.ReadOnly = col.Name != "DeliveredQty";
            }
        }


        private void LoadCustomersToComboBox()
        {
            string sql = "SELECT CustomerID, Name FROM Customers ORDER BY Name;";
            DataTable dt = db.GetData(sql);
            comboBoxCustomers.DataSource = dt;
            comboBoxCustomers.DisplayMember = "Name";
            comboBoxCustomers.ValueMember = "CustomerID";
        }

        private void LoadProductsToGrid()
        {
            string sql = "SELECT ProductID, Name, Price FROM Products ORDER BY Name;";
            DataTable dt = db.GetData(sql);
            dt.Columns.Add("Quantity", typeof(int));

            foreach (DataRow row in dt.Rows)
                row["Quantity"] = 0;

            dataGridViewProducts.DataSource = dt;
        }

        private void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            if (comboBoxCustomers.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента.");
                return;
            }

            int customerId = (int)comboBoxCustomers.SelectedValue;

            // ✅ Дата из календаря
            DateTime orderDate = dtpOrderDate.Value.Date;

            decimal totalAmount = 0;
            var orderItems = new List<(int productId, int quantity, decimal amount)>();

            foreach (DataGridViewRow row in dataGridViewProducts.Rows)
            {
                if (row.Cells["Quantity"].Value != null &&
                    int.TryParse(row.Cells["Quantity"].Value.ToString(), out int qty) && qty > 0)
                {
                    int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                    decimal amount = price * qty;

                    orderItems.Add((productId, qty, amount));
                    totalAmount += amount;
                }
            }

            if (orderItems.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один товар.");
                return;
            }

            using (var conn = db.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 🔥 ВАЖНО: передаём дату В ЯВНОМ ВИДЕ
                        var cmd = new NpgsqlCommand(
                            "INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) VALUES (@cid, @date, @total) RETURNING OrderID", conn);
                        cmd.Transaction = trans;

                        cmd.Parameters.AddWithValue("@cid", customerId);
                        cmd.Parameters.Add(new NpgsqlParameter("@date", NpgsqlTypes.NpgsqlDbType.Date) { Value = orderDate });
                        cmd.Parameters.AddWithValue("@total", totalAmount);

                        int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                        foreach (var item in orderItems)
                        {
                            var itemCmd = new NpgsqlCommand(
                                "INSERT INTO OrderItems (OrderID, ProductID, Quantity, Amount) VALUES (@oid, @pid, @qty, @amt)", conn);
                            itemCmd.Transaction = trans;

                            itemCmd.Parameters.AddWithValue("@oid", orderId);
                            itemCmd.Parameters.AddWithValue("@pid", item.productId);
                            itemCmd.Parameters.AddWithValue("@qty", item.quantity);
                            itemCmd.Parameters.AddWithValue("@amt", item.amount);
                            itemCmd.ExecuteNonQuery();
                        }

                        trans.Commit();

                        MessageBox.Show($"✅ Заказ создан на дату: {orderDate:yyyy-MM-dd}");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("❌ Ошибка: " + ex.Message);
                    }
                }
            }
        }


        private void GenerateReportByProducts(DateTime start, DateTime end)
        {
            string sql = @"
        SELECT 
            p.Name AS Product,
            COALESCE(SUM(oi.Amount), 0) AS TotalOrdered,
            COALESCE(SUM(di.DeliveredQty * p.Price), 0) AS TotalDelivered
        FROM Products p
        LEFT JOIN OrderItems oi ON oi.ProductID = p.ProductID
        LEFT JOIN Orders o ON o.OrderID = oi.OrderID AND o.OrderDate BETWEEN @date_start AND @date_end
        LEFT JOIN DeliveryItems di ON di.ProductID = p.ProductID
        LEFT JOIN Deliveries d ON d.DeliveryID = di.DeliveryID AND d.DeliveryDate BETWEEN @date_start AND @date_end
        GROUP BY p.Name
        ORDER BY p.Name;
    ";

            var dt = db.ExecuteQuery(sql, new[]
            {
        new NpgsqlParameter("date_start", start),
        new NpgsqlParameter("date_end", end)
    });

            dataGridViewReport.DataSource = dt;
        }


        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            isReportGenerated = true;
            DateTime start = dtpStart.Value.Date;
            DateTime end = dtpEnd.Value.Date;

            string reportType = comboBoxReportType?.SelectedItem?.ToString() ?? "По товарам";

            if (reportType == "По клиентам")
            {
                string sql = @"
           SELECT 
               d.day::date AS Date,
               c.Name AS Customer,
               COALESCE(SUM(o.TotalAmount), 0) AS TotalOrdered,
               COALESCE(SUM(dd.d_deliv), 0) AS TotalDelivered
           FROM
               (SELECT generate_series(@date_start::date, @date_end::date, INTERVAL '1 day')::date AS day) d
           CROSS JOIN Customers c
           LEFT JOIN Orders o ON o.CustomerID = c.CustomerID AND o.OrderDate = d.day
           LEFT JOIN (
               SELECT 
                   d2.DeliveryDate::date AS deliv_date,
                   d2.OrderID,
                   SUM(di.DeliveredQty * p.Price) AS d_deliv
               FROM Deliveries d2
               JOIN DeliveryItems di ON di.DeliveryID = d2.DeliveryID
               JOIN Orders o2 ON o2.OrderID = d2.OrderID
               JOIN Products p ON p.ProductID = di.ProductID
               GROUP BY d2.DeliveryDate::date, d2.OrderID
           ) dd ON dd.deliv_date = d.day AND dd.OrderID = o.OrderID
           GROUP BY d.day, c.Name
           ORDER BY d.day, c.Name;
        ";

                var dt = db.ExecuteQuery(sql, new[]
                {
            new NpgsqlParameter("date_start", start),
            new NpgsqlParameter("date_end", end)
        });

                dataGridViewReport.DataSource = dt;
                BuildChartByClients(); // 📈 строим линейную диаграмму
            }
            else // по товарам
            {
                string sql = @"
            SELECT 
                p.Name AS Product,
                COALESCE(SUM(oi.Amount), 0) AS TotalOrdered,
                COALESCE(SUM(di.DeliveredQty * p.Price), 0) AS TotalDelivered
            FROM Products p
            LEFT JOIN OrderItems oi ON oi.ProductID = p.ProductID
            LEFT JOIN Orders o ON o.OrderID = oi.OrderID AND o.OrderDate BETWEEN @start AND @end
            LEFT JOIN DeliveryItems di ON di.ProductID = p.ProductID
            LEFT JOIN Deliveries d ON d.DeliveryID = di.DeliveryID AND d.DeliveryDate BETWEEN @start AND @end
            GROUP BY p.Name
            ORDER BY p.Name;
        ";

                var dt = db.ExecuteQuery(sql, new[]
                {
            new NpgsqlParameter("start", start),
            new NpgsqlParameter("end", end)
        });

                dataGridViewReport.DataSource = dt;
                BuildChartByProducts(); // 📊 строим столбчатую диаграмму
            }
        }



        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dataGridViewReport.DataSource == null)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel файл (*.xlsx)|*.xlsx";
                sfd.FileName = "Отчет.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Преобразуем DataGridView в DataTable
                        var dt = new DataTable();

                        foreach (DataGridViewColumn col in dataGridViewReport.Columns)
                        {
                            dt.Columns.Add(col.HeaderText);
                        }

                        foreach (DataGridViewRow row in dataGridViewReport.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                var newRow = dt.NewRow();
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    newRow[i] = row.Cells[i].Value ?? "";
                                }
                                dt.Rows.Add(newRow);
                            }
                        }

                        // Создание Excel-файла
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Отчёт");

                            // 🗓 Вставим строку с диапазоном дат
                            worksheet.Cell(1, 1).Value = $"Период отчёта: с {dtpStart.Value:yyyy-MM-dd} по {dtpEnd.Value:yyyy-MM-dd}";
                            worksheet.Range(1, 1, 1, dt.Columns.Count).Merge().Style.Font.SetBold(); // Объединить ячейки и выделить жирным

                            // Вставим таблицу начиная со 2-й строки
                            worksheet.Cell(2, 1).InsertTable(dt, "ReportData", true);

                            worksheet.Columns().AdjustToContents(); // автоширина

                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Файл успешно сохранён.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при экспорте: " + ex.Message);
                    }
                }
            }
        }

        private void BuildChartByProducts()
        {
            chartReport.Visible = true;
            chartReport.Series.Clear();
            chartReport.ChartAreas.Clear();
            chartReport.Titles.Clear();

            // Фон — чтобы было видно точно
            chartReport.BackColor = Color.WhiteSmoke;

            // Добавляем область
            ChartArea area = new ChartArea("MainArea");
            chartReport.ChartAreas.Add(area);

            Series seriesOrdered = new Series("Заказы")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.SteelBlue
            };

            Series seriesDelivered = new Series("Доставки")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.MediumSeaGreen
            };

            var dt = (DataTable)dataGridViewReport.DataSource;
            if (dt == null)
            {
                MessageBox.Show("Нет данных для построения графика.");
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                string name = row["Product"].ToString();
                decimal ordered = Convert.ToDecimal(row["TotalOrdered"]);
                decimal delivered = Convert.ToDecimal(row["TotalDelivered"]);

                seriesOrdered.Points.AddXY(name, ordered);
                seriesDelivered.Points.AddXY(name, delivered);
            }

            chartReport.Series.Add(seriesOrdered);
            chartReport.Series.Add(seriesDelivered);
            chartReport.Titles.Add("Сравнение заказов и доставок по товарам");
        }

        private void BuildChartByClients()
        {
            chartReport.Visible = true;
            chartReport.Series.Clear();
            chartReport.ChartAreas.Clear();
            chartReport.Titles.Clear();

            var chartArea = new ChartArea("Main");
            chartArea.AxisX.Title = "Дата";
            chartArea.AxisY.Title = "Сумма";
            chartReport.ChartAreas.Add(chartArea);

            var dt = (DataTable)dataGridViewReport.DataSource;

            // Группировка по клиенту
            var clients = dt.AsEnumerable()
                .Select(r => r["Customer"].ToString())
                .Distinct();

            foreach (var client in clients)
            {
                var orders = new Series($"Заказы - {client}")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2
                };
                var deliveries = new Series($"Доставки - {client}")
                {
                    ChartType = SeriesChartType.Line,
                    BorderDashStyle = ChartDashStyle.Dash,
                    BorderWidth = 2
                };

                var rows = dt.AsEnumerable().Where(r => r["Customer"].ToString() == client);
                foreach (var row in rows)
                {
                    string dateStr = Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd");
                    decimal ordered = Convert.ToDecimal(row["TotalOrdered"]);
                    decimal delivered = Convert.ToDecimal(row["TotalDelivered"]);

                    orders.Points.AddXY(dateStr, ordered);
                    deliveries.Points.AddXY(dateStr, delivered);
                }

                chartReport.Series.Add(orders);
                chartReport.Series.Add(deliveries);
            }

            chartReport.Titles.Add("Отчёт по клиентам по дням");
            var dm = (DataTable)dataGridViewReport.DataSource;
            MessageBox.Show("Строк в отчёте: " + dt.Rows.Count); // временно вставь
        }


        private void btnCreateDelivery_Click(object sender, EventArgs e)
        {
            if (comboBoxOrders.SelectedValue == null)
            {
                MessageBox.Show("Выберите заказ.");
                return;
            }

            string invoiceNumber = txtInvoiceNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(invoiceNumber))
            {
                MessageBox.Show("Введите номер накладной.");
                return;
            }

            int orderId = (int)comboBoxOrders.SelectedValue;
            var deliveryItems = new List<(int productId, int deliveredQty)>();

            foreach (DataGridViewRow row in dataGridViewDelivery.Rows)
            {
                int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                int qty = Convert.ToInt32(row.Cells["DeliveredQty"].Value);

                if (qty > 0)
                {
                    deliveryItems.Add((productId, qty));
                }
            }

            if (deliveryItems.Count == 0)
            {
                MessageBox.Show("Укажите хотя бы один доставленный товар.");
                return;
            }

            db.InsertDelivery(orderId, invoiceNumber, deliveryItems);
            MessageBox.Show("Накладная успешно создана.");
        }

    }
}
