using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WpfControls.Elements;

namespace WpfControls
{
    public class PropertyButton : SelectableButton
    {
        public readonly static DependencyProperty IsUpperProperty =
            DependencyProperty.Register(
            "IsUpper",
            typeof(Boolean),
            typeof(PropertyButton));

        public Boolean IsUpper
        {
            get { return (Boolean)GetValue(IsUpperProperty); }
            set
            {
                if (IsUpper == value) return;
                SetValue(IsUpperProperty, value);
            }
        }

        static PropertyButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyButton), new FrameworkPropertyMetadata(typeof(PropertyButton)));
        }

        public PropertyButton()
        {
            this.Click += PropertyButton_Click;
        }

        private void PropertyButton_Click(object sender, RoutedEventArgs e)
        {
            IsSelected = true;
        }
    }
}
