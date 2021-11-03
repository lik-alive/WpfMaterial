using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace WpfControls
{
    public class CloseableWrapper : ContentControl
    {
        public event Action<CloseableWrapper> OnClose;

        public static RoutedCommand Close = new RoutedCommand();
        
        public CloseableWrapper()
        {
            this.CommandBindings.Add(new CommandBinding(Close, ClickClose));
        }

        public CloseableWrapper(Object content)
        {
            this.Content = content;
            this.CommandBindings.Add(new CommandBinding(Close, ClickClose));
        }

        private void ClickClose(object sender, ExecutedRoutedEventArgs args)
        {
            if (OnClose != null) OnClose(this);
        }

        static CloseableWrapper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseableWrapper), new FrameworkPropertyMetadata(typeof(CloseableWrapper)));
        }
    }
}
