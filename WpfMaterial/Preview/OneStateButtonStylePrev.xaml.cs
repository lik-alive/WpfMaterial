﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfMaterial.Preview
{
    /// <summary>
    /// Interaction logic for OneStateButtonStylePrev.xaml
    /// </summary>
    public partial class OneStateButtonStylePrev : Window
    {
        public OneStateButtonStylePrev()
        {
            InitializeComponent();

            //uriButton.IconURI = "/WpfMaterial;component/Preview/Icons/icon1.ico";
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(delegate()
            {
                int stride = 200 * 4;
                byte[] buf = new byte[200 * 200 * 4];
                for (int i = 0; i < 200; i++)
                    for (int j = 0; j < 200; j++)
                    {
                        buf[i * stride + j * 4 + 0] = 0;
                        buf[i * stride + j * 4 + 1] = (byte)i;
                        buf[i * stride + j * 4 + 2] = 0;
                        buf[i * stride + j * 4 + 3] = (byte)(i % 255);
                    }
                BitmapSource source = BitmapSource.Create(200, 200, 96, 96, PixelFormats.Bgra32, null, buf, stride);
                sourceButton.IconSource = source;
            }));
            
        }
    }
}
