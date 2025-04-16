using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace тест
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=shopsimulator.db;Version=3;";
        private Timer zoomTimer;

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadCategories();
            LoadProducts();
            this.Icon = new Icon("Images/app_icon.ico"); // Додайте іконку в папку Images, якщо є
        }

        private void InitializeDatabase()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string createTablesQuery = @"
                        CREATE TABLE IF NOT EXISTS users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL,
                            Role TEXT NOT NULL DEFAULT 'Customer',
                            IsBlocked INTEGER NOT NULL DEFAULT 0,
                            LoyaltyPoints REAL DEFAULT 0.00
                        );

                        CREATE TABLE IF NOT EXISTS categories (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL UNIQUE
                        );

                        CREATE TABLE IF NOT EXISTS products (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL UNIQUE,
                            Price REAL NOT NULL,
                            Description TEXT,
                            CategoryId INTEGER,
                            ImagePath TEXT,
                            Discount REAL DEFAULT 0.00,
                            FOREIGN KEY (CategoryId) REFERENCES categories(Id)
                        );

                        CREATE TABLE IF NOT EXISTS cart (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            UserId INTEGER,
                            ProductId INTEGER,
                            Quantity INTEGER NOT NULL,
                            UNIQUE(UserId, ProductId),
                            FOREIGN KEY (UserId) REFERENCES users(Id),
                            FOREIGN KEY (ProductId) REFERENCES products(Id)
                        );

                        CREATE TABLE IF NOT EXISTS orders (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            UserId INTEGER,
                            Address TEXT NOT NULL,
                            TotalPrice REAL NOT NULL,
                            OrderDate TEXT DEFAULT (datetime('now')),
                            Status TEXT DEFAULT 'В обробці',
                            FOREIGN KEY (UserId) REFERENCES users(Id)
                        );

                        CREATE TABLE IF NOT EXISTS orderdetails (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            OrderId INTEGER,
                            ProductId INTEGER,
                            Quantity INTEGER NOT NULL,
                            Price REAL NOT NULL,
                            FOREIGN KEY (OrderId) REFERENCES orders(Id),
                            FOREIGN KEY (ProductId) REFERENCES products(Id)
                        );";
                    SQLiteCommand cmd = new SQLiteCommand(createTablesQuery, conn);
                    cmd.ExecuteNonQuery();

                    // Перевіряємо, чи є записи в таблиці users
                    cmd.CommandText = "SELECT COUNT(*) FROM users";
                    long userCount = (long)cmd.ExecuteScalar();
                    if (userCount == 0)
                    {
                        cmd.CommandText = "INSERT INTO users (Username, Password, Role, LoyaltyPoints) " +
                                         "VALUES ('admin', 'admin123', 'Admin', 0.00), " +
                                         "('user1', 'user123', 'Customer', 50.00)";
                        cmd.ExecuteNonQuery();
                    }

                    // Перевіряємо, чи є записи в таблиці categories
                    cmd.CommandText = "SELECT COUNT(*) FROM categories";
                    long categoryCount = (long)cmd.ExecuteScalar();
                    if (categoryCount == 0)
                    {
                        cmd.CommandText = "INSERT INTO categories (Name) VALUES " +
                                          "('Електроніка'), " +
                                          "('Одяг'), " +
                                          "('Взуття'), " +
                                          "('Книги'), " +
                                          "('Побутова техніка'), " +
                                          "('Іграшки'), " +
                                          "('Продукти харчування')";
                        cmd.ExecuteNonQuery();
                    }

                    // Перевіряємо, чи є записи в таблиці products
                    cmd.CommandText = "SELECT COUNT(*) FROM products";
                    long productCount = (long)cmd.ExecuteScalar();
                    if (productCount == 0)
                    {
                        cmd.CommandText = "INSERT INTO products (Name, Price, Description, CategoryId, ImagePath, Discount) VALUES " +
                                          // Електроніка (CategoryId: 1)
                                          "('Смартфон Galaxy S23', 24999.99, 'Сучасний смартфон із великим екраном та камерою 108 МП', 1, 'Images/phone1.jpg', 10.00), " +
                                          "('Ноутбук Lenovo IdeaPad', 32999.99, 'Ноутбук для роботи та ігор, 16 ГБ RAM', 1, 'Images/laptop1.jpg', 5.00), " +
                                          "('Навушники Sony WH-1000XM5', 12999.99, 'Бездротові навушники з шумозаглушенням', 1, 'Images/headphones1.jpg', 15.00), " +
                                          "('Планшет iPad Air', 19999.99, 'Планшет із Retina дисплеєм, 256 ГБ', 1, 'Images/tablet1.jpg', 0.00), " +
                                          // Одяг (CategoryId: 2)
                                          "('Футболка Nike', 799.99, 'Бавовняна футболка, розмір M, чорна', 2, 'Images/shirt1.jpg', 0.00), " +
                                          "('Джинси Levi''s 501', 2499.99, 'Класичні джинси, розмір 32', 2, 'Images/jeans1.jpg', 20.00), " +
                                          "('Куртка Columbia', 4999.99, 'Зимова куртка, водонепроникна', 2, 'Images/jacket1.jpg', 10.00), " +
                                          // Взуття (CategoryId: 3)
                                          "('Кросівки Adidas Ultraboost', 3999.99, 'Кросівки для бігу, розмір 42', 3, 'Images/sneakers1.jpg', 15.00), " +
                                          "('Черевики Timberland', 5999.99, 'Зимові черевики, розмір 43', 3, 'Images/boots1.jpg', 0.00), " +
                                          // Книги (CategoryId: 4)
                                          "('Книга Гаррі Поттер і філософський камінь', 499.99, 'Перша книга серії, автор Дж. К. Роулінг', 4, 'Images/book1.jpg', 5.00), " +
                                          "('Книга 1984', 399.99, 'Роман-антиутопія, автор Джордж Орвелл', 4, 'Images/book2.jpg', 0.00), " +
                                          // Побутова техніка (CategoryId: 5)
                                          "('Мікрохвильова піч Samsung', 3999.99, 'Мікрохвильова піч із грилем, 23 л', 5, 'Images/microwave1.jpg', 10.00), " +
                                          "('Пилосос Dyson V11', 18999.99, 'Бездротовий пилосос із потужним всмоктуванням', 5, 'Images/vacuum1.jpg', 0.00), " +
                                          // Іграшки (CategoryId: 6)
                                          "('Конструктор LEGO City', 1999.99, 'Набір для дітей від 6 років, 500 деталей', 6, 'Images/lego1.jpg', 5.00), " +
                                          "('Лялька Barbie', 799.99, 'Лялька з аксесуарами, висота 30 см', 6, 'Images/barbie1.jpg', 0.00), " +
                                          // Продукти харчування (CategoryId: 7)
                                          "('Шоколад Milka', 49.99, 'Молочний шоколад, 100 г', 7, 'Images/chocolate1.jpg', 0.00), " +
                                          "('Кава Jacobs Monarch', 199.99, 'Мелена кава, 250 г', 7, 'Images/coffee1.jpg', 10.00), " +
                                          "('Чіпси Lay''s', 39.99, 'Чіпси зі смаком сиру, 150 г', 7, 'Images/chips1.jpg', 0.00);";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка ініціалізації бази даних: " + ex.Message);
            }
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

        private void LoadProducts(string searchTerm = "", int categoryId = 0)
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

                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                    }
                    if (categoryId > 0)
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    }

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

        private async void CreateProductCard(int productId, string name, decimal price, string description, string imagePath, decimal discount)
        {
            Panel productCard = new Panel
            {
                Size = new Size(240, 340),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(15),
                Tag = productId
            };

            Panel shadowPanel = new Panel
            {
                Size = new Size(244, 344),
                Location = new Point(-2, -2),
                BackColor = Color.FromArgb(200, 200, 200),
                Margin = new Padding(15)
            };
            productCard.Controls.Add(shadowPanel);
            shadowPanel.SendToBack();

            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(200, 160),
                Location = new Point(20, 20),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            pictureBox.BackColor = Color.FromArgb(230, 230, 230);
            pictureBox.Image = await LoadImageAsync(imagePath);
            productCard.Controls.Add(pictureBox);

            Label lblName = new Label
            {
                Text = name,
                Location = new Point(20, 190),
                Size = new Size(200, 30),
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(33, 37, 41)
            };
            lblName.Click += (s, e) => ShowProductDetails(productId);
            productCard.Controls.Add(lblName);

            Label lblPrice = new Label
            {
                Text = discount > 0
                    ? $"{price * (1 - discount / 100):F2} грн (Знижка {discount}%)"
                    : $"{price:F2} грн",
                Location = new Point(20, 220),
                Size = new Size(200, 20),
                Font = new Font("Arial", 9),
                ForeColor = discount > 0 ? Color.FromArgb(220, 53, 69) : Color.FromArgb(33, 37, 41),
                TextAlign = ContentAlignment.MiddleCenter
            };
            productCard.Controls.Add(lblPrice);

            Label lblDescription = new Label
            {
                Text = description.Length > 50 ? description.Substring(0, 50) + "..." : description,
                Location = new Point(20, 245),
                Size = new Size(200, 40),
                Font = new Font("Arial", 8),
                ForeColor = Color.FromArgb(108, 117, 125),
                TextAlign = ContentAlignment.MiddleCenter
            };
            productCard.Controls.Add(lblDescription);

            Button btnAddToCart = new Button
            {
                Text = "Додати до кошика",
                Location = new Point(20, 290),
                Size = new Size(200, 30),
                BackColor = Color.FromArgb(255, 165, 0),
                ForeColor = Color.White,
                Font = new Font("Arial", 9),
                FlatStyle = FlatStyle.Flat,
                Tag = productId
            };
            btnAddToCart.Click += BtnAddToCart_Click;
            productCard.Controls.Add(btnAddToCart);

            InitializeZoomTimer(pictureBox);
            productCard.MouseEnter += (s, e) =>
            {
                productCard.BackColor = Color.FromArgb(245, 245, 245);
                isZoomingIn = true;
                zoomTimer.Start();
            };
            productCard.MouseLeave += (s, e) =>
            {
                productCard.BackColor = Color.White;
                isZoomingIn = false;
                zoomTimer.Start();
            };

            foreach (Control control in productCard.Controls)
            {
                control.MouseEnter += (s, e) => productCard.BackColor = Color.FromArgb(245, 245, 245);
                control.MouseLeave += (s, e) => productCard.BackColor = Color.White;
            }

            flowLayoutPanelProducts.Controls.Add(productCard);
        }

        private async void ShowProductDetails(int productId)
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
                                Size = new Size(450, 550),
                                StartPosition = FormStartPosition.CenterParent,
                                BackColor = Color.FromArgb(248, 249, 250),
                                FormBorderStyle = FormBorderStyle.FixedSingle,
                                MaximizeBox = false,
                                MinimizeBox = false
                            };

                            PictureBox picture = new PictureBox
                            {
                                Size = new Size(380, 220),
                                Location = new Point(35, 20),
                                SizeMode = PictureBoxSizeMode.Zoom,
                                BorderStyle = BorderStyle.FixedSingle,
                                BackColor = Color.White
                            };
                            picture.Image = await LoadImageAsync(reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath")));
                            productDetails.Controls.Add(picture);

                            Label lblDetailsName = new Label
                            {
                                Text = reader.GetString(reader.GetOrdinal("Name")),
                                Location = new Point(35, 250),
                                Size = new Size(380, 30),
                                Font = new Font("Arial", 14, FontStyle.Bold),
                                TextAlign = ContentAlignment.MiddleCenter,
                                ForeColor = Color.FromArgb(33, 37, 41)
                            };
                            productDetails.Controls.Add(lblDetailsName);

                            Label lblDetailsPrice = new Label
                            {
                                Text = reader.GetDouble(reader.GetOrdinal("Discount")) > 0
                                    ? $"{Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Price")) * (1 - reader.GetDouble(reader.GetOrdinal("Discount")) / 100)):F2} грн (Знижка {Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Discount")))}%)"
                                    : $"{Convert.ToDecimal(reader.GetDouble(reader.GetOrdinal("Price"))):F2} грн",
                                Location = new Point(35, 290),
                                Size = new Size(380, 30),
                                Font = new Font("Arial", 12),
                                ForeColor = reader.GetDouble(reader.GetOrdinal("Discount")) > 0 ? Color.FromArgb(220, 53, 69) : Color.FromArgb(33, 37, 41),
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            productDetails.Controls.Add(lblDetailsPrice);

                            Label lblDetailsDescription = new Label
                            {
                                Text = reader.GetString(reader.GetOrdinal("Description")),
                                Location = new Point(35, 330),
                                Size = new Size(380, 150),
                                Font = new Font("Arial", 10),
                                ForeColor = Color.FromArgb(108, 117, 125),
                                TextAlign = ContentAlignment.TopLeft,
                                AutoSize = false
                            };
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
                    string checkQuery = "SELECT Quantity FROM cart WHERE UserId = @UserId AND ProductId = @ProductId";
                    SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                    checkCmd.Parameters.AddWithValue("@ProductId", productId);
                    object result = checkCmd.ExecuteScalar();

                    if (result != null && Convert.ToInt32(result) >= 99)
                    {
                        MessageBox.Show("Ви досягли максимальної кількості для цього товару!");
                        return;
                    }

                    string query = "INSERT INTO cart (UserId, ProductId, Quantity) VALUES (@UserId, @ProductId, 1) " +
                                  "ON CONFLICT(UserId, ProductId) DO UPDATE SET Quantity = Quantity + 1";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserId", Program.CurrentUser.Id);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.ExecuteNonQuery();

                    if (result == null)
                    {
                        MessageBox.Show("Товар додано до кошика!");
                    }
                    else
                    {
                        int newQuantity = Convert.ToInt32(result) + 1;
                        MessageBox.Show($"Товар уже в кошику! Кількість оновлено до {newQuantity}.");
                    }
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
            int categoryId = 0;
            if (comboBoxCategories.SelectedValue != null)
            {
                if (comboBoxCategories.SelectedValue is DataRowView rowView)
                {
                    categoryId = Convert.ToInt32(rowView["Id"]);
                }
                else
                {
                    categoryId = Convert.ToInt32(comboBoxCategories.SelectedValue);
                }
            }
            LoadProducts(txtSearch.Text, categoryId);
        }

        private void comboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCategories.SelectedValue != null)
            {
                int categoryId = 0;
                if (comboBoxCategories.SelectedValue is DataRowView rowView)
                {
                    categoryId = Convert.ToInt32(rowView["Id"]);
                }
                else
                {
                    categoryId = Convert.ToInt32(comboBoxCategories.SelectedValue);
                }
                LoadProducts(txtSearch.Text, categoryId);
            }
        }

        private async Task<Image> LoadImageAsync(string imagePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Image image = imagePath != null && File.Exists(imagePath)
                        ? Image.FromFile(imagePath)
                        : Image.FromFile("Images/placeholder.jpg");
                    return ResizeImage(image, 200, 160);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка завантаження зображення {imagePath}: {ex.Message}");
                    return Image.FromFile("Images/placeholder.jpg");
                }
            });
        }

        private Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            int newWidth, newHeight;
            if (image.Width > image.Height)
            {
                newWidth = maxWidth;
                newHeight = (int)(image.Height * ((float)maxWidth / image.Width));
            }
            else
            {
                newHeight = maxHeight;
                newWidth = (int)(image.Width * ((float)maxHeight / image.Height));
            }

            return new Bitmap(image, newWidth, newHeight);
        }

        private bool isZoomingIn = false;

        private void InitializeZoomTimer(PictureBox pictureBox)
        {
            zoomTimer = new Timer { Interval = 10 };
            zoomTimer.Tick += (s, e) =>
            {
                if (isZoomingIn)
                {
                    if (pictureBox.Width < 204)
                    {
                        pictureBox.Size = new Size(pictureBox.Width + 2, pictureBox.Height + 2);
                        pictureBox.Location = new Point(pictureBox.Location.X - 1, pictureBox.Location.Y - 1);
                    }
                    else
                    {
                        zoomTimer.Stop();
                    }
                }
                else
                {
                    if (pictureBox.Width > 200)
                    {
                        pictureBox.Size = new Size(pictureBox.Width - 2, pictureBox.Height - 2);
                        pictureBox.Location = new Point(pictureBox.Location.X + 1, pictureBox.Location.Y + 1);
                    }
                    else
                    {
                        zoomTimer.Stop();
                    }
                }
            };
        }
    }
}