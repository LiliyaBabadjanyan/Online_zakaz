using System;
using System.Data;
using Npgsql;

public class DbManager
{
    private readonly string connectionString;

    public DbManager(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public DataTable GetData(string query, params NpgsqlParameter[] parameters)
    {
        DataTable dt = new DataTable();
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
        }
        return dt;
    }

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
    public DataTable ExecuteQuery(string sql, NpgsqlParameter[] parameters = null)
    {
        DataTable dt = new DataTable();

        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }

        return dt;
    }
    public void InsertOrder(int customerId, DateTime orderDate, decimal totalAmount, List<(int productId, int quantity, decimal amount)> items)

    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();

        using var tran = conn.BeginTransaction();



        string insertOrderSql = "INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) VALUES (@customerId, @orderDate, @totalAmount) RETURNING OrderID;";
        int orderId;

        using (var cmd = new NpgsqlCommand(insertOrderSql, conn))
        {
            cmd.Parameters.AddWithValue("customerId", customerId);
            cmd.Parameters.AddWithValue("orderDate", orderDate.Date); // важно!
            cmd.Parameters.AddWithValue("totalAmount", totalAmount);
            orderId = (int)cmd.ExecuteScalar();
        }
        string insertItemSql = "INSERT INTO OrderItems (OrderID, ProductID, Quantity, Amount) VALUES (@orderId, @productId, @qty, @amount);";

        foreach (var item in items)
        {
            using var cmd = new NpgsqlCommand(insertItemSql, conn);
            cmd.Parameters.AddWithValue("orderId", orderId);
            cmd.Parameters.AddWithValue("productId", item.productId);
            cmd.Parameters.AddWithValue("qty", item.quantity);
            cmd.Parameters.AddWithValue("amount", item.amount);
            cmd.ExecuteNonQuery();
        }

        tran.Commit();
    }

    public NpgsqlDataAdapter GetAdapter(string selectSql, string tableName)
    {
        var conn = new NpgsqlConnection(connectionString);
        var adapter = new NpgsqlDataAdapter(selectSql, conn);
        var builder = new NpgsqlCommandBuilder(adapter);
        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        adapter.UpdateCommand = builder.GetUpdateCommand();
        adapter.InsertCommand = builder.GetInsertCommand();
        adapter.DeleteCommand = builder.GetDeleteCommand();
        return adapter;
    }

    public void InsertDelivery(int orderId, string invoiceNumber, List<(int productId, int deliveredQty)> items)
    {
        using var conn = new NpgsqlConnection(connectionString);
        conn.Open();
        using var tran = conn.BeginTransaction();

        // 1. Вставка накладной
        string sqlDelivery = "INSERT INTO Deliveries (OrderID, InvoiceNumber) VALUES (@orderId, @invoiceNumber) RETURNING DeliveryID;";
        int deliveryId;

        using (var cmd = new NpgsqlCommand(sqlDelivery, conn))
        {
            cmd.Parameters.AddWithValue("orderId", orderId);
            cmd.Parameters.AddWithValue("invoiceNumber", invoiceNumber);
            deliveryId = (int)cmd.ExecuteScalar();
        }

        // 2. Вставка позиций доставки
        string sqlItem = "INSERT INTO DeliveryItems (DeliveryID, ProductID, DeliveredQty) VALUES (@deliveryId, @productId, @qty);";

        foreach (var item in items)
        {
            using var cmd = new NpgsqlCommand(sqlItem, conn);
            cmd.Parameters.AddWithValue("deliveryId", deliveryId);
            cmd.Parameters.AddWithValue("productId", item.productId);
            cmd.Parameters.AddWithValue("qty", item.deliveredQty);
            cmd.ExecuteNonQuery();
        }

        tran.Commit();
    }


}
