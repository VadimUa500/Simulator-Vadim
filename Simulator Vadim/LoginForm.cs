using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;
using Simulator_Vadim;
using BCrypt.Net;
using Guna.UI2.WinForms;

namespace тест
{
    public partial class LoginForm : Form
    {
        private string connectionString = "Data Source=shopsimulator.db;Version=3;";
        private Timer fadeInTimer;

        public LoginForm()
        {
            InitializeComponent();
            ApplyCustomStyles();
            ApplyFadeInAnimation();
        }

        private void ApplyCustomStyles()
        {
            // Налаштування форми
            this.BackColor = Color.FromArgb(245, 245, 245); // Світло-сірий фон
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Фіксована рамка
            this.MaximizeBox = false; // Вимкнути максимізацію
            this.StartPosition = FormStartPosition.CenterScreen; // Центрування
        }

        private void ApplyFadeInAnimation()
        {
            this.Opacity = 0; // Початкова прозорість
            fadeInTimer = new Timer
            {
                Interval = 50 // Інтервал анімації
            };
            fadeInTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1)
                {
                    this.Opacity += 0.05; // Збільшення прозорості
                }
                else
                {
                    fadeInTimer.Stop(); // Зупинка таймера
                }
            };
            fadeInTimer.Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введіть ім'я користувача та пароль!");
                return;
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, Username, PasswordHash, Role, IsBlocked FROM users WHERE Username = @Username";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.GetBoolean(reader.GetOrdinal("IsBlocked")))
                            {
                                MessageBox.Show("Ваш акаунт заблоковано!");
                                return;
                            }

                            string storedHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                            if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                            {
                                Program.CurrentUser = new User
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    Role = reader.GetString(reader.GetOrdinal("Role"))
                                };
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неправильне ім'я користувача або пароль!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неправильне ім'я користувача або пароль!");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введіть ім'я користувача та пароль!");
                return;
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO users (Username, PasswordHash, Role, LoyaltyPoints) VALUES (@Username, @PasswordHash, 'Customer', 0.00)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", BCrypt.Net.BCrypt.HashPassword(password));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Реєстрація успішна! Увійдіть у систему.");
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    MessageBox.Show("Користувач із таким логіном уже існує!");
                }
                else
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }
    }
}