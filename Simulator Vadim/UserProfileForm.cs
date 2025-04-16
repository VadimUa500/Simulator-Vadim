using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing;

namespace тест
{
    public partial class UserProfileForm : Form
    {
        private string connectionString = "Data Source=shopsimulator.db;Version=3;";

        public UserProfileForm()
        {
            InitializeComponent();
            LoadUserInfo();
            LoadOrderHistory();
        }

        private void LoadUserInfo()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Username, LoyaltyPoints FROM users WHERE Id = @UserId";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblUsername.Text = $"Ім'я користувача: {reader.GetString(reader.GetOrdinal("Username"))}";
                            lblLoyaltyPoints.Text = $"Бали лояльності: {Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("LoyaltyPoints")))}";
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void LoadOrderHistory()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, TotalPrice, OrderDate, Status FROM orders WHERE UserId = @UserId";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewOrders.DataSource = dt;

                    // Додаємо кнопку для перегляду деталей
                    if (!dataGridViewOrders.Columns.Contains("Details"))
                    {
                        DataGridViewButtonColumn detailsColumn = new DataGridViewButtonColumn
                        {
                            Name = "Details",
                            HeaderText = "Деталі",
                            Text = "Переглянути",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridViewOrders.Columns.Add(detailsColumn);
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void dataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewOrders.Columns["Details"].Index && e.RowIndex >= 0)
            {
                int orderId = Convert.ToInt32(dataGridViewOrders.Rows[e.RowIndex].Cells["Id"].Value);
                ShowOrderDetails(orderId);
            }
        }

        private void ShowOrderDetails(int orderId)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT p.Name, od.Quantity, od.Price " +
                                  "FROM orderdetails od JOIN products p ON od.ProductId = p.Id " +
                                  "WHERE od.OrderId = @OrderId";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    Form detailsForm = new Form
                    {
                        Text = $"Деталі замовлення #{orderId}",
                        Size = new Size(400, 300)
                    };
                    DataGridView detailsGrid = new DataGridView
                    {
                        Dock = DockStyle.Fill,
                        DataSource = dt,
                        ReadOnly = true,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    };
                    detailsForm.Controls.Add(detailsGrid);
                    detailsForm.ShowDialog();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка перегляду деталей замовлення: " + ex.Message);
            }
        }
    }
}