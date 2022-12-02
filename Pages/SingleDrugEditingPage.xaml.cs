using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity.Migrations.Model;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            Debug.WriteLine($"ID - {drug.id}");
            UpdateData();
        }

        private void UpdateData()
        {
            if (_drug == null) return;

            DrugName.Text = _drug.name;
            DrugCost.Text = _drug.cost.ToString();
            DrugType.Text = _drug.typeId.ToString();
            IsDrugNeedPrescription.Text = _drug.isNeedPrescription.ToString();
            DrugImage.Text = _drug.image?.ToString();
        }

        private void ApplyChanges(object sender, RoutedEventArgs e)
        {
            StringBuilder warnings = new StringBuilder();
            if (string.IsNullOrEmpty(DrugName.Text)) warnings.Append("Не указано название препарата;\n");
            if (string.IsNullOrEmpty(DrugCost.Text)) warnings.Append("Не указана стоимость препарата;\n");
            if (string.IsNullOrEmpty(DrugType.Text)) warnings.Append("Не указан тип препарата;\n");

            if (warnings.Length != 0)
            {
                MessageBox.Show($"Исправьте следующие недочеты:\n\n{warnings}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //TODO:
            // 1. Доделать проверки на decimal.parse
            // 2. Доделать базу
            // 3. Изменить TextBox'ы на нужные объекты (checkbox, combobox, image!)
            drugs drug = new drugs
            {
                name = DrugName.Text,
                cost = decimal.Parse(DrugCost.Text),
                typeId = int.Parse(DrugType.Text)
            };
            //if (_drug == null) SourceCore.DataBase.drugs.Add(drug);

            //try { SourceCore.DataBase.SaveChanges(); }
            //catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            MessageBox.Show($"Успешно сохранено:\n\nНазвание: {drug.name}\nСтоимость: {drug.cost}\nТип: {drug.typeId}", "Сохранено", MessageBoxButton.OK, MessageBoxImage.Warning);
            NavigationService.GoBack();
        }

        private void CancelChanges(object sender, RoutedEventArgs e) => NavigationService.GoBack();

        private void AttemptToEditCost(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _numbersRegex.IsMatch(e.Text);
        }
    }
}
