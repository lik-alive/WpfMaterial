using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfControls
{
    public class NumericUpDown : Control
    {        
        public Double Minimum { get; set; }
        public Double Maximum { get; set; }
        public Double Increment { get; set; }

        public readonly static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Double), typeof(NumericUpDown),
            new FrameworkPropertyMetadata((Double)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Double Value
        {
            get { return (Double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        public readonly static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(Boolean), typeof(NumericUpDown));

        public Boolean IsReadOnly
        {
            get { return (Boolean)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static RoutedCommand Increase = new RoutedCommand();
        public static RoutedCommand Decrease = new RoutedCommand();

        private void ClickUp(object sender, ExecutedRoutedEventArgs args)
        {
            (this.Template.FindName("PART_textBox", this) as TextBox).Focus();
            if (Value + Increment > Maximum) Value = Maximum;
            else if (Value + Increment < Minimum) Value = Minimum;
            else Value += Increment;
        }

        private void ClickDown(object sender, ExecutedRoutedEventArgs args)
        {
            (this.Template.FindName("PART_textBox", this) as TextBox).Focus();
            if (Value - Increment < Minimum) Value = Minimum;
            else if (Value - Increment > Maximum) Value = Maximum;
            else Value -= Increment;
        }

        public NumericUpDown()
        {
            this.Value = 0;
            this.Minimum = 0;
            this.Maximum = 1;
            this.Increment = 1;

            this.CommandBindings.Add(new CommandBinding(Increase, ClickUp));
            this.CommandBindings.Add(new CommandBinding(Decrease, ClickDown));      
        }

        public override void OnApplyTemplate()
        {
            (this.Template.FindName("PART_textBox", this) as TextBox).LostKeyboardFocus += NumericUpDown_LostKeyboardFocus;
            (this.Template.FindName("PART_textBox", this) as TextBox).KeyDown += NumericUpDown_KeyDown;
            base.OnApplyTemplate();
        }

        private void NumericUpDown_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                Double value = Double.Parse(textBox.Text, CultureInfo.GetCultureInfo("en-US"));
                if (value < Minimum) Value = Minimum;
                else if (value > Maximum) Value = Maximum;
                else Value = value;
                textBox.Text = Value.ToString(CultureInfo.GetCultureInfo("en-US"));
            }
            catch (Exception ex)
            {
                if ((sender as TextBox).Text != "-")
                    (sender as TextBox).Text = Value.ToString(CultureInfo.GetCultureInfo("en-US"));
            }
        }

        private void NumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    NumericUpDown_LostKeyboardFocus(sender, null);
                }
                catch (Exception ex)
                {
                }
            }
        }        
        
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
        }
    }
}
