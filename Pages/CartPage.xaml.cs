using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ePharm.UserControls;
using ePharm.Base;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ePharm.Pages
{
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            LoadCart();
        }

        public void LoadCart()
        {
            decimal fullPrice = 0;

            if (DrugsCartCollection.Children.Count > 0)
                DrugsCartCollection.Children.RemoveRange(0, DrugsCartCollection.Children.Count);

            List<int> indexes = SourceCore.DataBase.usersCart.Where(uc => uc.userId == User.Id).Select(uc => uc.drugId).ToList(); // Get drug ids
            Dictionary<drugs, int> drugs = new Dictionary<drugs, int>();

            foreach(int idx in indexes)
            {
                drugs drug = SourceCore.DataBase.drugs.First(d => d.id == idx);
                if (drugs.ContainsKey(drug)) drugs[drug]++;
                else drugs.Add(drug, 1);
            }

            foreach (KeyValuePair<drugs, int> item in drugs)
            {
                drugs drug = item.Key;
                fullPrice += drug.cost * item.Value;
                DrugGoods dg = new DrugGoods
                {
                    DrugId = drug.id,
                    DrugName = drug.name,
                    DrugCost = drug.cost,
                    DrugType = drug.drugTypes.name,
                    DrugCount = item.Value
                };
                dg.OnClick += OnDrugClick;
                DrugsCartCollection.Children.Add(dg);
            }

            PriceTextBlock.Text = fullPrice.ToString();
        }

        private void OnDrugClick(DrugGoods sender)
        {
            drugs drug = SourceCore.DataBase.drugs.First(d => d.id == sender.DrugId);
            NavigationService.Navigate(new DrugInfo(drug, true));
        }

        private void PayCart(object sender, RoutedEventArgs e)
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
