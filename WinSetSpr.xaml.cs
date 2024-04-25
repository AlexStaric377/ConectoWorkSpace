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
    public partial class WinSetSpr : Window
    {
        public WinSetSpr()
        {
            InitializeComponent();
        }

        private void Close_Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate ()
            {
                ConectoWorkSpace.Administrator.AdminPanels ConectoWorkSpace_Off = AppStart.LinkMainWindow("WAdminPanels");
                ConectoWorkSpace_Off.datePicker25_Server.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_Off.CleanBD25_Label.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_Off.Combo_Label.Visibility = Visibility.Collapsed;
                ConectoWorkSpace_Off.ComboFiltr.Visibility = Visibility.Collapsed;

            }));
            this.Close();

        }
        private void Close_Grid_MouseMove(object sender, MouseEventArgs e)
        {
            //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/x2.png", UriKind.Relative);
            //this.Close_Grid.Source = new BitmapImage(uriSource);
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
        private void DateGridSpr_Loaded(object sender, RoutedEventArgs e)
        {
            Administrator.AdminPanels.SprGrid("SELECT * FROM SPR_ORG WHERE SPR_ORG.IS_SELF = 1");
        }

        private void GridSpr_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Administrator.AdminPanels.GridSpr();

            this.Close();
        }
    }
}
