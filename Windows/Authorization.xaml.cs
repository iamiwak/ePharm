using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ePharm.Base;

namespace ePharm.Windows
{
    public partial class Authorization : Window
    {
        private ePharmEntities _db;

        public Authorization()
        {
            InitializeComponent();
            try
            {
                _db = SourceCore.DataBase;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Не удалось подключиться к базе данных из-за непредвиденной ошибки.\n\nОшибка: {e.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
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
            string login = MailBox.Text, pass = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Вам нужно заполнить все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_db.users.SingleOrDefault(u => u.mail == login && u.password == pass) != null)
            {
                new Main().Show();
                Close();
            }
            else MessageBox.Show("Логин или пароль введён неверно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ChangePasswordState(object sender, MouseEventArgs e)
        {
            string password = PasswordBox.Password;
            Visibility visibility = PasswordBox.Visibility;

            PasswordBox.Password = PasswordTextBox.Text;
            PasswordBox.Visibility = PasswordTextBox.Visibility;

            PasswordTextBox.Text = password;
            PasswordTextBox.Visibility = visibility;
        }
    }
}
