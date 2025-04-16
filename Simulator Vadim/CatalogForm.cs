using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;

namespace тест
{
    public partial class CatalogForm : Form
    {
        private string connectionString = "Data Source=shopsimulator.db;Version=3;";

        public CatalogForm()
        {
            InitializeComponent();
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, Name FROM categories";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Id", typeof(int));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Rows.Add(0, "Усі категорії");
                    adapter.Fill(dt);
                    comboBoxCategories.DataSource = dt;
                    comboBoxCategories.DisplayMember = "Name";
                    comboBoxCategories.ValueMember = "Id";
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка завантаження категорій: " + ex.Message);
            }
        }

        private void LoadProducts(string searchTerm = "", int categoryId = 0, string sortBy = "Name", string sortOrder = "ASC", decimal minPrice = 0, decimal maxPrice = decimal.MaxValue)
        {
            flowLayoutPanelProducts.Controls.Clear();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, Name, Price, Description, ImagePath, Discount FROM products WHERE 1=1";
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " AND Name LIKE @SearchTerm";
                    }
                    if (categoryId > 0)
                    {
                        query += " AND CategoryId = @CategoryId";
                    }
                    query += " AND Price >= @MinPrice AND Price <= @MaxPrice";
                    query += $" ORDER BY {sortBy} {sortOrder}";

                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    }
                    if (categoryId > 0)
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    }
                    cmd.Parameters.AddWithValue("@MinPrice", minPrice);
                    cmd.Parameters.AddWithValue("@MaxPrice", maxPrice);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CreateProductCard(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Price"))),
                                reader.GetString(reader.GetOrdinal("Description")),
                                reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath")),
                                Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Discount")))
                            );
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка завантаження товарів: " + ex.Message);
            }
        }

        private void CreateProductCard(int productId, string name, decimal price, string description, string imagePath, decimal discount)
        {
            Panel productCard = new Panel
            {
                Size = new Size(220, 320),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(10)
            };

            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(200, 140),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = imagePath != null && System.IO.File.Exists(imagePath) ? Image.FromFile(imagePath) : null,
                BorderStyle = BorderStyle.FixedSingle
            };
            productCard.Controls.Add(pictureBox);

            Label lblName = new Label
            {
                Text = name,
                Location = new Point(10, 160),
                Size = new Size(200, 20),
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            lblName.Click += (s, e) => ShowProductDetails(productId);
            productCard.Controls.Add(lblName);

            Label lblPrice = new Label
            {
                Text = discount > 0 ? $"{price * (1 - discount / 100):F2} грн (Знижка {discount}%)" : $"{price:F2} грн",
                Location = new Point(10, 190),
                Size = new Size(200, 20),
                Font = new Font("Arial", 9),
                ForeColor = discount > 0 ? Color.Red : Color.Black,
                TextAlign = ContentAlignment.MiddleCenter
            };
            productCard.Controls.Add(lblPrice);

            Label lblDescription = new Label
            {
                Text = description.Length > 50 ? description.Substring(0, 50) + "..." : description,
                Location = new Point(10, 220),
                Size = new Size(200, 40),
                Font = new Font("Arial", 8),
                TextAlign = ContentAlignment.MiddleCenter
            };
            productCard.Controls.Add(lblDescription);

            Button btnAddToCart = new Button
            {
                Text = "Додати до кошика",
                Location = new Point(10, 270),
                Size = new Size(200, 30),
                BackColor = Color.FromArgb(255, 165, 0),
                ForeColor = Color.White,
                Font = new Font("Arial", 9),
                Tag = productId
            };
            btnAddToCart.Click += BtnAddToCart_Click;
            productCard.Controls.Add(btnAddToCart);

            productCard.MouseEnter += (s, e) => productCard.BackColor = Color.FromArgb(245, 245, 245);
            productCard.MouseLeave += (s, e) => productCard.BackColor = Color.White;

            flowLayoutPanelProducts.Controls.Add(productCard);
        }

        private void ShowProductDetails(int productId)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Name, Price, Description, ImagePath, Discount FROM products WHERE Id = @Id";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", productId);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Form productDetails = new Form
                            {
                                Text = reader.GetString(reader.GetOrdinal("Name")),
                                Size = new Size(400, 500),
                                StartPosition = FormStartPosition.CenterParent
                            };
                            PictureBox picture = new PictureBox
                            {
                                Size = new Size(350, 200),
                                Location = new Point(25, 10),
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Image = reader.IsDBNull(reader.GetOrdinal("ImagePath")) || !System.IO.File.Exists(reader.GetString(reader.GetOrdinal("ImagePath")))
                                    ? null
                                    : Image.FromFile(reader.GetString(reader.GetOrdinal("ImagePath")))
                            };
                            Label lblDetailsName = new Label
                            {
                                Text = reader.GetString(reader.GetOrdinal("Name")),
                                Location = new Point(25, 220),
                                Size = new Size(350, 20),
                                Font = new Font("Arial", 12, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            Label lblDetailsPrice = new Label
                            {
                                Text = reader.GetDouble(reader.GetOrdinal("Discount")) > 0
                                    ? $"{Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Price")) * (1 - reader.GetDouble(reader.GetOrdinal("Discount")) / 100)):F2} грн (Знижка {Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Discount")))}%)"
                                    : $"{Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Price"))):F2} грн",
                                Location = new Point(25, 250),
                                Size = new Size(350, 20),
                                Font = new Font("Arial", 10),
                                ForeColor = reader.GetDouble(reader.GetOrdinal("Discount")) > 0 ? Color.Red : Color.Black,
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            Label lblDetailsDescription = new Label
                            {
                                Text = reader.GetString(reader.GetOrdinal("Description")),
                                Location = new Point(25, 280),
                                Size = new Size(350, 150),
                                Font = new Font("Arial", 9),
                                TextAlign = ContentAlignment.TopLeft
                            };
                            productDetails.Controls.Add(picture);
                            productDetails.Controls.Add(lblDetailsName);
                            productDetails.Controls.Add(lblDetailsPrice);
                            productDetails.Controls.Add(lblDetailsDescription);
                            productDetails.ShowDialog();
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int productId = (int)btn.Tag;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO cart (UserId, ProductId, Quantity) VALUES (@UserId, @ProductId, 1) " +
                                  "ON CONFLICT(UserId, ProductId) DO UPDATE SET Quantity = Quantity + 1";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Товар додано до кошика!");
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            CartForm cartForm = new CartForm();
            cartForm.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            int categoryId = 0;
            if (comboBoxCategories.SelectedValue != null)
            {
                // Перевіряємо, чи SelectedValue є DataRowView
                if (comboBoxCategories.SelectedValue is DataRowView rowView)
                {
                    categoryId = Convert.ToInt32(rowView["Id"]);
                }
                else
                {
                    // Якщо SelectedValue уже є int, просто приводимо його
                    categoryId = Convert.ToInt32(comboBoxCategories.SelectedValue);
                }
            }

            string sortBy = "Name";
            string sortOrder = "ASC";
            if (comboBoxSort.SelectedItem != null)
            {
                string sortOption = comboBoxSort.SelectedItem.ToString();
                if (sortOption == "За ціною (зростання)")
                {
                    sortBy = "Price";
                    sortOrder = "ASC";
                }
                else if (sortOption == "За ціною (спадання)")
                {
                    sortBy = "Price";
                    sortOrder = "DESC";
                }
                else if (sortOption == "За знижкою")
                {
                    sortBy = "Discount";
                    sortOrder = "DESC";
                }
                else if (sortOption == "За назвою (А-Я)")
                {
                    sortBy = "Name";
                    sortOrder = "ASC";
                }
                else if (sortOption == "За назвою (Я-А)")
                {
                    sortBy = "Name";
                    sortOrder = "DESC";
                }
            }

            decimal minPrice = 0;
            decimal maxPrice = decimal.MaxValue;
            if (!string.IsNullOrEmpty(txtMinPrice.Text) && decimal.TryParse(txtMinPrice.Text, out decimal min))
            {
                minPrice = min;
            }
            if (!string.IsNullOrEmpty(txtMaxPrice.Text) && decimal.TryParse(txtMaxPrice.Text, out decimal max))
            {
                maxPrice = max;
            }

            LoadProducts(txtSearch.Text, categoryId, sortBy, sortOrder, minPrice, maxPrice);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            UserProfileForm profileForm = new UserProfileForm();
            profileForm.ShowDialog();
        }
    }
}












