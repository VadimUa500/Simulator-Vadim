using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace тест
{
    public partial class AdminPanelForm : Form
    {
        private string connectionString = "Data Source=shopsimulator.db;Version=3;";
        private string selectedImagePath; // Змінна для зберігання шляху до вибраного зображення

        public AdminPanelForm()
        {
            InitializeComponent();
            LoadProducts();
            LoadOrders();
            LoadUsers();
            LoadReports();
        }

        private void LoadProducts()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, Name, Price, Description, CategoryId, Discount, ImagePath FROM products";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewProducts.DataSource = dt;
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка завантаження товарів: " + ex.Message);
            }
        }

        private void LoadOrders()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT o.Id, o.UserId, u.Username, o.TotalPrice, o.OrderDate, o.Status " +
                                  "FROM orders o JOIN users u ON o.UserId = u.Id";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewOrders.DataSource = dt;
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка завантаження замовлень: " + ex.Message);
            }
        }

        private void LoadUsers()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, Username, Role, IsBlocked FROM users";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewUsers.DataSource = dt;
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка завантаження користувачів: " + ex.Message);
            }
        }

        private void LoadReports()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // оновим гроші прибуток
                    string profitQuery = "SELECT SUM(TotalPrice) as TotalProfit FROM orders";
                    SQLiteCommand profitCmd = new SQLiteCommand(profitQuery, conn);
                    object profitResult = profitCmd.ExecuteScalar();
                    lblTotalProfit.Text = $"Загальний прибуток: {(profitResult != DBNull.Value ? Convert.ToDecimal(profitResult) : 0)} грн";

                    // поп товар
                    string popularQuery = "SELECT p.Name, SUM(od.Quantity) as TotalSold " +
                                        "FROM orderdetails od JOIN products p ON od.ProductId = p.Id " +
                                        "GROUP BY p.Id, p.Name ORDER BY TotalSold DESC LIMIT 1";
                    SQLiteCommand popularCmd = new SQLiteCommand(popularQuery, conn);
                    using (SQLiteDataReader reader = popularCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblPopularProduct.Text = $"Популярний товар: {reader.GetString(reader.GetOrdinal("Name"))} ({reader.GetInt32(reader.GetOrdinal("TotalSold"))} продано)";
                        }
                        else
                        {
                            lblPopularProduct.Text = "Популярний товар: немає даних";
                        }
                    }

                    // Завантаження таблиці всіх продажів
                    string salesQuery = @"SELECT o.Id AS OrderId, u.Username, o.OrderDate, p.Name AS ProductName, od.Quantity, od.Price, (od.Quantity * od.Price) AS Total
                                        FROM orders o
                                        JOIN users u ON o.UserId = u.Id
                                        JOIN orderdetails od ON o.Id = od.OrderId
                                        JOIN products p ON od.ProductId = p.Id
                                        ORDER BY o.OrderDate DESC";
                    SQLiteDataAdapter salesAdapter = new SQLiteDataAdapter(salesQuery, conn);
                    DataTable salesDt = new DataTable();
                    salesAdapter.Fill(salesDt);

                    // Створення DataGridView для відображення продажів
                    DataGridView dataGridViewSales = new DataGridView
                    {
                        Location = new System.Drawing.Point(10, 80),
                        Size = new System.Drawing.Size(720, 280),
                        DataSource = salesDt,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                        ReadOnly = true,
                        ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
                    };

                    // Налаштування назв колонок для зрозумілості
                    if (dataGridViewSales.Columns["OrderId"] != null)
                        dataGridViewSales.Columns["OrderId"].HeaderText = "ID Замовлення";
                    if (dataGridViewSales.Columns["Username"] != null)
                        dataGridViewSales.Columns["Username"].HeaderText = "Користувач";
                    if (dataGridViewSales.Columns["OrderDate"] != null)
                        dataGridViewSales.Columns["OrderDate"].HeaderText = "Дата";
                    if (dataGridViewSales.Columns["ProductName"] != null)
                        dataGridViewSales.Columns["ProductName"].HeaderText = "Товар";
                    if (dataGridViewSales.Columns["Quantity"] != null)
                        dataGridViewSales.Columns["Quantity"].HeaderText = "Кількість";
                    if (dataGridViewSales.Columns["Price"] != null)
                        dataGridViewSales.Columns["Price"].HeaderText = "Ціна за одиницю";
                    if (dataGridViewSales.Columns["Total"] != null)
                        dataGridViewSales.Columns["Total"].HeaderText = "Загальна сума";

                    // Очищення попередніх контролів (окрім lblTotalProfit та lblPopularProduct)
                    foreach (Control control in tabReports.Controls)
                    {
                        if (control != lblTotalProfit && control != lblPopularProduct)
                        {
                            tabReports.Controls.Remove(control);
                            control.Dispose();
                        }
                    }

                    // Додавання таблиці до вкладки
                    tabReports.Controls.Add(dataGridViewSales);
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка завантаження звітів: " + ex.Message);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtProductCategory.Text, out int categoryId))
                {
                    MessageBox.Show("Будь ласка, введіть коректний ID категорії (ціле число).");
                    return;
                }

                if (!decimal.TryParse(txtProductPrice.Text, out decimal price))
                {
                    MessageBox.Show("Будь ласка, введіть коректну ціну (число).");
                    return;
                }

                if (!decimal.TryParse(txtProductDiscount.Text, out decimal discount))
                {
                    MessageBox.Show("Будь ласка, введіть коректну знижку (число).");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Будь ласка, введіть назву товару.");
                    return;
                }

                string imagePath = SaveImageFile(); // Збереження зображення, якщо вибрано

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO products (Name, Price, Description, CategoryId, Discount, ImagePath) " +
                                  "VALUES (@Name, @Price, @Description, @CategoryId, @Discount, @ImagePath)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Description", txtProductDescription.Text);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@Discount", discount);
                    cmd.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Товар додано!");
                    LoadProducts();
                    ClearProductFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка додавання товару: " + ex.Message);
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dataGridViewProducts.SelectedRows[0].Cells["Id"].Value);
                try
                {
                    if (!int.TryParse(txtProductCategory.Text, out int categoryId))
                    {
                        MessageBox.Show("Будь ласка, введіть коректний ID категорії (ціле число).");
                        return;
                    }

                    if (!decimal.TryParse(txtProductPrice.Text, out decimal price))
                    {
                        MessageBox.Show("Будь ласка, введіть коректну ціну (число).");
                        return;
                    }

                    if (!decimal.TryParse(txtProductDiscount.Text, out decimal discount))
                    {
                        MessageBox.Show("Будь ласка, введіть коректну знижку (число).");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtProductName.Text))
                    {
                        MessageBox.Show("Будь ласка, введіть назву товару.");
                        return;
                    }

                    string imagePath = SaveImageFile(); // Збереження нового зображення, якщо вибрано

                    using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE products SET Name = @Name, Price = @Price, Description = @Description, " +
                                      "CategoryId = @CategoryId, Discount = @Discount, ImagePath = @ImagePath WHERE Id = @Id";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Description", txtProductDescription.Text);
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                        cmd.Parameters.AddWithValue("@Discount", discount);
                        cmd.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Id", productId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Товар оновлено!");
                        LoadProducts();
                        ClearProductFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка оновлення товару: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть товар для редагування.");
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dataGridViewProducts.SelectedRows[0].Cells["Id"].Value);
                DialogResult result = MessageBox.Show("Ви впевнені, що хочете видалити цей товар?", "Підтвердження видалення", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                        {
                            conn.Open();
                            string query = "DELETE FROM products WHERE Id = @Id";
                            SQLiteCommand cmd = new SQLiteCommand(query, conn);
                            cmd.Parameters.AddWithValue("@Id", productId);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Товар видалено!");
                            LoadProducts();
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Помилка видалення товару: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть товар для видалення.");
            }
        }

        private void btnBlockUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["Id"].Value);
                DialogResult result = MessageBox.Show("Ви впевнені, що хочете заблокувати цього користувача?", "Підтвердження блокування", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                        {
                            conn.Open();
                            string query = "UPDATE users SET IsBlocked = 1 WHERE Id = @Id";
                            SQLiteCommand cmd = new SQLiteCommand(query, conn);
                            cmd.Parameters.AddWithValue("@Id", userId);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Користувача заблоковано!");
                            LoadUsers();
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Помилка блокування користувача: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть користувача для блокування.");
            }
        }

        private void btnUnblockUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["Id"].Value);
                DialogResult result = MessageBox.Show("Ви впевнені, що хочете розблокувати цього користувача?", "Підтвердження розблокування", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                        {
                            conn.Open();
                            string query = "UPDATE users SET IsBlocked = 0 WHERE Id = @Id";
                            SQLiteCommand cmd = new SQLiteCommand(query, conn);
                            cmd.Parameters.AddWithValue("@Id", userId);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Користувача розблоковано!");
                            LoadUsers();
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Помилка розблокування користувача: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть користувача для розблокування.");
            }
        }

        private void btnChangeOrderStatus_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                int orderId = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["Id"].Value);
                string[] statuses = { "В обробці", "Відправлено", "Доставлено" };
                string newStatus = (string)InputBox("Змінити статус замовлення", "Виберіть новий статус:", statuses);
                if (!string.IsNullOrEmpty(newStatus))
                {
                    try
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                        {
                            conn.Open();
                            string query = "UPDATE orders SET Status = @Status WHERE Id = @Id";
                            SQLiteCommand cmd = new SQLiteCommand(query, conn);
                            cmd.Parameters.AddWithValue("@Status", newStatus);
                            cmd.Parameters.AddWithValue("@Id", orderId);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Статус замовлення оновлено!");
                            LoadOrders();
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Помилка оновлення статусу: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть замовлення для зміни статусу.");
            }
        }

        private object InputBox(string title, string promptText, string[] options)
        {
            Form form = new Form()
            {
                Width = 300,
                Height = 150,
                Text = title,
                StartPosition = FormStartPosition.CenterParent
            };
            Label label = new Label() { Left = 20, Top = 20, Text = promptText };
            ComboBox comboBox = new ComboBox() { Left = 20, Top = 50, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            comboBox.Items.AddRange(options);
            Button confirmation = new Button() { Text = "OK", Left = 20, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Скасувати", Left = 130, Width = 100, Top = 80, DialogResult = DialogResult.Cancel };
            form.Controls.Add(label);
            form.Controls.Add(comboBox);
            form.Controls.Add(confirmation);
            form.Controls.Add(cancel);
            if (form.ShowDialog() == DialogResult.OK && comboBox.SelectedItem != null)
            {
                return comboBox.SelectedItem;
            }
            return null;
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Зображення|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Виберіть зображення товару";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                    txtProductImagePath.Text = selectedImagePath;
                }
            }
        }

        private string SaveImageFile()
        {
            if (string.IsNullOrEmpty(selectedImagePath) || !File.Exists(selectedImagePath))
            {
                return null; // Якщо зображення не вибрано, повертаємо null
            }

            try
            {
                // Створюємо папку Images, якщо вона не існує
                string imagesFolder = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                // Генеруємо унікальне ім'я файлу
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(selectedImagePath)}";
                string destinationPath = Path.Combine(imagesFolder, fileName);

                // Копіюємо зображення до папки Images
                File.Copy(selectedImagePath, destinationPath, true);

                // Повертаємо відносний шлях для збереження в базі
                return Path.Combine("Images", fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження зображення: " + ex.Message);
                return null;
            }
        }

        private void ClearProductFields()
        {
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductDescription.Clear();
            txtProductCategory.Clear();
            txtProductDiscount.Clear();
            txtProductImagePath.Clear();
            selectedImagePath = null;
        }

        private void dataGridViewProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                var row = dataGridViewProducts.SelectedRows[0];
                txtProductName.Text = row.Cells["Name"].Value?.ToString();
                txtProductPrice.Text = row.Cells["Price"].Value?.ToString();
                txtProductDescription.Text = row.Cells["Description"].Value?.ToString();
                txtProductCategory.Text = row.Cells["CategoryId"].Value?.ToString();
                txtProductDiscount.Text = row.Cells["Discount"].Value?.ToString();
                txtProductImagePath.Text = row.Cells["ImagePath"].Value?.ToString();
                selectedImagePath = txtProductImagePath.Text;
            }
        }
    }
}