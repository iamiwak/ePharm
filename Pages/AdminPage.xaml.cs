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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ePharm.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void MoveToDrugsEditing(object sender, MouseButtonEventArgs e)
        {

        }

        private void MoveToUsersEditing(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Данная функция временно отключена!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
