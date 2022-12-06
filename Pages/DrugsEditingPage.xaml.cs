using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ePharm.Base;
using ePharm.UserControls;

namespace ePharm.Pages
{
    public partial class DrugsEditingPage : Page
    {
        private ObservableCollection<drugs> _drugs;

        public DrugsEditingPage()
        {
            InitializeComponent();
            LoadDrugsCollection();
        }

        public void LoadDrugsCollection()
        {
            if (DrugsCollection.Children.Count > 0)
                DrugsCollection.Children.RemoveRange(0, DrugsCollection.Children.Count);

            _drugs = new ObservableCollection<drugs>(SourceCore.DataBase.drugs);
            for (int i = 0; i < _drugs.Count; ++i)
            {
                DrugsCollection.Children.Add(new DrugSquare
                {
                    DrugId = _drugs[i].id,
                    DrugName = _drugs[i].name,
                    DrugType = _drugs[i].drugTypes.name,
                    IsDrugNeedPrescription = _drugs[i].isNeedPrescription
                    //DrugImage = _drugs[i].image
                });
            }
        }

        private void OpenDrugEditing(object sender, MouseButtonEventArgs e)
        {
            drugs drug = _drugs.First(d => d.id == (e.Source as DrugSquare).DrugId);
            NavigationService.Navigate(new SingleDrugEditingPage(drug));
        }

        private void AddNewDrugItem(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new SingleDrugEditingPage());
        }
    }
}
