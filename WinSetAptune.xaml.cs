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
    public partial class WinSetAptune : Window
    {
        public WinSetAptune()
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

 
        #region Обработка событий любой клавиатуры
        private void ConectoW_KeyDown(object sender, KeyEventArgs e)
        {
            // Да завершить
            if (e.Key == Key.Return)
            {
                AppStart.EndWorkPC(); // Это работает

            }
            else
            {

                // Нет отказаться
                if (e.Key == Key.Escape)
                {
                    this.Close();

                }

            }
            

           // Отладка
           // MessageBox.Show(e.Key.ToString());

        }

        #endregion

 
        // Процедура загрузки таблицы в окно Датагрид.
        private void DateGrid25_Loaded(object sender, RoutedEventArgs e)
        {
            if (Administrator.AdminPanels.SetWinSetHub == "AppFront")
            {
                NameTable.Content = "Таблица установленных Фронтов";
            }
            if (Administrator.AdminPanels.SetWinSetHub == "AppBack")
            {
                NameTable.Content = "Таблица установленных Бек Офисов";
            }
 
            if (Administrator.AdminPanels.SetWinSetHub == "AptuneFront" || Administrator.AdminPanels.SetWinSetHub == "GnclientFront" )
            {
                Administrator.AdminPanels.AptuneGrid_Puth("SELECT * from REESTRFRONT");

            }
            if (Administrator.AdminPanels.SetWinSetHub == "AptuneBack" || Administrator.AdminPanels.SetWinSetHub == "GnclientBack" )
            {
                Administrator.AdminPanels.AptuneGrid_Puth("SELECT * from REESTRBACK");

            }
        }
        // Процедура выбора строки и запуска редактора файла Aptune.ini 
        private void grid25_MouseUp(object sender, MouseButtonEventArgs e)
        {

            Administrator.AdminPanels.AptuneGrid();
            if (Administrator.AdminPanels.SetWinSetHub == "AptuneFront" ) Administrator.AdminPanels.AptuneFront();
            if (Administrator.AdminPanels.SetWinSetHub == "GnclientFront") Administrator.AdminPanels.GnclientFront();
            if (Administrator.AdminPanels.SetWinSetHub == "AptuneBack") Administrator.AdminPanels.AptuneBack();
            if (Administrator.AdminPanels.SetWinSetHub == "GnclientBack") Administrator.AdminPanels.GnclientBack();
            this.Close();

        }

    }
}
