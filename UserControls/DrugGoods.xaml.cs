using System.Windows;
using System.Windows.Controls;

namespace ePharm.UserControls
{
    public partial class DrugGoods : UserControl
    {
        public DrugGoods() => InitializeComponent();

        public string DrugName
        {
            get { return (string)GetValue(DrugNameProperty); }
            set { SetValue(DrugNameProperty, value); }
        }

        public static readonly DependencyProperty DrugNameProperty =
            DependencyProperty.Register("DrugName", typeof(string), typeof(DrugGoods), new PropertyMetadata("Название", OnDrugNameChanged));

        private static void OnDrugNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as DrugGoods).OnDrugNameChanged(e);

        private void OnDrugNameChanged(DependencyPropertyChangedEventArgs e) => DrugNameTextBlock.Text = e.NewValue.ToString();

        public string DrugType
        {
            get { return (string)GetValue(DrugTypeProperty); }
            set { SetValue(DrugTypeProperty, value); }
        }

        public static readonly DependencyProperty DrugTypeProperty =
            DependencyProperty.Register("DrugType", typeof(string), typeof(DrugGoods), new PropertyMetadata("Тип", OnDrugTypeChanged));

        private static void OnDrugTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as DrugGoods).OnDrugTypeChanged(e);

        private void OnDrugTypeChanged(DependencyPropertyChangedEventArgs e) => DrugTypeTextBlock.Text = e.NewValue.ToString();

        public decimal DrugPrice
        {
            get { return (decimal)GetValue(DrugPriceProperty); }
            set { SetValue(DrugPriceProperty, value); }
        }

        public static readonly DependencyProperty DrugPriceProperty =
            DependencyProperty.Register("DrugPrice", typeof(decimal), typeof(DrugGoods), new PropertyMetadata(99.99m, OnDrugPriceChanged));

        private static void OnDrugPriceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as DrugGoods).OnDrugPriceChanged(e);

        private void OnDrugPriceChanged(DependencyPropertyChangedEventArgs e) => DrugPriceTextBlock.Text = e.NewValue.ToString();

        public string DrugImage
        {
            get { return (string)GetValue(DrugImageProperty); }
            set { SetValue(DrugImageProperty, value); }
        }

        public static readonly DependencyProperty DrugImageProperty =
            DependencyProperty.Register("DrugImage", typeof(string), typeof(DrugGoods), new PropertyMetadata("../Images/avatar_ex.png", OnDrugImageChanged));

        private static void OnDrugImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as DrugGoods).OnDrugImageChanged(e);

        private void OnDrugImageChanged(DependencyPropertyChangedEventArgs e)
        {
            //DrugImage.Text = e.NewValue.ToString();
            //(DrugImageBorder.Background as ImageBrush).ImageSource = 
        }
    }
}
