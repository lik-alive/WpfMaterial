using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfControls.Elements
{
    public class SelectableButton : IconButton
    {
        public readonly static DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
            "IsSelected",
            typeof(Boolean),
            typeof(SelectableButton));

        public Boolean IsSelected
        {
            get { return (Boolean)GetValue(IsSelectedProperty); }
            set
            {
                if (IsSelected == value) return;
                SetValue(IsSelectedProperty, value);
            }
        }

        public ImageSource SelectedIcon { get; protected set; }

        protected override void CreateIconsFromSource()
        {
            base.CreateIconsFromSource();
            CreateSelectedIcon();
        }

        protected override void CreateIconsFromURI()
        {
            base.CreateIconsFromURI();
            CreateSelectedIcon();
        }

        private void CreateSelectedIcon()
        {
            if (sourceBitmap == null) return;
            try
            {
                WriteableBitmap bitmap = sourceBitmap.Clone();
                Int32Rect rect = new Int32Rect(0, 0, (Int32)bitmap.Width, (Int32)bitmap.Height);
                Int32 stride = (Int32)(bitmap.Width * 4);
                byte[] pixels = new byte[(Int32)(stride * bitmap.Height)];
                bitmap.CopyPixels(rect, pixels, stride, 0);

                for (Int32 i = 0; i < pixels.Length / 4; i++)
                {
                    Byte mean = (Byte)(255 - (pixels[i * 4 + 0] + pixels[i * 4 + 1] + pixels[i * 4 + 2]) / 3.0 / 1.2);
                    pixels[i * 4 + 0] = mean;
                    pixels[i * 4 + 1] = mean;
                    pixels[i * 4 + 2] = mean;
                }

                bitmap.WritePixels(rect, pixels, stride, 0);
                SelectedIcon = bitmap;
            }
            catch (Exception ex)
            {
            }
        }

        #region Оповещение обновления параметров
        
        protected override void AllPropertyChanged()
        {
            base.AllPropertyChanged();
            MassivePropertyChanged("SelectedIcon");
        }

        #endregion
    }
}
