using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfStyles
{
    internal static class LocalExtensions
    {
        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
            if (window != null) action(window);
        }
    }

    public partial class WindowStyle
    {
        #region event handlers

        void CloseButtonClick(object sender, RoutedEventArgs e)
        {   
            sender.ForWindowFromTemplate(w => w.Close());
        }

        void MinButtonClick(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.WindowState = WindowState.Minimized);
        }

        void MaxButtonClick(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => {
                w.MaxHeight = Int32.MaxValue;
                w.WindowState = (w.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
            });
        }

        void SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double height = (sender as Grid).ActualHeight;
            sender.ForWindowFromTemplate(w => {
                if (w.WindowState == WindowState.Maximized)
                {
                    if (w.MaxHeight == Double.PositiveInfinity)
                    {
                        int offset = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
                        w.MaxHeight = height - offset - 7;
                    }
                    
                }
                else w.MaxHeight = Double.PositiveInfinity;
            });
        }

        #endregion

        #region P/Invoke

        const int WM_SYSCOMMAND = 0x112;
        const int SC_SIZE = 0xF000;
        const int SC_KEYMENU = 0xF100;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        void DragSize(IntPtr handle)
        {
            SendMessage(handle, WM_SYSCOMMAND, (IntPtr)(SC_SIZE + 8), IntPtr.Zero);
            SendMessage(handle, 514, IntPtr.Zero, IntPtr.Zero);
        }

        #endregion
    }
}
