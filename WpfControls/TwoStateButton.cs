using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfControls.Elements;

namespace WpfControls
{
    public class TwoStateButton : SelectableButton
    {
        static TwoStateButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TwoStateButton), new FrameworkPropertyMetadata(typeof(TwoStateButton)));
        }
    }
}
