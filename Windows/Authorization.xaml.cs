using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ePharm.Windows
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void GoToRegistration(object sender, MouseButtonEventArgs e)
        {
            new Registration().Show();
            Close();
        }

        private void OnControlGotFocused(object sender, RoutedEventArgs e) => (sender as Control).BorderThickness = new Thickness(0);

        private void OnControlLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sender.GetType() == typeof(TextBox) ? (sender as TextBox).Text : (sender as PasswordBox).Password)) return;

            (sender as Control).BorderThickness = new Thickness(1.5);
            (sender as Control).BorderBrush = FindResource("Red") as Brush;
        }

        private void OnUserAuthorized(object sender, RoutedEventArgs e)
        {
            string login = MailBox.Text, pass = PasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Вам нужно заполнить все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Авторизация пользователя в базе
        }
    }
}
