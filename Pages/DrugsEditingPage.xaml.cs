using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
using ePharm.UserControls;

namespace ePharm.Pages
{
    public partial class DrugsEditingPage : Page
    {
        private ObservableCollection<drugs> _drugs;

        public DrugsEditingPage()
        {
            InitializeComponent();

            _drugs = new ObservableCollection<drugs>
            {
                new drugs
                {
                    id = 1,
                    name = "Название #1",
                    cost = 159.99m,
                    typeId = 1
                },
                new drugs
                {
                    id = 2,
                    name = "Название #2",
                    cost = 159.99m,
                    typeId = 2
                },
                new drugs
                {
                    id = 3,
                    name = "Название #3",
                    cost = 599.99m,
                    typeId = 2
                },
                new drugs
                {
                    id = 4,
                    name = "Название #4",
                    cost = 69.99m,
                    typeId = 3
                },
                new drugs
                {
                    id = 7,
                    name = "Название #5",
                    cost = 119.99m,
                    typeId = 2
                },
                new drugs
                {
                    id = 10,
                    name = "Название #6",
                    cost = 129.99m,
                    typeId = 3
                },
                new drugs
                {
                    id = 11,
                    name = "Название #6",
                    cost = 129.99m,
                    typeId = 3
                },
                new drugs
                {
                    id = 12,
                    name = "Название #6",
                    cost = 129.99m,
                    typeId = 3
                },
                new drugs
                {
                    id = 13,
                    name = "Название #6",
                    cost = 129.99m,
                    typeId = 3
                },
                new drugs
                {
                    id = 14,
                    name = "Название #6",
                    cost = 129.99m,
                    typeId = 3
                },
                new drugs
                {
                    id = 15,
                    name = "Название #6",
                    cost = 129.99m,
                    typeId = 3
                }
            };

            LoadDrugsCollection();
        }

        private void LoadDrugsCollection()
        {
            for (int i = 0; i < _drugs.Count; ++i)
            {
                DrugsCollection.Children.Add(new DrugSquare
                {
                    DrugId = _drugs[i].id,
                    DrugName = _drugs[i].name,
                    DrugType = _drugs[i].typeId.ToString()
                });
            }
        }

        private void OpenDrugEditing(object sender, MouseButtonEventArgs e)
        {
            drugs drug = _drugs.First(d => d.id == (e.Source as DrugSquare).DrugId);
            NavigationService.Navigate(new SingleDrugEditingPage(drug));
        }
    }
}
