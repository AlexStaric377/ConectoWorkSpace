using System;
using System.IO;
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

namespace ConectoWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class WinSetTableLog : Window
    {
        public WinSetTableLog()
        {
            InitializeComponent();
        }

        private void Close_Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             this.Close();
        }
        private void Close_Grid_MouseMove(object sender, MouseEventArgs e)
        {
             this.Close();
        }

        // Процедура загрузки таблицы в окно Датагрид.
        private void DateTableLog_Loaded(object sender, RoutedEventArgs e)
        {
            Administrator.AdminPanels.LoadedTableLog();
        }

        private void GridTableLog_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.GridTableLog();
            this.Close();
        }
       #region Обработка событий любой клавиатуры
        private void ConectoW_KeyDown(object sender, KeyEventArgs e)
        {
            // Да завершить
            if (e.Key == Key.Return)AppStart.EndWorkPC(); // Это работает
            else if (e.Key == Key.Escape)this.Close();
        }
        #endregion

    
    }
}
