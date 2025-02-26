using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace ChessTracker
{
    internal static class GuiManager
    {
        public static MainWindow MainWindow { get; set; }
        private static UserControl currentControl;
        private static UserControl previousControl;
        public static void OpenContent(UserControl control)
        {
            previousControl = MainWindow.contentPanel.Content as UserControl;
            MainWindow.contentPanel.Content = control;
            currentControl = MainWindow.contentPanel.Content as UserControl;
        }
        public static void CloseContent()
        {
            OpenContent(previousControl);
        }
    }
}
