using ePharm.Windows;
using System.Windows;
using System.Windows.Input;
using ePharm.Base;

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
    }
}
