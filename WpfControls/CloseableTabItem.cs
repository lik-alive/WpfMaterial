using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfControls
{
    public class CloseableTabItem : TabItem
    {
        public event Action<CloseableTabItem> OnClose;

        public static RoutedCommand Close = new RoutedCommand();
        
        public CloseableTabItem()
        {
            this.CommandBindings.Add(new CommandBinding(Close, ClickClose));
        }

        private void ClickClose(object sender, ExecutedRoutedEventArgs args)
        {
            if (OnClose != null) OnClose(this);
        }

        static CloseableTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseableTabItem), new FrameworkPropertyMetadata(typeof(CloseableTabItem)));
        }
    }
}
