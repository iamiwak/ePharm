using ePharm.Windows;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using ePharm.Base;
using System.Diagnostics;

namespace ePharm
{
    public partial class Main : Window
    {
        private ePharmEntities _db;
        private users _user;

        public Main(users user = null)
        {
            InitializeComponent();
            _db = SourceCore.DataBase;
            if (user == null)
            {
                MessageBox.Show("Вы зашли не под пользователем!");
                user = new users
                {
                    id = 1000,
                    mail = "guest@mail.ru",
                    password = "123",
                    name = "Guest",
                    family = "Guests'",
                    balance = 1000m,
                    isAdmin = true,
                };
            }

            _user = user;
            UserName.Text = _user.name;
            UserBalance.Text = _user.balance.ToString();

            UserAdminTextBlock.Visibility = _user.isAdmin ? Visibility.Visible : Visibility.Collapsed;
            AdminStackPanel.Visibility = _user.isAdmin ? Visibility.Visible : Visibility.Collapsed;
            LogOutTextBlock.Margin = new Thickness(0, _user.isAdmin ? 60 : 108, 0, 0);
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e) => Close();

        private void CollapseWindow(object sender, MouseButtonEventArgs e) => WindowState = WindowState.Minimized;

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void MakeSearch(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            MainStackPanel.Visibility = Visibility.Visible;
            if (text.Length < 3) return;

            Debug.WriteLine(text);
            MainStackPanel.Visibility = Visibility.Collapsed;
            // Make search...
        }

        private void HideHintText(object sender, RoutedEventArgs e) => SearchHintTextBlock.Visibility = Visibility.Collapsed;

        private void AddHintText(object sender, RoutedEventArgs e) => SearchHintTextBlock.Visibility = Visibility.Visible;

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = sender as ScrollViewer;
            scv.ScrollToHorizontalOffset(scv.HorizontalOffset - e.Delta);
        }

        private void LogOut(object sender, MouseButtonEventArgs e)
        {
            new Authorization().Show();
            Close();
        }
    }
}
