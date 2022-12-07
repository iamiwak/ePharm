using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using ePharm.Base;
using ePharm.UserControls;

namespace ePharm.Pages
{
    public partial class MainPage : Page
    {
        private ObservableCollection<drugs> _sliderDrugs;
        private readonly int RANDOM_DRUG_COMPILATION_COUNT;
        private int _currentDrugItem;

        public MainPage()
        {
            InitializeComponent();

            RANDOM_DRUG_COMPILATION_COUNT = DotsStackPanel.Children.Count;
            _currentDrugItem = 0;

            LeftItem.Visibility = Visibility.Collapsed;
            RightItem.Visibility = Visibility.Visible;

            LoadSlider();
            LoadSuggestType();
        }

        private void MakeSearch(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            MainStackPanel.Visibility = Visibility.Visible;
            SearchStackPanel.Visibility = Visibility.Collapsed;
            if (text.Length < 3) return;

            SearchStackPanel.Visibility = Visibility.Visible;
            MainStackPanel.Visibility = Visibility.Collapsed;
            SearchHintTextBlock.Visibility = Visibility.Hidden;

            if (DrugsCollection.Children.Count > 0)
                DrugsCollection.Children.RemoveRange(0, DrugsCollection.Children.Count);

            List<drugs> drugs = SourceCore.DataBase.drugs.Where(d => d.name.Contains(text)).ToList();
            foreach (drugs drug in drugs)
            {
                DrugsCollection.Children.Add(new DrugSquare
                {
                    DrugId = drug.id,
                    DrugName = drug.name,
                    DrugType = drug.drugTypes.name,
                    DrugImage = drug.image,
                    IsDrugNeedPrescription = drug.isNeedPrescription
                });
            }
        }

        private void OnSearchBarGetFocus(object sender, RoutedEventArgs e) => SearchHintTextBlock.Visibility = Visibility.Collapsed;

        private void OnSearchBarLostFocus(object sender, RoutedEventArgs e) => SearchHintTextBlock.Visibility = SearchBarTextBox.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = sender as ScrollViewer;
            scv.ScrollToHorizontalOffset(scv.HorizontalOffset - e.Delta);
        }

        private void SlideItemToLeft(object sender, MouseButtonEventArgs e)
        {
            _currentDrugItem--;
            RightArrowSlider.Opacity = 1;
            if (_currentDrugItem <= 0)
            {
                _currentDrugItem = 0;
                LeftItem.Visibility = Visibility.Collapsed;
                LeftArrowSlider.Opacity = 0.1;
            }
            else LeftItem.Visibility = Visibility.Visible;
            RightItem.Visibility = Visibility.Visible;

            if (_currentDrugItem > 0) SetSliderData(LeftItem, _sliderDrugs[_currentDrugItem - 1]);
            SetSliderData(CenterItem, _sliderDrugs[_currentDrugItem]);
            if (_currentDrugItem < RANDOM_DRUG_COMPILATION_COUNT - 1) SetSliderData(RightItem, _sliderDrugs[_currentDrugItem + 1]);

            ChangeDotsOpacity();
        }

        private void SlideItemToRight(object sender, MouseButtonEventArgs e)
        {
            _currentDrugItem++;
            LeftArrowSlider.Opacity = 1;
            if (_currentDrugItem >= RANDOM_DRUG_COMPILATION_COUNT - 1)
            {
                _currentDrugItem = RANDOM_DRUG_COMPILATION_COUNT - 1;
                RightItem.Visibility = Visibility.Collapsed;
                RightArrowSlider.Opacity = 0.1;
            }
            else RightItem.Visibility = Visibility.Visible;


            if (_currentDrugItem > 0) SetSliderData(LeftItem, _sliderDrugs[_currentDrugItem - 1]);
            SetSliderData(CenterItem, _sliderDrugs[_currentDrugItem]);
            if (_currentDrugItem < RANDOM_DRUG_COMPILATION_COUNT - 1) SetSliderData(RightItem, _sliderDrugs[_currentDrugItem + 1]);


            LeftItem.Visibility = Visibility.Visible;
            ChangeDotsOpacity();
        }

        private void ChangeDotsOpacity()
        {
            for (int i = 0; i < RANDOM_DRUG_COMPILATION_COUNT; i++)
            {
                Ellipse item = DotsStackPanel.Children[i] as Ellipse;
                if (i == _currentDrugItem) item.Opacity = 1;
                else item.Opacity = 0.25;
            }
        }

        private void LoadSlider()
        {
            _sliderDrugs = new ObservableCollection<drugs>();

            List<int> values = new List<int>();
            Random random = new Random();
            int maxValue = SourceCore.DataBase.drugs.OrderByDescending(d => d.id).FirstOrDefault().id + 1;
            while (_sliderDrugs.Count != RANDOM_DRUG_COMPILATION_COUNT)
            {
                int value = random.Next(1, maxValue);
                drugs drug = SourceCore.DataBase.drugs.FirstOrDefault(d => d.id == value);
                if (!values.Contains(value) && drug != null)
                {
                    _sliderDrugs.Add(drug);
                    values.Add(value);
                }
            }

            SetSliderData(CenterItem, _sliderDrugs[0]);
            SetSliderData(RightItem, _sliderDrugs[1]);
        }

        private void SetSliderData(DrugSquare banner, drugs drug)
        {
            banner.DrugId = drug.id;
            banner.DrugName = drug.name;
            banner.DrugType = drug.drugTypes.name;
            banner.DrugImage = drug.image;
            banner.IsDrugNeedPrescription = drug.isNeedPrescription;
        }

        private void LoadSuggestType()
        {
            Random random = new Random();
            int maxValue = SourceCore.DataBase.drugTypes.OrderByDescending(dt => dt.id).FirstOrDefault().id + 1;
            int value = 0;
            drugs tempDrug = null;
            while (tempDrug is null)
            {
                value = random.Next(1, maxValue);
                tempDrug = SourceCore.DataBase.drugs.FirstOrDefault(d => d.typeId == value);
            }

            ObservableCollection<drugs> drugs = new ObservableCollection<drugs>(SourceCore.DataBase.drugs.Where(drug => drug.typeId == value).ToList());
            SuggestedDrugTypeTextBlock.Text = "Предлагаемый тип: " + drugs[0].drugTypes.name;
            for (int i = 0; i < drugs.Count; ++i)
            {
                drugs drug = drugs[i];
                DrugSquare ds = new DrugSquare
                {
                    DrugId = drug.id,
                    DrugName = drug.name,
                    DrugType = drug.drugTypes.name,
                    DrugImage = drug.image,
                    IsDrugNeedPrescription = drug.isNeedPrescription
                };

                ds.OnClick += OnDrugClick;
                TodayDrugSellsCollection.Children.Add(ds);
            }
        }

        private void OnDrugClick(DrugSquare sender)
        {
            drugs drug = SourceCore.DataBase.drugs.First(d => d.id == sender.DrugId);
            NavigationService.Navigate(new DrugInfo(drug));
        }

        private void OnSearchDrugClick(object sender, MouseButtonEventArgs e)
        {
            int drugid = (e.Source as DrugSquare).DrugId;
            drugs drug = SourceCore.DataBase.drugs.First(d => d.id == drugid);
            NavigationService.Navigate(new DrugInfo(drug));
        }
    }
}
