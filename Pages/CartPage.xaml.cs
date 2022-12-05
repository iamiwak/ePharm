using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ePharm.UserControls;
using ePharm.Base;
using System.Linq;

namespace ePharm.Pages
{
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            //LoadCart();
        }

        private void OnClickDrug(DrugGoods sender)
        {
            drugs drug = SourceCore.DataBase.drugs.First(d => d.id == sender.DrugId);
            NavigationService.Navigate(new DrugInfo(drug));
        }

        private void LoadCart()
        {
            // Get cart from <usersCart>

            // Add each item into <DrugsCartCollection>
            // Subscribe OnClickDrug for each item
        }

        private void PayCart(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите купить весь товар в корзине?", "Покупка", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            // Check if cart have drugs with prescriptions

            // Check if user balance greater or equals cart sum

            // Clear <usersCart>, add <sells>, subtract count <drugs>

            MessageBox.Show("Благодарим за покупку!", "Спасибо", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
