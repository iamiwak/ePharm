using System;
using System.Diagnostics;
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
        public Authorization() => InitializeComponent();

        private void AuthorizeUser(string login, string pass)
        {
            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Вам нужно заполнить все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            users user = SourceCore.DataBase.users.SingleOrDefault(u => u.mail == login && u.password == pass);

            if (user is null)
            {
                MessageBox.Show("Логин или пароль введён неверно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            new Main(user).Show();
            Close();
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

        private void OnUserAttemptedAuthorize(object sender, RoutedEventArgs e) => AuthorizeUser(MailBox.Text, PasswordBox.Password);

        private void ChangePasswordState(object sender, MouseEventArgs e)
        {
            string password = PasswordBox.Password;
            Visibility visibility = PasswordBox.Visibility;

            PasswordBox.Password = PasswordTextBox.Text;
            PasswordBox.Visibility = PasswordTextBox.Visibility;

            PasswordTextBox.Text = password;
            PasswordTextBox.Visibility = visibility;
        }

        private void OnUserAttemptedAuthorize(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            AuthorizeUser(MailBox.Text, PasswordBox.Password);
        }
    }
}
