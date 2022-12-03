using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ePharm.Pages
{
    public partial class AdminPage : Page
    {
        public AdminPage() => InitializeComponent();

        private void MoveToDrugsEditing(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new DrugsEditingPage());
        }

        private void MoveToUsersEditing(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Данная функция временно отключена!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
