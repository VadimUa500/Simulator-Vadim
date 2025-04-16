using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Simulator_Vadim;

namespace тест
{
    public partial class MainMenuForm : Form
    {
        private Timer fadeInTimer;

        public MainMenuForm()
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
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            if (loginForm.DialogResult == DialogResult.OK)
            {
                Form nextForm;
                if (Program.CurrentUser.Role == "Admin")
                {
                    nextForm = new AdminPanelForm();
                }
                else
                {
                    nextForm = new CatalogForm();
                }

                nextForm.FormClosed += (s, args) =>
                {
                    this.Close();
                };
                nextForm.Show();
                this.Hide();
            }
        }
    }
}