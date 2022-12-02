﻿using System;
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

namespace ePharm.UserControls
{
    public partial class DrugSquare : UserControl
    {
        public int DrugId { get; set; }

        public DrugSquare() => InitializeComponent();

        public string DrugName
        {
            get { return (string)GetValue(DrugNameProperty); }
            set { SetValue(DrugNameProperty, value); }
        }

        public static readonly DependencyProperty DrugNameProperty =
            DependencyProperty.Register("DrugName", typeof(string), typeof(DrugSquare), new PropertyMetadata("Название препарата", OnDrugNameChanged));

        private static void OnDrugNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as DrugSquare).OnDrugNameChanged(e);

        private void OnDrugNameChanged(DependencyPropertyChangedEventArgs e) => DrugNameTextBlock.Text = e.NewValue.ToString();

        //TODO: Сделать ID -> название типа
        public string DrugType
        {
            get { return (string)GetValue(DrugTypeProperty); }
            set { SetValue(DrugTypeProperty, value); }
        }

        public static readonly DependencyProperty DrugTypeProperty =
            DependencyProperty.Register("DrugType", typeof(string), typeof(DrugSquare), new PropertyMetadata("Тип препарата", OnDrugTypeChanged));

        private static void OnDrugTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as DrugSquare).OnDrugTypeChanged(e);

        private void OnDrugTypeChanged(DependencyPropertyChangedEventArgs e) => DrugTypeTextBlock.Text = e.NewValue.ToString();

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
    }
}
