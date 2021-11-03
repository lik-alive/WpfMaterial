using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using WpfControls.Elements;

namespace WpfControls
{
    public class OnOffSwitcher : SelectableButton
    {
        static OnOffSwitcher()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OnOffSwitcher), new FrameworkPropertyMetadata(typeof(OnOffSwitcher)));
        }

        public OnOffSwitcher()
        {
            this.Click += new RoutedEventHandler(OnOffSwitcher_Click);
        }

        void OnOffSwitcher_Click(object sender, RoutedEventArgs e)
        {
            IsSelected = !IsSelected;
        }
    }
}
