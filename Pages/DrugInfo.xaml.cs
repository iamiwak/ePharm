using ePharm.Base;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ePharm.Pages
{
    public partial class DrugInfo : Page
    {
        public DrugInfo(drugs drug)
        {
            InitializeComponent();
            LoadData(drug);
        }

        private void LoadData(drugs drug)
        {
            // TODO: LOAD DRUG IMAGE
            DrugName.Text = drug.name;
            DrugCost.Text = drug.cost.ToString();
            DrugType.Text = drug.drugTypes.name;
            DrugPrescription.Visibility = drug.isNeedPrescription ? Visibility.Visible : Visibility.Hidden;
        }

        private void GoBack(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}
