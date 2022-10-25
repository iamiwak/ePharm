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

        public Main()
        {
            InitializeComponent();
            _db = SourceCore.DataBase;

            // Get user name?
            //UserName.Text = ???;
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

        private void MakeSearch(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            if (text.Length < 3) return;

            Debug.WriteLine(text);
            // Make search...
        }

        private void HideHintText(object sender, RoutedEventArgs e) => SearchHintTextBlock.Visibility = Visibility.Collapsed;

        private void AddHintText(object sender, RoutedEventArgs e) => SearchHintTextBlock.Visibility = Visibility.Visible;

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = sender as ScrollViewer;
            scv.ScrollToHorizontalOffset(scv.HorizontalOffset - e.Delta);
        }
    }
}
