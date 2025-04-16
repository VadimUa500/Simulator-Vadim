using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace тест
{
    public partial class CheckoutForm : Form
    {
        private string connectionString = "Data Source=shopsimulator.db;Version=3;";

        public CheckoutForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text;

            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Введіть адресу доставки!");
                return;
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteTransaction transaction = conn.BeginTransaction()) // Починаємо транзакцію
                    {
                        try
                        {
                            // щитає усю ціну
                            decimal total = 0;
                            using (SQLiteCommand cmd = new SQLiteCommand(
                                "SELECT p.Price, c.Quantity, p.Discount FROM cart c JOIN products p ON c.ProductId = p.Id WHERE c.UserId = @UserId", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                                using (SQLiteDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        decimal price = Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Price")));
                                        int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                                        decimal discount = Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Discount")));
                                        total += price * (1 - discount / 100) * quantity;
                                    }
                                }
                            }

                            // Додаємо бали лояльності (1 бал за кожні 100 грн)
                            decimal loyaltyPoints = total / 100;
                            using (SQLiteCommand cmd = new SQLiteCommand(
                                "UPDATE users SET LoyaltyPoints = LoyaltyPoints + @Points WHERE Id = @UserId", conn))
                            {
                                cmd.Parameters.AddWithValue("@Points", loyaltyPoints);
                                cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                                cmd.ExecuteNonQuery();
                            }

                            // Створюємо замовлення
                            int orderId;
                            using (SQLiteCommand cmd = new SQLiteCommand(
                                "INSERT INTO orders (UserId, Address, TotalPrice, Status) VALUES (@UserId, @Address, @TotalPrice, 'В обробці'); SELECT last_insert_rowid();", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                                cmd.Parameters.AddWithValue("@Address", address);
                                cmd.Parameters.AddWithValue("@TotalPrice", total);
                                orderId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // Переносимо товари з кошика в orderdetails
                            using (SQLiteCommand cmd = new SQLiteCommand(
                                "SELECT ProductId, Quantity FROM cart WHERE UserId = @UserId", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                                using (SQLiteDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        int productId = reader.GetInt32(reader.GetOrdinal("ProductId"));
                                        int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));

                                        // Отримуємо ціну та знижку для товару
                                        using (SQLiteCommand cmd2 = new SQLiteCommand(
                                            "SELECT Price, Discount FROM products WHERE Id = @ProductId", conn))
                                        {
                                            cmd2.Parameters.AddWithValue("@ProductId", productId);
                                            using (SQLiteDataReader reader2 = cmd2.ExecuteReader())
                                            {
                                                if (reader2.Read())
                                                {
                                                    decimal price = Convert.ToDecimal(reader2.GetDouble(reader2.GetOrdinal("Price")));
                                                    decimal discount = Convert.ToDecimal(reader2.GetDouble(reader2.GetOrdinal("Discount")));
                                                    decimal finalPrice = price * (1 - discount / 100);

                                                    // Додаємо запис у orderdetails
                                                    using (SQLiteCommand cmd3 = new SQLiteCommand(
                                                        "INSERT INTO orderdetails (OrderId, ProductId, Quantity, Price) VALUES (@OrderId, @ProductId, @Quantity, @Price)", conn))
                                                    {
                                                        cmd3.Parameters.AddWithValue("@OrderId", orderId);
                                                        cmd3.Parameters.AddWithValue("@ProductId", productId);
                                                        cmd3.Parameters.AddWithValue("@Quantity", quantity);
                                                        cmd3.Parameters.AddWithValue("@Price", finalPrice);
                                                        cmd3.ExecuteNonQuery();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show($"Товар із ID {productId} не знайдено!");
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            // Очищаємо кошик
                            using (SQLiteCommand cmd = new SQLiteCommand(
                                "DELETE FROM cart WHERE UserId = @UserId", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                                cmd.ExecuteNonQuery();
                            }

                            // Підтверджуємо транзакцію
                            transaction.Commit();
                            MessageBox.Show($"Замовлення оформлено! Ви отримали {loyaltyPoints} балів лояльності.");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            // У разі помилки відкочуємо транзакцію
                            transaction.Rollback();
                            throw new Exception("Помилка під час оформлення замовлення: " + ex.Message, ex);
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Загальна помилка: " + ex.Message);
            }
        }
    }
}