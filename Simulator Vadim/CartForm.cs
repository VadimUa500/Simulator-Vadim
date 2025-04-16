using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;
using Simulator_Vadim;

namespace тест
{
    public partial class CartForm : Form
    {
        private string connectionString = "Data Source=shopsimulator.db;Version=3;";

        public CartForm()
        {
            InitializeComponent();
            LoadCart();
        }

        private void LoadCart()
        {
            flowLayoutPanelCart.Controls.Clear();
            decimal total = 0;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT c.Id, p.Name, p.Price, c.Quantity, p.ImagePath, p.Discount " +
                                  "FROM cart c JOIN products p ON c.ProductId = p.Id WHERE c.UserId = @UserId";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int cartId = reader.GetInt32(reader.GetOrdinal("Id"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            decimal price = Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Price")));
                            int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                            string imagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"));
                            decimal discount = Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Discount")));

                            decimal finalPrice = price * (1 - discount / 100);
                            CreateCartItem(cartId, name, finalPrice, quantity, imagePath);
                            total += finalPrice * quantity;
                        }
                    }
                }
                lblTotal.Text = $"Загальна сума: {total} грн";
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка завантаження кошика: " + ex.Message);
            }
        }

        private void CreateCartItem(int cartId, string name, decimal price, int quantity, string imagePath)
        {
            Panel cartItem = new Panel
            {
                Size = new Size(600, 120),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(240, 240, 240),
                Margin = new Padding(10)
            };

            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(100, 100),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = imagePath != null && System.IO.File.Exists(imagePath) ? Image.FromFile(imagePath) : null
            };
            cartItem.Controls.Add(pictureBox);

            Label lblName = new Label
            {
                Text = name,
                Location = new Point(120, 10),
                Size = new Size(300, 20),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            cartItem.Controls.Add(lblName);

            Label lblPrice = new Label
            {
                Text = $"{price} грн",
                Location = new Point(120, 40),
                Size = new Size(300, 20),
                Font = new Font("Arial", 9)
            };
            cartItem.Controls.Add(lblPrice);

            NumericUpDown nudQuantity = new NumericUpDown
            {
                Value = quantity,
                Minimum = 1,
                Maximum = 100,
                Location = new Point(120, 60),
                Size = new Size(50, 20),
                Tag = cartId
            };
            nudQuantity.ValueChanged += NudQuantity_ValueChanged;
            cartItem.Controls.Add(nudQuantity);

            Button btnRemove = new Button
            {
                Text = "Видалити",
                Location = new Point(450, 40),
                Size = new Size(100, 30),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Tag = cartId
            };
            btnRemove.Click += BtnRemove_Click;
            cartItem.Controls.Add(btnRemove);

            flowLayoutPanelCart.Controls.Add(cartItem);
        }

        private void NudQuantity_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            int cartId = (int)nud.Tag;
            int newQuantity = (int)nud.Value;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE cart SET Quantity = @Quantity WHERE Id = @Id";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                    cmd.Parameters.AddWithValue("@Id", cartId);
                    cmd.ExecuteNonQuery();
                    LoadCart();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int cartId = (int)btn.Tag;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM cart WHERE Id = @Id";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", cartId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Товар видалено з кошика!");
                    LoadCart();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            CheckoutForm checkoutForm = new CheckoutForm();
            checkoutForm.ShowDialog();
            LoadCart();
        }
    }
}