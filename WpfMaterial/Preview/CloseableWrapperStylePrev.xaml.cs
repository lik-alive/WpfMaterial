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
    /// Interaction logic for CloseableWrapperStylePrev.xaml
    /// </summary>
    public partial class CloseableWrapperStylePrev : Window
    {
        public CloseableWrapperStylePrev()
        {
            InitializeComponent();
            closeableWrapper.OnClose += closeableWrapper_OnClose;
        }

        void closeableWrapper_OnClose(WpfControls.CloseableWrapper obj)
        {
            obj.Visibility = Visibility.Hidden;
        }
    }
}
