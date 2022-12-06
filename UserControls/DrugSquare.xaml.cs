using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ePharm.UserControls
{
    public partial class DrugSquare : UserControl
    {
        public delegate void ClickHandler(DrugSquare sender);
        public event ClickHandler OnClick;

        public DrugSquare() => InitializeComponent();

        public int DrugId { get; set; }

        public string DrugName
        {
            get { return (string)GetValue(DrugNameProperty); }
            set { SetValue(DrugNameProperty, value); }
        }

        public static readonly DependencyProperty DrugNameProperty =
            DependencyProperty.Register("DrugName", typeof(string), typeof(DrugSquare), new PropertyMetadata("Название препарата", OnDrugNameChanged));

        private static void OnDrugNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as DrugSquare).OnDrugNameChanged(e);

        private void OnDrugNameChanged(DependencyPropertyChangedEventArgs e)
        {
            string newName = e.NewValue.ToString();
            DrugNameTextBlock.Text = newName.Length < 12 ? newName : newName.Substring(0, newName.Length - 3) + "...";
        }


        public string DrugType
        {
            get { return (string)GetValue(DrugTypeProperty); }
            set { SetValue(DrugTypeProperty, value); }
        }

        public static readonly DependencyProperty DrugTypeProperty =
            DependencyProperty.Register("DrugType", typeof(string), typeof(DrugSquare), new PropertyMetadata("Тип препарата", OnDrugTypeChanged));

        private static void OnDrugTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as DrugSquare).OnDrugTypeChanged(e);

        private void OnDrugTypeChanged(DependencyPropertyChangedEventArgs e)
        {
            string newName = e.NewValue.ToString();
            DrugTypeTextBlock.Text = newName.Length < 12 ? newName : newName.Substring(0, newName.Length - 3) + "...";
        }

        public bool IsDrugNeedPrescription
        {
            get { return (bool)GetValue(IsDrugNeedPrescriptionProperty); }
            set { SetValue(IsDrugNeedPrescriptionProperty, value); }
        }

        public static readonly DependencyProperty IsDrugNeedPrescriptionProperty =
            DependencyProperty.Register("IsDrugNeedPrescription", typeof(bool), typeof(DrugSquare), new PropertyMetadata(false, OnDrugNeededPrescriptionChanged));

        private static void OnDrugNeededPrescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as DrugSquare).OnDrugNeededPrescriptionChanged(e);

        private void OnDrugNeededPrescriptionChanged(DependencyPropertyChangedEventArgs e) => DrugSquarePrescriptionSign.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Hidden;

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (double.IsNaN(Width) || double.IsNaN(Height)) return;

            Debug.WriteLine($"Size changed, new size: {Width} x {Height} | OLD: {ActualWidth} x {ActualHeight}");

            DrugBorder.Width = 160;
            DrugBorder.Height = 160;
            DrugImage.Width = 80;
            DrugImage.Height = 80;
            DrugNameTextBlock.FontSize = 14;
            DrugTypeTextBlock.FontSize = 12;
        }

        private void OnDrugSquareClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => OnClick?.Invoke(this);
    }
}
