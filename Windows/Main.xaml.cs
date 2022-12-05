using ePharm.Windows;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using ePharm.Base;
using ePharm.Pages;

namespace ePharm
{
    public partial class Main : Window
    {
        private string _currentPageName = "HomeStackPanel";

        public Main(users user = null)
        {
            InitializeComponent();
            if (user == null)
            {
                // TODO: Расскомменитровать, когда потребуется
                //MessageBox.Show("Вы зашли не под пользователем!");
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

            User.SetUserData(user);

            UserName.Text = User.Name;
            UserBalance.Text = User.Balance.ToString();

            UserAdminTextBlock.Visibility = User.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            AdminStackPanel.Visibility = User.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            LogOutTextBlock.Margin = new Thickness(0, User.IsAdmin ? 60 : 108, 0, 0);

            PagePlace.Content = new MainPage();
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e) => Close();

        private void CollapseWindow(object sender, MouseButtonEventArgs e) => WindowState = WindowState.Minimized;

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void LogOut(object sender, MouseButtonEventArgs e)
        {
            new Authorization().Show();
            Close();
        }

        private void ChangePage(object sender, MouseButtonEventArgs e)
        {
            string name = (sender as StackPanel).Name;
            if (name == _currentPageName) return;

            _currentPageName = name;
            switch (name)
            {
                case "HomeStackPanel":
                    HomeIcon.Opacity = 1;
                    CartIcon.Opacity = 0.25;
                    AdminIcon.Opacity = 0.25;
                    PagePlace.Content = new MainPage();
                    break;
                case "CartStackPanel":
                    HomeIcon.Opacity = 0.25;
                    CartIcon.Opacity = 1;
                    AdminIcon.Opacity = 0.25;
                    PagePlace.Content = new CartPage();
                    break;
                case "AdminStackPanel":
                    HomeIcon.Opacity = 0.25;
                    CartIcon.Opacity = 0.25;
                    AdminIcon.Opacity = 1;
                    PagePlace.Content = new AdminPage();
                    break;
                default:
                    MessageBox.Show("Куда Вы нажали то хоть?", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
    }
}
