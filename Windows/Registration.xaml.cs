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
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void GoToAuthorization(object sender, MouseButtonEventArgs e)
        {
            new Authorization().Show();
            Close();
        }

        private void OnRegisterUser(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text, family = FamilyBox.Text, login = MailBox.Text, pass = PasswordBox.Password, requiredPass = RequirePasswordBox.Password;

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(family) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(pass) ||
                string.IsNullOrWhiteSpace(requiredPass))
            {
                MessageBox.Show("Вам нужно заполнить все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка почты

            // Регистрация пользователя в базе
        }

        private void OnControlGotFocused(object sender, RoutedEventArgs e)
        {
            // Проверка почты

            (sender as Control).BorderThickness = new Thickness(0);
        } 

        private void OnControlLostFocus(object sender, RoutedEventArgs e)
        {
            // Проверка почты

            if (!string.IsNullOrWhiteSpace(sender.GetType() == typeof(TextBox) ? (sender as TextBox).Text : (sender as PasswordBox).Password)) return;

            (sender as Control).BorderThickness = new Thickness(1.5);
            (sender as Control).BorderBrush = FindResource("Red") as Brush;
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
