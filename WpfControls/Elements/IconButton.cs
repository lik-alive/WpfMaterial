using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfControls.Elements
{
    public class IconButton : Button, INotifyPropertyChanged
    {
        protected ImageSource iconSource;
        private String iconURI;
        protected WriteableBitmap sourceBitmap;

        public Boolean IsGrayscale { get; set; }

        public ImageSource IconSource
        {
            get { return iconSource; }
            set
            {
                iconSource = value;
                CreateIconsFromSource();
                AllPropertyChanged();
            }
        }

        public String IconURI
        {
            get { return iconURI; }
            set
            {
                iconURI = value;
                CreateIconsFromURI();
                AllPropertyChanged();
            }
        }

        public ImageSource DefaultIcon { get; protected set; }

        public ImageSource PressedIcon { get; protected set; }

        protected virtual void CreateIconsFromSource()
        {
            try
            {
                sourceBitmap = new WriteableBitmap((BitmapSource)iconSource);
            }
            catch (Exception ex)
            {
            }
            CreateDefaultIcon();
            CreatePressedIcon();
        }

        protected virtual void CreateIconsFromURI()
        {
            try
            {
                sourceBitmap = new WriteableBitmap(new BitmapImage(new Uri(@"pack://application:,,," + iconURI, UriKind.Absolute)));                
            }
            catch (Exception ex)
            {
            }
            CreateDefaultIcon();
            CreatePressedIcon();
        }

        private void CreateDefaultIcon()
        {
            if (sourceBitmap == null) return;
            try
            {
                WriteableBitmap bitmap = sourceBitmap.Clone();
                if (IsGrayscale)
                {
                    Int32Rect rect = new Int32Rect(0, 0, (Int32)bitmap.Width, (Int32)bitmap.Height);
                    Int32 stride = (Int32)(bitmap.Width * 4);
                    byte[] pixels = new byte[(Int32)(stride * bitmap.Height)];
                    bitmap.CopyPixels(rect, pixels, stride, 0);

                    for (Int32 i = 0; i < pixels.Length / 4; i++)
                    {
                        Byte mean = (Byte)((pixels[i * 4 + 0] + pixels[i * 4 + 1] + pixels[i * 4 + 2]) / 3.0);
                        pixels[i * 4 + 0] = mean;
                        pixels[i * 4 + 1] = mean;
                        pixels[i * 4 + 2] = mean;
                    }

                    bitmap.WritePixels(rect, pixels, stride, 0);
                }
                DefaultIcon = bitmap;
            }
            catch (Exception ex)
            {
            }
        }

        private void CreatePressedIcon()
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
                    Byte mean = (Byte)(255 - (pixels[i * 4 + 0] + pixels[i * 4 + 1] + pixels[i * 4 + 2]) / 3.0);
                    pixels[i * 4 + 0] = mean;
                    pixels[i * 4 + 1] = mean;
                    pixels[i * 4 + 2] = mean;
                }

                bitmap.WritePixels(rect, pixels, stride, 0);
                PressedIcon = bitmap;
            }
            catch (Exception ex)
            {
            }
        }

        #region Оповещение обновления параметров

        public event PropertyChangedEventHandler PropertyChanged;

        protected void MassivePropertyChanged(params String[] propertyNames)
        {
            if (propertyNames != null && PropertyChanged != null)
            {
                for (Int32 i = 0; i < propertyNames.Length; i++)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyNames[i]));
                }
            }
        }
                
        protected virtual void AllPropertyChanged()
        {
            MassivePropertyChanged("DefaultIcon", "PressedIcon");
        }

        #endregion
    }
}

