using System;
using System.Collections.Generic;
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

namespace ePharm.Pages
{
    public partial class MainPage : Page
    {
        private readonly int RANDOM_DRUG_COMPILATION_COUNT;
        private int _currentDrugItem;

        public MainPage()
        {
            InitializeComponent();

            RANDOM_DRUG_COMPILATION_COUNT = DotsStackPanel.Children.Count;
            _currentDrugItem = 0;

            LeftItem.Visibility = Visibility.Collapsed;
            RightItem.Visibility = Visibility.Visible;
        }

        private void MakeSearch(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            MainStackPanel.Visibility = Visibility.Visible;
            if (text.Length < 3) return;

            Debug.WriteLine(text);
            MainStackPanel.Visibility = Visibility.Collapsed;
            // Make search...
        }

        private void HideHintText(object sender, RoutedEventArgs e) => SearchHintTextBlock.Visibility = Visibility.Collapsed;

        private void AddHintText(object sender, RoutedEventArgs e) => SearchHintTextBlock.Visibility = Visibility.Visible;

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = sender as ScrollViewer;
            scv.ScrollToHorizontalOffset(scv.HorizontalOffset - e.Delta);
        }

        private void SlideItemToLeft(object sender, MouseButtonEventArgs e)
        {
            _currentDrugItem--;
            if (_currentDrugItem <= 0)
            {
                _currentDrugItem = 0;
                LeftItem.Visibility = Visibility.Collapsed;
            }
            else LeftItem.Visibility = Visibility.Visible;
            RightItem.Visibility = Visibility.Visible;

            ChangeDotsOpacity();
        }

        private void SlideItemToRight(object sender, MouseButtonEventArgs e)
        {
            _currentDrugItem++;
            if (_currentDrugItem >= RANDOM_DRUG_COMPILATION_COUNT - 1)
            {
                _currentDrugItem = RANDOM_DRUG_COMPILATION_COUNT - 1;
                RightItem.Visibility = Visibility.Collapsed;
            }
            else RightItem.Visibility = Visibility.Visible;

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
    }
}
