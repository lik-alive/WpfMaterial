using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WpfControls
{
    public class SliderSwitcher : ToggleButton
    {
        public readonly static DependencyProperty TextOnProperty =
            DependencyProperty.Register(
            "TextOn",
            typeof(String),
            typeof(SliderSwitcher));

        public readonly static DependencyProperty TextOffProperty =
            DependencyProperty.Register(
            "TextOff",
            typeof(String),
            typeof(SliderSwitcher));

        public String TextOn
        {
            get { return (String)GetValue(TextOnProperty); }
            set
            {
                if (TextOn == value) return;
                SetValue(TextOnProperty, value);
            }
        }

        public String TextOff
        {
            get { return (String)GetValue(TextOffProperty); }
            set
            {
                if (TextOff == value) return;
                SetValue(TextOffProperty, value);
            }
        }

        static SliderSwitcher()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SliderSwitcher), new FrameworkPropertyMetadata(typeof(SliderSwitcher)));
        }
    }
}