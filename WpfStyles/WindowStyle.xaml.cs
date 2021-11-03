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
        public static void ForWindowFromChild(this object childDependencyObject, Action<Window> action)
        {
            var element = childDependencyObject as DependencyObject;
            while (element != null)
            {
                element = VisualTreeHelper.GetParent(element);
                if (element is Window) { action(element as Window); break; }
            }
        }

        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
            if (window != null) action(window);
        }

        public static IntPtr GetWindowHandle(this Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            return helper.Handle;
        }

        public static void ForTabControlFromTemplate(this object templateFrameworkElement, Action<TabControl> action)
        {
            TabControl tabControl = ((FrameworkElement)templateFrameworkElement).TemplatedParent as TabControl;
            if (tabControl != null) action(tabControl);
        }
    }

    public partial class WindowStyle
    {
        #region sizing event handlers

        private Boolean dragWindow = false;

        private Boolean doubleClick = false;

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
            sender.ForWindowFromTemplate(w => w.WindowState = (w.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized);
        }

        void TitleBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                doubleClick = true;
                MaxButtonClick(sender, e);
            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                dragWindow = true;
                sender.ForWindowFromTemplate(w =>
                {
                    if (w.WindowState == WindowState.Normal)
                    {
                        w.DragMove();
                        if (w.PointToScreen(e.GetPosition(null)).Y < 2)
                        {
                            w.BeginInit();
                            w.WindowState = WindowState.Maximized;
                            w.EndInit();
                        }
                    }
                });
            }
        }
        
        void TitleBarMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !doubleClick)
            {
                sender.ForWindowFromTemplate(w =>
                {
                    if (w.WindowState == WindowState.Maximized)
                    {
                        if (w.PointToScreen(e.GetPosition(null)).Y > 10)
                        {
                            w.BeginInit();
                            double fullWidth = w.ActualWidth;                            
                            double fullX = w.PointToScreen(e.GetPosition(null)).X;
                            double fullY = w.PointToScreen(e.GetPosition(null)).Y;
                            w.WindowState = WindowState.Normal;
                            double smallWidth = w.ActualWidth;
                            double smallX = fullX / fullWidth * smallWidth;
                            if ((fullX < fullWidth / 3.0) && (fullX < smallWidth / 2.0)) w.Left = 0;
                            else if ((fullX > fullWidth * 2 / 3.0) && (fullX > fullWidth - smallWidth / 2.0)) w.Left = fullWidth - smallWidth;
                            else w.Left = fullX - smallX;
                            w.Top = fullY - 10;
                            w.EndInit();                            
                            w.DragMove();
                            if (w.PointToScreen(e.GetPosition(null)).Y < 2)
                            {
                                w.BeginInit();
                                w.WindowState = WindowState.Maximized;
                                w.EndInit();
                            }
                        }
                    }
                });
            }
        }

        void OnSizeSouthEast(object sender, MouseButtonEventArgs e) 
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                sender.ForWindowFromTemplate(w =>
                {
                    w.Cursor = Cursors.SizeNWSE;
                    if (w.WindowState == WindowState.Normal)
                        DragSize(w.GetWindowHandle());
                    w.Cursor = Cursors.Arrow;
                });
            }
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
