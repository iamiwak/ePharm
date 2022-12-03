using System.Windows.Controls;
using System.Windows.Input;

namespace ePharm.Pages
{
    public partial class CartPage : Page
    {
        public CartPage() => InitializeComponent();

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = sender as ScrollViewer;
            //scv.ScrollToVerticalOffset(scv.VerticalOffset + e.Delta);

            scv.ScrollToVerticalOffset(120);
        }
    }
}
