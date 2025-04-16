using System;
using System.Windows.Forms;
using System.Data.SQLite;
using BCrypt.Net;

namespace тест
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

    static class Program
    {
        public static User CurrentUser { get; set; }
        private static string connectionString = "Data Source=shopsimulator.db;Version=3;";

        [STAThread]
        static void Main()
        {
            InitializeDatabase(); // Ініціалізація бази даних один раз при запуску

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenuForm());
        }

        private static void InitializeDatabase()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Створюємо таблиці
                    string createTablesQuery = @"
    CREATE TABLE IF NOT EXISTS users (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Username TEXT NOT NULL UNIQUE,
        PasswordHash TEXT NOT NULL,
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
        UNIQUE(UserId, ProductId), -- Додаємо унікальне обмеження
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
                        cmd.CommandText = "INSERT INTO users (Username, PasswordHash, Role, LoyaltyPoints) " +
                                         "VALUES ('admin', @AdminPassword, 'Admin', 0.00), " +
                                         "('user1', @UserPassword, 'Customer', 50.00)";
                        cmd.Parameters.AddWithValue("@AdminPassword", BCrypt.Net.BCrypt.HashPassword("admin123"));
                        cmd.Parameters.AddWithValue("@UserPassword", BCrypt.Net.BCrypt.HashPassword("user123"));
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
                                          "('Конструктор LEGO City', 1d999.99, 'Набір для дітей від 6 років, 500 деталей', 6, 'Images/lego1.jpg', 5.00), " +
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
    }
}