using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMaterial.Preview
{
    /// <summary>
    /// Interaction logic for NumericUpDownStylePrev.xaml
    /// </summary>
    public partial class NumericUpDownStylePrev : Window
    {
        private Double age;
        public Double Age
        {
            get { return age; }
            set { age = value; }
        }
        public NumericUpDownStylePrev()
        {
            age = 22;
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
    }
}
