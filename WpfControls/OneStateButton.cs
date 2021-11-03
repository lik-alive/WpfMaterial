using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfControls.Elements;

namespace WpfControls
{
    public class OneStateButton : IconButton
    {   
        static OneStateButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OneStateButton), new FrameworkPropertyMetadata(typeof(OneStateButton)));
        }
    }
}
