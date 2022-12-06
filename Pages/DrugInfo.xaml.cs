using ePharm.Base;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ePharm.Pages
{
    public partial class DrugInfo : Page
    {
        private bool _isFromCart;
        private drugs _drug;

        public DrugInfo(drugs drug, bool isFromCart = false)
        {
            InitializeComponent();
            _drug = drug;
            _isFromCart = isFromCart;

            DeleteButton.Visibility = _isFromCart ? Visibility.Visible : Visibility.Collapsed;
            LoadData();
        }

        private void LoadData()
        {
            // TODO: LOAD DRUG IMAGE
            DrugName.Text = _drug.name;
            DrugCost.Text = _drug.cost.ToString();
            DrugType.Text = _drug.drugTypes.name;
            DrugPrescription.Visibility = _drug.isNeedPrescription ? Visibility.Visible : Visibility.Hidden;
        }

        private void ClickOnBackButton(object sender, RoutedEventArgs e) => GoBack();


        private void GoBack()
        {
            if (_isFromCart) NavigationService.Navigated += PageNavigated;
            NavigationService.GoBack();
        }

        private void PageNavigated(object sender, NavigationEventArgs e)
        {
            CartPage page = e.Content as CartPage;
            page.LoadCart();
            (sender as Frame).NavigationService.Navigated -= PageNavigated;
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            decimal.TryParse(DrugCost.Text, out decimal cost);

            usersCart cart = new usersCart
            {
                drugId = _drug.id,
                userId = User.Id
            };
            SourceCore.DataBase.usersCart.Add(cart);
            try
            {
                SourceCore.DataBase.SaveChanges();
                MessageBox.Show("Препарат успешно добавлен в корзину!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show($"{ex.Message}\n\n{ex.InnerException}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteItemFromCart(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы хотите удалить данный препарат (если более одного в корзине, удалится лишь 1 из них)?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;

            usersCart cart = SourceCore.DataBase.usersCart.First(uc => uc.drugId == _drug.id && uc.userId == User.Id);

            SourceCore.DataBase.usersCart.Remove(cart);
            SourceCore.DataBase.Entry(cart).State = EntityState.Deleted;
            SourceCore.DataBase.SaveChanges();

            GoBack();
        }
    }
}
