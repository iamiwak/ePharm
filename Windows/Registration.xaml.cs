using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ePharm.Base;

namespace ePharm.Windows
{
    public partial class Registration : Window
    {
        private ePharmEntities _db;

        public Registration()
        {
            InitializeComponent();
            _db = SourceCore.DataBase;
        }

        private void GoToAuthorization(object sender, MouseButtonEventArgs e)
        {
            new Authorization().Show();
            Close();
        }

        private void OnRegisterUser(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text, family = FamilyBox.Text, login = MailBox.Text, pass = PasswordBox.Password, requiredPass = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(family) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(pass) ||
                string.IsNullOrWhiteSpace(requiredPass))
            {
                MessageBox.Show("Вам нужно заполнить все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$#~!%*?&])[A-Za-z\d@$!%#~*?&]{8,}$";

            if (_db.users.SingleOrDefault(u => u.mail == login) != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Regex.IsMatch(pass, passwordPattern))
            {
                MessageBox.Show(
                    "Пароль должен состоять минимум из 8 символов, а также содержать заглавные и строчные буквы латинского алфавита, цифры и специальные знаки.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!pass.Equals(requiredPass))
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            users user = new users
            {
                mail = login,
                password = pass,
                name = name,
                family = family
            };

            _db.users.Add(user);
            try
            {
                _db.SaveChanges();
                new Main(user).Show();
                Close();
            }
            catch (Exception err)
            {
                MessageBox.Show($"Произошла ошибка!\n\n{err.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
            PasswordBox passwordBox;
            TextBox passwordTextBox;

            string hintName = (sender as FrameworkElement).Name;

            if (hintName == "RegistrationPasswordHint")
            {
                passwordBox = PasswordBox;
                passwordTextBox = PasswordTextBox;
            }
            else
            {
                passwordBox = ConfirmPasswordBox;
                passwordTextBox = ConfirmPasswordTextBox;
            }

            string password = passwordBox.Password;
            Visibility visibility = passwordBox.Visibility;

            passwordBox.Password = passwordTextBox.Text;
            passwordBox.Visibility = passwordTextBox.Visibility;

            passwordTextBox.Text = password;
            passwordTextBox.Visibility = visibility;
        }
    }
}
