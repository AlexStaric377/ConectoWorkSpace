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
    public partial class WinOblakoNiz : Window
    {
        public WinOblakoNiz()
        {
            InitializeComponent();
        }

        #region События Click (Клик) функцилнальных клавиш рабочего стола
        private void Ok_But_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppStart.EndWorkPC(); // Это работает
        }
        private void Close_F_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            this.Close();
            // this.Owner = null;
        }
        #endregion


        #region Изображения как клавиши - Визуализация интерфейса 
        private void Ok_But_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Ok_But.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/knp_zelt1.png")));
        }
        private void Ok_But_MouseMove(object sender, MouseEventArgs e)
        {
            //var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/knp_zelt1.png", UriKind.Relative); - в фоне не работает
            this.Ok_But.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/knp_zelt2.png")));
        }

        private void Close_But_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Close_But.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/knp_zelt1.png")));
        }
        private void Close_But_MouseMove(object sender, MouseEventArgs e)
        {
            this.Close_But.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Conecto®%20WorkSpace;component/Images/knp_zelt2.png")));
        }
        private void Close_F_MouseMove(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/x2.png", UriKind.Relative);
            this.Close_F.Source = new BitmapImage(uriSource);
        }

        private void Close_F_MouseLeave(object sender, MouseEventArgs e)
        {
            var uriSource = new Uri(@"/Conecto®%20WorkSpace;component/Images/x1.png", UriKind.Relative);
            this.Close_F.Source = new BitmapImage(uriSource);
        }
        #endregion


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










    }
}
