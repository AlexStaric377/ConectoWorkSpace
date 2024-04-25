using System;
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
    public partial class Conector : Window
    {
        public Conector()
        {
            InitializeComponent();
        }


        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Close_F_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CloseWindows();
        }

        private void SitePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Пример с сайтом - Process.Start("IExplore.exe", "http://www.google.ru/");
            System.Diagnostics.Process.Start("http://conecto.ua/");
        }

        private void Butt_Conector_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // устарела
        }


        private void Butt_Conector_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CloseWindows();
        }


        private void CloseWindows()
        {


            //MainWindow FonWait = (MainWindow)App.Current.MainWindow;

            //foreach ( MainWindow ctrl in App.Current.MainWindow[])
            //{

            //}
            var Window = SystemConecto.ListWindowMain("WaitFonW");
            //if (Window!=null)
            //{
            //    Window.Hide();
            //}
            Window.Close();
            this.Close();
            this.Owner = null;

        }

        /// <summary>
        /// Перегрузить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RezetPC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SystemConecto.ShutdownPC(true, false);
        }
        /// <summary>
        /// Выключить PC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PowerOff_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SystemConecto.ShutdownPC(false, true);
        }
        #endregion


        #region Изображения как клавиши - Визуализация интерфейса

        private void Close_F_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_2.png", UriKind.Relative);
            this.Close_F.Source = new BitmapImage(uriSource);
        }

        private void Close_F_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knop2_1.png", UriKind.Relative);
            this.Close_F.Source = new BitmapImage(uriSource);
        }

        private void PowerOff_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vykl2.png", UriKind.Relative);
            this.PowerOff.Source = new BitmapImage(uriSource);
        }
        private void PowerOff_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/vykl1.png", UriKind.Relative);
            this.PowerOff.Source = new BitmapImage(uriSource);
        }

        private void RezetPC_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/term_peregr2.png", UriKind.Relative);
            this.RezetPC.Source = new BitmapImage(uriSource);
        }
        private void RezetPC_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/term_peregr1.png", UriKind.Relative);
            this.RezetPC.Source = new BitmapImage(uriSource);
        }

        #endregion


        #region Обработка событий любой клавиатуры
        private void ConectorW_KeyDown(object sender, KeyEventArgs e)
        {
            // Да завершить
            if (e.Key == Key.Return)
            {
               // SystemConecto.EndWorkPC(); // Это работает

            }
            else
            {

                // Нет отказаться
                if (e.Key == Key.Escape)
                {
                    CloseWindows();

                }

            }


            // Отладка
            // MessageBox.Show(e.Key.ToString());

        }
        #endregion






        private void ConectorI_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

      










    }
}
