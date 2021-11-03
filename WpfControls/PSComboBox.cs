using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfControls
{
    public class PSComboBox : ComboBox
    {
        /// <summary>
        /// Стили открывания списка
        /// </summary>
        public enum DropModes
        {
            Default,
            OnMouseHover,
            OnMouseClick,
            OnFocus
        }

        public DropModes DropMode { get; set; }
        
        public readonly static DependencyProperty ValueProperty =
            DependencyProperty.Register("DropMode", typeof(DropModes), typeof(PSComboBox),
            new FrameworkPropertyMetadata(DropModes.Default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
                
        /*public override void OnApplyTemplate()
        {
            (this.Template.FindName("PART_textBox", this) as TextBox).LostKeyboardFocus += NumericUpDown_LostKeyboardFocus;
            (this.Template.FindName("PART_textBox", this) as TextBox).KeyDown += NumericUpDown_KeyDown;
            base.OnApplyTemplate();
        }*/

        static PSComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PSComboBox), new FrameworkPropertyMetadata(typeof(PSComboBox)));
        }
    }
}
