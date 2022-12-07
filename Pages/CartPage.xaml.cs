using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ePharm.UserControls;
using ePharm.Base;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Data.Entity;
using System;

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

            foreach (int idx in indexes)
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
                    IsDrugNeedPrescription = drug.isNeedPrescription,
                    DrugImage = drug.image,
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
            foreach (DrugGoods item in DrugsCartCollection.Children)
            {
                if (!item.IsDrugNeedPrescription) continue;

                MessageBox.Show($"{item.DrugName} имеет рецепт, данный препарат можно купить только в аптеке.\nУдалите данный препарат, чтобы продолжить покупку.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check if user balance greater or equals cart sum
            decimal price = decimal.Parse(PriceTextBlock.Text);
            if (User.Balance < price)
            {
                MessageBox.Show("У Вас недостаточно средств для покупки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            User.Balance = SourceCore.DataBase.users.First(u => u.id == User.Id).balance -= price;

            // Clear <usersCart>, add <sells>, subtract count <drugs>
            List<usersCart> carts = SourceCore.DataBase.usersCart.Where(uc => uc.userId == User.Id).ToList();
            foreach (usersCart cart in carts)
            {
                SourceCore.DataBase.usersCart.Remove(cart);
                SourceCore.DataBase.Entry(cart).State = EntityState.Deleted;
                SourceCore.DataBase.sells.Add(new sells
                {
                    userId = User.Id,
                    drugId = cart.drugId,
                    date = DateTime.Now
                });
            }

            SourceCore.DataBase.SaveChanges();
            LoadCart();

            MessageBox.Show("Благодарим за покупку!", "Спасибо", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
