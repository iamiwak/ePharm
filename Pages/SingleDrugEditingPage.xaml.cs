using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Data.Entity;
using ePharm.Base;

namespace ePharm.Pages
{
    public partial class SingleDrugEditingPage : Page
    {
        private Regex _numbersRegex = new Regex("[^0-9,]+");
        private drugs _drug;

        public SingleDrugEditingPage(drugs drug = null)
        {
            InitializeComponent();
            _drug = drug;

            DeleteItemButton.Visibility = Visibility.Visible;
            DrugType.ItemsSource = SourceCore.DataBase.drugTypes.ToList();
            UpdateData();
        }

        private void UpdateData()
        {
            if (_drug == null)
            {
                DeleteItemButton.Visibility = Visibility.Hidden;
                return;
            }

            DrugName.Text = _drug.name;
            DrugCost.Text = _drug.cost.ToString();
            DrugType.Text = _drug.drugTypes.name;
            IsDrugNeedPrescription.IsChecked = _drug.isNeedPrescription;
            DrugImage.Text = _drug.image?.ToString();
        }

        private void ApplyChanges(object sender, RoutedEventArgs e) => SaveData();

        // TODO: проверять содержимое полей, если пусто - не показывать диалог
        private void CancelChanges(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите отменить внесенные изменения?", "Отмена действия", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            NavigationService.GoBack();
        }

        private void AttemptToEditCost(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _numbersRegex.IsMatch(e.Text);
        }

        private void TextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            e.Handled = true;
            SaveData();
        }

        private void SaveData()
        {
            //TODO:
            // 3. Изменить TextBox'ы на нужные объекты (image!)
            if (!ValidateData()) return;

            decimal.TryParse(DrugCost.Text, out decimal cost);

            drugs drug = (_drug is null) ? new drugs() : SourceCore.DataBase.drugs.First(d => d.id == _drug.id);
            drug.name = DrugName.Text;
            drug.cost = cost;
            drug.typeId = (DrugType.SelectedItem as drugTypes).id;
            drug.drugTypes = DrugType.SelectedItem as drugTypes;
            drug.isNeedPrescription = IsDrugNeedPrescription.IsChecked ?? false;

            if (_drug is null) SourceCore.DataBase.drugs.Add(drug);
            try { SourceCore.DataBase.SaveChanges(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            GoBack();
        }

        private void PageNavigated(object sender, NavigationEventArgs e)
        {
            DrugsEditingPage page = e.Content as DrugsEditingPage;
            page.LoadDrugsCollection();
            (sender as Frame).NavigationService.Navigated -= PageNavigated;
        }

        private bool ValidateData()
        {
            StringBuilder warnings = new StringBuilder();
            if (string.IsNullOrEmpty(DrugName.Text)) warnings.Append("Не указано название препарата;\n");
            if (string.IsNullOrEmpty(DrugCost.Text)) warnings.Append("Не указана стоимость препарата;\n");
            if (string.IsNullOrEmpty(DrugType.Text)) warnings.Append("Не указан тип препарата;\n");

            if (warnings.Length != 0)
            {
                MessageBox.Show($"Исправьте следующие недочеты:\n\n{warnings}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            MessageBoxResult result = MessageBox.Show("Вы действительно хотите сохранить изменения?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return false;

            return true;
        }

        private void DeleteItem(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы хотите удалить данный элемент? Данное действие невозможно отменить!", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;

            SourceCore.DataBase.drugs.Remove(_drug);
            SourceCore.DataBase.Entry(_drug).State = EntityState.Deleted;
            SourceCore.DataBase.SaveChanges();

            GoBack();
        }

        private void GoBack()
        {
            NavigationService.Navigated += PageNavigated;
            NavigationService.GoBack();
        }
    }
}
